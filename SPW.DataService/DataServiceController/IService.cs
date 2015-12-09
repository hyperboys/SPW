using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SPW.DAL;

namespace SPW.DataService
{
    public interface IService
    {
        SPWEntities Datacontext { get; set; }
    }
}
