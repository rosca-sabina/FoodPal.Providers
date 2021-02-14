using FoodPal.Providers.DTOs;
using FoodPal.Providers.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace FoodPal.Providers.API.Controllers
{
    [Route("api/providers/{providerId}/menu")]
    [ApiController]
    public class CatalogueItemController : ControllerBase
    {
        private readonly ICatalogueItemService _catalogueItemService;
        private readonly IProviderSevice _providerSevice;
        public CatalogueItemController(ICatalogueItemService catalogueItemService, IProviderSevice providerSevice)
        {
            _catalogueItemService = catalogueItemService ?? throw new ArgumentNullException(nameof(catalogueItemService));
            _providerSevice = providerSevice ?? throw new ArgumentNullException(nameof(providerSevice));
        }

        [HttpGet]
        public async Task<IActionResult> GetCatalogueItems(int providerId)
        {
            try
            {
                if (await _providerSevice.GetByIdAsync(providerId, false) == null)
                {
                    return NotFound("No provider with the specified id exists.");
                }

                var catalogueItems = await _catalogueItemService.GetCatalogueItemsForProviderAsync(providerId);
                return Ok(catalogueItems);
            }
            catch(Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occured.");
            }
        }

        [HttpGet("{itemId}", Name = "GetCatalogueItemById")]
        public async Task<IActionResult> GetCatalogueItem(int providerId, int itemId)
        {
            try
            {
                if (await _providerSevice.GetByIdAsync(providerId, false) == null)
                {
                    return NotFound("No provider with the specified id exists.");
                }

                var catalogueItem = await _catalogueItemService.GetCatalogueItemByIdAsync(itemId);
                if(catalogueItem == null)
                {
                    return NotFound();
                }

                return Ok(catalogueItem);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occured.");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateCatalogueItem(int providerId, NewCatalogueItemDTO newCatalogueItem)
        {
            try
            {
                var provider = await _providerSevice.GetByIdAsync(providerId, true);
                if (provider == null)
                {
                    return NotFound("No provider with the specified id exists.");
                }

                if(provider.Catalogue == null)
                {
                    return Problem("The provider has no catalogue.");
                }

                if(provider.Catalogue.Id != newCatalogueItem.CatalogueId)
                {
                    ModelState.AddModelError("CatalogueId", "Provider does not have a catalogue with the specified id.");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var newItemId = await _catalogueItemService.CreateAsync(newCatalogueItem);
                var createdCatalogueItem = await _catalogueItemService.GetCatalogueItemByIdAsync(newItemId);
                if(createdCatalogueItem == null)
                {
                    return Problem("The catalogue item could not be saved.");
                }

                return CreatedAtRoute("GetCatalogueItemById", new { providerId = providerId, itemId = newItemId }, createdCatalogueItem);
            }
            catch(Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occured.");
            }
        }

        [HttpPut("{itemId}")]
        public async Task<IActionResult> UpdateCatalogueItem(int providerId, int itemId, CatalogueItemDTO catalogueItem)
        {
            try
            {
                if (await _providerSevice.GetByIdAsync(providerId, false) == null)
                {
                    return NotFound("No provider with the specified id exists.");
                }

                if (await _catalogueItemService.GetCatalogueItemByIdAsync(itemId) == null)
                {
                    return NotFound("No catalogue item with the specified id exists.");
                }

                if(itemId != catalogueItem.Id)
                {
                    ModelState.AddModelError("Identifier", "Specified id does not match request body.");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                await _catalogueItemService.UpdateAsync(catalogueItem);

                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occured.");
            }
        }

        [HttpDelete("{itemId}")]
        public async Task<IActionResult> DeleteCatalogueItem(int providerId, int itemId)
        {
            try
            {
                if (await _providerSevice.GetByIdAsync(providerId, false) == null)
                {
                    return NotFound("No provider with the specified id exists.");
                }

                if(await _catalogueItemService.GetCatalogueItemByIdAsync(itemId) == null)
                {
                    return NotFound("No catalogue item with the specified id exists.");
                }

                await _catalogueItemService.DeleteAsync(itemId);
                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occured.");
            }
        }
    }
}
