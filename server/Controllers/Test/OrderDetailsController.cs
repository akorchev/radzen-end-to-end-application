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
  [ODataRoute("odata/Test/OrderDetails")]
  public partial class OrderDetailsController : Controller
  {
    private TestContext context;

    public OrderDetailsController(TestContext context)
    {
      this.context = context;
    }

    // GET /odata/Test/OrderDetails
    [HttpGet]
    public IEnumerable<OrderDetail> Get()
    {
      return this.context.OrderDetails;
    }

    [HttpGet("{Id}")]
    public IActionResult GetOrderDetail(int? key)
    {
        var item = this.context.OrderDetails.Where(i=>i.Id == key).SingleOrDefault();

        if (item == null)
        {
            return NotFound();
        }

        return new ObjectResult(item);
    }

    partial void OnOrderDetailDeleted(OrderDetail item);

    [HttpDelete("{Id}")]
    public IActionResult DeleteOrderDetail(int? key)
    {
        var item = this.context.OrderDetails
            .Where(i => i.Id == key)
            .SingleOrDefault();

        if (item == null)
        {
            return NotFound();
        }

        this.OnOrderDetailDeleted(item);
        this.context.OrderDetails.Remove(item);
        this.context.SaveChanges();

        return new NoContentResult();
    }

    partial void OnOrderDetailUpdated(OrderDetail item);

    [HttpPut("{Id}")]
    public IActionResult PutOrderDetail(int? key, [FromBody]OrderDetail newItem)
    {
        if (newItem == null || newItem.Id != key)
        {
            return BadRequest();
        }

        this.OnOrderDetailUpdated(newItem);
        this.context.OrderDetails.Update(newItem);
        this.context.SaveChanges();

        return new NoContentResult();
    }

    [HttpPatch("{Id}")]
    public IActionResult PatchOrderDetail(int? key, [FromBody]OrderDetail newItem)
    {
        if (newItem == null || newItem.Id != key)
        {
            return BadRequest();
        }

        this.OnOrderDetailUpdated(newItem);
        this.context.OrderDetails.Update(newItem);
        this.context.SaveChanges();

        return new NoContentResult();
    }

    partial void OnOrderDetailCreated(OrderDetail item);

    [HttpPost]
    public IActionResult Post([FromBody] OrderDetail item)
    {
        if (item == null)
        {
            return BadRequest();
        }

        this.OnOrderDetailCreated(item);
        this.context.OrderDetails.Add(item);
        this.context.SaveChanges();

        return Created($"odata/Test/OrderDetails/{item.Id}", item);
    }
  }
}
