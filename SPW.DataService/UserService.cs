using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SPW.DAL;
using SPW.Model;

namespace SPW.DataService
{
    public class UserService : ServiceBase, IDataService<USER>, IService 
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

        #region IDataService<USER> Members

        public void Add(USER obj)
        {
            this.Datacontext.USER.Add(obj);
            this.Datacontext.SaveChanges();
        }

        public void AddList(List<USER> obj)
        {
            throw new NotImplementedException();
        }

        public void Edit(USER obj)
        {
            var item = this.Datacontext.USER.Where(x => x.USER_ID == obj.USER_ID).FirstOrDefault();
            item.USER_NAME = obj.USER_NAME;
            item.PASSWORD = obj.PASSWORD;
            item.EMPLOYEE_ID = obj.EMPLOYEE_ID;
            item.UPDATE_DATE = obj.UPDATE_DATE;
            item.UPDATE_EMPLOYEE_ID = obj.UPDATE_EMPLOYEE_ID;
            this.Datacontext.SaveChanges();
        }

        public void EditList(List<USER> obj)
        {
            throw new NotImplementedException();
        }

        public USER Select()
        {
            throw new NotImplementedException();
        }

        public USER Select(int ID)
        {
            return this.Datacontext.USER.Where(x => x.USER_ID == ID).FirstOrDefault();
        }

        public USER Select(string username)
        {
            return this.Datacontext.USER.Where(x => x.USER_NAME.ToUpper() == username.ToUpper()).FirstOrDefault();
        }

        public USER SelectIncludeEmployee(int ID)
        {
            return this.Datacontext.USER.Include("EMPLOYEE").Where(x => x.USER_ID == ID).FirstOrDefault();
        }

        public USER SelectIncludeUserRole(int ID)
        {
            return this.Datacontext.USER.Include("ROLE").Where(x => x.USER_ID == ID).FirstOrDefault();
        }

        public USER SelectInclude(string username, string password)
        {
            return this.Datacontext.USER.Include("EMPLOYEE").Include("ROLE").Where(x => x.USER_NAME == username && x.PASSWORD == password).FirstOrDefault();
        }

        public List<USER> GetAll()
        {
            return this.Datacontext.USER.Where(x => x.SYE_DEL == false).ToList();
        }

        public List<USER> GetAllInclude()
        {
            return this.Datacontext.USER.Include("EMPLOYEE").Where(x => x.SYE_DEL == false).ToList();
        }

        public List<USER> GetAll(string name)
        {
            return this.Datacontext.USER.Where(x => x.USER_NAME.Contains(name)).ToList();
        }

        #endregion
   
    }
}
