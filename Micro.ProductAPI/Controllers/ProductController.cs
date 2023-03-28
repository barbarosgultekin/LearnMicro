using Micro.ProductAPI.Models;
using Micro.ProductAPI.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Micro.ProductAPI.Controllers
{
    [Authorize]
    [Route("api/products")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        protected ResponseDTO _response;
        private IProductRepository _productRepository;
        

        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
           
            this._response = new ResponseDTO();
        }

        [HttpGet("allproducts")]
        public async Task<object> Get()
        {
            try
            {
                IEnumerable<ProductDTO> productDtos = await _productRepository.GetProducts();
                _response.Result = productDtos;
            }
            catch (Exception ex)
            {

                _response.IsSuccess = false;
                _response.ErrorMessages
                    = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        [HttpGet("idbyproduct/{id}")]
        public async Task<object> Get(int id)
        {
            try
            {
                ProductDTO productDto = await _productRepository.GetProductById(id);
                _response.Result = productDto;
            }
            catch (Exception ex)
            {

                _response.IsSuccess = false;
                _response.ErrorMessages
                    = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        [HttpPost]
        public async Task<object> Post([FromBody] ProductDTO productDto)
        {
            try
            {
                ProductDTO model = await _productRepository.CreateUpdateProduct(productDto);
                _response.Result = model;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages
                     = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        [HttpDelete("delete/{id}")]
        public async Task<object> Delete(int id)
        {
            try
            {
                bool isSuccess = await _productRepository.DeleteProduct(id);
                _response.Result = isSuccess;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages
                     = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        [HttpPut]
        public async Task<object> Put([FromBody] ProductDTO productDto)
        {
            try
            {
                ProductDTO model = await _productRepository.CreateUpdateProduct(productDto);
                _response.Result = model;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages
                     = new List<string>() { ex.ToString() };
            }
            return _response;
        }
    }
}
