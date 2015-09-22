using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SPW.DAL;
using SPW.Model;

namespace SPW.DataService
{
    public class DeliveryOrderDetailService
    {
        private DELIVERY_ORDER_DETAIL _item = new DELIVERY_ORDER_DETAIL();
        private List<DELIVERY_ORDER_DETAIL> _lstItem = new List<DELIVERY_ORDER_DETAIL>();

        public DeliveryOrderDetailService()
        {

        }

        public DeliveryOrderDetailService(DELIVERY_ORDER_DETAIL item)
        {
            _item = item;
        }

        public DeliveryOrderDetailService(List<DELIVERY_ORDER_DETAIL> lstItem)
        {
            _lstItem = lstItem;
        }

        public List<DELIVERY_ORDER_DETAIL> GetALL()
        {
            using (var ctx = new SPWEntities())
            {
                var list = ctx.DELIVERY_ORDER_DETAIL.Where(x => x.SYE_DEL == true).ToList();
                return list;
            }
        }

        public List<DELIVERY_ORDER_DETAIL> GetALLInclude()
        {
            using (var ctx = new SPWEntities())
            {
                var list = ctx.DELIVERY_ORDER_DETAIL.Include("DELIVERY_ORDER").Where(x => x.SYE_DEL == true).ToList();
                return list;
            }
        }

        public List<DELIVERY_ORDER_DETAIL> GetALLInclude(int ID)
        {
            using (var ctx = new SPWEntities())
            {
                var list = ctx.DELIVERY_ORDER_DETAIL.Include("PRODUCT").Where(x => x.SYE_DEL == true && x.DELORDER_ID == ID).ToList();
                return list;
            }
        }

        public List<DELIVERY_ORDER_DETAIL> GetALLIncludeProduct()
        {
            using (var ctx = new SPWEntities())
            {
                var list = ctx.DELIVERY_ORDER_DETAIL.Include("PRODUCT").Where(x => x.SYE_DEL == true).ToList();
                return list;
            }
        }

        public DELIVERY_ORDER_DETAIL Select(int ID)
        {
            using (var ctx = new SPWEntities())
            {
                var list = ctx.DELIVERY_ORDER_DETAIL.Where(x => x.DELORDER_ID == ID).FirstOrDefault();
                return list;
            }
        }

        public void Add()
        {
            using (var ctx = new SPWEntities())
            {
                ctx.DELIVERY_ORDER_DETAIL.Add(_item);
                ctx.SaveChanges();
            }
        }
    }
}
