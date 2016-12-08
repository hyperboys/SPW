using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SPW.DAL;
using SPW.Model;
using SPW.Common;

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
            try
            {
                if (item.Action == ActionEnum.Create)
                {
                    Datacontext.AP_VEHICLE_TRANS.Add(item);
                    Datacontext.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                DebugLog.WriteLog(ex.ToString());
            }
        }

        public void AddList(List<AP_VEHICLE_TRANS> lstItem)
        {
            try
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
            catch (Exception ex)
            {
                DebugLog.WriteLog(ex.ToString());
            }
        }

        public void Edit(AP_VEHICLE_TRANS item)
        {
            AP_VEHICLE_TRANS obj = Datacontext.AP_VEHICLE_TRANS.Where(x => x.AP_VEHICLE_TRANS_ID == (item.AP_VEHICLE_TRANS_ID)).FirstOrDefault();
            obj.ASSET_TYPE_ID = item.ASSET_TYPE_ID;
            obj.VEHICLE_ID = item.VEHICLE_ID;
            obj.VEHICLE_CODE = item.VEHICLE_CODE;
            obj.VENDOR_ID = item.VENDOR_ID;
            obj.VENDOR_CODE = item.VENDOR_CODE;
            obj.MILE_NO = item.MILE_NO;
            obj.MA_START_DATE = item.MA_START_DATE;
            obj.MA_FINISH_DATE = item.MA_FINISH_DATE;
            obj.MA_AMOUNT = item.MA_AMOUNT;
            obj.PAY_TYPE = item.PAY_TYPE;
            obj.PAY_DATE = item.PAY_DATE;
            obj.UPDATE_DATE = item.UPDATE_DATE;
            obj.UPDATE_EMPLOYEE_ID = item.UPDATE_EMPLOYEE_ID;
            obj.MA_DESC = item.MA_DESC;
            Datacontext.SaveChanges();
        }

        public void EditOrderStepCancel(int AP_VEHICLE_TRANS_ID)
        {
            var item = this.Datacontext.AP_VEHICLE_TRANS.Where(x => x.AP_VEHICLE_TRANS_ID == AP_VEHICLE_TRANS_ID).FirstOrDefault();
            item.SYE_DEL = true; //Cancel
            item.UPDATE_DATE = DateTime.Now;
            this.Datacontext.SaveChanges();
        }


        public void EditList(List<AP_VEHICLE_TRANS> lstItem)
        {

        }

        public AP_VEHICLE_TRANS Select()
        {
            throw new NotImplementedException();
        }

        public AP_VEHICLE_TRANS Select(int AP_VEHICLE_TRANS_ID)
        {
            return this.Datacontext.AP_VEHICLE_TRANS.Where(x => x.AP_VEHICLE_TRANS_ID == AP_VEHICLE_TRANS_ID && x.SYE_DEL == false).FirstOrDefault();
        }

        public List<AP_VEHICLE_TRANS> GetAll()
        {
            try
            {
                return this.Datacontext.AP_VEHICLE_TRANS.Include("ASSET_TYPE").Include("VEHICLE").Include("VENDOR").Where(x => x.SYE_DEL == false).ToList();
            }
            catch (Exception ex)
            {
                DebugLog.WriteLog(ex.ToString());
                return null;
            }
        }
        public int GetNextTransID()
        {
            try
            {
                return this.Datacontext.AP_VEHICLE_TRANS.ToList().Count + 1;
            }
            catch (Exception ex)
            {
                DebugLog.WriteLog(ex.ToString());
                return 0;
            }
        }
    }
}
