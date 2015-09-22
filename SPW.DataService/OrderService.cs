using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SPW.DAL;
using SPW.Model;

namespace SPW.DataService
{
    public class OrderService
    {
        private ORDER _item = new ORDER();
        private List<ORDER> _lstItem = new List<ORDER>();

        public OrderService()
        {

        }

        public OrderService(ORDER item)
        {
            _item = item;
        }

        public OrderService(List<ORDER> lstItem)
        {
            _lstItem = lstItem;
        }

        public List<ORDER> GetALL()
        {
            using (var ctx = new SPWEntities())
            {
                var list = ctx.ORDER.Where(x => x.SYE_DEL == true).ToList();
                return list;
            }
        }

        public List<ORDER> GetALLInclude()
        {
            using (var ctx = new SPWEntities())
            {
                var list = ctx.ORDER.Include("ORDER_DETAIL").Where(x => x.SYE_DEL == true).ToList();
                return list;
            }
        }

        public List<ORDER> GetALLIncludeStore()
        {
            using (var ctx = new SPWEntities())
            {
                var list = ctx.ORDER.Include("STORE").Where(x => x.SYE_DEL == true && x.ORDER_APPROVE == "2").ToList();
                return list;
            }
        }

        public List<ORDER> GetALLIncludeByStore(int ID)
        {
            using (var ctx = new SPWEntities())
            {
                var list = ctx.ORDER.Include("ORDER_DETAIL").Where(x => x.SYE_DEL == true && x.STORE_ID == ID).ToList();
                return list;
            }
        }

        public List<STORE> GetStoreInOrder()
        {
            using (var ctx = new SPWEntities())
            {
                var list = ctx.STORE.Include("STORE").Where(x => x.SYE_DEL == true).ToList();
                return list;
            }
        }

        public ORDER Select(int ID)
        {
            using (var ctx = new SPWEntities())
            {
                var list = ctx.ORDER.Include("ORDER_DETAIL").Where(x => x.ORDER_ID == ID).FirstOrDefault();
                return list;
            }
        }

        public ORDER SelectIncludeStore(int ID)
        {
            using (var ctx = new SPWEntities())
            {
                var list = ctx.ORDER.Include("STORE").Where(x => x.ORDER_ID == ID).FirstOrDefault();
                return list;
            }
        }

        public void Add()
        {
            using (var ctx = new SPWEntities())
            {
                ctx.ORDER.Add(_item);
                ctx.SaveChanges();
            }
        }

        public void Edit()
        {
            using (var ctx = new SPWEntities())
            {
                var obj = ctx.ORDER.Where(x => x.ORDER_ID == _item.ORDER_ID).FirstOrDefault();
                obj.ORDER_CODE = _item.ORDER_CODE;
                obj.UPDATE_DATE = _item.UPDATE_DATE;
                obj.UPDATE_EMPLOYEE_ID = _item.UPDATE_EMPLOYEE_ID;
                ctx.SaveChanges();
            }
        }
    }
}
