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
    public class WrDtTransService : ServiceBase, IDataService<WR_DT_TRANS>, IService
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

        #region IDataService<WR_DT_TRANS> Members

        public void Add(WR_DT_TRANS obj)
        {
            this.Datacontext.WR_DT_TRANS.Add(obj);
            this.Datacontext.SaveChanges();
        }
        public void AddList(List<WR_DT_TRANS> obj)
        {
            throw new NotImplementedException();
        }
        public bool AddLists(List<WR_DT_TRANS> obj)
        {
            try
            {
                foreach (var item in obj)
                {
                    Datacontext.WR_DT_TRANS.Add(item);
                }
                Datacontext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                DebugLog.WriteLog(ex.ToString());
                return false;
            }
        }

        public void Edit(WR_DT_TRANS obj)
        {
            var item = this.Datacontext.WR_DT_TRANS.Where(x => x.SYE_DEL == false).FirstOrDefault();

            item.UPDATE_DATE = obj.UPDATE_DATE;
            item.UPDATE_EMPLOYEE_ID = obj.UPDATE_EMPLOYEE_ID;
            this.Datacontext.SaveChanges();
        }

        public void Delete(string PR_BK_NO, string PR_RN_NO)
        {
            var obj = this.Datacontext.WR_DT_TRANS.Where(x => x.SYE_DEL == false).FirstOrDefault();
            this.Datacontext.WR_DT_TRANS.Remove(obj);
            this.Datacontext.SaveChanges();
        }

        public void EditList(List<WR_DT_TRANS> obj)
        {
            throw new NotImplementedException();
        }

        public WR_DT_TRANS Select()
        {
            throw new NotImplementedException();
        }

        public WR_DT_TRANS Select(string WR_BK_NO, string WR_RN_NO, int RAW_ID)
        {
            return this.Datacontext.WR_DT_TRANS.Where(x => x.SYE_DEL == false && x.WR_BK_NO == WR_BK_NO && x.WR_RN_NO == WR_RN_NO && x.RAW_ID == RAW_ID).FirstOrDefault();
        }

        public List<WR_DT_TRANS> GetAll()
        {
            return this.Datacontext.WR_DT_TRANS.Where(x => x.SYE_DEL == false).ToList();
        }
        public List<WR_DT_TRANS> GetAll(string WR_BK_NO, string WR_RN_NO)
        {
            return this.Datacontext.WR_DT_TRANS.Where(x => x.SYE_DEL == false && x.WR_BK_NO == WR_BK_NO && x.WR_RN_NO == WR_RN_NO).ToList();
        }
        public List<WR_DT_TRANS> GetAllByStatusRequest()
        {
            return this.Datacontext.WR_DT_TRANS.Where(x => x.SYE_DEL == false && x.WR_DT_STATUS == "10").ToList();
        }
        public List<WR_DT_TRANS> GetAllByStatusApprove()
        {
            return this.Datacontext.WR_DT_TRANS.Where(x => x.SYE_DEL == false && x.WR_DT_STATUS == "20").ToList();
        }
        public int GetAllCount()
        {
            return GetAll().Count();
        }
        public List<WR_DT_TRANS> GetAllByFilter(int PageIndex, int PageLimit)
        {
            return this.Datacontext.WR_DT_TRANS.Where(x => x.SYE_DEL == false).OrderBy(x => x.WR_BK_NO).Skip(PageLimit * (PageIndex - 1)).Take(PageLimit).ToList();
        }
        public string GetMaxBKNo()
        {
            WR_DT_TRANS _WR_DT_TRANS = new WR_DT_TRANS();
            _WR_DT_TRANS = this.Datacontext.WR_DT_TRANS.Where(e => e.WR_BK_NO.Contains("BK-WR-")).OrderByDescending(x => x.WR_BK_NO).FirstOrDefault();
            return (_WR_DT_TRANS == null) ? null : _WR_DT_TRANS.WR_BK_NO;
        }
        public string GetMaxRNNo(string _maxBKNo)
        {
            WR_DT_TRANS _WR_DT_TRANS = new WR_DT_TRANS();
            _WR_DT_TRANS = this.Datacontext.WR_DT_TRANS.Where(e => e.WR_BK_NO.Contains(_maxBKNo)).OrderByDescending(x => x.WR_RN_NO).FirstOrDefault();
            return (_WR_DT_TRANS == null) ? null : _WR_DT_TRANS.WR_RN_NO;
        }
        public void UpdateStatusWrDt(string WR_BK_NO, string WR_RN_NO, int EMPLOYEE_ID, string WR_HD_STATUS)
        {
            try
            {
                var lstItem = this.Datacontext.WR_DT_TRANS.Where(x => x.WR_BK_NO == WR_BK_NO && x.WR_RN_NO == WR_RN_NO);
                foreach (var item in lstItem)
                {
                    item.WR_DT_STATUS = WR_HD_STATUS;
                    item.UPDATE_DATE = DateTime.Now;
                    item.UPDATE_EMPLOYEE_ID = EMPLOYEE_ID;
                }
                Datacontext.SaveChanges();
            }
            catch (Exception ex)
            {
                DebugLog.WriteLog(ex.ToString());
            }
        }
        public void UpdateStatusWrDtByProduct(int RAW_ID,string WR_BK_NO, string WR_RN_NO, int EMPLOYEE_ID, string WR_HD_STATUS)
        {
            try
            {
                var lstItem = this.Datacontext.WR_DT_TRANS.Where(x => x.WR_BK_NO == WR_BK_NO && x.WR_RN_NO == WR_RN_NO && x.RAW_ID == RAW_ID);
                foreach (var item in lstItem)
                {
                    item.WR_DT_STATUS = WR_HD_STATUS;
                    item.UPDATE_DATE = DateTime.Now;
                    item.UPDATE_EMPLOYEE_ID = EMPLOYEE_ID;
                }
                Datacontext.SaveChanges();
            }
            catch (Exception ex)
            {
                DebugLog.WriteLog(ex.ToString());
            }
        }

        #endregion
    }
}