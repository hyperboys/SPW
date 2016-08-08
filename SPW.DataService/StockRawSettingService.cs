using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SPW.DAL;
using SPW.Model;

namespace SPW.DataService
{
    public class StockRawSettingService : ServiceBase, IDataService<STOCK_RAW_SETTING>, IService
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

        #region IDataService<STOCK_RAW_SETTING> Members

        public void Add(STOCK_RAW_SETTING obj)
        {
            this.Datacontext.STOCK_RAW_SETTING.Add(obj);
            this.Datacontext.SaveChanges();
        }

        public void AddList(List<STOCK_RAW_SETTING> obj)
        {
            throw new NotImplementedException();
        }

        public void Edit(STOCK_RAW_SETTING obj)
        {
            var item = this.Datacontext.STOCK_RAW_SETTING.Where(x => x.RAW_ID == obj.RAW_ID && x.VENDOR_ID == obj.VENDOR_ID).FirstOrDefault();
            item.STOCK_QTY = obj.STOCK_QTY;
            item.UPDATE_DATE = obj.UPDATE_DATE;
            item.UPDATE_EMPLOYEE_ID = obj.UPDATE_EMPLOYEE_ID;
            this.Datacontext.SaveChanges();
        }

        public void Delete(int ID)
        {
            var obj = this.Datacontext.STOCK_RAW_SETTING.Where(x => x.RAW_ID == ID).FirstOrDefault();
            this.Datacontext.STOCK_RAW_SETTING.Remove(obj);
            this.Datacontext.SaveChanges();
        }

        public void EditList(List<STOCK_RAW_SETTING> obj)
        {
            throw new NotImplementedException();
        }

        public STOCK_RAW_SETTING Select()
        {
            throw new NotImplementedException();
        }

        public STOCK_RAW_SETTING Select(int RAW_ID)
        {
            return this.Datacontext.STOCK_RAW_SETTING.Where(x => x.SYE_DEL == false && x.RAW_ID == RAW_ID).FirstOrDefault();
        }

        public List<STOCK_RAW_SETTING> GetAll()
        {
            return this.Datacontext.STOCK_RAW_SETTING.Where(x => x.SYE_DEL == false).ToList();
        }

        public List<STOCK_RAW_SETTING> GetAll(int RAW_ID)
        {
            return this.Datacontext.STOCK_RAW_SETTING.Where(x => x.SYE_DEL == false && x.RAW_ID == RAW_ID).ToList();
        }

        public int GetAllCount()
        {
            return GetAll().Count();
        }
        public int GetNextTran()
        {
            return this.Datacontext.STOCK_RAW_SETTING.Count() + 1;
        }
        public List<STOCK_RAW_SETTING> GetAllByFilter(int PageIndex, int PageLimit)
        {
            return this.Datacontext.STOCK_RAW_SETTING.Where(x => x.SYE_DEL == false).OrderBy(x => x.RAW_ID).Skip(PageLimit * (PageIndex - 1)).Take(PageLimit).ToList();
        }
        #endregion

    }
}
