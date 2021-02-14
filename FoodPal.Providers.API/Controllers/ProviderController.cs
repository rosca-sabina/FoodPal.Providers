using FoodPal.Providers.DTOs;
using FoodPal.Providers.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace FoodPal.Providers.API.Controllers
{
    [Route("api/providers")]
    [ApiController]
    public class ProviderController : ControllerBase
    {
        private readonly IProviderSevice _providerService;
        public ProviderController(IProviderSevice providerService)
        {
            _providerService = providerService ?? throw new ArgumentNullException(nameof(providerService));
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProviders(bool includeCatalogueItems)
        {
            try
            {
                var providers = await _providerService.GetAllAsync(includeCatalogueItems);
                return Ok(providers);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occured.");
            }
        }

        [HttpGet("{id}", Name = "GetProvider")]
        public async Task<IActionResult> GetProvider(int id, bool includeCatalogueItems)
        {
            try
            {
                var provider = await _providerService.GetByIdAsync(id, includeCatalogueItems);

                if(provider == null)
                {
                    return NotFound();
                }

                return Ok(provider);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occured.");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateProvider(NewProviderDTO newProvider)
        {
            try
            {
                if (newProvider.Name.Equals(newProvider.Location))
                {
                    ModelState.AddModelError("Location", "Provider location and name cannot be identical.");
                }

                if(await _providerService.ProviderExistsAsync(newProvider.Name))
                {
                    ModelState.AddModelError("Name", "A provider with the specified name already exists!");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                ProviderDTO createdProvider = await _providerService.CreateAsync(newProvider);
                if(createdProvider == null)
                {
                    return Problem();
                }

                return CreatedAtRoute("GetProvider", new { id = createdProvider.Id }, createdProvider);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occured.");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProvider(int id, ProviderDTO provider)
        {
            try
            {
                if(id != provider.Id)
                {
                    ModelState.AddModelError("Identifier", "Specified id does not match request body.");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (await _providerService.GetByIdAsync(id, false) == null)
                {
                    return NotFound();
                }

                await _providerService.UpdateAsync(provider);

                return NoContent();
            }
            catch(Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occured.");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProvider(int id)
        {
            try
            {
                if(await _providerService.GetByIdAsync(id, false) == null)
                {
                    return NotFound();
                }

                await _providerService.DeleteAsync(id);

                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occured.");
            }
        }
    }
}
