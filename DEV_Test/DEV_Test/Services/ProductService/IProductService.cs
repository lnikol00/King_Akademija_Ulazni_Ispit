using DEV_Test.Controllers.DTO;
using DEV_Test.Services.ProductService.Models;

namespace DEV_Test.Services.ProductService
{
    public interface IProductService
    {
        Task<List<ResultModel>> GetAllProducts();
        Task<ResultModel> GetProductById(int id);

        Task<List<ResultModel>> GetFilterProducts(SearchRequestDTO searchRequest);

        Task<List<ResultModel>> GetProductsBySearch(string search);
    }
}