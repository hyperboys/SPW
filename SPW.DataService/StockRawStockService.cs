using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SPW.DAL;
using SPW.Model;

namespace SPW.DataService
{
    public class StockRawStockService : ServiceBase, IDataService<STOCK_RAW_STOCK>, IService
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

        public void Add(STOCK_RAW_STOCK item)
        {
            if (item.Action == ActionEnum.Create)
            {
                Datacontext.STOCK_RAW_STOCK.Add(item);
                Datacontext.SaveChanges();
            }
        }

        public void AddList(List<STOCK_RAW_STOCK> lstItem)
        {
            foreach (var item in lstItem)
            {
                if (item.Action == ActionEnum.Create)
                {
                    Datacontext.STOCK_RAW_STOCK.Add(item);
                }
            }
            Datacontext.SaveChanges();
        }

        public void Edit(STOCK_RAW_STOCK item)
        {
            STOCK_RAW_STOCK obj = Datacontext.STOCK_RAW_STOCK.Where(x => x.RAW_ID == item.RAW_ID).FirstOrDefault();
            obj.RAW_REMAIN = item.RAW_REMAIN;
            obj.UPDATE_DATE = item.UPDATE_DATE;
            obj.UPDATE_EMPLOYEE_ID = item.UPDATE_EMPLOYEE_ID;
            Datacontext.SaveChanges();
        }

        public void EditList(List<STOCK_RAW_STOCK> lstItem)
        {
            foreach (var item in lstItem)
            {
                if (item.Action == ActionEnum.Update)
                {
                    STOCK_RAW_STOCK obj = Datacontext.STOCK_RAW_STOCK.Where(x => x.RAW_ID == item.RAW_ID).FirstOrDefault();
                    if (obj != null)
                    {
                        obj.RAW_REMAIN = item.RAW_REMAIN;
                        obj.RAW_MINIMUM = item.RAW_MINIMUM;
                        obj.UPDATE_DATE = item.UPDATE_DATE;
                        obj.UPDATE_EMPLOYEE_ID = item.UPDATE_EMPLOYEE_ID;
                        Datacontext.SaveChanges();
                    }
                }
            }
        }

        public STOCK_RAW_STOCK Select()
        {
            throw new NotImplementedException();
        }

        public STOCK_RAW_STOCK Select(int ID)
        {
            return Datacontext.STOCK_RAW_STOCK.Where(x => x.RAW_ID == ID).FirstOrDefault();
        }

        public List<STOCK_RAW_STOCK> GetAll()
        {
            return this.Datacontext.STOCK_RAW_STOCK.Where(x => x.SYE_DEL == false).ToList();
        }

        public List<STOCK_RAW_STOCK> GetAll(STOCK_RAW_STOCK item)
        {
            return Datacontext.STOCK_RAW_STOCK.Where(x => x.RAW_ID == item.RAW_ID).ToList();
        }
        public int GetRemainQty(int RAW_ID)
        {
            STOCK_RAW_STOCK _STOCK_RAW_STOCK = Datacontext.STOCK_RAW_STOCK.Where(x => x.RAW_ID == RAW_ID).FirstOrDefault();
            if (_STOCK_RAW_STOCK != null)
            {
                return (int)_STOCK_RAW_STOCK.RAW_REMAIN;
            }
            else
            {
                return 0;
            }
        }
        public void SetRawStockQty(int RAW_ID, int RAW_REMAIN, int UPDATE_EMPLOYEE_ID)
        {
            STOCK_RAW_STOCK obj = Datacontext.STOCK_RAW_STOCK.Where(x => x.RAW_ID == RAW_ID).FirstOrDefault();
            obj.RAW_REMAIN = RAW_REMAIN;
            obj.UPDATE_DATE = DateTime.Now;
            obj.UPDATE_EMPLOYEE_ID = UPDATE_EMPLOYEE_ID;
            Datacontext.SaveChanges();
        }
    }
}