using DEV_Test.Controllers.DTO;
using DEV_Test.Exceptions;
using DEV_Test.Models;
using DEV_Test.Services.ProductService.Models;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace DEV_Test.Services.ProductService
{
    public class ProductService : IProductService
    {
        private readonly IOptions<ConnectionApi> _connectionApi;

        public ProductService(IOptions<ConnectionApi> connectionApi)
        {
            _connectionApi = connectionApi;
        }

        public async Task<List<ResultModel>> GetAllProducts()
        {
            string url = _connectionApi.Value.ConnectionString;
            if (!string.IsNullOrEmpty(url))
            {
                url += "/products";
            }

            HttpClient client = new HttpClient();
            List<ResultModel> request = new List<ResultModel>();
            try
            {
                var response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var reposnseContext = await response.Content.ReadAsStringAsync();

                    if (!string.IsNullOrEmpty(reposnseContext))
                    {
                        var results = JsonSerializer.Deserialize<SearchResult>(reposnseContext);

                        if (results != null && results.products.Count > 0)
                        {
                            var products = results.products.Select(x => new ResultModel
                            {
                                Id = x.id,
                                Image = x.images[0],
                                Title = x.title,
                                Description = x.description,
                                Price = x.price

                            }).ToList();

                            if (products != null)
                            {
                                request.AddRange(products);
                            }
                        }
                        else
                        {
                            throw new ErrorMessage("No products to match this parameters");
                        }

                    }
                    else
                    {
                        throw new ErrorMessage("Invalid response context!");
                    }

                }
                else
                {
                    throw new ErrorMessage("No response!");
                }
            }
            catch (Exception ex)
            {
                throw new ErrorMessage($"An error occurred: {ex.Message}");
            }

            return request;
        }

        public async Task<ResultModel> GetProductById(int id)
        {
            string url = _connectionApi.Value.ConnectionString;
            if (!string.IsNullOrEmpty(url))
            {
                url += $"/products/{id}";
            }

            HttpClient client = new HttpClient();
            ResultModel request = null;
            try
            {
                var response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var reposnseContext = await response.Content.ReadAsStringAsync();

                    if (!string.IsNullOrEmpty(reposnseContext))
                    {
                        var product = JsonSerializer.Deserialize<Product>(reposnseContext);

                        if (product != null)
                        {
                            request = new ResultModel
                            {
                                Id = product.id,
                                Image = product.images[0],
                                Title = product.title,
                                Description = product.description,
                                Price = product.price
                            };

                        }
                        else
                        {
                            throw new ErrorMessage("Product with specified ID doesn't exist!");
                        }

                    }
                    else
                    {
                        throw new ErrorMessage("Invalid response context!");
                    }
                }
                else
                {
                    throw new ErrorMessage("No response!");
                }
            }
            catch (Exception ex)
            {
                throw new ErrorMessage($"An error occurred: {ex.Message}");
            }

            return request;
        }

        public async Task<List<ResultModel>> GetFilterProducts(SearchRequestDTO searchRequest)
        {
            string url = _connectionApi.Value.ConnectionString;
            if (!string.IsNullOrEmpty(url))
            {
                url += $"/products?price={searchRequest.Price}&category={searchRequest.Category}";
            }

            HttpClient client = new HttpClient();
            List<ResultModel> request = new List<ResultModel>();
            try
            {
                var response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var reposnseContext = await response.Content.ReadAsStringAsync();

                    if (!string.IsNullOrEmpty(reposnseContext))
                    {
                        var results = JsonSerializer.Deserialize<SearchResult>(reposnseContext);

                        if (results != null && results.products.Count > 0)
                        {
                            var products = results.products.Select(x => new ResultModel
                            {
                                Id = x.id,
                                Image = x.images[0],
                                Title = x.title,
                                Description = x.description,
                                Price = x.price

                            }).ToList();

                            if (products != null)
                            {
                                request.AddRange(products);
                            }
                        }
                        else
                        {
                            throw new ErrorMessage("No results to match this parameters");
                        }

                    }
                    else
                    {
                        throw new ErrorMessage("Invalid response context!");
                    }

                }
                else
                {
                    throw new ErrorMessage("No response!");
                }
            }
            catch (Exception ex)
            {
                throw new ErrorMessage($"An error occurred: {ex.Message}");
            }

            return request;
        }

        public async Task<List<ResultModel>> GetProductsBySearch(string search)
        {
            string url = _connectionApi.Value.ConnectionString;
            if (!string.IsNullOrEmpty(url))
            {
                if (!string.IsNullOrEmpty(search))
                {
                    url += $"/products/search?q={search}";
                }
                else
                {
                    url += "/products";
                }
            }

            HttpClient client = new HttpClient();
            List<ResultModel> request = new List<ResultModel>();
            try
            {
                var response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var reposnseContext = await response.Content.ReadAsStringAsync();

                    if (!string.IsNullOrEmpty(reposnseContext))
                    {
                        var results = JsonSerializer.Deserialize<SearchResult>(reposnseContext);

                        if (results != null && results.products.Count > 0)
                        {
                            var products = results.products.Select(x => new ResultModel
                            {
                                Id = x.id,
                                Image = x.images[0],
                                Title = x.title,
                                Description = x.description,
                                Price = x.price

                            }).ToList();

                            if (products != null)
                            {
                                request.AddRange(products);
                            }
                        }
                        else
                        {
                            throw new ErrorMessage("No products to match this parameters");
                        }

                    }
                    else
                    {
                        throw new ErrorMessage("Invalid response context!");
                    }

                }
                else
                {
                    throw new ErrorMessage("No response!");
                }
            }
            catch (Exception ex)
            {
                throw new ErrorMessage($"An error occurred: {ex.Message}");
            }

            return request;
        }
    }
}
