using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SPW.DAL;
using SPW.Model;

namespace SPW.DataService
{
    public class ProductPromotionService
    {
        private PRODUCT_PROMOTION _item = new PRODUCT_PROMOTION();
        private List<PRODUCT_PROMOTION> _lstItem = new List<PRODUCT_PROMOTION>();

        public ProductPromotionService()
        {

        }

        public ProductPromotionService(PRODUCT_PROMOTION item)
        {
            _item = item;
        }

        public ProductPromotionService(List<PRODUCT_PROMOTION> lstItem)
        {
            _lstItem = lstItem;
        }

        public List<PRODUCT_PROMOTION> GetALL(int productID)
        {
            using (var ctx = new SPWEntities())
            {
                var list = ctx.PRODUCT_PROMOTION.Where(x => x.SYE_DEL == true && x.PRODUCT_ID == productID).OrderBy(x => x.ZONE_ID).ToList();
                return list;
            }
        }

        public List<PRODUCT_PROMOTION> GetALLInclude(int productID)
        {
            using (var ctx = new SPWEntities())
            {
                var list = ctx.PRODUCT_PROMOTION.Include("PRODUCT").Where(x => x.SYE_DEL == true && x.PRODUCT_ID == productID).ToList();
                return list;
            }
        }

        public PRODUCT_PROMOTION SelectByProductZone(int productID, int zoneId)
        {
            using (var ctx = new SPWEntities())
            {
                var list = ctx.PRODUCT_PROMOTION.Include("PRODUCT").Where(x => x.SYE_DEL == true && x.PRODUCT_ID == productID && x.ZONE_ID == zoneId).FirstOrDefault();
                return list;
            }
        }

        public List<PRODUCT_PROMOTION> GetALLIncludeZone(int productID)
        {
            using (var ctx = new SPWEntities())
            {
                var list = ctx.PRODUCT_PROMOTION.Include("ZONE").Where(x => x.SYE_DEL == true && x.PRODUCT_ID == productID).ToList();
                return list;
            }
        }

        public PRODUCT_PROMOTION Select(int ID)
        {
            using (var ctx = new SPWEntities())
            {
                var list = ctx.PRODUCT_PROMOTION.Include("ZONE").Where(x => x.PROMOTION_ID == ID).FirstOrDefault();
                return list;
            }
        }


        public void AddList()
        {
            using (var ctx = new SPWEntities())
            {
                foreach (var item in _lstItem)
                {
                    if (item.Action == ActionEnum.Create)
                    {
                        ctx.PRODUCT_PROMOTION.Add(item);
                    }
                }
                ctx.SaveChanges();
            }
        }

        public void Edit(PRODUCT_PROMOTION item)
        {
            using (var ctx = new SPWEntities())
            {
                if (item.Action == ActionEnum.Update)
                {
                    var obj = ctx.PRODUCT_PROMOTION.Where(x => x.PROMOTION_ID == item.PROMOTION_ID).FirstOrDefault();
                    if (obj != null)
                    {
                        obj.PRODUCT_CONDITION_QTY = item.PRODUCT_CONDITION_QTY;
                        obj.PRODUCT_FREE_QTY = item.PRODUCT_FREE_QTY;
                        obj.ZONE_ID = item.ZONE_ID;
                        obj.UPDATE_DATE = item.UPDATE_DATE;
                        obj.UPDATE_EMPLOYEE_ID = item.UPDATE_EMPLOYEE_ID;
                    }
                }
                ctx.SaveChanges();
            }
        }
    }
}
