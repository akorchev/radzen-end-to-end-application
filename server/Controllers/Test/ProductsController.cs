using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData;
using Microsoft.AspNetCore.OData.Routing;
using Microsoft.EntityFrameworkCore;
using RadzenEndToEndAngularApplication.Models;
using RadzenEndToEndAngularApplication.Data;
using RadzenEndToEndAngularApplication.Models.Test;

namespace RadzenEndToEndAngularApplication.Controllers.Test
{
  [EnableQuery]
  [ODataRoute("odata/Test/Products")]
  public partial class ProductsController : Controller
  {
    private TestContext context;

    public ProductsController(TestContext context)
    {
      this.context = context;
    }

    // GET /odata/Test/Products
    [HttpGet]
    public IEnumerable<Product> Get()
    {
      return this.context.Products;
    }

    [HttpGet("{Id}")]
    public IActionResult GetProduct(int? key)
    {
        var item = this.context.Products.Where(i=>i.Id == key).SingleOrDefault();

        if (item == null)
        {
            return NotFound();
        }

        return new ObjectResult(item);
    }

    partial void OnProductDeleted(Product item);

    [HttpDelete("{Id}")]
    public IActionResult DeleteProduct(int? key)
    {
        var item = this.context.Products
            .Where(i => i.Id == key)
            .SingleOrDefault();

        if (item == null)
        {
            return NotFound();
        }

        this.OnProductDeleted(item);
        this.context.Products.Remove(item);
        this.context.SaveChanges();

        return new NoContentResult();
    }

    partial void OnProductUpdated(Product item);

    [HttpPut("{Id}")]
    public IActionResult PutProduct(int? key, [FromBody]Product newItem)
    {
        if (newItem == null || newItem.Id != key)
        {
            return BadRequest();
        }

        this.OnProductUpdated(newItem);
        this.context.Products.Update(newItem);
        this.context.SaveChanges();

        return new NoContentResult();
    }

    [HttpPatch("{Id}")]
    public IActionResult PatchProduct(int? key, [FromBody]Product newItem)
    {
        if (newItem == null || newItem.Id != key)
        {
            return BadRequest();
        }

        this.OnProductUpdated(newItem);
        this.context.Products.Update(newItem);
        this.context.SaveChanges();

        return new NoContentResult();
    }

    partial void OnProductCreated(Product item);

    [HttpPost]
    public IActionResult Post([FromBody] Product item)
    {
        if (item == null)
        {
            return BadRequest();
        }

        this.OnProductCreated(item);
        this.context.Products.Add(item);
        this.context.SaveChanges();

        return Created($"odata/Test/Products/{item.Id}", item);
    }
  }
}
