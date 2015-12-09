using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SPW.DAL;
using SPW.Model;

namespace SPW.DataService
{
    public class ProductPromotionService : ServiceBase, IDataService<PRODUCT_PROMOTION>, IService 
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

        #region IDataService<CATEGORY> Members

        public void Add(PRODUCT_PROMOTION obj)
        {
            this.Datacontext.PRODUCT_PROMOTION.Add(obj);
            this.Datacontext.SaveChanges();
        }

        public void AddList(List<PRODUCT_PROMOTION> obj)
        {
            foreach (var item in obj)
            {
                if (item.Action == ActionEnum.Create)
                {
                    this.Datacontext.PRODUCT_PROMOTION.Add(item);
                }
            }
            this.Datacontext.SaveChanges();
        }

        public void Edit(PRODUCT_PROMOTION obj)
        {
            if (obj.Action == ActionEnum.Update)
            {
                var item = this.Datacontext.PRODUCT_PROMOTION.Where(x => x.PROMOTION_ID == obj.PROMOTION_ID).FirstOrDefault();
                if (item != null)
                {
                    item.PRODUCT_CONDITION_QTY = obj.PRODUCT_CONDITION_QTY;
                    item.PRODUCT_FREE_QTY = obj.PRODUCT_FREE_QTY;
                    item.ZONE_ID = obj.ZONE_ID;
                    item.UPDATE_DATE = obj.UPDATE_DATE;
                    item.UPDATE_EMPLOYEE_ID = obj.UPDATE_EMPLOYEE_ID;
                }
            }
            this.Datacontext.SaveChanges();
        }

        public void EditList(List<PRODUCT_PROMOTION> obj)
        {
            throw new NotImplementedException();
        }

        public PRODUCT_PROMOTION Select()
        {
            throw new NotImplementedException();
        }

        public PRODUCT_PROMOTION Select(int ID)
        {
            return this.Datacontext.PRODUCT_PROMOTION.Include("ZONE").Where(x => x.PROMOTION_ID == ID).FirstOrDefault();
        }

        public PRODUCT_PROMOTION SelectByProductZone(int productID, int zoneId)
        {
            return this.Datacontext.PRODUCT_PROMOTION.Include("PRODUCT").Where(x => x.SYE_DEL == false && x.PRODUCT_ID == productID && x.ZONE_ID == zoneId).FirstOrDefault();
        }

        public List<PRODUCT_PROMOTION> GetAll()
        {
            return this.Datacontext.PRODUCT_PROMOTION.Where(x => x.SYE_DEL == false).ToList();
        }

        public List<PRODUCT_PROMOTION> GetAll(int productID)
        {
            return this.Datacontext.PRODUCT_PROMOTION.Where(x => x.SYE_DEL == false && x.PRODUCT_ID == productID).OrderBy(x => x.ZONE_ID).ToList();
        }

        public List<PRODUCT_PROMOTION> GetAllInclude(int productID)
        {
            return this.Datacontext.PRODUCT_PROMOTION.Include("PRODUCT").Where(x => x.SYE_DEL == false && x.PRODUCT_ID == productID).ToList();
        }


        public List<PRODUCT_PROMOTION> GetAllIncludeZone(int productID)
        {
            return this.Datacontext.PRODUCT_PROMOTION.Include("ZONE").Where(x => x.SYE_DEL == false && x.PRODUCT_ID == productID).ToList();
        }
        #endregion
    
    }
}
