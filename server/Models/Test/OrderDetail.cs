using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RadzenEndToEndAngularApplication.Models.Test
{
  [Table("OrderDetails", Schema = "dbo")]
  public class OrderDetail
  {
    [Key]
    public int? Id
    {
      get;
      set;
    }

    public int? OrderId
    {
      get;
      set;
    }

    public int? ProductId
    {
      get;
      set;
    }

    public int? Quantity
    {
      get;
      set;
    }
  }
}
