using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SPW.DAL;
using SPW.Model;
using SPW.Common;

namespace SPW.DataService
{
    public class StockRawTransService : ServiceBase, IDataService<STOCK_RAW_TRANS>, IService
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

        #region IDataService<STOCK_RAW_TRANS> Members

        public void Add(STOCK_RAW_TRANS obj)
        {
            this.Datacontext.STOCK_RAW_TRANS.Add(obj);
            this.Datacontext.SaveChanges();
        }

        public void AddList(List<STOCK_RAW_TRANS> lstItem)
        {
            try
            {
                foreach (var item in lstItem)
                {
                    if (item.Action == ActionEnum.Create)
                    {
                        Datacontext.STOCK_RAW_TRANS.Add(item);
                    }
                }
                Datacontext.SaveChanges();
            }
            catch (Exception ex)
            {
                DebugLog.WriteLog(ex.ToString());
            }
        }

        public void Edit(STOCK_RAW_TRANS obj)
        {
            var item = this.Datacontext.STOCK_RAW_TRANS.Where(x => x.TRANS_ID == obj.TRANS_ID).FirstOrDefault();

            item.UPDATE_DATE = obj.UPDATE_DATE;
            item.UPDATE_EMPLOYEE_ID = obj.UPDATE_EMPLOYEE_ID;
            this.Datacontext.SaveChanges();
        }

        public void Delete(string PO_BK_NO, string PO_RN_NO)
        {
            throw new NotImplementedException();
        }

        public void EditList(List<STOCK_RAW_TRANS> obj)
        {
            throw new NotImplementedException();
        }

        public STOCK_RAW_TRANS Select()
        {
            throw new NotImplementedException();
        }

        public STOCK_RAW_TRANS Select(string PO_BK_NO, string PO_RN_NO)
        {
            throw new NotImplementedException();
        }

        public List<STOCK_RAW_TRANS> GetAll()
        {
            return this.Datacontext.STOCK_RAW_TRANS.Where(x => x.SYE_DEL == false).ToList();
        }
        public List<STOCK_RAW_TRANS> GetAll(string PO_BK_NO, string PO_RN_NO)
        {
            throw new NotImplementedException();
        }
        public int GetAllCount()
        {
            return GetAll().Count();
        }
        public List<STOCK_RAW_TRANS> GetAllByFilter(int PageIndex, int PageLimit)
        {
            return this.Datacontext.STOCK_RAW_TRANS.Where(x => x.SYE_DEL == false).OrderBy(x => x.TRANS_ID).Skip(PageLimit * (PageIndex - 1)).Take(PageLimit).ToList();
        }
        public int GetNextTransID()
        {
            STOCK_RAW_TRANS _STOCK_RAW_TRANS = Datacontext.STOCK_RAW_TRANS.OrderByDescending(e => e.TRANS_ID).FirstOrDefault();
            if (_STOCK_RAW_TRANS != null)
            {
                return (int)_STOCK_RAW_TRANS.TRANS_ID+1;
            }
            else
            {
                return 0;
            }
        }
        #endregion
    }
}
