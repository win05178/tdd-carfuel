using CarFuel.Models;
using CarFuel.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CarFuel.Facts.Services
{
  public class CarServiceFact
  {
    public class add
    {
      [Fact]
      public void ValidMake_AddOK()
      {
        using (var app = new App(testing: true))
        {
          var c = new Car();
          c.Make = "Honda";

          app.Cars.Add(c);
          app.Cars.SaveChanges();

          var n = app.Cars.All().Count();

          Assert.Equal(1, n);
        }
      }
      [Fact]
      public void InvaildMake_AddFailed()
      {
        using (var app = new App(testing: true))
        {
          var c = new Car();
          c.Make = "Google";

          Assert.ThrowsAny<Exception>(() =>
          {
            app.Cars.Add(c);
          });
          
        
        }
      }

    }
  }
}
