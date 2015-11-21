using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SPW.DAL;
using SPW.Model;

namespace SPW.DataService
{
    public class RoleService : ServiceBase, IDataService<ROLE>, IService 
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

        #region IDataService<ROLE> Members

        public void Add(ROLE obj)
        {
            this.Datacontext.ROLE.Add(obj);
            this.Datacontext.SaveChanges();
        }

        public void AddList(List<ROLE> obj)
        {
            throw new NotImplementedException();
        }

        public void Edit(ROLE obj)
        {
            var item = this.Datacontext.ROLE.Where(x => x.ROLE_ID == obj.ROLE_ID).FirstOrDefault();
            item.ROLE_CODE = obj.ROLE_CODE;
            item.ROLE_NAME = obj.ROLE_NAME;
            item.UPDATE_DATE = obj.UPDATE_DATE;
            item.UPDATE_EMPLOYEE_ID = obj.UPDATE_EMPLOYEE_ID;
            this.Datacontext.SaveChanges();
        }

        public void EditList(List<ROLE> obj)
        {
            throw new NotImplementedException();
        }

        public ROLE Select()
        {
            throw new NotImplementedException();
        }

        public ROLE Select(int ID)
        {
            return this.Datacontext.ROLE.Where(x => x.ROLE_ID == ID).FirstOrDefault();
        }

        public ROLE SelectIncludeEmployee(int ID)
        {
            return this.Datacontext.ROLE.Include("ROLE_FUNCTION").Where(x => x.ROLE_ID == ID).FirstOrDefault();
        }

        public List<ROLE> GetAll()
        {
            return this.Datacontext.ROLE.Where(x => x.SYE_DEL == false).ToList();
        }

        public List<ROLE> GetAll(string name)
        {
            return this.Datacontext.ROLE.Where(x => x.ROLE_NAME.ToUpper().Contains(name.ToUpper())).ToList();
        }

        #endregion
    }
}
