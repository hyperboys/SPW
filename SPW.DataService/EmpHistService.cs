using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SPW.DAL;
using SPW.Model;

namespace SPW.DataService
{
    public class EmpHistService : ServiceBase, IDataService<EMPLOYEE_HIST>, IService
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

        #region IDataService<EMPLOYEE_HIST> Members

        public void Add(EMPLOYEE_HIST obj)
        {
            this.Datacontext.EMPLOYEE_HIST.Add(obj);
            this.Datacontext.SaveChanges();
        }

        public void AddList(List<EMPLOYEE_HIST> obj)
        {
            throw new NotImplementedException();
        }

        public void Edit(EMPLOYEE_HIST obj)
        {
            var item = this.Datacontext.EMPLOYEE_HIST.Where(x => x.HIST_SEQ_NO == obj.HIST_SEQ_NO && x.EMPLOYEE_ID == obj.EMPLOYEE_ID).FirstOrDefault();
            item.DEPARTMENT_ID = obj.DEPARTMENT_ID;
            item.EFF_DATE = obj.EFF_DATE;
            item.EXP_DATE = obj.EXP_DATE;
            item.JOBB_AMT = obj.JOBB_AMT;
            item.POSI_AMT = obj.POSI_AMT;
            item.POSITION_ID = obj.POSITION_ID;
            item.SAL_AMT = obj.SAL_AMT;
            item.UPDATE_DATE = obj.UPDATE_DATE;
            item.UPDATE_EMPLOYEE_ID = obj.UPDATE_EMPLOYEE_ID;
            this.Datacontext.SaveChanges();
        }

        public void EditList(List<EMPLOYEE_HIST> obj)
        {
            throw new NotImplementedException();
        }

        public EMPLOYEE_HIST Select()
        {
            throw new NotImplementedException();
        }

        public EMPLOYEE_HIST Select(int HIST_ID, int EMP_ID)
        {
            return this.Datacontext.EMPLOYEE_HIST.Where(x => x.HIST_SEQ_NO == HIST_ID && x.EMPLOYEE_ID == EMP_ID).FirstOrDefault();
        }

        public List<EMPLOYEE_HIST> GetAll()
        {
            return this.Datacontext.EMPLOYEE_HIST.Where(x => x.SYE_DEL == false).ToList();
        }

        public List<EMPLOYEE_HIST> GetAll(int EMP_ID)
        {
            return this.Datacontext.EMPLOYEE_HIST.Where(x => x.SYE_DEL == false && x.EMPLOYEE_ID == EMP_ID).ToList();
        }

        public int GetSeqNo(int EMP_ID)
        {
            return this.Datacontext.EMPLOYEE_HIST.Where(x => x.SYE_DEL == false && x.EMPLOYEE_ID == EMP_ID).ToList().Count();
        }

        public void Delete(int ID)
        {
            var obj = this.Datacontext.EMPLOYEE_HIST.Where(x => x.HIST_SEQ_NO == ID).FirstOrDefault();
            obj.SYE_DEL = false;
            //this.Datacontext.EMPLOYEE_HIST.Remove(obj);
            this.Datacontext.SaveChanges();
        }
        #endregion
    }
}
