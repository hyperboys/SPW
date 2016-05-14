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
    public class PrDtTransService : ServiceBase, IDataService<PR_DT_TRANS>, IService
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

        #region IDataService<PR_DT_TRANS> Members

        public void Add(PR_DT_TRANS obj)
        {
            this.Datacontext.PR_DT_TRANS.Add(obj);
            this.Datacontext.SaveChanges();
        }

        public void AddList(List<PR_DT_TRANS> lstItem)
        {
            try
            {
                foreach (var item in lstItem)
                {
                    //if (item.Action == ActionEnum.Create)
                    //{
                    //    Datacontext.PR_DT_TRANS.Add(item);
                    //}
                }
                Datacontext.SaveChanges();
            }
            catch (Exception ex)
            {
                DebugLog.WriteLog(ex.ToString());
            }
        }

        public void Edit(PR_DT_TRANS obj)
        {
            var item = this.Datacontext.PR_DT_TRANS.Where(x => x.PR_BK_NO == obj.PR_BK_NO && x.PR_RN_NO == obj.PR_RN_NO).FirstOrDefault();

            item.UPDATE_DATE = obj.UPDATE_DATE;
            item.UPDATE_EMPLOYEE_ID = obj.UPDATE_EMPLOYEE_ID;
            this.Datacontext.SaveChanges();
        }

        public void Delete(string PR_BK_NO, string PR_RN_NO)
        {
            var obj = this.Datacontext.PR_DT_TRANS.Where(x => x.PR_BK_NO == PR_BK_NO && x.PR_RN_NO == PR_RN_NO).FirstOrDefault();
            this.Datacontext.PR_DT_TRANS.Remove(obj);
            this.Datacontext.SaveChanges();
        }

        public void EditList(List<PR_DT_TRANS> obj)
        {
            throw new NotImplementedException();
        }

        public PR_DT_TRANS Select()
        {
            throw new NotImplementedException();
        }

        public List<PR_DT_TRANS> Select(string PR_BK_NO, string PR_RN_NO)
        {
            return this.Datacontext.PR_DT_TRANS.Where(x => x.PR_BK_NO == PR_BK_NO && x.PR_RN_NO == PR_RN_NO).ToList();
        }

        public List<PR_DT_TRANS> GetAll()
        {
            return this.Datacontext.PR_DT_TRANS.Where(x => x.SYE_DEL == false).ToList();
        }
        public List<PR_DT_TRANS> GetAll(string PR_BK_NO, string PR_RN_NO)
        {
            return this.Datacontext.PR_DT_TRANS.Where(x => x.SYE_DEL == false && x.PR_BK_NO.Contains(PR_BK_NO) && x.PR_RN_NO.Contains(PR_RN_NO)).ToList();
        }
        public int GetAllCount()
        {
            return GetAll().Count();
        }
        public List<PR_DT_TRANS> GetAllByFilter(int PageIndex, int PageLimit)
        {
            return this.Datacontext.PR_DT_TRANS.Where(x => x.SYE_DEL == false).OrderBy(x => x.PR_RN_NO).Skip(PageLimit * (PageIndex - 1)).Take(PageLimit).ToList();
        }
        public string GetMaxBKNo()
        {
            PR_DT_TRANS _PR_DT_TRANS = new PR_DT_TRANS();
            _PR_DT_TRANS = this.Datacontext.PR_DT_TRANS.Where(e => e.PR_BK_NO.Contains("BK-PR-")).OrderByDescending(x => x.PR_BK_NO).FirstOrDefault();
            return (_PR_DT_TRANS == null) ? null : _PR_DT_TRANS.PR_BK_NO;
        }
        public string GetMaxRNNo(string _maxBKNo)
        {
            PR_DT_TRANS _PR_DT_TRANS = new PR_DT_TRANS();
            _PR_DT_TRANS = this.Datacontext.PR_DT_TRANS.Where(e => e.PR_BK_NO.Contains(_maxBKNo)).OrderByDescending(x => x.PR_RN_NO).FirstOrDefault();
            return (_PR_DT_TRANS == null) ? null : _PR_DT_TRANS.PR_RN_NO;
        }
        public void UpdateStatusPRToConvert(string PR_BK_NO, string PR_RN_NO, int EMPLOYEE_ID)
        {
            try
            {
                var lstItem = this.Datacontext.PR_DT_TRANS.Where(x => x.PR_BK_NO == PR_BK_NO && x.PR_RN_NO == PR_RN_NO);
                foreach (var item in lstItem)
                {
                    item.PR_DT_STATUS = "20";
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
