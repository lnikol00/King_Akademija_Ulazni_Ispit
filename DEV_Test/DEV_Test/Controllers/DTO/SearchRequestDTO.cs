using DEV_Test.Services.ProductService.Models;

namespace DEV_Test.Controllers.DTO
{
    public class SearchRequestDTO
    {
        public string search { get; set; } = "";

        public SearchParams ToModel()
        {
            return new SearchParams
            {
                Search = search
            };
        }
    }


}
