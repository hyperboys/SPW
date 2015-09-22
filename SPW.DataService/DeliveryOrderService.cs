using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SPW.DAL;
using SPW.Model;

namespace SPW.DataService
{
    public class DeliveryOrderService
    {
        private DELIVERY_ORDER _item = new DELIVERY_ORDER();
        private List<DELIVERY_ORDER> _lstItem = new List<DELIVERY_ORDER>();

        public DeliveryOrderService()
        {

        }

        public DeliveryOrderService(DELIVERY_ORDER item)
        {
            _item = item;
        }

        public DeliveryOrderService(List<DELIVERY_ORDER> lstItem)
        {
            _lstItem = lstItem;
        }

        public List<DELIVERY_ORDER> GetALL()
        {
            using (var ctx = new SPWEntities())
            {
                var list = ctx.DELIVERY_ORDER.Where(x => x.SYE_DEL == true).ToList();
                return list;
            }
        }

        public List<DELIVERY_ORDER> GetALLInclude()
        {
            using (var ctx = new SPWEntities())
            {
                var list = ctx.DELIVERY_ORDER.Include("DELIVERY_ORDER_DETAIL").Where(x => x.SYE_DEL == true).ToList();
                return list;
            }
        }

        public DELIVERY_ORDER Select(int ID)
        {
            using (var ctx = new SPWEntities())
            {
                var list = ctx.DELIVERY_ORDER.Include("DELIVERY_ORDER_DETAIL").Where(x => x.DELORDER_ID == ID).FirstOrDefault();
                return list;
            }
        }

        public void Add()
        {
            using (var ctx = new SPWEntities())
            {
                ctx.DELIVERY_ORDER.Add(_item);
                ctx.SaveChanges();
            }
        }

        public void Edit()
        {
            using (var ctx = new SPWEntities())
            {
                var obj = ctx.DELIVERY_ORDER.Where(x => x.DELORDER_ID == _item.DELORDER_ID).FirstOrDefault();
                obj.DELORDER_CODE = _item.DELORDER_CODE;
                obj.UPDATE_DATE = _item.UPDATE_DATE;
                obj.UPDATE_EMPLOYEE_ID = _item.UPDATE_EMPLOYEE_ID;
                ctx.SaveChanges();
            }
        }
    }
}
