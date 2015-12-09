using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SPW.DAL;
using SPW.Model;

namespace SPW.DataService
{
    public class DepartmentService : ServiceBase, IDataService<DEPARTMENT>, IService 
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

        #region IDataService<DELIVERY_ORDER> Members

        public void Add(DEPARTMENT obj)
        {
            this.Datacontext.DEPARTMENT.Add(obj);
            this.Datacontext.SaveChanges();
        }

        public void AddList(List<DEPARTMENT> obj)
        {
            throw new NotImplementedException();
        }

        public void Edit(DEPARTMENT obj)
        {
            var item = this.Datacontext.DEPARTMENT.Where(x => x.DEPARTMENT_ID == obj.DEPARTMENT_ID).FirstOrDefault();
            item.DEPARTMENT_CODE = obj.DEPARTMENT_CODE;
            item.DEPARTMENT_NAME = obj.DEPARTMENT_NAME;
            item.UPDATE_DATE = obj.UPDATE_DATE;
            item.UPDATE_EMPLOYEE_ID = obj.UPDATE_EMPLOYEE_ID;
            this.Datacontext.SaveChanges();
        }

        public void EditList(List<DEPARTMENT> obj)
        {
            throw new NotImplementedException();
        }

        public DEPARTMENT Select()
        {
            throw new NotImplementedException();
        }

        public DEPARTMENT Select(int ID)
        {
            return this.Datacontext.DEPARTMENT.Where(x => x.DEPARTMENT_ID == ID).FirstOrDefault();
        }

        public List<DEPARTMENT> GetAll()
        {
            return this.Datacontext.DEPARTMENT.Where(x => x.SYE_DEL == false).ToList();
        }

        public List<DEPARTMENT> GetAll(DEPARTMENT item)
        {
            return this.Datacontext.DEPARTMENT.Where(x => x.DEPARTMENT_CODE.ToUpper().Contains(item.DEPARTMENT_CODE.ToUpper())).ToList();
        }

        #endregion
    }
}
