using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SPW.DAL;
using SPW.Model;

namespace SPW.DataService
{
    public class VehicleService : ServiceBase, IDataService<VEHICLE>, IService 
    {
        #region IService Members
        public DAL.SPWEntities Datacontext
        {
            get
            {
                return this._Datacontext;
            }
            set
            {
                this._Datacontext = value;
            }
        }
        #endregion

        #region IDataService<PROVINCE> Members

        public void Add(VEHICLE obj)
        {
            this.Datacontext.VEHICLE.Add(obj);
            this.Datacontext.SaveChanges();
        }

        public void AddList(List<VEHICLE> obj)
        {
            throw new NotImplementedException();
        }

        public void Edit(VEHICLE obj)
        {
            var item = this.Datacontext.VEHICLE.Where(x => x.VEHICLE_ID == obj.VEHICLE_ID).FirstOrDefault();
            item.VEHICLE_REGNO = obj.VEHICLE_REGNO;
            item.VEHICLE_TYPENO = obj.VEHICLE_TYPENO;
            item.VEHICLE_CODE = obj.VEHICLE_CODE;
            item.UPDATE_DATE = obj.UPDATE_DATE;
            item.UPDATE_EMPLOYEE_ID = obj.UPDATE_EMPLOYEE_ID;
            this.Datacontext.SaveChanges();
        }

        public void EditList(List<VEHICLE> obj)
        {
            throw new NotImplementedException();
        }

        public VEHICLE Select()
        {
            throw new NotImplementedException();
        }

        public VEHICLE Select(int ID)
        {
            return this.Datacontext.VEHICLE.Where(x => x.VEHICLE_ID == ID).FirstOrDefault();
        }

        public List<VEHICLE> GetAll()
        {
            return this.Datacontext.VEHICLE.Where(x => x.SYE_DEL == false).ToList();
        }

        public List<VEHICLE> GetAll(string subname, string name)
        {
            return this.Datacontext.VEHICLE.Where(x => x.VEHICLE_CODE.ToUpper().Contains(subname.ToUpper())
                && x.VEHICLE_REGNO.ToUpper().Contains(name.ToUpper())).ToList();
        }

        public VEHICLE GetCurrentByID(int VehicleID)
        {
            return this.Datacontext.VEHICLE.FirstOrDefault(x => x.VEHICLE_ID == VehicleID);
        }
        
        public void Delete(int ID)
        {
            var obj = this.Datacontext.VEHICLE.Where(x => x.VEHICLE_ID == ID).FirstOrDefault();
            this.Datacontext.VEHICLE.Remove(obj);
            this.Datacontext.SaveChanges();
        }


        #endregion
    }
}
