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


      
    }
}
