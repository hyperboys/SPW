using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SPW.DAL;
using SPW.Model;

namespace SPW.DataService
{
    public class OrderDetailService
    {
        private ORDER_DETAIL _item = new ORDER_DETAIL();
        private List<ORDER_DETAIL> _lstItem = new List<ORDER_DETAIL>();

        public OrderDetailService()
        {

        }

        public OrderDetailService(ORDER_DETAIL item)
        {
            _item = item;
        }

        public OrderDetailService(List<ORDER_DETAIL> lstItem)
        {
            _lstItem = lstItem;
        }

        public List<ORDER_DETAIL> GetALL()
        {
            using (var ctx = new SPWEntities())
            {
                var list = ctx.ORDER_DETAIL.Where(x => x.SYE_DEL == true).ToList();
                return list;
            }
        }

        public List<ORDER_DETAIL> GetALLInclude()
        {
            using (var ctx = new SPWEntities())
            {
                var list = ctx.ORDER_DETAIL.Include("PRODUCT").Where(x => x.SYE_DEL == true).ToList();
                return list;
            }
        }

        public List<ORDER_DETAIL> GetALLInclude(int ID)
        {
            using (var ctx = new SPWEntities())
            {
                var list = ctx.ORDER_DETAIL.Include("PRODUCT").Where(x => x.SYE_DEL == true && x.ORDER_ID == ID).ToList();
                return list;
            }
        }

        public List<ORDER_DETAIL> GetALLIncludeByOrder(int ID)
        {
            using (var ctx = new SPWEntities())
            {
                var list = ctx.ORDER_DETAIL.Include("PRODUCT").Where(x => x.SYE_DEL == true && x.ORDER_ID == ID).ToList();
                return list;
            }
        }

        public ORDER_DETAIL Select(int ID)
        {
            using (var ctx = new SPWEntities())
            {
                var list = ctx.ORDER_DETAIL.Where(x => x.ORDER_DETAIL_ID == ID).FirstOrDefault();
                return list;
            }
        }

        public void UpdateList()
        {
            using (var ctx = new SPWEntities())
            {
                foreach (var item in _lstItem)
                {
                    var obj = ctx.ORDER_DETAIL.Where(x => x.ORDER_DETAIL_ID == item.ORDER_DETAIL_ID).FirstOrDefault();
                    if (obj != null)
                    {
                        obj.PRODUCT_SEND_QTY = item.PRODUCT_SEND_QTY;
                        obj.UPDATE_DATE = item.UPDATE_DATE;
                        obj.UPDATE_EMPLOYEE_ID = item.UPDATE_EMPLOYEE_ID;
                    }
                }
                ctx.SaveChanges();
            }
        }
    }
}
