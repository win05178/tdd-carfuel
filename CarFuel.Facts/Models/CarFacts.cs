using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using CarFuel.Models;

namespace CarFuel.Facts.Models
{
  public class CarFacts
  {
    public class GeneralUsage
    {
      [Fact]
      public void NewCar_HasCorrectInitialValues()
      {
        Car c = new Car();

        c.Make = "Honda";
        c.Model = "Accord";

        Assert.Equal("Honda", c.Make);
        Assert.Equal("Accord", c.Model);
        Assert.Equal(0, c.FillUps.Count());
      }

     

    }
    public class AddFillUpMethod
    {
      [Fact]
      public void CanAddNewFillUp()
      {
        var c = new Car();

        FillUp f =c.AddFillUp(odometer: 1000, liters: 50.5, isFull: true);

        Assert.Equal(1, c.FillUps.Count());
        Assert.Equal(1000, f.Odometer);
        Assert.Equal(50.5, f.Liters);
        Assert.True(f.IsFull);


      }

      [Fact]
      public void CanAddTwoFillUp()
      {
        var c = new Car();

        FillUp f1 = c.AddFillUp(odometer: 1000, liters: 50.5, isFull: true);
        FillUp f2 = c.AddFillUp(odometer: 1000, liters: 50.5, isFull: true);

        Assert.Equal(2, c.FillUps.Count());
        Assert.Same(f2, f1.NextFillUp);
        Assert.Same(f1, f2.PreviousFillUp);


      }

      [Fact]
      public void CanAddTreeFillUp()
      {
        var c = new Car();

        FillUp f1 = c.AddFillUp(odometer: 1000, liters: 50.5, isFull: true);
        FillUp f2 = c.AddFillUp(odometer: 1000, liters: 50.5, isFull: true);
        FillUp f3 = c.AddFillUp(odometer: 1000, liters: 50.5, isFull: true);

        Assert.Equal(3, c.FillUps.Count());
        Assert.Same(f2, f1.NextFillUp);
        Assert.Same(f3, f2.NextFillUp);


      }
      
    }
    public class AvverageKilomate{


      [Fact]
      public void NoFillUp_Isnull()
      {
        var c = new Car();


        Assert.Null(c.get_AveragekilomaterPerLister);



      }



      [Fact]
      public void CanTreeFillUp()
      {
        var c = new Car();

        FillUp f1 = c.AddFillUp(odometer: 1000, liters: 50, isFull: true);
        FillUp f2 = c.AddFillUp(odometer: 1600, liters: 60, isFull: true);
        FillUp f3 = c.AddFillUp(odometer: 2000, liters: 50, isFull: true);

        Assert.Equal(9.1, c.get_AveragekilomaterPerLister);
      


      }


      
    }
    public class AverageKmlWhenforgotFillUps
    {

      [Fact]
      public void oneBlock()
      {
        var c = new Car();

        FillUp f1 = c.AddFillUp(1000, 50, true, false);
        FillUp f2 = c.AddFillUp(1600, 60, true, true);
        FillUp f3 = c.AddFillUp(2000, 50, true, false);
        c.AddFillUp(2600, 60, true);

        var kml = c.get_AveragekilomaterPerLister;

        Assert.Equal(9.1, kml);

      }
      [Fact]
      public void twoBlock()
      {
        var c = new Car();

        c.AddFillUp(1000, 50);
        c.AddFillUp(1600, 60);
        c.AddFillUp(2000, 50);

        c.AddFillUp(4000, 50, isForgot: true);
        c.AddFillUp(4600, 50);
        c.AddFillUp(5000, 50);


        var kml = c.get_AveragekilomaterPerLister;

        Assert.Equal(9.6, kml);



      }


      



    }
  }
}
