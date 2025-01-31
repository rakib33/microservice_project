using Catalog.API.Interfaces.Manager;
using Catalog.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using System.Net;
using CoreApiResponse;
using MongoDB.Bson;
namespace Catalog.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CatalogController : BaseController
    {
        IProductManager _productManager;
        public CatalogController(IProductManager productManager)
        {
            _productManager = productManager;
        }
        /// <summary>
        /// return type of IEnumerable<Product>
        /// always try to apply try catch on controller.
        /// and response type of (int)HttpStatusCode.OK
        /// add response cache to prevent multiple request within sort time default 30 sec
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Product>),(int)HttpStatusCode.OK)]
        [ResponseCache(Duration = 30)]
        public IActionResult GetProducts()
        {
            try
            {
                var products = _productManager.GetAll();
                return CustomResult("Data load succesfully", products);
            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message,HttpStatusCode.BadRequest);
            }

        }


        /// <summary>
        /// return type of Product
        /// always try to apply try catch on controller.
        /// and response type of (int)HttpStatusCode.OK
        /// add response cache to prevent multiple request within sort time default 30 sec
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
        public IActionResult GetProductById(string id)
        {
            try
            {
                var product = _productManager.GetById(id);
                return CustomResult("Data load succesfully", product);
            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.BadRequest);
            }

        }


        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Product>), (int)HttpStatusCode.OK)]
        [ResponseCache(Duration = 10)]
        public IActionResult GetByCategory(string category)
        {
            try
            {
                var products = _productManager.GetByCategory(category);
                return CustomResult("Data load succesfully", products);
            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.BadRequest);
            }

        }


        [HttpPost]
        [ProducesResponseType(typeof(Product),(int)HttpStatusCode.Created)]
        public IActionResult CreateProduct([FromBody] Product product)
        {
            try
            {
                //generate product id
                product.Id = ObjectId.GenerateNewId().ToString(); // Guid.NewGuid().ToString();
                bool isSaved = _productManager.Add(product);
                _productManager.Add(product);

                if(isSaved)
                {
                    return CustomResult("Product added successfully",product, HttpStatusCode.Created);
                }
                else
                {
                    return CustomResult("Product save failed.",product, HttpStatusCode.BadRequest);
                }

            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.BadRequest);
            }
        }

        [HttpPut]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.Created)]
        public IActionResult UpdateProduct([FromBody] Product product)
        {
            try
            {
                if(string.IsNullOrEmpty(product.Id))
                {
                    return CustomResult("Product id is required", HttpStatusCode.NotFound);
                }

                bool isUpdated = _productManager.Update(product.Id, product);
                _productManager.Add(product);

                if (isUpdated)
                {
                    return CustomResult("Product modified successfully", product, HttpStatusCode.OK);
                }
                else
                {
                    return CustomResult("Product modified failed.", product, HttpStatusCode.BadRequest);
                }

            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.BadRequest);
            }
        }


        [HttpDelete]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public IActionResult DeleteProduct(string id)
        {
            try
            {
                if (string.IsNullOrEmpty(id))
                {
                    return CustomResult("Data not found.", HttpStatusCode.NotFound);
                }

                bool isDeleted = _productManager.Delete(id);

                if (isDeleted)
                {
                    return CustomResult("Product has been deleted successfully",  HttpStatusCode.OK);
                }
                else
                {
                    return CustomResult("Product deleted  failed.", HttpStatusCode.BadRequest);
                }

            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.BadRequest);
            }
        }

    }
}
