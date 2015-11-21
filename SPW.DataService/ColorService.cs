using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SPW.DAL;
using SPW.Model;

namespace SPW.DataService
{
    public class ColorService : ServiceBase, IDataService<COLOR>, IService 
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

        #region IDataService<COLOR> Members

        public void Add(COLOR obj)
        {
            this.Datacontext.COLOR.Add(obj);
            this.Datacontext.SaveChanges();
        }

        public void AddList(List<COLOR> obj)
        {
            throw new NotImplementedException();
        }

        public void Edit(COLOR obj)
        {
            var item = this.Datacontext.COLOR.Where(x => x.COLOR_ID == obj.COLOR_ID).FirstOrDefault();
            item.COLOR_NAME = obj.COLOR_NAME;
            item.COLOR_SUBNAME = obj.COLOR_SUBNAME;
            item.UPDATE_DATE = obj.UPDATE_DATE;
            item.UPDATE_EMPLOYEE_ID = obj.UPDATE_EMPLOYEE_ID;
            this.Datacontext.SaveChanges();
        }

        public void EditList(List<COLOR> obj)
        {
            throw new NotImplementedException();
        }

        public COLOR Select()
        {
            throw new NotImplementedException();
        }

        public COLOR Select(int ID)
        {
            return this.Datacontext.COLOR.Where(x => x.COLOR_ID == ID).FirstOrDefault();
        }

        public List<COLOR> GetAll()
        {
            return this.Datacontext.COLOR.Where(x => x.SYE_DEL == false).ToList();
        }
        
        #endregion
   
    }
}
