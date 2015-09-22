using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SPW.DAL;
using SPW.Model;

namespace SPW.DataService
{
    public class ZoneDetailService
    {
        private ZONE_DETAIL _item = new ZONE_DETAIL();
        private List<ZONE_DETAIL> _lstItem = new List<ZONE_DETAIL>();

        public ZoneDetailService()
        {

        }

        public ZoneDetailService(ZONE_DETAIL item)
        {
            _item = item;
        }

        public ZoneDetailService(List<ZONE_DETAIL> lstItem)
        {
            _lstItem = lstItem;
        }

        public List<ZONE_DETAIL> GetALL()
        {
            using (var ctx = new SPWEntities())
            {
                var list = ctx.ZONE_DETAIL.ToList().Where(x => x.SYE_DEL == true).ToList();
                return list;
            }
        }

        public List<ZONE_DETAIL> GetALLByUser(int ID)
        {
            using (var ctx = new SPWEntities())
            {
                var list = ctx.ZONE_DETAIL.ToList().Where(x => x.SYE_DEL == true && x.EMPLOYEE_ID == ID).ToList();
                return list;
            }
        }

        public List<ZONE_DETAIL> GetALLInclude()
        {
            using (var ctx = new SPWEntities())
            {
                var list = ctx.ZONE_DETAIL.Include("ZONE").ToList().Where(x =>x.SYE_DEL == true).ToList();
                return list;
            }
        }

        public List<ZONE_DETAIL> GetALLInclude(int ID)
        {
            using (var ctx = new SPWEntities())
            {
                var list = ctx.ZONE_DETAIL.Include("ZONE").ToList().Where(x => x.SYE_DEL == true && x.EMPLOYEE_ID == ID).ToList();
                return list;
            }
        }

        public ZONE_DETAIL Select(int ID)
        {
            using (var ctx = new SPWEntities())
            {
                var list = ctx.ZONE_DETAIL.Where(x => x.ZONE_ID == ID).FirstOrDefault();
                return list;
            }
        }

        public void Add()
        {
            using (var ctx = new SPWEntities())
            {
                ctx.ZONE_DETAIL.Add(_item);
                ctx.SaveChanges();
            }
        }

        //public void Edit()
        //{
        //    using (var ctx = new SPWEntities())
        //    {
        //        var obj = ctx.ZONE_DETAIL.Where(x => x.STORE_ID == _item.STORE_ID).FirstOrDefault();
        //        //obj.EMPLOYEE_ID = _item.EMPLOYEE_ID;
        //        obj.UPDATE_DATE = _item.UPDATE_DATE;
        //        obj.UPDATE_EMPLOYEE_ID = _item.UPDATE_EMPLOYEE_ID;
        //        ctx.SaveChanges();
        //    }
        //}

        public void AddList()
        {
            using (var ctx = new SPWEntities())
            {
                foreach (var item in _lstItem)
                {
                    if (item.Action == ActionEnum.Create)
                    {
                        ctx.ZONE_DETAIL.Add(item);
                    }
                }
                ctx.SaveChanges();
            }
        }

        public void Delete(int ID)
        {
            using (var ctx = new SPWEntities())
            {
                var obj = ctx.ZONE_DETAIL.Where(x => x.ZONE_DETAIL_ID == ID).FirstOrDefault();
                obj.SYE_DEL = false;
                obj.UPDATE_DATE = _item.UPDATE_DATE;
                obj.UPDATE_EMPLOYEE_ID = _item.UPDATE_EMPLOYEE_ID;
                ctx.SaveChanges();
            }
        }
    }
}
