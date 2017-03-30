using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

using RadzenEndToEndAngularApplication.Models.Test;

namespace RadzenEndToEndAngularApplication.Data
{
  public class TestContext : IdentityDbContext<IdentityUser>
  {
    public TestContext(DbContextOptions<TestContext> options):base(options)
    {
    }

    public TestContext()
    {
    }
    


    public DbSet<Order> Orders
    {
      get;
      set;
    }

    public DbSet<OrderDetail> OrderDetails
    {
      get;
      set;
    }

    public DbSet<Product> Products
    {
      get;
      set;
    }
  }
}
