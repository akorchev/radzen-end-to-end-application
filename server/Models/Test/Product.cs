using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RadzenEndToEndAngularApplication.Models.Test
{
  [Table("Products", Schema = "dbo")]
  public class Product
  {
    [Key]
    public int? Id
    {
      get;
      set;
    }

    public string ProductName
    {
      get;
      set;
    }

    public decimal? ProductPrice
    {
      get;
      set;
    }
  }
}
