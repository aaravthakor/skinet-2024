using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController(IProductRepository repo) : ControllerBase
    {
        
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<Product>>>  GetProducts(string? brand, 
            string? type,string? sort)
        {
            return Ok(await repo.GetProductsAsync(brand,type,sort));
        }

        [HttpGet("{id:int}")]

        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await repo.GetByProductIdAsync(id);

            if (product == null) return NotFound();

            return product;

        }

        [HttpPost]
        public async Task<ActionResult<Product>> CreateProduct(Product product)
        {
            repo.AddProduct(product);

            if (await repo.SaveChangesAsync())
            {
                return CreatedAtAction("GetProduct", new { id = product.Id, product });
            }

            return BadRequest("Problem creating Prodcut");
        }


        [HttpPut("{id:int}")]
        public async Task<ActionResult> UpdateProduct(int id, Product product)
        {
            if (product.Id != id || !ProductExists(id))             
                return BadRequest("Can not update this Product");

            repo.UpdateProduct(product);

            if (await repo.SaveChangesAsync())
            {
                return NoContent();
            }
            
            return BadRequest("Problem Updating Product"); 
        }


        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteProduct(int id)
        {
            var product = await repo.GetByProductIdAsync(id);

            if(product == null) return NotFound();

            repo.DeleteProduct(product);

            if (await repo.SaveChangesAsync())
            {
                return NoContent();
            }

            return BadRequest("Problem Deleting Product");
        }

        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<string>>> GetBrands()
        {
            return Ok(await repo.GetBrandsAsync());
        }

        [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<string>>> GetTypes()
        {
            return Ok(await repo.GetBrandsAsync());
        }

        private bool ProductExists(int id) 
        {             
            return repo.ProductExists(id);
        }



    }
}
