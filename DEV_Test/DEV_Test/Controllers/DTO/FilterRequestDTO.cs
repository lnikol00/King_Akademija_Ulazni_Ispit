using DEV_Test.Services.ProductService.Models;

namespace DEV_Test.Controllers.DTO
{
    public class FilterRequestDTO
    {
        public string order { get; set; }

        public string category { get; set; }

        public FilterParams ToModel()
        {
            return new FilterParams
            {
                Order = order,
                Category = category
            };
        }
    }
}
