using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SPW.DAL;
using SPW.Model;

namespace SPW.DataService
{
    public class RoleFunctionService : ServiceBase, IDataService<ROLE_FUNCTION>, IService 
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

        #region IDataService<ROLE_FUNCTION> Members

        public void Add(ROLE_FUNCTION obj)
        {
            this.Datacontext.ROLE_FUNCTION.Add(obj);
            this.Datacontext.SaveChanges();
        }

        public void AddList(List<ROLE_FUNCTION> obj)
        {
            foreach (var item in obj)
            {
                if (item.Action == ActionEnum.Create)
                {
                    this.Datacontext.ROLE_FUNCTION.Add(item);
                }
            }
            this.Datacontext.SaveChanges();
        }

        public void Edit(ROLE_FUNCTION obj)
        {
            throw new NotImplementedException();
        }

        public void EditList(List<ROLE_FUNCTION> obj)
        {
            throw new NotImplementedException();
        }

        public ROLE_FUNCTION Select()
        {
            throw new NotImplementedException();
        }

        public ROLE_FUNCTION Select(int ID)
        {
            return this.Datacontext.ROLE_FUNCTION.Where(x => x.ROLE_FUNCTION_ID == ID).FirstOrDefault();
        }

        public List<ROLE_FUNCTION> GetAll()
        {
            return this.Datacontext.ROLE_FUNCTION.Where(x => x.SYE_DEL == false).ToList();
        }

        public List<ROLE_FUNCTION> GetAllIncludeFunction(int roleId)
        {
            return this.Datacontext.ROLE_FUNCTION.Include("FUNCTION").Where(x => x.ROLE_ID == roleId && x.SYE_DEL == false).ToList();
        }

        public List<ROLE_FUNCTION> SelectByRole(int ID)
        {
            return this.Datacontext.ROLE_FUNCTION.Where(x => x.ROLE_ID == ID).ToList();
        }

        public void Delete(int ID)
        {
            var obj = this.Datacontext.ROLE_FUNCTION.Where(x => x.ROLE_FUNCTION_ID == ID).FirstOrDefault();
            obj.SYE_DEL = true;
            this.Datacontext.SaveChanges();
        }

        #endregion
    }
}
