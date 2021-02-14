using AutoMapper;
using FoodPal.Providers.DataAccess.UnitsOfWork;
using FoodPal.Providers.DomainModels;
using FoodPal.Providers.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FoodPal.Providers.Services
{
    public class CatalogueItemService: ICatalogueItemService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CatalogueItemService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<bool> CatalogueItemExistsAsync(string catalogueItemName, int providerId)
        {
            CatalogueItem catalogueItem = await _unitOfWork.CatalogueItemRepository
                .SingleOrDefaultAsync(ci => ci.Name.ToLower().Equals(catalogueItemName.ToLower())
                                            && ci.Catalogue != null
                                            && ci.Catalogue.ProviderId == providerId);

            return catalogueItem != null;
        }

        public async Task<int> CreateAsync(NewCatalogueItemDTO newCatalogueItem)
        {
            CatalogueItem catalogueItemModel = _mapper.Map<NewCatalogueItemDTO, CatalogueItem>(newCatalogueItem);
            Catalogue catalogueModel = await _unitOfWork.CatalogueRepository.SingleOrDefaultAsync(c => c.Id == catalogueItemModel.CatalogueId);
            catalogueItemModel.Catalogue = catalogueModel;

            await _unitOfWork.CatalogueItemRepository.AddAsync(catalogueItemModel);
            await _unitOfWork.CommitAsync();

            return catalogueItemModel.Id;
        }

        public async Task DeleteAsync(int catalogueItemId)
        {
            CatalogueItem catalogueItem = await _unitOfWork.CatalogueItemRepository.GetCatalogueItemByIdAsync(catalogueItemId);

            _unitOfWork.CatalogueItemRepository.Remove(catalogueItem);
            await _unitOfWork.CommitAsync();
        }

        public async Task<CatalogueItemDTO> GetCatalogueItemByIdAsync(int catalogueItemId)
        {
            CatalogueItem catalogueItemModel = await _unitOfWork.CatalogueItemRepository.GetCatalogueItemByIdAsync(catalogueItemId);
            return _mapper.Map<CatalogueItem, CatalogueItemDTO>(catalogueItemModel);
        }

        public async Task<IEnumerable<CatalogueItemDTO>> GetCatalogueItemsForProviderAsync(int providerId)
        {
            IEnumerable<CatalogueItem> catalogueItems = await _unitOfWork.CatalogueItemRepository.GetCatalogueItemsForProviderAsync(providerId);
            return _mapper.Map<IEnumerable<CatalogueItem>, IEnumerable<CatalogueItemDTO>>(catalogueItems);
        }

        public async Task UpdateAsync(CatalogueItemDTO catalogueItem)
        {
            CatalogueItem catalogueItemModel = await _unitOfWork.CatalogueItemRepository.GetCatalogueItemByIdAsync(catalogueItem.Id);

            catalogueItemModel.Name = catalogueItem.Name;
            catalogueItemModel.Price = catalogueItem.Price;
            if(catalogueItem.Category != null)
            {
                catalogueItemModel.CategoryId = catalogueItem.Category.Id;
            }

            await _unitOfWork.CommitAsync();
        }
    }
}
