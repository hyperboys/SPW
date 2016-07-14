using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SPW.DAL;
using SPW.Model;


namespace SPW.DataService
{
    public class RawProductService : ServiceBase, IDataService<RAW_PRODUCT>, IService
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

        #region IDataService<RAW_PRODUCT> Members

        public void Add(RAW_PRODUCT obj)
        {
            this.Datacontext.RAW_PRODUCT.Add(obj);
            this.Datacontext.SaveChanges();
        }

        public void AddList(List<RAW_PRODUCT> obj)
        {
            throw new NotImplementedException();
        }

        public void Edit(RAW_PRODUCT obj)
        {
            var item = this.Datacontext.RAW_PRODUCT.Where(x => x.RAW_ID == obj.RAW_ID).FirstOrDefault();
            item.RAW_BD = obj.RAW_BD;
            item.RAW_HG = obj.RAW_HG;
            item.RAW_NAME1 = obj.RAW_NAME1;
            item.RAW_NAME2 = obj.RAW_NAME2;
            item.RAW_WD = obj.RAW_WD;
            item.RAW_TYPE_ID = obj.RAW_TYPE_ID;
            item.RAW_WD_UID = obj.RAW_WD_UID;
            item.RAW_HG_UID = obj.RAW_HG_UID;
            item.RAW_BD_UID = obj.RAW_BD_UID;
            item.UPDATE_DATE = obj.UPDATE_DATE;
            item.UPDATE_EMPLOYEE_ID = obj.UPDATE_EMPLOYEE_ID;
            this.Datacontext.SaveChanges();
        }

        public void Delete(int ID)
        {
            var obj = this.Datacontext.RAW_PRODUCT.Where(x => x.RAW_ID == ID).FirstOrDefault();
            this.Datacontext.RAW_PRODUCT.Remove(obj);
            this.Datacontext.SaveChanges();
        }

        public void EditList(List<RAW_PRODUCT> obj)
        {
            throw new NotImplementedException();
        }

        public RAW_PRODUCT Select()
        {
            throw new NotImplementedException();
        }

        public RAW_PRODUCT Select(int ID)
        {
            return this.Datacontext.RAW_PRODUCT.Where(x => x.RAW_ID == ID).FirstOrDefault();
        }

        public RAW_PRODUCT Select(string RAW_NAME)
        {
            return this.Datacontext.RAW_PRODUCT.Where(x => x.RAW_NAME1 == RAW_NAME).FirstOrDefault();
        }

        public List<RAW_PRODUCT> GetAll()
        {
            return this.Datacontext.RAW_PRODUCT.Where(x => x.SYE_DEL == false).ToList();
        }
        public List<RAW_PRODUCT> GetAll(int RAW_TYPE_ID)
        {
            return this.Datacontext.RAW_PRODUCT.Where(x => x.SYE_DEL == false && x.RAW_TYPE_ID == RAW_TYPE_ID).ToList();
        }
        public List<RAW_PRODUCT> GetAll(int RAW_ID, string RAW_PRODUCT_NAME)
        {
            return this.Datacontext.RAW_PRODUCT.Where(x => x.SYE_DEL == false && x.RAW_ID == RAW_ID && x.RAW_NAME1.Contains(RAW_PRODUCT_NAME)).ToList();
        }
        public int GetAllCount()
        {
            return GetAll().Count();
        }
        public List<RAW_PRODUCT> GetAllByFilter(int PageIndex, int PageLimit)
        {
            return this.Datacontext.RAW_PRODUCT.Where(x => x.SYE_DEL == false).OrderBy(x => x.RAW_ID).Skip(PageLimit * (PageIndex - 1)).Take(PageLimit).ToList();
        }
        #endregion

    }
}
