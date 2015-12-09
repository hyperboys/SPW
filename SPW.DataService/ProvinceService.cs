using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SPW.DAL;
using SPW.Model;

namespace SPW.DataService
{
    public class ProvinceService : ServiceBase, IDataService<PROVINCE>, IService 
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

        public void Add(PROVINCE obj)
        {
            this.Datacontext.PROVINCE.Add(obj);
            this.Datacontext.SaveChanges();
        }

        public void AddList(List<PROVINCE> obj)
        {
            throw new NotImplementedException();
        }

        public void Edit(PROVINCE obj)
        {
            throw new NotImplementedException();
        }

        public void EditList(List<PROVINCE> obj)
        {
            throw new NotImplementedException();
        }

        public PROVINCE Select()
        {
            throw new NotImplementedException();
        }

        public PROVINCE Select(int ID)
        {
            return this.Datacontext.PROVINCE.Where(x => x.PROVINCE_ID == ID).FirstOrDefault();
        }

        public List<PROVINCE> GetAll()
        {
            return this.Datacontext.PROVINCE.Where(x => x.SYE_DEL == false).ToList();
        }
        #endregion
    
    }
}
