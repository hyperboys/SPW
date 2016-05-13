using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SPW.DAL;
using SPW.Model;

namespace SPW.DataService
{
    public class UnitTypeService : ServiceBase, IDataService<UNIT_TYPE>, IService
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

        #region IDataService<UNIT_TYPE> Members

        public void Add(UNIT_TYPE obj)
        {
            this.Datacontext.UNIT_TYPE.Add(obj);
            this.Datacontext.SaveChanges();
        }

        public void AddList(List<UNIT_TYPE> obj)
        {
            throw new NotImplementedException();
        }

        public void Edit(UNIT_TYPE obj)
        {
            var item = this.Datacontext.UNIT_TYPE.Where(x => x.UNIT_TYPE_ID == obj.UNIT_TYPE_ID).FirstOrDefault();
            item.UNIT_TYPE_NAME = obj.UNIT_TYPE_NAME;
            item.UNIT_TYPE_SUB_NAME = obj.UNIT_TYPE_SUB_NAME;
            item.UPDATE_DATE = obj.UPDATE_DATE;
            item.UPDATE_EMPLOYEE_ID = obj.UPDATE_EMPLOYEE_ID;
            this.Datacontext.SaveChanges();
        }

        public void Delete(int ID)
        {
            var obj = this.Datacontext.UNIT_TYPE.Where(x => x.UNIT_TYPE_ID == ID).FirstOrDefault();
            this.Datacontext.UNIT_TYPE.Remove(obj);
            this.Datacontext.SaveChanges();
        }

        public void EditList(List<UNIT_TYPE> obj)
        {
            throw new NotImplementedException();
        }

        public UNIT_TYPE Select()
        {
            throw new NotImplementedException();
        }

        public UNIT_TYPE Select(int ID)
        {
            return this.Datacontext.UNIT_TYPE.Where(x => x.UNIT_TYPE_ID == ID).FirstOrDefault();
        }

        public List<UNIT_TYPE> GetAll()
        {
            return this.Datacontext.UNIT_TYPE.Where(x => x.SYE_DEL == false).ToList();
        }
        public List<UNIT_TYPE> GetAll(int RAW_PACK_ID, string UNIT_TYPE_NAME)
        {
            return this.Datacontext.UNIT_TYPE.Where(x => x.SYE_DEL == false && x.UNIT_TYPE_ID == RAW_PACK_ID && x.UNIT_TYPE_NAME.Contains(UNIT_TYPE_NAME)).ToList();
        }
        public int GetAllCount()
        {
            return GetAll().Count();
        }
        public List<UNIT_TYPE> GetAllByFilter(int PageIndex, int PageLimit)
        {
            return this.Datacontext.UNIT_TYPE.Where(x => x.SYE_DEL == false).OrderBy(x => x.UNIT_TYPE_ID).Skip(PageLimit * (PageIndex - 1)).Take(PageLimit).ToList();
        }
        #endregion
    }
}
