using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RadzenEndToEndAngularApplication.Models.Test
{
  [Table("Orders", Schema = "dbo")]
  public class Order
  {
    [Key]
    public int? Id
    {
      get;
      set;
    }

    public DateTime? OrderDate
    {
      get;
      set;
    }

    public string UserName
    {
      get;
      set;
    }
  }
}
