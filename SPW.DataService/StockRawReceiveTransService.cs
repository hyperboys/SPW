using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SPW.DAL;
using SPW.Model;

namespace SPW.DataService
{
    public class StockRawReceiveTransService : ServiceBase, IDataService<STOCK_RAW_RECEIVE_TRANS>, IService
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

        public void Add(STOCK_RAW_RECEIVE_TRANS item)
        {
            Datacontext.STOCK_RAW_RECEIVE_TRANS.Add(item);
            Datacontext.SaveChanges();
        }

        public void AddList(List<STOCK_RAW_RECEIVE_TRANS> lstItem)
        {
            foreach (var item in lstItem)
            {
                if (item.Action == ActionEnum.Create)
                {
                    Datacontext.STOCK_RAW_RECEIVE_TRANS.Add(item);
                }
            }
            Datacontext.SaveChanges();
        }

        public void Edit(STOCK_RAW_RECEIVE_TRANS item)
        {
            STOCK_RAW_RECEIVE_TRANS obj = Datacontext.STOCK_RAW_RECEIVE_TRANS.Where(x => x.RAW_ID == item.RAW_ID).FirstOrDefault();
            //obj.STOCK_REMAIN = item.STOCK_REMAIN;
            obj.UPDATE_DATE = item.UPDATE_DATE;
            obj.UPDATE_EMPLOYEE_ID = item.UPDATE_EMPLOYEE_ID;
            Datacontext.SaveChanges();
        }

        public void EditList(List<STOCK_RAW_RECEIVE_TRANS> lstItem)
        {
            foreach (var item in lstItem)
            {
                if (item.Action == ActionEnum.Update)
                {
                    STOCK_RAW_RECEIVE_TRANS obj = Datacontext.STOCK_RAW_RECEIVE_TRANS.Where(x => x.RAW_ID == item.RAW_ID).FirstOrDefault();
                    if (obj != null)
                    {
                        //obj.STOCK_REMAIN = item.STOCK_REMAIN;
                        obj.UPDATE_DATE = item.UPDATE_DATE;
                        obj.UPDATE_EMPLOYEE_ID = item.UPDATE_EMPLOYEE_ID;
                        Datacontext.SaveChanges();
                    }
                }
            }
        }

        public STOCK_RAW_RECEIVE_TRANS Select()
        {
            throw new NotImplementedException();
        }

        public STOCK_RAW_RECEIVE_TRANS Select(int ID)
        {
            return Datacontext.STOCK_RAW_RECEIVE_TRANS.Include("COLOR").Where(x => x.RAW_ID == ID).FirstOrDefault();
        }

        public List<STOCK_RAW_RECEIVE_TRANS> GetAll()
        {
            return this.Datacontext.STOCK_RAW_RECEIVE_TRANS.Include("COLOR").Where(x => x.SYE_DEL == false).ToList();
        }

        public List<STOCK_RAW_RECEIVE_TRANS> GetAllColorDetail()
        {
            return this.Datacontext.STOCK_RAW_RECEIVE_TRANS.Include("COLOR").Include("COLOR_TYPE").Where(x => x.SYE_DEL == false).ToList();
        }

        public List<STOCK_RAW_RECEIVE_TRANS> GetAll(int CATEGORY)
        {
            var a = Datacontext.STOCK_RAW_RECEIVE_TRANS.Include("COLOR")
                .ToList();
            return null;
        }

        public List<STOCK_RAW_RECEIVE_TRANS> GetAll(STOCK_RAW_RECEIVE_TRANS item)
        {
            return Datacontext.STOCK_RAW_RECEIVE_TRANS.Where(x => x.RAW_ID == item.RAW_ID).ToList();
        }
    }
}