using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SPW.DAL;
using SPW.Model;

namespace SPW.DataService
{
    public class SubFunctionService : ServiceBase, IDataService<SUB_FUNCTION>, IService 
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

        public void Add(SUB_FUNCTION obj)
        {
            this.Datacontext.SUB_FUNCTION.Add(obj);
            this.Datacontext.SaveChanges();
        }

        public void AddList(List<SUB_FUNCTION> obj)
        {
            throw new NotImplementedException();
        }

        public void Edit(SUB_FUNCTION obj)
        {
            var item = this.Datacontext.SUB_FUNCTION.Where(x => x.FUNCTION_ID == obj.FUNCTION_ID).FirstOrDefault();
            //item.FUNCTION_NAME = obj.FUNCTION_NAME;
            item.UPDATE_DATE = obj.UPDATE_DATE;
            item.UPDATE_EMPLOYEE_ID = obj.UPDATE_EMPLOYEE_ID;
            this.Datacontext.SaveChanges();
        }

        public void EditList(List<SUB_FUNCTION> obj)
        {
            throw new NotImplementedException();
        }

        public SUB_FUNCTION Select()
        {
            throw new NotImplementedException();
        }

        public SUB_FUNCTION Select(int ID)
        {
            return this.Datacontext.SUB_FUNCTION.Where(x => x.SUB_FUNCTION_ID == ID).FirstOrDefault();
        }

        public List<SUB_FUNCTION> GetAll()
        {
            return this.Datacontext.SUB_FUNCTION.Where(x => x.SYE_DEL == false).ToList();
        }

        public List<SUB_FUNCTION> GetAllInclude()
        {
            return this.Datacontext.SUB_FUNCTION.Include("SUB_FUNCTION").Where(x => x.SYE_DEL == false).ToList();
        }

        public List<SUB_FUNCTION> GetAll(int ID)
        {
            return this.Datacontext.SUB_FUNCTION.Where(x => x.FUNCTION_ID == ID).ToList();
        }

        #endregion
    }
}
