using DEV_Test.Controllers.DTO;
using DEV_Test.Exceptions;
using DEV_Test.Models;
using DEV_Test.Services.ProductService.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace DEV_Test.Services.ProductService
{
    public class ProductService : IProductService
    {
        private readonly IOptions<ConnectionApi> _connectionApi;

        private readonly HttpClient _httpClient;

        private DatabaseContext _db;

        public ProductService(IOptions<ConnectionApi> connectionApi, DatabaseContext db, HttpClient httpClient)
        {
            _connectionApi = connectionApi;
            _db = db;
            _httpClient = httpClient;
        }

        public async Task<List<ResultModel>> GetAllProducts()
        {
            string url = _connectionApi.Value.ConnectionString;
            if (!string.IsNullOrEmpty(url))
            {
                url += "/products";
            }

            List<ResultModel> request = new List<ResultModel>();
            try
            {
                var response = await _httpClient.GetAsync(url);

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

            ResultModel request = null;
            try
            {
                var response = await _httpClient.GetAsync(url);

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

        public async Task<List<ResultModel>> GetFilterProducts(FilterRequestDTO filterRequest)
        {
            string url = _connectionApi.Value.ConnectionString;
            if (!string.IsNullOrEmpty(url))
            {
                url += $"/products/category/{filterRequest.category}?sortBy=price&order={filterRequest.order}";
            }

            List<ResultModel> request = new List<ResultModel>();
            try
            {
                var response = await _httpClient.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var reposnseContext = await response.Content.ReadAsStringAsync();

                    if (!string.IsNullOrEmpty(reposnseContext))
                    {
                        var results = JsonSerializer.Deserialize<SearchResult>(reposnseContext);

                        var filterParams = filterRequest.ToModel();

                        var existingFilter = await _db.Filters.FirstOrDefaultAsync(x =>
                                x.Order == filterParams.Order
                                && x.Category == filterParams.Category
                        );

                        if (existingFilter == null)
                        {
                            _db.Add(filterParams);

                            try
                            {
                                await _db.SaveChangesAsync();
                            }
                            catch
                            {
                                throw new ErrorMessage("An error occurred while connecting to the database.");
                            }
                        }
                        else
                        {
                            filterParams = existingFilter;
                        }

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

        public async Task<List<ResultModel>> GetProductsBySearch(SearchRequestDTO searchRequest)
        {
            string url = _connectionApi.Value.ConnectionString;
            if (!string.IsNullOrEmpty(url))
            {
                if (!string.IsNullOrEmpty(searchRequest.search))
                {
                    url += $"/products/search?q={searchRequest.search}";
                }
                else
                {
                    url += "/products";
                }
            }

            List<ResultModel> request = new List<ResultModel>();
            try
            {
                var response = await _httpClient.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var reposnseContext = await response.Content.ReadAsStringAsync();

                    if (!string.IsNullOrEmpty(reposnseContext))
                    {
                        var results = JsonSerializer.Deserialize<SearchResult>(reposnseContext);

                        var searchParams = searchRequest.ToModel();

                        var existingSearch = await _db.Searches.FirstOrDefaultAsync(x =>
                                x.Search == searchParams.Search
                        );

                        if (existingSearch == null)
                        {
                            _db.Add(searchParams);

                            try
                            {
                                await _db.SaveChangesAsync();
                            }
                            catch
                            {
                                throw new ErrorMessage("An error occurred while connecting to the database.");
                            }
                        }
                        else
                        {
                            searchParams = existingSearch;
                        }

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
