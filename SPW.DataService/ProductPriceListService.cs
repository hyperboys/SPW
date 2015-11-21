using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SPW.DAL;
using SPW.Model;

namespace SPW.DataService
{
    public class ProductPriceListService : ServiceBase, IDataService<PRODUCT_PRICELIST>, IService 
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

        #region IDataService<PRODUCT_PRICELIST> Members

        public void Add(PRODUCT_PRICELIST obj)
        {
            this.Datacontext.PRODUCT_PRICELIST.Add(obj);
            this.Datacontext.SaveChanges();
        }

        public void AddList(List<PRODUCT_PRICELIST> obj)
        {
            throw new NotImplementedException();
        }

        public void AddUpdateList(List<PRODUCT_PRICELIST> _lstItem)
        {
            foreach (var item in _lstItem)
            {
                if (item.Action == ActionEnum.Create)
                {
                    this.Datacontext = new SPWEntities();
                    this.Datacontext.PRODUCT_PRICELIST.Add(item);
                }
                else if (item.Action == ActionEnum.Update)
                {
                    var obj = this.Datacontext.PRODUCT_PRICELIST.Where(x => x.PRODUCT_ID == item.PRODUCT_ID && x.ZONE_ID == item.ZONE_ID).FirstOrDefault();
                    if (obj != null)
                    {
                        obj.PRODUCT_PRICE = item.PRODUCT_PRICE;
                        obj.UPDATE_DATE = item.UPDATE_DATE;
                        obj.UPDATE_EMPLOYEE_ID = item.UPDATE_EMPLOYEE_ID;
                    }
                }
            }
            this.Datacontext.SaveChanges();
        }

        public void Edit(PRODUCT_PRICELIST obj)
        {
            throw new NotImplementedException();
        }

        public void EditList(List<PRODUCT_PRICELIST> obj)
        {
            throw new NotImplementedException();
        }

        public PRODUCT_PRICELIST Select()
        {
            throw new NotImplementedException();
        }

        public PRODUCT_PRICELIST Select(int ID)
        {
            return this.Datacontext.PRODUCT_PRICELIST.Where(x => x.PRODUCT_PRICELIST_ID == ID).FirstOrDefault();
        }

        //public List<PRODUCT_PRICELIST> GetAll(int ID)
        //{
        //    return this.Datacontext.PRODUCT_PRICELIST.Include("ZONE").Where(x => x.PRODUCT_ID == ID).ToList();
        //}

        public List<PRODUCT_PRICELIST> GetAll()
        {
            return this.Datacontext.PRODUCT_PRICELIST.Where(x => x.SYE_DEL == false).ToList();
        }

        public List<PRODUCT_PRICELIST> GetAll(int productID)
        {
            return this.Datacontext.PRODUCT_PRICELIST.Include("ZONE").Where(x => x.SYE_DEL == false && x.PRODUCT_ID == productID).OrderBy(x => x.ZONE_ID).ToList();
        }

        public List<PRODUCT_PRICELIST> GetAllInclude()
        {
            return this.Datacontext.PRODUCT_PRICELIST.Include("PRODUCT").Where(x => x.SYE_DEL == false).ToList();
        }
        #endregion
    
    }
}
