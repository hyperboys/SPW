using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SPW.DAL;
using SPW.Model;

namespace SPW.DataService
{
    public class ZoneService : ServiceBase, IDataService<ZONE>, IService 
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

        #region IDataService<ZONE> Members

        public void Add(ZONE obj)
        {
            this.Datacontext.ZONE.Add(obj);
            this.Datacontext.SaveChanges();
        }

        public void AddList(List<ZONE> obj)
        {
            throw new NotImplementedException();
        }

        public void Edit(ZONE obj)
        {
            var item = this.Datacontext.ZONE.Where(x => x.ZONE_ID == obj.ZONE_ID).FirstOrDefault();
            item.ZONE_CODE = obj.ZONE_CODE;
            item.ZONE_NAME = obj.ZONE_NAME;
            item.UPDATE_DATE = obj.UPDATE_DATE;
            item.UPDATE_EMPLOYEE_ID = obj.UPDATE_EMPLOYEE_ID;
            this.Datacontext.SaveChanges();
        }

        public void EditList(List<ZONE> obj)
        {
            throw new NotImplementedException();
        }

        public ZONE Select()
        {
            throw new NotImplementedException();
        }

        public ZONE Select(int ID)
        {
            return this.Datacontext.ZONE.Where(x => x.ZONE_ID == ID).FirstOrDefault();
        }

        public List<ZONE> GetAll()
        {
            return this.Datacontext.ZONE.Where(x => x.SYE_DEL == false).ToList();
        }

        public List<ZONE> GetAll(ZONE item)
        {
            return this.Datacontext.ZONE.Where(x => x.ZONE_CODE.Contains(item.ZONE_CODE)).ToList();
        }

        #endregion
    }
}
