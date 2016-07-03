using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SPW.DAL;
using SPW.Model;

namespace SPW.DataService
{
    public class EmpPositionService : ServiceBase, IDataService<EMP_POSITION>, IService 
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

        #region IDataService<EMP_POSITION> Members

        public void Add(EMP_POSITION obj)
        {
            this.Datacontext.EMP_POSITION.Add(obj);
            this.Datacontext.SaveChanges();
        }

        public void AddList(List<EMP_POSITION> obj)
        {
            throw new NotImplementedException();
        }

        public void Edit(EMP_POSITION obj)
        {
            var item = this.Datacontext.EMP_POSITION.Where(x => x.POSITION_ID == obj.POSITION_ID).FirstOrDefault();
            item.POSITION_NAME = obj.POSITION_NAME;
            item.UPDATE_DATE = obj.UPDATE_DATE;
            item.UPDATE_EMPLOYEE_ID = obj.UPDATE_EMPLOYEE_ID;
            this.Datacontext.SaveChanges();
        }

        public void EditList(List<EMP_POSITION> obj)
        {
            throw new NotImplementedException();
        }

        public EMP_POSITION Select()
        {
            throw new NotImplementedException();
        }

        public EMP_POSITION Select(int ID)
        {
            return this.Datacontext.EMP_POSITION.Where(x => x.POSITION_ID == ID).FirstOrDefault();
        }

        public List<EMP_POSITION> GetAll()
        {
            return this.Datacontext.EMP_POSITION.Where(x => x.SYE_DEL == false).ToList();
        }

        public void Delete(int ID)
        {
            var obj = this.Datacontext.EMP_POSITION.Where(x => x.POSITION_ID == ID).FirstOrDefault();
            this.Datacontext.EMP_POSITION.Remove(obj);
            this.Datacontext.SaveChanges();
        }
        #endregion
    
    }
}
