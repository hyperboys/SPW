using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPW.DataService
{
    public class DataServiceEngine : DataServiceBase , IDisposable
    {
        List<IService> _serviceItems;
        public IService GetDataService(Type ServiceType)
        {
            this.InitializeEntities();
            IService objService = _serviceItems.Where(x => x.GetType() == ServiceType).FirstOrDefault();
            if (objService == null)
            {
                objService = (IService)Activator.CreateInstance(ServiceType);
                objService.Datacontext = this.Datacontext;
                _serviceItems.Add(objService);
            }
            return (objService);
        }

        public DataServiceEngine() 
        {
            this._serviceItems = new List<IService>();
        }

        #region IDisposable Members

        public void Dispose()
        {
            this.Dispose();
        }

        #endregion
    }
}
