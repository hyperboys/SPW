using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SPW.DAL;
using SPW.Model;

namespace SPW.DataService
{
    public class ProductPriceListService
    {
        private PRODUCT_PRICELIST _item = new PRODUCT_PRICELIST();
        private List<PRODUCT_PRICELIST> _lstItem = new List<PRODUCT_PRICELIST>();

        public ProductPriceListService()
        {

        }

        public ProductPriceListService(PRODUCT_PRICELIST item)
        {
            _item = item;
        }

        public ProductPriceListService(List<PRODUCT_PRICELIST> lstItem)
        {
            _lstItem = lstItem;
        }

        public List<PRODUCT_PRICELIST> GetALL(int productID)
        {
            using (var ctx = new SPWEntities())
            {
                var list = ctx.PRODUCT_PRICELIST.Where(x => x.SYE_DEL == true && x.PRODUCT_ID == productID).OrderBy(x => x.ZONE_ID).ToList();
                return list;
            }
        }

        public List<PRODUCT_PRICELIST> GetALLInclude()
        {
            using (var ctx = new SPWEntities())
            {
                var list = ctx.PRODUCT_PRICELIST.Include("PRODUCT").Where(x => x.SYE_DEL == true).ToList();
                return list;
            }
        }

        public List<PRODUCT_PRICELIST> Select(int ID)
        {
            using (var ctx = new SPWEntities())
            {
                var list = ctx.PRODUCT_PRICELIST.Include("ZONE").Where(x => x.PRODUCT_ID == ID).ToList();
                return list;
            }
        }

        public void AddUpdateList()
        {
            using (var ctx = new SPWEntities())
            {
                foreach (var item in _lstItem)
                {
                    if (item.Action == ActionEnum.Create)
                    {
                        ctx.PRODUCT_PRICELIST.Add(item);
                    }
                    else if (item.Action == ActionEnum.Update)
                    {
                        var obj = ctx.PRODUCT_PRICELIST.Where(x => x.PRODUCT_ID == item.PRODUCT_ID && x.ZONE_ID == item.ZONE_ID).FirstOrDefault();
                        if (obj != null)
                        {
                            obj.PRODUCT_PRICE = item.PRODUCT_PRICE;
                            obj.UPDATE_DATE = item.UPDATE_DATE;
                            obj.UPDATE_EMPLOYEE_ID = item.UPDATE_EMPLOYEE_ID;
                        }
                    }
                }
                ctx.SaveChanges();
            }
        }
    }
}
