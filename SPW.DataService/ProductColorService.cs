using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SPW.DAL;
using SPW.Model;

namespace SPW.DataService
{
    public class ProductColorService
    {
        private PRODUCT_COLOR _item = new PRODUCT_COLOR();
        private List<PRODUCT_COLOR> _lstItem = new List<PRODUCT_COLOR>();

        public ProductColorService()
        {

        }

        public ProductColorService(PRODUCT_COLOR item)
        {
            _item = item;
        }

        public ProductColorService(List<PRODUCT_COLOR> lstItem)
        {
            _lstItem = lstItem;
        }

        public List<PRODUCT_COLOR> GetALL(int productID)
        {
            using (var ctx = new SPWEntities())
            {
                var list = ctx.PRODUCT_COLOR.Include("COLOR").Where(x => x.SYE_DEL == true && x.PRODUCT_ID == productID).OrderBy(x => x.COLOR.COLOR_TYPE_ID).ToList();
                return list;
            }
        }

        public List<PRODUCT_COLOR> GetALLInclude(int productID)
        {
            using (var ctx = new SPWEntities())
            {
                var list = ctx.PRODUCT_COLOR.Include("COLOR").Where(x => x.SYE_DEL == true && x.PRODUCT_ID == productID).OrderBy(x => x.COLOR.COLOR_TYPE_ID).ToList();
                return list;
            }
        }

        public List<PRODUCT_COLOR> GetALLInclude()
        {
            using (var ctx = new SPWEntities())
            {
                var list = ctx.PRODUCT_COLOR.Include("PRODUCT").Where(x => x.SYE_DEL == true).ToList();
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
                        ctx.PRODUCT_COLOR.Add(item);
                    }
                }
                ctx.SaveChanges();
            }
        }

        public void Del(int id)
        {
            using (var ctx = new SPWEntities())
            {
                PRODUCT_COLOR item = ctx.PRODUCT_COLOR.Where(x => x.PRODUCT_COLOR_ID == id).FirstOrDefault();
                if (item != null)
                {
                    ctx.PRODUCT_COLOR.Remove(item);
                }

                ctx.SaveChanges();
            }
        }
    }
}
