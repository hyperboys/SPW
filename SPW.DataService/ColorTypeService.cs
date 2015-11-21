using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SPW.DAL;
using SPW.Model;

namespace SPW.DataService
{
    public class ColorTypeService : ServiceBase, IDataService<COLOR_TYPE>, IService 
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

        #region IDataService<COLOR_TYPE> Members

        public void Add(COLOR_TYPE obj)
        {
            this.Datacontext.COLOR_TYPE.Add(obj);
            this.Datacontext.SaveChanges();
        }

        public void AddList(List<COLOR_TYPE> obj)
        {
            throw new NotImplementedException();
        }

        public void Edit(COLOR_TYPE obj)
        {
            var item = this.Datacontext.COLOR_TYPE.Where(x => x.COLOR_TYPE_ID == obj.COLOR_TYPE_ID).FirstOrDefault();
            item.COLOR_TYPE_NAME = obj.COLOR_TYPE_NAME;
            item.COLOR_TYPE_SUBNAME = obj.COLOR_TYPE_SUBNAME;
            item.UPDATE_DATE = obj.UPDATE_DATE;
            item.UPDATE_EMPLOYEE_ID = obj.UPDATE_EMPLOYEE_ID;
            this.Datacontext.SaveChanges();
        }

        public void EditList(List<COLOR_TYPE> obj)
        {
            throw new NotImplementedException();
        }

        public COLOR_TYPE Select()
        {
            throw new NotImplementedException();
        }

        public COLOR_TYPE Select(int ID)
        {
            return this.Datacontext.COLOR_TYPE.Where(x => x.COLOR_TYPE_ID == ID).FirstOrDefault();
        }

        public List<COLOR_TYPE> GetAll()
        {
            return this.Datacontext.COLOR_TYPE.Where(x => x.SYE_DEL == false).ToList();
        }

        #endregion

    }
}
