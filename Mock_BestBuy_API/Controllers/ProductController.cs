using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.IO;

namespace Mock_BestBuy_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepo _repo;

        public ProductController(IProductRepo repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Product>> GetAllProducts()
        {
            return Ok(JsonConvert.SerializeObject(_repo.GetProducts()));
        }

        [HttpGet("{id}")]
        public ActionResult <Product> Get(int id)
        {
            return Ok(JsonConvert.SerializeObject(_repo.GetProduct(id)));
        }

        [HttpPost]
        public void Post(Product product)
        {
            // update ProductID to be one greater than the last in repo
            //var lastProductID = _repo.GetProducts().LastOrDefault().ProductID;
            //product.ProductID = ++lastProductID;
            _repo.InsertProduct(product);
        }

        [HttpPut("{id}")]
        public void Put(int id, Product product)
        {            
            product.ProductID = id;
            _repo.UpdateProduct(product);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var selected = _repo.GetProduct(id);
            _repo.DeleteProduct(selected);
        }


    }
}
