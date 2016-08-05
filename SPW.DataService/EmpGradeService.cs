using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SPW.DAL;
using SPW.Model;

namespace SPW.DataService
{
    public class EmpGradeService : ServiceBase, IDataService<EMP_GRADE_SET>, IService 
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

        #region IDataService<EMP_GRADE_SET> Members

        public void Add(EMP_GRADE_SET obj)
        {
            this.Datacontext.EMP_GRADE_SET.Add(obj);
            this.Datacontext.SaveChanges();
        }

        public void AddList(List<EMP_GRADE_SET> obj)
        {
            throw new NotImplementedException();
        }

        public void Edit(EMP_GRADE_SET obj)
        {
            var item = this.Datacontext.EMP_GRADE_SET.Where(x => x.GRADE_SET_SEQ_NO == obj.GRADE_SET_SEQ_NO && x.GRADE_SET == obj.GRADE_SET).FirstOrDefault();
            item.GRADE_SET_PERCENTAGE = obj.GRADE_SET_PERCENTAGE;
            item.GRADE_SET = obj.GRADE_SET;
            item.GRADE_SET_ACTIVE = obj.GRADE_SET_ACTIVE;
            item.UPDATE_DATE = obj.UPDATE_DATE;
            item.UPDATE_EMPLOYEE_ID = obj.UPDATE_EMPLOYEE_ID;
            this.Datacontext.SaveChanges();
        }

        public void EditList(List<EMP_GRADE_SET> obj)
        {
            throw new NotImplementedException();
        }

        public EMP_GRADE_SET Select()
        {
            throw new NotImplementedException();
        }

        public EMP_GRADE_SET Select(int ID)
        {
            return this.Datacontext.EMP_GRADE_SET.Where(x => x.GRADE_SET_SEQ_NO == ID).FirstOrDefault();
        }

        public List<EMP_GRADE_SET> GetAll()
        {
            return this.Datacontext.EMP_GRADE_SET.Where(x => x.SYE_DEL == false).ToList();
        }

        public void Delete(int ID)
        {
            var obj = this.Datacontext.EMP_GRADE_SET.Where(x => x.GRADE_SET_SEQ_NO == ID).FirstOrDefault();
            this.Datacontext.EMP_GRADE_SET.Remove(obj);
            this.Datacontext.SaveChanges();
        }
        #endregion
    
    }
}
