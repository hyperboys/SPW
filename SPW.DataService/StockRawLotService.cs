using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SPW.DAL;
using SPW.Model;

namespace SPW.DataService
{
    public class StockRawLotService : ServiceBase, IDataService<STOCK_RAW_LOT>, IService
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

        #region IDataService<STOCK_RAW_LOT> Members

        public void Add(STOCK_RAW_LOT obj)
        {
            this.Datacontext.STOCK_RAW_LOT.Add(obj);
            this.Datacontext.SaveChanges();
        }

        public void AddList(List<STOCK_RAW_LOT> obj)
        {
            throw new NotImplementedException();
        }

        public void Edit(STOCK_RAW_LOT obj)
        {
            var item = this.Datacontext.STOCK_RAW_LOT.Where(x => x.RAW_ID == obj.RAW_ID && x.LOT_NO == obj.LOT_NO).FirstOrDefault();
            item.RAW_REMAIN = obj.RAW_REMAIN;
            item.UPDATE_DATE = obj.UPDATE_DATE;
            item.UPDATE_EMPLOYEE_ID = obj.UPDATE_EMPLOYEE_ID;
            this.Datacontext.SaveChanges();
        }
        public void Edit(int RAW_ID, string LOT_NO, int RAW_REMAIN, int EMPLOYEE_ID)
        {
            var item = this.Datacontext.STOCK_RAW_LOT.Where(x => x.RAW_ID == RAW_ID && x.LOT_NO == LOT_NO).FirstOrDefault();
            item.RAW_REMAIN = RAW_REMAIN;
            item.UPDATE_DATE = DateTime.Now;
            item.UPDATE_EMPLOYEE_ID = EMPLOYEE_ID;
            this.Datacontext.SaveChanges();
        }

        public void Delete(int ID)
        {
            var obj = this.Datacontext.STOCK_RAW_LOT.Where(x => x.RAW_ID == ID).FirstOrDefault();
            this.Datacontext.STOCK_RAW_LOT.Remove(obj);
            this.Datacontext.SaveChanges();
        }

        public void EditList(List<STOCK_RAW_LOT> obj)
        {
            throw new NotImplementedException();
        }

        public STOCK_RAW_LOT Select()
        {
            throw new NotImplementedException();
        }

        public STOCK_RAW_LOT Select(int RAW_ID)
        {
            return this.Datacontext.STOCK_RAW_LOT.Where(x => x.SYE_DEL == false && x.RAW_ID == RAW_ID).FirstOrDefault();
        }

        public List<STOCK_RAW_LOT> GetAll()
        {
            return this.Datacontext.STOCK_RAW_LOT.Where(x => x.SYE_DEL == false).ToList();
        }

        public List<STOCK_RAW_LOT> GetAll(int RAW_ID)
        {
            return this.Datacontext.STOCK_RAW_LOT.Where(x => x.SYE_DEL == false && x.RAW_ID == RAW_ID).ToList();
        }

        public int GetAllCount()
        {
            return GetAll().Count();
        }
        public int GetNextTran()
        {
            return this.Datacontext.STOCK_RAW_LOT.Count() + 1;
        }
        public List<STOCK_RAW_LOT> GetAllByFilter(int PageIndex, int PageLimit)
        {
            return this.Datacontext.STOCK_RAW_LOT.Where(x => x.SYE_DEL == false).OrderBy(x => x.RAW_ID).Skip(PageLimit * (PageIndex - 1)).Take(PageLimit).ToList();
        }
        public bool isHasLot(int RAW_ID, string LOT_NO)
        {
            return this.Datacontext.STOCK_RAW_LOT.Any(x => x.SYE_DEL == false && x.RAW_ID == RAW_ID &&x.LOT_NO == LOT_NO);
        }
        public int GetRemainQty(int RAW_ID, string LOT_NO)
        {
            STOCK_RAW_LOT STOCK_RAW_LOT = Datacontext.STOCK_RAW_LOT.Where(x => x.SYE_DEL == false && x.RAW_ID == RAW_ID && x.LOT_NO == LOT_NO).FirstOrDefault();
            if (STOCK_RAW_LOT != null)
            {
                return (int)STOCK_RAW_LOT.RAW_REMAIN;
            }
            else
            {
                return 0;
            }
        }
        public void SetRawLotQty(int RAW_ID, int RAW_REMAIN, int UPDATE_EMPLOYEE_ID,string LOT_NO)
        {
            STOCK_RAW_LOT obj = Datacontext.STOCK_RAW_LOT.Where(x => x.RAW_ID == RAW_ID && x.LOT_NO == LOT_NO).FirstOrDefault();
            obj.RAW_REMAIN = RAW_REMAIN;
            obj.UPDATE_DATE = DateTime.Now;
            obj.UPDATE_EMPLOYEE_ID = UPDATE_EMPLOYEE_ID;
            Datacontext.SaveChanges();
        }
        #endregion

    }
}
