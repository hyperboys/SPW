using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SPW.DAL;
using SPW.Model;

namespace SPW.DataService
{
    public class EmployeeService : ServiceBase, IDataService<EMPLOYEE>, IService 
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

        #region IDataService<EMPLOYEE> Members

        public void Add(EMPLOYEE obj)
        {
            this.Datacontext.EMPLOYEE.Add(obj);
            this.Datacontext.SaveChanges();
        }

        public void AddList(List<EMPLOYEE> obj)
        {
            throw new NotImplementedException();
        }

        public void Edit(EMPLOYEE obj)
        {
            var item = this.Datacontext.EMPLOYEE.Where(x => x.EMPLOYEE_ID == obj.EMPLOYEE_ID).FirstOrDefault();
            item.EMPLOYEE_NAME = obj.EMPLOYEE_NAME;
            item.EMPLOYEE_SURNAME = obj.EMPLOYEE_SURNAME;
            item.EMPLOYEE_CODE = obj.EMPLOYEE_CODE;
            item.DEPARTMENT_ID = obj.DEPARTMENT_ID;
            item.UPDATE_DATE = obj.UPDATE_DATE;
            item.UPDATE_EMPLOYEE_ID = obj.UPDATE_EMPLOYEE_ID;
            this.Datacontext.SaveChanges();
        }

        public void EditList(List<EMPLOYEE> obj)
        {
            throw new NotImplementedException();
        }

        public EMPLOYEE Select()
        {
            throw new NotImplementedException();
        }

        public EMPLOYEE Select(int ID)
        {
            return this.Datacontext.EMPLOYEE.Where(x => x.EMPLOYEE_ID == ID).FirstOrDefault();
        }

        public List<EMPLOYEE> GetAll()
        {
            return this.Datacontext.EMPLOYEE.Where(x => x.SYE_DEL == false).ToList();
        }

        public List<EMPLOYEE> GetAllInclude()
        {
            return this.Datacontext.EMPLOYEE.Include("DEPARTMENT").Where(x => x.SYE_DEL == false).ToList();
        }

        public List<EMPLOYEE> GetAll(EMPLOYEE item)
        {
            return this.Datacontext.EMPLOYEE.Where(x => x.EMPLOYEE_CODE.Contains(item.EMPLOYEE_CODE) && x.SYE_DEL == false).ToList();
        }

        #endregion
    }
}
