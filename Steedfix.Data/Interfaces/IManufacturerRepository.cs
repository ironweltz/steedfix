using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Steedfix.Domain;

namespace Steedfix.Data.Interfaces
{
    public interface IManufacturerRepository : IRepository<Manufacturer>
    {
    }
}
