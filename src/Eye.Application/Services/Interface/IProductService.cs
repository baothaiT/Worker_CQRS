using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eye.Contract.Share.DTO;

namespace Eye.Application.Services
{
    public interface IProductService
    {
        Task<IEnumerable<GetProductDTO>> GetProductsAsync();
        Task<GetProductDTO> GetProductByIdAsync(string id);
        Task CreateProductAsync(CreateProductDto product);
        Task UpdateProductAsync(UpdateProductDto product);
        Task DeleteProductAsync(string id);
    }
}
