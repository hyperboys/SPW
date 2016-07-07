using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SPW.DAL;
using SPW.Model;

namespace SPW.DataService
{
    public class PoHdTransService : ServiceBase, IDataService<PO_HD_TRANS>, IService
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

        #region IDataService<PO_HD_TRANS> Members

        public void Add(PO_HD_TRANS obj)
        {
            this.Datacontext.PO_HD_TRANS.Add(obj);
            this.Datacontext.SaveChanges();
        }

        public void AddList(List<PO_HD_TRANS> obj)
        {
            throw new NotImplementedException();
        }

        public void Edit(PO_HD_TRANS obj)
        {
            var item = this.Datacontext.PO_HD_TRANS.Where(x => x.PO_BK_NO == obj.PO_BK_NO && x.PO_RN_NO == obj.PO_RN_NO).FirstOrDefault();

            item.UPDATE_DATE = obj.UPDATE_DATE;
            item.UPDATE_EMPLOYEE_ID = obj.UPDATE_EMPLOYEE_ID;
            this.Datacontext.SaveChanges();
        }

        public void Delete(string PO_BK_NO, string PO_RN_NO)
        {
            var obj = this.Datacontext.PO_HD_TRANS.Where(x => x.PO_BK_NO == PO_BK_NO && x.PO_RN_NO == PO_RN_NO).FirstOrDefault();
            this.Datacontext.PO_HD_TRANS.Remove(obj);
            this.Datacontext.SaveChanges();
        }

        public void EditList(List<PO_HD_TRANS> obj)
        {
            throw new NotImplementedException();
        }

        public PO_HD_TRANS Select()
        {
            throw new NotImplementedException();
        }

        public PO_HD_TRANS Select(string PO_BK_NO, string PO_RN_NO)
        {
            return this.Datacontext.PO_HD_TRANS.Where(x => x.PO_BK_NO == PO_BK_NO && x.PO_RN_NO == PO_RN_NO).FirstOrDefault();
        }

        public List<PO_HD_TRANS> GetAll()
        {
            return this.Datacontext.PO_HD_TRANS.Where(x => x.SYE_DEL == false).ToList();
        }
        public List<PO_HD_TRANS> GetAll(string PO_BK_NO, string PO_RN_NO)
        {
            return this.Datacontext.PO_HD_TRANS.Where(x => x.SYE_DEL == false && x.PO_BK_NO.Contains(PO_BK_NO) && x.PO_RN_NO.Contains(PO_RN_NO)).ToList();
        }
        public int GetAllCount()
        {
            return GetAll().Count();
        }
        public List<PO_HD_TRANS> GetAllByStatusApprove()
        {
            return this.Datacontext.PO_HD_TRANS.Where(x => x.SYE_DEL == false).ToList();
        }
        public List<PO_HD_TRANS> GetAllByFilter(int PageIndex, int PageLimit)
        {
            return this.Datacontext.PO_HD_TRANS.Where(x => x.SYE_DEL == false).OrderBy(x => x.PO_RN_NO).Skip(PageLimit * (PageIndex - 1)).Take(PageLimit).ToList();
        }
        public string GetMaxBKNo()
        {
            PO_HD_TRANS _PO_HD_TRANS = new PO_HD_TRANS();
            _PO_HD_TRANS = this.Datacontext.PO_HD_TRANS.Where(e => e.PO_BK_NO.Contains("BK-PO-")).OrderByDescending(x => x.PO_BK_NO).FirstOrDefault();
            return (_PO_HD_TRANS == null) ? null : _PO_HD_TRANS.PO_BK_NO;
        }
        public string GetMaxRNNo(string _maxBKNo)
        {
            PO_HD_TRANS _PO_HD_TRANS = new PO_HD_TRANS();
            _PO_HD_TRANS = this.Datacontext.PO_HD_TRANS.Where(e => e.PO_BK_NO.Contains(_maxBKNo)).OrderByDescending(x => x.PO_RN_NO).FirstOrDefault();
            return (_PO_HD_TRANS == null) ? null : _PO_HD_TRANS.PO_RN_NO;
        }
        public bool UpdateStatusPoHd(string PO_BK_NO, string PO_RN_NO, int EMPLOYEE_ID, string PO_HD_STATUS)
        {
            var item = this.Datacontext.PO_HD_TRANS.Where(x => x.PO_BK_NO == PO_BK_NO && x.PO_RN_NO == PO_RN_NO).FirstOrDefault();

            item.PO_HD_STATUS = PO_HD_STATUS;
            item.UPDATE_DATE = DateTime.Now;
            item.UPDATE_EMPLOYEE_ID = EMPLOYEE_ID;
            this.Datacontext.SaveChanges();
            return true;
        }
        #endregion
    }
}
