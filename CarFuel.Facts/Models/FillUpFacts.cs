﻿using CarFuel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CarFuel.Facts.Models
{
  public class FillUpFacts {

    public class GeneralUsage {

      [Fact]
      public void NewFillUp_HasCorrectInitialValues() {
        // Arrange, Act
        var f = new FillUp();

        double liters = f.Liters;

        Assert.Equal(0, f.Odometer);
        Assert.Equal(0.0, liters);
        Assert.True(f.IsFull);
        Assert.False(f.IsForgot);
      }

    }

    public class KilometersPerLiterProperty {

      //[Fact(Skip="Reason to skip this method")]
      [Fact]
      public void FirstFillUp_DontKnowKmL() {
        // Arrange
        var f = new FillUp();
        f.Odometer = 1000;
        f.Liters = 50;
        f.IsFull = true;

        // Act
        double? kml = f.KilometersPerLiter;

        // Assert
        Assert.Null(kml);
      }

      [Fact]
      public void SecondFillUps_TheFirstHasCorrectKmL() {
        var f1 = new FillUp();
        f1.Odometer = 1000;
        f1.Liters = 50.0;
        f1.IsFull = true;

        var f2 = new FillUp();
        f2.Odometer = 1500;
        f2.Liters = 40;
        f2.IsFull = true;

        f1.NextFillUp = f2;

        var kml1 = f1.KilometersPerLiter;
        var kml2 = f2.KilometersPerLiter;

        Assert.NotNull(kml1);
        Assert.Equal(12.5, kml1);
        Assert.Null(kml2);
      }

      [Fact]
      public void ThirdFillUps_TheSecondHasCorrectKmL() {
        var f2 = new FillUp(odometer: 1500, liters:40.0); 
        var f3 = new FillUp(2250, 56, true); 
        f2.NextFillUp = f3;

        var kml1 = f2.KilometersPerLiter;
        var kml2 = f3.KilometersPerLiter;

        Assert.NotNull(kml1);
        Assert.Equal(13.4, kml1);
        Assert.Null(kml2);
      }
    }

    public class IsForgotProperty
    {
      [Fact]
      public void ForgotInSecondFillUps_TheFirstIsNotKnown()
      {
        var f1 = new FillUp();
        f1.Odometer = 1000;
        f1.Liters = 50.0;
        f1.IsFull = true;

        var f2 = new FillUp();
        f2.Odometer = 1500;
        f2.Liters = 40;
        f2.IsFull = true;
        f2.IsForgot = true;

        f1.NextFillUp = f2;

        var kml1 = f1.KilometersPerLiter;
        var kml2 = f2.KilometersPerLiter;

        Assert.Null(kml1);
        Assert.Null(kml2);
      }


      [Fact]
      public void ForgotTreeFillUps_TheFirstIsNotKnown()
      {
        var f1 = new FillUp();
        f1.Odometer = 1000;
        f1.Liters = 50.0;
        f1.IsFull = true;

        var f2 = new FillUp();
        f2.Odometer = 1600;
        f2.Liters = 60;
        f2.IsFull = true;
        f2.IsForgot = true;

        f1.NextFillUp = f2;


        var f3 = new FillUp(2000, 50, true);
        f2.NextFillUp = f3;

        var kml1 = f1.KilometersPerLiter;
        var kml2 = f2.KilometersPerLiter;
        var kml3 = f3.KilometersPerLiter;

        Assert.Null(kml1);
        Assert.Equal(8,kml2);
        Assert.Null(kml3);
      }
    }

  }
}
