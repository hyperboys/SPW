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
    public class ReceiveRawTransService : ServiceBase, IDataService<RECEIVE_RAW_TRANS>, IService
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

        #region IDataService<RECEIVE_RAW_TRANS> Members

        public void Add(RECEIVE_RAW_TRANS obj)
        {
            this.Datacontext.RECEIVE_RAW_TRANS.Add(obj);
            this.Datacontext.SaveChanges();
        }

        public void AddList(List<RECEIVE_RAW_TRANS> lstItem)
        {
            try
            {
                foreach (var item in lstItem)
                {
                    if (item.Action == ActionEnum.Create)
                    {
                        Datacontext.RECEIVE_RAW_TRANS.Add(item);
                    }
                }
                Datacontext.SaveChanges();
            }
            catch (Exception ex)
            {
                DebugLog.WriteLog(ex.ToString());
            }
        }

        public void Edit(RECEIVE_RAW_TRANS obj)
        {
            var item = this.Datacontext.RECEIVE_RAW_TRANS.Where(x => x.RECEIVE_NO == obj.RECEIVE_NO && x.RECEIVE_YY == obj.RECEIVE_YY).FirstOrDefault();

            item.UPDATE_DATE = obj.UPDATE_DATE;
            item.UPDATE_EMPLOYEE_ID = obj.UPDATE_EMPLOYEE_ID;
            this.Datacontext.SaveChanges();
        }

        public void Delete(string PO_BK_NO, string PO_RN_NO)
        {
            throw new NotImplementedException();
        }

        public void EditList(List<RECEIVE_RAW_TRANS> obj)
        {
            throw new NotImplementedException();
        }

        public RECEIVE_RAW_TRANS Select()
        {
            throw new NotImplementedException();
        }

        public List<RECEIVE_RAW_TRANS> Select(string PO_BK_NO, string PO_RN_NO, string PO_YY)
        {
            return Datacontext.RECEIVE_RAW_TRANS.Where(x => x.PO_BK_NO == PO_BK_NO && x.PO_RN_NO == PO_RN_NO && x.PO_YY == PO_YY).ToList();
        }

        public List<RECEIVE_RAW_TRANS> GetAll()
        {
            return this.Datacontext.RECEIVE_RAW_TRANS.Where(x => x.SYE_DEL == false).ToList();
        }
        public List<RECEIVE_RAW_TRANS> GetAll(string PO_BK_NO, string PO_RN_NO)
        {
            throw new NotImplementedException();
        }
        public int GetAllCount()
        {
            return GetAll().Count();
        }
        public List<RECEIVE_RAW_TRANS> GetAllByFilter(int PageIndex, int PageLimit)
        {
            return this.Datacontext.RECEIVE_RAW_TRANS.Where(x => x.SYE_DEL == false).OrderBy(x => x.RECEIVE_NO).Skip(PageLimit * (PageIndex - 1)).Take(PageLimit).ToList();
        }
        public string GetMaxRecNo()
        {
            RECEIVE_RAW_TRANS _RECEIVE_RAW_TRANS = new RECEIVE_RAW_TRANS();
            _RECEIVE_RAW_TRANS = this.Datacontext.RECEIVE_RAW_TRANS.Where(e => e.RECEIVE_NO.Contains("REC-")).OrderByDescending(x => x.RECEIVE_NO).FirstOrDefault();
            return (_RECEIVE_RAW_TRANS == null) ? null : _RECEIVE_RAW_TRANS.RECEIVE_NO;
        }
        public int GetSumReceiveQty(string PO_BK_NO, string PO_RN_NO, string PO_YY, int RAW_ID)
        {
            List<RECEIVE_RAW_TRANS> lstReceive = Datacontext.RECEIVE_RAW_TRANS.Where(x => x.PO_BK_NO == PO_BK_NO && x.PO_RN_NO == PO_RN_NO && x.PO_YY == PO_YY && x.RAW_ID == RAW_ID).ToList();
            if (lstReceive.Count > 0)
            {
                return lstReceive.Sum(x => Convert.ToInt32(x.RECEIVE_QTY));
            }
            else
            {
                return 0;
            }
        }
        #endregion
    }
}
