using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SPW.DAL;
using SPW.Model;

namespace SPW.DataService
{
    public class ProductDetailService
    {
        private PRODUCT_DETAIL _item = new PRODUCT_DETAIL();
        private List<PRODUCT_DETAIL> _lstItem = new List<PRODUCT_DETAIL>();

        public ProductDetailService()
        {

        }

        public ProductDetailService(PRODUCT_DETAIL item)
        {
            _item = item;
        }

        public ProductDetailService(List<PRODUCT_DETAIL> lstItem)
        {
            _lstItem = lstItem;
        }

        public List<PRODUCT_DETAIL> GetALL(int productID)
        {
            using (var ctx = new SPWEntities())
            {
                var list = ctx.PRODUCT_DETAIL.Where(x => x.SYE_DEL == true && x.PRODUCT_ID == productID).OrderBy(x => x.ZONE_ID).ToList();
                return list;
            }
        }

        public List<PRODUCT_DETAIL> GetALLInclude()
        {
            using (var ctx = new SPWEntities())
            {
                var list = ctx.PRODUCT_DETAIL.Include("PRODUCT").Where(x => x.SYE_DEL == true).ToList();
                return list;
            }
        }

        public List<PRODUCT_DETAIL> Select(int ID)
        {
            using (var ctx = new SPWEntities())
            {
                var list = ctx.PRODUCT_DETAIL.Include("ZONE").Where(x => x.PRODUCT_ID == ID).ToList();
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
                        ctx.PRODUCT_DETAIL.Add(item);
                    }
                    else if (item.Action == ActionEnum.Update)
                    {
                        var obj = ctx.PRODUCT_DETAIL.Where(x => x.PRODUCT_ID == item.PRODUCT_ID && x.ZONE_ID == item.ZONE_ID).FirstOrDefault();
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
