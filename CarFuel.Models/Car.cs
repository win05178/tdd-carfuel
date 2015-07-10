using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CarFuel.Models
{
  public class Car
  {
    public string Make { get; set; }

    public string Model { get; set; }

    public virtual ICollection<FillUp> FillUps { get; set; }

     public double? get_AveragekilomaterPerLister
    {
       get {

     
         if (this.FillUps.Count< 1)
         {

           return null;
         }
         else
         {



           FillUp first;
             FillUp  last = FillUps.Last();
             double sumKML = 0.0;
             int blocks = 0;



             do
             {
               var liters = 0.0;
               while (last.IsForgot && last.PreviousFillUp != null)
               {
                 last = last.PreviousFillUp;
               }

               first = last;
               while (first.PreviousFillUp != null)
               {
                 liters += first.Liters;
                 first = first.PreviousFillUp;
                 if (first.IsForgot) break;
               }

               var distance = last.Odometer - first.Odometer;

               if (liters > 0)
               {
                 var kml = Math.Round(distance / liters, 1, MidpointRounding.AwayFromZero);

                 sumKML += kml;
                 blocks++;
               }

               last = first.PreviousFillUp;

             } while (last!=null);

             return blocks;

          
         }
      }
    }

    public Car()
    {
      FillUps = new HashSet<FillUp>();
    }

    public FillUp AddFillUp(int odometer, double liters, bool isFull,bool isForgot=false)
    {
      var f = new FillUp();
      f.Odometer = odometer;
      f.Liters = liters;
      f.IsFull = isFull;
      f.IsForgot = isForgot;
     

      if (this.FillUps.Count > 0)
      {
        this.FillUps.Last().NextFillUp = f;
        f.PreviousFillUp = this.FillUps.Last();

      }

      this.FillUps.Add(f);

      return f;
    }

   
  }

}
