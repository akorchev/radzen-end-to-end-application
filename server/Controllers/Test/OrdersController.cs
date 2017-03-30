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
  [ODataRoute("odata/Test/Orders")]
  public partial class OrdersController : Controller
  {
    private TestContext context;

    public OrdersController(TestContext context)
    {
      this.context = context;
    }

    // GET /odata/Test/Orders
    [HttpGet]
    public IEnumerable<Order> Get()
    {
      return this.context.Orders;
    }

    [HttpGet("{Id}")]
    public IActionResult GetOrder(int? key)
    {
        var item = this.context.Orders.Where(i=>i.Id == key).SingleOrDefault();

        if (item == null)
        {
            return NotFound();
        }

        return new ObjectResult(item);
    }

    partial void OnOrderDeleted(Order item);

    [HttpDelete("{Id}")]
    public IActionResult DeleteOrder(int? key)
    {
        var item = this.context.Orders
            .Where(i => i.Id == key)
            .SingleOrDefault();

        if (item == null)
        {
            return NotFound();
        }

        this.OnOrderDeleted(item);
        this.context.Orders.Remove(item);
        this.context.SaveChanges();

        return new NoContentResult();
    }

    partial void OnOrderUpdated(Order item);

    [HttpPut("{Id}")]
    public IActionResult PutOrder(int? key, [FromBody]Order newItem)
    {
        if (newItem == null || newItem.Id != key)
        {
            return BadRequest();
        }

        this.OnOrderUpdated(newItem);
        this.context.Orders.Update(newItem);
        this.context.SaveChanges();

        return new NoContentResult();
    }

    [HttpPatch("{Id}")]
    public IActionResult PatchOrder(int? key, [FromBody]Order newItem)
    {
        if (newItem == null || newItem.Id != key)
        {
            return BadRequest();
        }

        this.OnOrderUpdated(newItem);
        this.context.Orders.Update(newItem);
        this.context.SaveChanges();

        return new NoContentResult();
    }

    partial void OnOrderCreated(Order item);

    [HttpPost]
    public IActionResult Post([FromBody] Order item)
    {
        if (item == null)
        {
            return BadRequest();
        }

        this.OnOrderCreated(item);
        this.context.Orders.Add(item);
        this.context.SaveChanges();

        return Created($"odata/Test/Orders/{item.Id}", item);
    }
  }
}
