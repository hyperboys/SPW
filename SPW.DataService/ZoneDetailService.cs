using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SPW.DAL;
using SPW.Model;

namespace SPW.DataService
{
    public class ZoneDetailService : ServiceBase, IDataService<ZONE_DETAIL>, IService 
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

        #region IDataService<ZONE_DETAIL> Members

        public void Add(ZONE_DETAIL obj)
        {
            this.Datacontext.ZONE_DETAIL.Add(obj);
            this.Datacontext.SaveChanges();
        }

        public void AddList(List<ZONE_DETAIL> obj)
        {
            foreach (var item in obj)
            {
                if (item.Action == ActionEnum.Create)
                {
                    this.Datacontext.ZONE_DETAIL.Add(item);
                }
            }
            this.Datacontext.SaveChanges();
        }

        public void Edit(ZONE_DETAIL obj)
        {
            throw new NotImplementedException();
        }

        public void EditList(List<ZONE_DETAIL> obj)
        {
            throw new NotImplementedException();
        }

        public ZONE_DETAIL Select()
        {
            throw new NotImplementedException();
        }

        public ZONE_DETAIL Select(int ID)
        {
            return this.Datacontext.ZONE_DETAIL.Where(x => x.ZONE_DETAIL_ID == ID).FirstOrDefault();
        }

        public List<ZONE_DETAIL> GetAll()
        {
            return this.Datacontext.ZONE_DETAIL.Where(x => x.SYE_DEL == false).ToList();
        }

        public List<ZONE_DETAIL> GetAllByUser(int ID)
        {
            return this.Datacontext.ZONE_DETAIL.ToList().Where(x => x.SYE_DEL == false && x.EMPLOYEE_ID == ID).ToList();
        }

        public List<ZONE_DETAIL> GetAllInclude()
        {
            return this.Datacontext.ZONE_DETAIL.Include("ZONE").ToList().Where(x => x.SYE_DEL == false).ToList();
        }

        public List<ZONE_DETAIL> GetAllInclude(int ID)
        {
            return this.Datacontext.ZONE_DETAIL.Include("ZONE").ToList().Where(x => x.SYE_DEL == false && x.EMPLOYEE_ID == ID).ToList();
        }

        public void Delete(int ID)
        {
            var obj = this.Datacontext.ZONE_DETAIL.Where(x => x.ZONE_DETAIL_ID == ID).FirstOrDefault();
            obj.SYE_DEL = true;
            this.Datacontext.SaveChanges();
        }

        #endregion
    }
}
