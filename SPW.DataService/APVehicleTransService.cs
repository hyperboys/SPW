﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SPW.DAL;
using SPW.Model;

namespace SPW.DataService
{
    public class APVehicleTransService : ServiceBase, IDataService<AP_VEHICLE_TRANS>, IService
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

        public void Add(AP_VEHICLE_TRANS item)
        {
            if (item.Action == ActionEnum.Create)
            {
                Datacontext.AP_VEHICLE_TRANS.Add(item);
                Datacontext.SaveChanges();
            }
        }

        public void AddList(List<AP_VEHICLE_TRANS> lstItem)
        {
            foreach (var item in lstItem)
            {
                if (item.Action == ActionEnum.Create)
                {
                    Datacontext.AP_VEHICLE_TRANS.Add(item);
                }
            }
            Datacontext.SaveChanges();
        }

        public void Edit(AP_VEHICLE_TRANS item)
        {

        }

        public void EditList(List<AP_VEHICLE_TRANS> lstItem)
        {

        }

        public AP_VEHICLE_TRANS Select()
        {
            throw new NotImplementedException();
        }

        public List<AP_VEHICLE_TRANS> GetAll()
        {
            return this.Datacontext.AP_VEHICLE_TRANS.Where(x => x.SYE_DEL == false).ToList();
        }
        public int GetNextTransID()
        {
            return this.Datacontext.AP_VEHICLE_TRANS.Where(x => x.SYE_DEL == false).ToList().Count + 1;
        } 
    }
}
