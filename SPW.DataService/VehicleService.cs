using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SPW.DAL;
using SPW.Model;

namespace SPW.DataService
{
    public class VehicleService
    {
        private VEHICLE _item = new VEHICLE();
        private List<VEHICLE> _lstItem = new List<VEHICLE>();

        public VehicleService()
        {

        }

        public VehicleService(VEHICLE item)
        {
            _item = item;
        }

        public VehicleService(List<VEHICLE> lstItem)
        {
            _lstItem = lstItem;
        }

        public List<VEHICLE> GetALL()
        {
            using (var ctx = new SPWEntities())
            {
                var list = ctx.VEHICLE.Where(x => x.SYE_DEL == true).ToList();
                return list;
            }
        }

        public List<VEHICLE> GetALL(string subname, string name)
        {
            using (var ctx = new SPWEntities())
            {
                var list = ctx.VEHICLE.Where(x => x.VEHICLE_CODE.Contains(subname)
                    && x.VEHICLE_REGNO.Contains(name)).ToList();
                return list;
            }
        }

        public VEHICLE Select(int ID)
        {
            using (var ctx = new SPWEntities())
            {
                var list = ctx.VEHICLE.Where(x => x.VEHICLE_ID == ID).FirstOrDefault();
                return list;
            }
        }

        public void Add()
        {
            using (var ctx = new SPWEntities())
            {
                ctx.VEHICLE.Add(_item);
                ctx.SaveChanges();
            }
        }

        public void Edit()
        {
            using (var ctx = new SPWEntities())
            {
                var obj = ctx.VEHICLE.Where(x => x.VEHICLE_ID == _item.VEHICLE_ID).FirstOrDefault();
                obj.VEHICLE_REGNO = _item.VEHICLE_REGNO;
                obj.VEHICLE_TYPENO = _item.VEHICLE_TYPENO;
                obj.VEHICLE_CODE = _item.VEHICLE_CODE;
                obj.UPDATE_DATE = _item.UPDATE_DATE;
                obj.UPDATE_EMPLOYEE_ID = _item.UPDATE_EMPLOYEE_ID;
                ctx.SaveChanges();
            }
        }
    }
}
