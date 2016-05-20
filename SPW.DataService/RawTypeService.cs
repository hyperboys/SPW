using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SPW.DAL;
using SPW.Model;

namespace SPW.DataService
{
    public class RawTypeService : ServiceBase, IDataService<RAW_TYPE>, IService
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

        #region IDataService<RAW_TYPE> Members

        public void Add(RAW_TYPE obj)
        {
            this.Datacontext.RAW_TYPE.Add(obj);
            this.Datacontext.SaveChanges();
        }

        public void AddList(List<RAW_TYPE> obj)
        {
            throw new NotImplementedException();
        }

        public void Edit(RAW_TYPE obj)
        {
            var item = this.Datacontext.RAW_TYPE.Where(x => x.RAW_TYPE_ID == obj.RAW_TYPE_ID).FirstOrDefault();
            item.RAW_TYPE_NAME = obj.RAW_TYPE_NAME;
            item.UPDATE_DATE = obj.UPDATE_DATE;
            item.UPDATE_EMPLOYEE_ID = obj.UPDATE_EMPLOYEE_ID;
            this.Datacontext.SaveChanges();
        }

        public void Delete(int ID)
        {
            var obj = this.Datacontext.RAW_TYPE.Where(x => x.RAW_TYPE_ID == ID).FirstOrDefault();
            this.Datacontext.RAW_TYPE.Remove(obj);
            this.Datacontext.SaveChanges();
        }

        public void EditList(List<RAW_TYPE> obj)
        {
            throw new NotImplementedException();
        }

        public RAW_TYPE Select()
        {
            throw new NotImplementedException();
        }

        public RAW_TYPE Select(int ID)
        {
            return this.Datacontext.RAW_TYPE.Where(x => x.RAW_TYPE_ID == ID).FirstOrDefault();
        }

        public List<RAW_TYPE> GetAll()
        {
            return this.Datacontext.RAW_TYPE.Where(x => x.SYE_DEL == false).ToList();
        }
        public List<RAW_TYPE> GetAll(int RAW_PACK_ID, string RAW_TYPE_NAME)
        {
            return this.Datacontext.RAW_TYPE.Where(x => x.SYE_DEL == false && x.RAW_TYPE_ID == RAW_PACK_ID && x.RAW_TYPE_NAME.Contains(RAW_TYPE_NAME)).ToList();
        }
        public int GetAllCount()
        {
            return GetAll().Count();
        }
        public List<RAW_TYPE> GetAllByFilter(int PageIndex, int PageLimit)
        {
            return this.Datacontext.RAW_TYPE.Where(x => x.SYE_DEL == false).OrderBy(x => x.RAW_TYPE_ID).Skip(PageLimit * (PageIndex - 1)).Take(PageLimit).ToList();
        }
        #endregion
    }
}
