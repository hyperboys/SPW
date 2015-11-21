using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SPW.Model;

namespace SPW.DataService
{
    public class DeliveryIndexDetailsService : ServiceBase, IDataService<DELIVERY_INDEX_DETAIL>, IService 
    {
        #region IService Members

        public DAL.SPWEntities Datacontext
        {
            get
            {
                return this._Datacontext;
            }
            set
            {
                this._Datacontext = value;
            }
        }

        #endregion

        #region IDataService<DELIVERY_INDEX_DETAIL> Members

        public void Add(DELIVERY_INDEX_DETAIL obj)
        {
            throw new NotImplementedException();
        }

        public void AddList(List<DELIVERY_INDEX_DETAIL> obj)
        {
            throw new NotImplementedException();
        }

        public void Edit(DELIVERY_INDEX_DETAIL obj)
        {
            throw new NotImplementedException();
        }

        public void EditList(List<DELIVERY_INDEX_DETAIL> obj)
        {
            throw new NotImplementedException();
        }

        public DELIVERY_INDEX_DETAIL Select()
        {
            throw new NotImplementedException();
        }

        public List<DELIVERY_INDEX_DETAIL> GetAll()
        {
            throw new NotImplementedException();
        }

        public List<DELIVERY_INDEX_DETAIL> GetAllIncludeByIndex(int IndexID)
        {
            return this._Datacontext.DELIVERY_INDEX_DETAIL.Include("PRODUCT").Where(x => x.DELIND_ID == IndexID).ToList();
        }
        #endregion
    }
}
