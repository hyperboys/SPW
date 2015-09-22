using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SPW.DAL;
using SPW.Model;

namespace SPW.DataService
{
    public class ProductService
    {
        private PRODUCT _item = new PRODUCT();
        private List<PRODUCT> _lstItem = new List<PRODUCT>();

        public ProductService()
        {

        }

        public ProductService(PRODUCT item)
        {
            _item = item;
        }

        public ProductService(List<PRODUCT> lstItem)
        {
            _lstItem = lstItem;
        }

        public List<PRODUCT> GetALL()
        {
            using (var ctx = new SPWEntities())
            {
                var list = ctx.PRODUCT.Where(x => x.SYE_DEL == true).ToList();
                return list;
            }
        }

        public List<string> GetUDescPacking() 
        {
            using (var ctx = new SPWEntities())
            {
                var list = ctx.PRODUCT.Select(x=>x.PRODUCT_PACKING_PER_UDESC).Distinct().ToList();
                return list;
            }
        }

        public List<string> GetPDescPacking()
        {
            using (var ctx = new SPWEntities())
            {
                var list = ctx.PRODUCT.Select(x => x.PRODUCT_PACKING_PER_PDESC).Distinct().ToList();
                return list;
            }
        }

        public List<PRODUCT> GetALL(PRODUCT item)
        {
            using (var ctx = new SPWEntities())
            {
                var list = ctx.PRODUCT.Where(x => x.PRODUCT_CODE.Contains(item.PRODUCT_CODE)).ToList();
                return list;
            }
        }

        public List<PRODUCT> GetALLInclude()
        {
            using (var ctx = new SPWEntities())
            {
                var list = ctx.PRODUCT.Include("PRODUCT_PRICELIST").Where(x => x.SYE_DEL == true).ToList();
                return list;
            }
        }

        public List<PRODUCT> GetALLIncludeBySale()
        {
            using (var ctx = new SPWEntities())
            {
                var list = ctx.PRODUCT.Include("PRODUCT_PRICELIST").Where(x => x.SYE_DEL == true).ToList();
                return list;
            }
        }

        public List<PRODUCT> GetALLIncludeOrder()
        {
            using (var ctx = new SPWEntities())
            {
                var list = ctx.PRODUCT.Include("ORDER_DETAIL").Where(x => x.SYE_DEL == true).ToList();
                return list;
            }
        }

        public PRODUCT Select(int ID)
        {
            using (var ctx = new SPWEntities())
            {
                var list = ctx.PRODUCT.Include("PRODUCT_PRICELIST").Where(x => x.PRODUCT_ID == ID).FirstOrDefault();
                return list;
            }
        }

        public void Add()
        {
            using (var ctx = new SPWEntities())
            {
                ctx.PRODUCT.Add(_item);
                ctx.SaveChanges();
            }
        }

        public void Edit()
        {
            using (var ctx = new SPWEntities())
            {
                var obj = ctx.PRODUCT.Where(x => x.PRODUCT_ID == _item.PRODUCT_ID).FirstOrDefault();
                obj.PRODUCT_CODE = _item.PRODUCT_CODE;
                obj.PRODUCT_NAME = _item.PRODUCT_NAME;
                obj.PRODUCT_SIZE = _item.PRODUCT_SIZE;
                obj.PRODUCT_WEIGHT = _item.PRODUCT_WEIGHT;
                obj.PRODUCT_PACKING_DESC = _item.PRODUCT_PACKING_DESC;
                obj.PRODUCT_PACKING_PER_PDESC = _item.PRODUCT_PACKING_PER_PDESC;
                obj.PRODUCT_PACKING_PER_UDESC = _item.PRODUCT_PACKING_PER_UDESC;
                obj.PRODUCT_PACKING_QTY = _item.PRODUCT_PACKING_QTY;
                obj.PRODUCT_TYPE_CODE = _item.PRODUCT_TYPE_CODE;
                obj.PRODUCT_WEIGHT_DEFINE = _item.PRODUCT_WEIGHT_DEFINE;
                if (_item.PRODUCT_IMAGE_PATH != null)
                {
                    obj.PRODUCT_IMAGE_PATH = _item.PRODUCT_IMAGE_PATH;
                }
                obj.CATEGORY_ID = _item.CATEGORY_ID;
                obj.UPDATE_DATE = _item.UPDATE_DATE;
                obj.UPDATE_EMPLOYEE_ID = _item.UPDATE_EMPLOYEE_ID;
                ctx.SaveChanges();
            }
        }
    }
}
