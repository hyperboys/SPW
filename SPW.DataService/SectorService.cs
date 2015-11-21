using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SPW.DAL;
using SPW.Model;

namespace SPW.DataService
{
    public class SectorService : ServiceBase, IDataService<SECTOR>, IService 
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

        #region IDataService<PROVINCE> Members

        public void Add(SECTOR obj)
        {
            this.Datacontext.SECTOR.Add(obj);
            this.Datacontext.SaveChanges();
        }

        public void AddList(List<SECTOR> obj)
        {
            throw new NotImplementedException();
        }

        public void Edit(SECTOR obj)
        {
            throw new NotImplementedException();
        }

        public void EditList(List<SECTOR> obj)
        {
            throw new NotImplementedException();
        }

        public SECTOR Select()
        {
            throw new NotImplementedException();
        }

        public SECTOR Select(int ID)
        {
            return this.Datacontext.SECTOR.Where(x => x.SECTOR_ID == ID).FirstOrDefault();
        }

        public List<SECTOR> GetAll()
        {
            return this.Datacontext.SECTOR.Where(x => x.SYE_DEL == false).ToList();
        }

        public List<SECTOR> GetAllInclude()
        {
            return this.Datacontext.SECTOR.Include("PROVINCE").ToList();
        }

        #endregion
    }
}
