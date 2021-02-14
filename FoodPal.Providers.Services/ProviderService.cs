using AutoMapper;
using FoodPal.Providers.DataAccess.UnitsOfWork;
using FoodPal.Providers.DomainModels;
using FoodPal.Providers.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FoodPal.Providers.Services
{
    public class ProviderService : IProviderSevice
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public ProviderService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<ProviderDTO> CreateAsync(NewProviderDTO newProvider)
        {
            Provider providerModel = _mapper.Map<NewProviderDTO, Provider>(newProvider);
            await _unitOfWork.ProviderRepository.AddAsync(providerModel);
            await _unitOfWork.CommitAsync();

            return _mapper.Map<Provider, ProviderDTO>(providerModel);
        }

        public async Task DeleteAsync(int providerId)
        {
            Provider providerToDelete = await _unitOfWork.ProviderRepository.GetWithCatalogueItemsByIdAsync(providerId);

            foreach(CatalogueItem ci in providerToDelete.Catalogue.Items)
            {
                _unitOfWork.CatalogueItemRepository.Remove(ci);
            }

            _unitOfWork.CatalogueRepository.Remove(providerToDelete.Catalogue);
            _unitOfWork.ProviderRepository.Remove(providerToDelete);

            await _unitOfWork.CommitAsync();
        }

        public async Task<IEnumerable<ProviderDTO>> GetAllAsync(bool includeCatalogueItems = false)
        {
            IEnumerable<Provider> providerModels;

            if (includeCatalogueItems)
            {
                providerModels = await _unitOfWork.ProviderRepository.GetAllWithCatalogueItemsAsync();
            }
            else
            {
                providerModels = await _unitOfWork.ProviderRepository.GetAllAsync();
            }

            return _mapper.Map<IEnumerable<Provider>, IEnumerable<ProviderDTO>>(providerModels);
        }

        public async Task<ProviderDTO> GetByIdAsync(int providerId, bool includeCatalogueItems = false)
        {
            Provider providerModel;
            if (includeCatalogueItems)
            {
                providerModel = await _unitOfWork.ProviderRepository.GetWithCatalogueItemsByIdAsync(providerId);
            }
            else
            {
                providerModel = await _unitOfWork.ProviderRepository.SingleOrDefaultAsync(p => p.Id == providerId);
            }

            return _mapper.Map<Provider, ProviderDTO>(providerModel);
        }

        public async Task<bool> ProviderExistsAsync(string providerName)
        {
            Provider provider = await _unitOfWork.ProviderRepository.SingleOrDefaultAsync(p => p.Name.ToLower().Equals(providerName.ToLower()));
            return provider != null;
        }

        public async Task UpdateAsync(ProviderDTO provider)
        {
            Provider providerModel = await _unitOfWork.ProviderRepository.SingleOrDefaultAsync(p => p.Id == provider.Id);
            Catalogue catalogueModel = await _unitOfWork.CatalogueRepository.SingleOrDefaultAsync(c => c.Id == provider.Catalogue.Id);

            providerModel.Name = provider.Name;
            catalogueModel.Description = provider.Catalogue.Description;

            await _unitOfWork.CommitAsync();
        }
    }
}
