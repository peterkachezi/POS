using POS.Data.DTOs.ProductModule;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace POS.Data.Services.ProductModule
{
    public interface IProductService
    {
        Task<ProductDTO> Create(ProductDTO productDTO);
        Task<ProductDTO> Update(ProductDTO productDTO);
        Task<bool> Delete(Guid Id);
        Task<bool> DeleteProductName(Guid Id);
        Task<ProductDTO> GetById(Guid Id);
        Task<List<ProductDTO>> GetAll();
        Task<ProductNameDTO> CreateProductName(ProductNameDTO productNameDTO);
        Task<List<ProductNameDTO>> GetAllProductName();
        Task<ProductNameDTO> GetProductNamesById(Guid Id);
        Task<ProductNameDTO> UpdateProductName(ProductNameDTO productNameDTO);

        Task<ProductDTO> GetByProductCode(string productCode);
    }
}