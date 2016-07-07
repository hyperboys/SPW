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
    public class PoDtTransService : ServiceBase, IDataService<PO_DT_TRANS>, IService
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

        #region IDataService<PO_DT_TRANS> Members

        public void Add(PO_DT_TRANS obj)
        {
            this.Datacontext.PO_DT_TRANS.Add(obj);
            this.Datacontext.SaveChanges();
        }

        public void AddList(List<PO_DT_TRANS> lstItem)
        {
            try
            {
                foreach (var item in lstItem)
                {
                    if (item.Action == ActionEnum.Create)
                    {
                        Datacontext.PO_DT_TRANS.Add(item);
                    }
                }
                Datacontext.SaveChanges();
            }
            catch (Exception ex)
            {
                DebugLog.WriteLog(ex.ToString());
            }
        }

        public void Edit(PO_DT_TRANS obj)
        {
            var item = this.Datacontext.PO_DT_TRANS.Where(x => x.PO_BK_NO == obj.PO_BK_NO && x.PO_RN_NO == obj.PO_RN_NO).FirstOrDefault();

            item.UPDATE_DATE = obj.UPDATE_DATE;
            item.UPDATE_EMPLOYEE_ID = obj.UPDATE_EMPLOYEE_ID;
            this.Datacontext.SaveChanges();
        }

        public void Delete(string PO_BK_NO, string PO_RN_NO)
        {
            var obj = this.Datacontext.PO_DT_TRANS.Where(x => x.PO_BK_NO == PO_BK_NO && x.PO_RN_NO == PO_RN_NO).FirstOrDefault();
            this.Datacontext.PO_DT_TRANS.Remove(obj);
            this.Datacontext.SaveChanges();
        }

        public void EditList(List<PO_DT_TRANS> obj)
        {
            throw new NotImplementedException();
        }

        public PO_DT_TRANS Select()
        {
            throw new NotImplementedException();
        }

        public List<PO_DT_TRANS> Select(string PO_BK_NO, string PO_RN_NO)
        {
            return this.Datacontext.PO_DT_TRANS.Where(x => x.PO_BK_NO == PO_BK_NO && x.PO_RN_NO == PO_RN_NO).ToList();
        }

        public List<PO_DT_TRANS> GetAll()
        {
            return this.Datacontext.PO_DT_TRANS.Where(x => x.SYE_DEL == false).ToList();
        }
        public List<PO_DT_TRANS> GetAll(string PO_BK_NO, string PO_RN_NO)
        {
            return this.Datacontext.PO_DT_TRANS.Where(x => x.SYE_DEL == false && x.PO_BK_NO.Contains(PO_BK_NO) && x.PO_RN_NO.Contains(PO_RN_NO)).ToList();
        }
        public int GetAllCount()
        {
            return GetAll().Count();
        }
        public List<PO_DT_TRANS> GetAllByFilter(int PageIndex, int PageLimit)
        {
            return this.Datacontext.PO_DT_TRANS.Where(x => x.SYE_DEL == false).OrderBy(x => x.PO_RN_NO).Skip(PageLimit * (PageIndex - 1)).Take(PageLimit).ToList();
        }
        public string GetMaxBKNo()
        {
            PO_DT_TRANS _PO_DT_TRANS = new PO_DT_TRANS();
            _PO_DT_TRANS = this.Datacontext.PO_DT_TRANS.Where(e => e.PO_BK_NO.Contains("BK-PO-")).OrderByDescending(x => x.PO_BK_NO).FirstOrDefault();
            return (_PO_DT_TRANS == null) ? null : _PO_DT_TRANS.PO_BK_NO;
        }
        public string GetMaxRNNo(string _maxBKNo)
        {
            PO_DT_TRANS _PO_DT_TRANS = new PO_DT_TRANS();
            _PO_DT_TRANS = this.Datacontext.PO_DT_TRANS.Where(e => e.PO_BK_NO.Contains(_maxBKNo)).OrderByDescending(x => x.PO_RN_NO).FirstOrDefault();
            return (_PO_DT_TRANS == null) ? null : _PO_DT_TRANS.PO_RN_NO;
        }
        public void UpdateStatusPoDt(string PO_BK_NO, string PO_RN_NO, int EMPLOYEE_ID, string PO_HD_STATUS)
        {
            try
            {
                var lstItem = this.Datacontext.PO_DT_TRANS.Where(x => x.PO_BK_NO == PO_BK_NO && x.PO_RN_NO == PO_RN_NO);
                foreach (var item in lstItem)
                {
                    item.PO_DT_STATUS = PO_HD_STATUS;
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
