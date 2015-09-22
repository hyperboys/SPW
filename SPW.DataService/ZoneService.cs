using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SPW.DAL;
using SPW.Model;

namespace SPW.DataService
{
    public class ZoneService
    {
        private ZONE _item = new ZONE();
        private List<ZONE> _lstItem = new List<ZONE>();

        public ZoneService()
        {

        }

        public ZoneService(ZONE item)
        {
            _item = item;
        }

        public ZoneService(List<ZONE> lstItem)
        {
            _lstItem = lstItem;
        }

        public List<ZONE> GetALL()
        {
            using (var ctx = new SPWEntities())
            {
                var list = ctx.ZONE.Where(x => x.SYE_DEL == true).ToList();
                return list;
            }
        }

        public List<ZONE> GetALL(ZONE item)
        {
            using (var ctx = new SPWEntities())
            {
                var list = ctx.ZONE.Where(x => x.ZONE_CODE.Contains(item.ZONE_CODE)).ToList();
                return list;
            }
        }

        public ZONE Select(int ID)
        {
            using (var ctx = new SPWEntities())
            {
                var list = ctx.ZONE.Where(x => x.ZONE_ID == ID).FirstOrDefault();
                return list;
            }
        }

        public void Add()
        {
            using (var ctx = new SPWEntities())
            {
                ctx.ZONE.Add(_item);
                ctx.SaveChanges();
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
                        ctx.ZONE.Add(item);
                    }
                }
                ctx.SaveChanges();
            }
        }

        public void Edit()
        {
            using (var ctx = new SPWEntities())
            {
                var obj = ctx.ZONE.Where(x => x.ZONE_ID == _item.ZONE_ID).FirstOrDefault();
                obj.ZONE_CODE = _item.ZONE_CODE;
                obj.ZONE_NAME = _item.ZONE_NAME;
                obj.UPDATE_DATE = _item.UPDATE_DATE;
                obj.UPDATE_EMPLOYEE_ID = _item.UPDATE_EMPLOYEE_ID;
                ctx.SaveChanges();
            }
        }

        public void EditList()
        {
            using (var ctx = new SPWEntities())
            {
                foreach (var item in _lstItem)
                {
                    if (item.Action == ActionEnum.Update)
                    {
                        var obj = ctx.ZONE.Where(x => x.ZONE_ID == item.ZONE_ID).FirstOrDefault();
                        if (obj != null)
                        {
                            obj.ZONE_CODE = _item.ZONE_CODE;
                            obj.ZONE_NAME = _item.ZONE_NAME;
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
