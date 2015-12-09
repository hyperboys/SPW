using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SPW.DAL;
using SPW.Model;

namespace SPW.DataService
{
    public class CategoryService : ServiceBase, IDataService<CATEGORY>, IService 
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

        #region IDataService<CATEGORY> Members

        public void Add(CATEGORY obj)
        {
            this.Datacontext.CATEGORY.Add(obj);
            this.Datacontext.SaveChanges();
        }

        public void AddList(List<CATEGORY> obj)
        {
            throw new NotImplementedException();
        }

        public void Edit(CATEGORY obj)
        {
            var item = this.Datacontext.CATEGORY.Where(x => x.CATEGORY_ID == obj.CATEGORY_ID).FirstOrDefault();
            item.CATEGORY_CODE = obj.CATEGORY_CODE;
            item.CATEGORY_NAME = obj.CATEGORY_NAME;
            item.UPDATE_DATE = obj.UPDATE_DATE;
            item.UPDATE_EMPLOYEE_ID = obj.UPDATE_EMPLOYEE_ID;
            this.Datacontext.SaveChanges();
        }

        public void EditList(List<CATEGORY> obj)
        {
            throw new NotImplementedException();
        }

        public CATEGORY Select()
        {
            throw new NotImplementedException();
        }

        public CATEGORY Select(int ID)
        {
            return this.Datacontext.CATEGORY.Where(x => x.CATEGORY_ID == ID).FirstOrDefault();
        }

        public List<CATEGORY> GetAll()
        {
            return this.Datacontext.CATEGORY.Where(x => x.SYE_DEL == false).ToList();
        }

        public void Delete(int ID)
        {
            var obj = this.Datacontext.CATEGORY.Where(x => x.CATEGORY_ID == ID).FirstOrDefault();
            //obj.SYE_DEL = true;
            this.Datacontext.CATEGORY.Remove(obj);
            this.Datacontext.SaveChanges();
        }
        #endregion
    
    }
}
