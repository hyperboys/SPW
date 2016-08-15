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
    public class EmpMeasureTransService : ServiceBase, IDataService<EMP_MEASURE_TRANS>, IService
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

        #region IDataService<EMP_MEASURE_TRANS> Members

        public void Add(EMP_MEASURE_TRANS obj)
        {
            try
            {
                this.Datacontext.EMP_MEASURE_TRANS.Add(obj);
                this.Datacontext.SaveChanges();
            }
            catch (Exception ex)
            {
                DebugLog.WriteLog(ex.ToString());
                throw ex;
            }
        }

        public void AddList(List<EMP_MEASURE_TRANS> obj)
        {
            throw new NotImplementedException();
        }

        public void Edit(EMP_MEASURE_TRANS obj)
        {
            var item = this.Datacontext.EMP_MEASURE_TRANS.Where(x => x.EMPLOYEE_ID == obj.EMPLOYEE_ID
                && x.MEASURE_YY == obj.MEASURE_YY && x.MEASURE_SEQ_NO == obj.MEASURE_SEQ_NO && x.WEIGHT_ID == obj.WEIGHT_ID
                && x.TEMPLATE_ID == obj.TEMPLATE_ID && x.EMP_SKILL_TYPE_ID == obj.EMP_SKILL_TYPE_ID && x.SEQ_NO == obj.SEQ_NO).FirstOrDefault();
            item.MEASURE_DATE = obj.MEASURE_DATE;
            item.EMP_SKILL_TYPE_ACTUAL = obj.EMP_SKILL_TYPE_ACTUAL;
            item.SCORE_ACTUAL = obj.SCORE_ACTUAL;
            item.WEIGHT_VALUE = obj.WEIGHT_VALUE;
            item.UPDATE_DATE = obj.UPDATE_DATE;
            item.UPDATE_EMPLOYEE_ID = obj.UPDATE_EMPLOYEE_ID;
            this.Datacontext.SaveChanges();
        }

        public void EditList(List<EMP_MEASURE_TRANS> obj)
        {
            throw new NotImplementedException();
        }

        public EMP_MEASURE_TRANS Select()
        {
            throw new NotImplementedException();
        }

        public EMP_MEASURE_TRANS Select(int ID)
        {
            return this.Datacontext.EMP_MEASURE_TRANS.Where(x => x.WEIGHT_ID == ID).FirstOrDefault();
        }

        public EMP_MEASURE_TRANS Select(int WEIGHT_ID, string TEMPLATE_ID, int EMPLOYEE_ID, string MEASURE_YY, int MEASURE_SEQ_NO, int EMP_SKILL_TYPE_ID)
        {
            try
            {
                return this.Datacontext.EMP_MEASURE_TRANS.Where(x => x.WEIGHT_ID == WEIGHT_ID && x.TEMPLATE_ID == TEMPLATE_ID && x.EMPLOYEE_ID == EMPLOYEE_ID
                       && x.MEASURE_YY == MEASURE_YY && x.MEASURE_SEQ_NO == MEASURE_SEQ_NO && x.EMP_SKILL_TYPE_ID == EMP_SKILL_TYPE_ID && x.SYE_DEL == false).FirstOrDefault();
            }
            catch (Exception ex)
            {
                DebugLog.WriteLog(ex.ToString());
                return null;
            }
        }

        public List<EMP_MEASURE_TRANS> GetAll()
        {
            return this.Datacontext.EMP_MEASURE_TRANS.Where(x => x.SYE_DEL == false).ToList();
        }

        public List<EMP_MEASURE_TRANS> GetAll(int empId)
        {
            return this.Datacontext.EMP_MEASURE_TRANS.Where(x => x.SYE_DEL == false && x.EMPLOYEE_ID == empId).ToList();
        }

        public void Delete(int ID)
        {
            var obj = this.Datacontext.EMP_MEASURE_TRANS.Where(x => x.WEIGHT_ID == ID).FirstOrDefault();
            this.Datacontext.EMP_MEASURE_TRANS.Remove(obj);
            this.Datacontext.SaveChanges();
        }

        public int GetCount(string TEMPLATE_ID, int EMPLOYEE_ID, string MEASURE_YY, int MEASURE_SEQ_NO, int EMP_SKILL_TYPE_ID)
        {
            try
            {
                return this.Datacontext.EMP_MEASURE_TRANS.Where(x => x.TEMPLATE_ID == TEMPLATE_ID && x.EMPLOYEE_ID == EMPLOYEE_ID
                   && x.MEASURE_YY == MEASURE_YY && x.MEASURE_SEQ_NO == MEASURE_SEQ_NO && x.EMP_SKILL_TYPE_ID == EMP_SKILL_TYPE_ID && x.SYE_DEL == false).Count();
            }
            catch (Exception ex) 
            {
                DebugLog.WriteLog(ex.ToString());
                return 0;
            }
        }

        #endregion

    }
}
