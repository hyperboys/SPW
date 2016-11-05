using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SPW.DAL;
using SPW.Model;

namespace SPW.DataService
{
    public class RoadService : ServiceBase, IDataService<ROAD>, IService 
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

        #region IDataService<ROAD> Members

        public void Add(ROAD obj)
        {
            this.Datacontext.ROAD.Add(obj);
            this.Datacontext.SaveChanges();
        }

        public void AddList(List<ROAD> obj)
        {
            throw new NotImplementedException();
        }

        public void Edit(ROAD obj)
        {
            throw new NotImplementedException();
        }

        public void EditList(List<ROAD> obj)
        {
            throw new NotImplementedException();
        }

        public ROAD Select()
        {
            throw new NotImplementedException();
        }

        public ROAD Select(int ID)
        {
            return this.Datacontext.ROAD.Where(x => x.ROAD_ID == ID).FirstOrDefault();
        }

        public ROAD Select(string Name)
        {
            return this.Datacontext.ROAD.Where(x => x.ROAD_NAME.Contains(Name)).FirstOrDefault();
        }

        public int GetCount() 
        {
            return this.Datacontext.ROAD.Count();
        }

        public List<ROAD> GetAll()
        {
            return this.Datacontext.ROAD.ToList();
        }
        #endregion
    }
}
