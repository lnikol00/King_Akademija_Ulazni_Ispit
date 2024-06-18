using DEV_Test.Controllers.DTO;
using DEV_Test.Services.ProductService;
using DEV_Test.Services.ProductService.Models;
using Microsoft.AspNetCore.Mvc;

namespace DEV_Test.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        // Endpoint vraća listu svih proizvoda (limit dummyJSON je stavljen na 30 tako da vraća prvih 30 proizvoda u listi)
        // Primjer zahtjeva:
        // GET https://localhost:7197/api/Products
        [HttpGet]
        public async Task<ActionResult<ResultModel>> GetAllProducts()
        {
            var allProducts = await _productService.GetAllProducts();
            return Ok(allProducts);
        }

        // Endpoint vraća detalje pojedinačnog proizvoda kojeg pretražujemo ovisno o njegovom ID-u.
        // Primjer zahtjeva:
        // GET https://localhost:7197/api/Products/5
        // Endpoint vraća detalje o proizvodu sa ID-om 5. Ukoliko proizvod sa traženim ID-om ne postoji dobit ćemo
        // 404 odgovor Not Found, odnosno da proizvod sa traženim ID-om ne postoji
        [HttpGet("{id}")]
        public async Task<ActionResult<ResultModel>> GetProductById(int id)
        {
            var singleProduct = await _productService.GetProductById(id);

            if (singleProduct is null)
            {
                return NotFound($"Product with id:{id} doesn't exist!");
            }
            else
            {
                return Ok(singleProduct);
            }
        }

        // Endpoint vraća proizvode filtrirane ovisno o kategoriji kojoj pripadaju i cijeni.
        // Primjer zahtjeva:
        // GET https://localhost:7197/api/Products/Filter?order=desc&category=beauty
        // Cijena može biti filtrirana od veće prema manjoj te od manje prema većoj (odnosno asc i desc), također neke
        // od kategorija su beauty, furniture, groceries itd. Vrijednosti se unose preko Query-ja tako da se na frontendu
        // korisniku mogu ponuditi mogućnosti koje on može razumjeti, a šalje točnu vrijednost.
        // Primjerice, da korisnik odabere opciju (OD VEĆE PREMA MANJOJ) koja šalje value desc.
        // Također rezultati filtriranja spremaju se lokalnu u bazu podataka i po ponovnoj pretrazi dohvaća se iz baze.
        [HttpGet("Filter")]
        public async Task<ActionResult<ResultModel>> GetFilterProducts([FromQuery] FilterRequestDTO filterRequest)
        {
            var filteredProduct = await _productService.GetFilterProducts(filterRequest);
            return Ok(filteredProduct);
        }

        // Endpoint vraća proizvode ovisno o traženom nazivu. 
        // Primjerice za pretraživanje riječi Phone pronaći će sve proizvode koji su vezani sa mobitelima
        // Primjer zahtjeva:
        // GET https://localhost:7197/api/Products/Search?search=Phone
        // Endpoint također sprema pretrage za uneseni tekst u bazu podataka, u slučaju da ponovno pretražujemo po istim
        // pretragama pozvia se na bazu podataka, umjesto da čeka odgovor dummyJSON-a.
        [HttpGet("Search")]
        public async Task<ActionResult<ResultModel>> GetProductsBySearch([FromQuery] SearchRequestDTO searchRequest)
        {
            var searchProducts = await _productService.GetProductsBySearch(searchRequest);
            return Ok(searchProducts);
        }
    }
}
