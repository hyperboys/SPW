using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SPW.DAL;

namespace SPW.DataService
{
    public class DataServiceBase
    {
        protected SPWEntities Datacontext { get; set; }
        protected void InitializeEntities() 
        {
            if (Datacontext == null)
            {
                this.Datacontext = new SPWEntities();
                this.Datacontext.Database.Connection.Open();
                this.Datacontext.Configuration.LazyLoadingEnabled = false;
            }
        }
    }
}
