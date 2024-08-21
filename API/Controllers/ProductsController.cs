﻿using API.RequestHelpers;
using Core.Entities;
using Core.Interfaces;
using Core.Specification;
using Infrastructure.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace API.Controllers
{
    public class ProductsController(IGenericRepository<Product> repo) : BaseApiController
    {
        
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<Product>>>  GetProducts([FromQuery]ProductSpecParams specParams)
        {
            var spec = new ProductSpecification(specParams);                            

            return await CreatePagedResult(repo,spec,specParams.PageIndex,specParams.PageSize);
        }

        [HttpGet("{id:int}")]

        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await repo.GetByIdAsync(id);

            if (product == null) return NotFound();

            return product;

        }

        [HttpPost]
        public async Task<ActionResult<Product>> CreateProduct(Product product)
        {
            repo.Add(product);

            if (await repo.SaveAllAsync())
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

            repo.Update(product);

            if (await repo.SaveAllAsync())
            {
                return NoContent();
            }
            
            return BadRequest("Problem Updating Product"); 
        }


        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteProduct(int id)
        {
            var product = await repo.GetByIdAsync(id);

            if(product == null) return NotFound();

            repo.Remove(product);

            if (await repo.SaveAllAsync())
            {
                return NoContent();
            }

            return BadRequest("Problem Deleting Product");
        }

        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<string>>> GetBrands()
        {
            var spec = new BrandListSpecification();

            return Ok(await repo.ListAsync(spec));

           // return Ok(await repo.GetBrandsAsync());
            //return Ok();
        }

        [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<string>>> GetTypes()
        {
            var spec = new TypeListSpecification();

            return Ok(await repo.ListAsync(spec));
           // return Ok(await repo.GetBrandsAsync());
           // return Ok();
        }

        private bool ProductExists(int id) 
        {             
            return repo.Exists(id);
        }



    }
}
