using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SPW.DAL;
using SPW.Model;

namespace SPW.DataService
{
    public class StockProductWithdrawService : ServiceBase, IDataService<STOCK_PRODUCT_WITHDRAW_TRANS>, IService
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

        public void Add(STOCK_PRODUCT_WITHDRAW_TRANS item)
        {
            Datacontext.STOCK_PRODUCT_WITHDRAW_TRANS.Add(item);
            Datacontext.SaveChanges();
        }


        public void AddList(List<STOCK_PRODUCT_WITHDRAW_TRANS> obj)
        {
            throw new NotImplementedException();
        }

        public void Edit(STOCK_PRODUCT_WITHDRAW_TRANS obj)
        {
            throw new NotImplementedException();
        }

        public void EditList(List<STOCK_PRODUCT_WITHDRAW_TRANS> obj)
        {
            throw new NotImplementedException();
        }

        public STOCK_PRODUCT_WITHDRAW_TRANS Select()
        {
            throw new NotImplementedException();
        }

        public List<STOCK_PRODUCT_WITHDRAW_TRANS> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
