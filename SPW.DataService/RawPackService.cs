using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SPW.DAL;
using SPW.Model;

namespace SPW.DataService
{
    public class RawPackService : ServiceBase, IDataService<RAW_PACK_SIZE>, IService 
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

        #region IDataService<RAW_PACK_SIZE> Members

        public void Add(RAW_PACK_SIZE obj)
        {
            this.Datacontext.RAW_PACK_SIZE.Add(obj);
            this.Datacontext.SaveChanges();
        }

        public void AddList(List<RAW_PACK_SIZE> obj)
        {
            throw new NotImplementedException();
        }

        public void Edit(RAW_PACK_SIZE obj)
        {
            var item = this.Datacontext.RAW_PACK_SIZE.Where(x => x.RAW_PACK_ID == obj.RAW_PACK_ID).FirstOrDefault();
            item.RAW_PACK_SIZE1 = obj.RAW_PACK_SIZE1;
            item.RAW_PACK_SIZE_UNIT_QTY	= obj.RAW_PACK_SIZE_UNIT_QTY;
            item.RAW_PACK_SIZE_UNIT_DESC = obj.RAW_PACK_SIZE_UNIT_DESC;
            item.RAW_PACK_SIZE_PACK_QTY = obj.RAW_PACK_SIZE_PACK_QTY;
            item.RAW_PACK_SIZE_PACK_DESC = obj.RAW_PACK_SIZE_PACK_DESC;
            item.UPDATE_DATE = obj.UPDATE_DATE;
            item.UPDATE_EMPLOYEE_ID = obj.UPDATE_EMPLOYEE_ID;
            this.Datacontext.SaveChanges();
        }

        public void Delete(int ID)
        {
            var obj = this.Datacontext.RAW_PACK_SIZE.Where(x => x.RAW_PACK_ID == ID).FirstOrDefault();
            this.Datacontext.RAW_PACK_SIZE.Remove(obj);
            this.Datacontext.SaveChanges();
        }

        public void EditList(List<RAW_PACK_SIZE> obj)
        {
            throw new NotImplementedException();
        }

        public RAW_PACK_SIZE Select()
        {
            throw new NotImplementedException();
        }

        public RAW_PACK_SIZE Select(int ID)
        {
            return this.Datacontext.RAW_PACK_SIZE.Where(x => x.RAW_PACK_ID == ID).FirstOrDefault();
        }

        public List<RAW_PACK_SIZE> GetAll()
        {
            return this.Datacontext.RAW_PACK_SIZE.Where(x => x.SYE_DEL == false).ToList();
        }
        public List<RAW_PACK_SIZE> GetAll(int RAW_PACK_ID, string RAW_PACK_SIZE_NAME)
        {
            return this.Datacontext.RAW_PACK_SIZE.Where(x => x.SYE_DEL == false && x.RAW_PACK_ID == RAW_PACK_ID && x.RAW_PACK_SIZE1.Contains(RAW_PACK_SIZE_NAME)).ToList();
        }
        public int GetAllCount()
        {
            return GetAll().Count();
        }
        public List<RAW_PACK_SIZE> GetAllByFilter(int PageIndex, int PageLimit)
        {
            return this.Datacontext.RAW_PACK_SIZE.Where(x => x.SYE_DEL == false).OrderBy(x => x.RAW_PACK_ID).Skip(PageLimit * (PageIndex - 1)).Take(PageLimit).ToList();
        }
        #endregion

    }
}
