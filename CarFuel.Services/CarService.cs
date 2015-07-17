using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarFuel.DataAccess;
using CarFuel.Models;
using CarFuel.DataAccess.Repository;
using CarFuel.DataAccess.Context;
using GFX.Core;

namespace CarFuel.Services
{
    public class CarService : AppServiceBase<Car>
    {

      #region Service<T>
      public override IRepository<Car> Repository { get; set; }

      public override Car Find(params object[] keys)
      {
        Guid key1 = (Guid)keys[0];
        return Query(x => x.Id == key1).SingleOrDefault();
      }
      #endregion

      public override Car Add(Car item)
      {
        var allowedMakes = new string[] { "Honda", "Toyota" };
        if (!allowedMakes.Contains(item.Make))
        {
          throw new Exception("Invalid make. Can't add.");
        }
       
        return base.Add(item);
      }


      
    }
}
