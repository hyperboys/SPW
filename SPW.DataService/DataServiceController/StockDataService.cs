//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using SPW.Model;

//namespace SPW.DataService.DataServiceController
//{
//    public class StockDataService : ServiceBase, IDataService<STOCK_PRODUCT_WITHDRAW_TRANS> , IService 
//    {
//        #region IService Members
//        public DAL.SPWEntities Datacontext
//        {
//            get
//            {
//                return this._Datacontext;
//            }
//            set
//            {
//                this._Datacontext = value;
//            }
//        }
//        #endregion

//        #region IDataService<STOCK_PRODUCT_WITHDRAW_TRANS> Members

//        public void Add(STOCK_PRODUCT_WITHDRAW_TRANS obj)
//        {
//            throw new NotImplementedException();
//        }

//        public void AddList(List<STOCK_PRODUCT_WITHDRAW_TRANS> obj)
//        {
//            throw new NotImplementedException();
//        }

//        public void Edit(STOCK_PRODUCT_WITHDRAW_TRANS obj)
//        {
//            throw new NotImplementedException();
//        }

//        public void EditList(List<STOCK_PRODUCT_WITHDRAW_TRANS> obj)
//        {
//            throw new NotImplementedException();
//        }

//        public STOCK_PRODUCT_WITHDRAW_TRANS Select()
//        {
//            throw new NotImplementedException();
//        }

//        public List<STOCK_PRODUCT_WITHDRAW_TRANS> GetAll()
//        {
//            return this.Datacontext.STOCK_PRODUCT_WITHDRAW_TRANS.ToList();
//        }
//        #endregion
//    }
//}
