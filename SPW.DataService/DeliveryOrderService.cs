using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SPW.DAL;
using SPW.Model;

namespace SPW.DataService
{
    public class DeliveryOrderService : ServiceBase, IDataService<DELIVERY_ORDER>, IService 
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

        #region IDataService<DELIVERY_ORDER> Members

        public void Add(DELIVERY_ORDER obj)
        {
            this.Datacontext.DELIVERY_ORDER.Add(obj);
            this.Datacontext.SaveChanges();
        }

        public void AddList(List<DELIVERY_ORDER> obj)
        {
            throw new NotImplementedException();
        }

        public void Edit(DELIVERY_ORDER obj)
        {
            var item = this.Datacontext.DELIVERY_ORDER.Where(x => x.DELORDER_ID == obj.DELORDER_ID).FirstOrDefault();
            item.DELORDER_CODE = obj.DELORDER_CODE;
            item.DELORDER_STEP = obj.DELORDER_STEP;
            item.VEHICLE_ID = obj.VEHICLE_ID;
            item.UPDATE_DATE = obj.UPDATE_DATE;
            item.UPDATE_EMPLOYEE_ID = obj.UPDATE_EMPLOYEE_ID;
            this.Datacontext.SaveChanges();
        }

        public void EditList(List<DELIVERY_ORDER> obj)
        {
            throw new NotImplementedException();
        }

        public DELIVERY_ORDER Select()
        {
            throw new NotImplementedException();
        }

        public DELIVERY_ORDER Select(int ID)
        {
            return this.Datacontext.DELIVERY_ORDER.Include("STORE").Include("VEHICLE").Include("DELIVERY_ORDER_DETAIL").Where(x => x.DELORDER_ID == ID).FirstOrDefault();
        }

        public List<DELIVERY_ORDER> GetAll()
        {
            return this.Datacontext.DELIVERY_ORDER.Where(x => x.SYE_DEL == false).ToList();
        }

        public List<DELIVERY_ORDER> GetAllInclude()
        {
            return this.Datacontext.DELIVERY_ORDER.Include("DELIVERY_ORDER_DETAIL").Where(x => x.SYE_DEL == false).ToList();
        }

        public int GetAllCount()
        {
            return GetAll().Count();
        }

        public int GetID(DateTime date)
        {
            return this._Datacontext.DELIVERY_ORDER.Where(x => x.CREATE_DATE == date.Date).Count();
        }

        public List<DELIVERY_ORDER> GetAllByFilter(int PageIndex, int PageLimit)
        {
            return this.Datacontext.DELIVERY_ORDER.Include("VEHICLE").Where(x => x.SYE_DEL == false).OrderBy(x=> x.CREATE_DATE).Skip(PageLimit * (PageIndex - 1)).Take(PageLimit).ToList();
        }

        public List<DELIVERY_ORDER> GetAllByFilterCondition(string DelOrderCode, int VehicleID,DateTime? BeginDate,DateTime? EndDate, int PageIndex, int PageLimit,ref int ItemsCount)
        {
            List<DELIVERY_ORDER> SourceItems = GetAll();
            if (DelOrderCode.Trim() != "")
            {
                SourceItems = SourceItems.Where(x => x.DELORDER_CODE.ToUpper().Contains(DelOrderCode.ToUpper())).ToList();
            }
            if (VehicleID > 0)
            {
                SourceItems = SourceItems.Where(x => x.VEHICLE_ID == VehicleID).ToList();
            }
            if (BeginDate != null && EndDate != null)
            {
                SourceItems = SourceItems.Where(x => x.CREATE_DATE.Value.Date >= BeginDate.Value.Date && x.CREATE_DATE.Value.Date <= EndDate.Value.Date).ToList();
            }
            ItemsCount = SourceItems.Count();
            return SourceItems.Skip(PageLimit * (PageIndex - 1)).Take(PageLimit).ToList();
        }

        public List<DELIVERY_ORDER> GetAllByDelIndexID(int DelIndexID)
        {
            return (this._Datacontext.DELIVERY_ORDER.Include("STORE").Include("DELIVERY_ORDER_DETAIL").Where(x => x.DELIND_ID == DelIndexID).ToList());
        }


        public void ConfirmDelOrder(List<DELIVERY_ORDER> delOrderItems)
        {
            foreach (var delOrder in delOrderItems)
            {
                DELIVERY_ORDER objDelOrderUpdate = Select(delOrder.DELORDER_ID);
                if (objDelOrderUpdate != null)
                {
                    foreach (var Details in objDelOrderUpdate.DELIVERY_ORDER_DETAIL)
                    {
                        DELIVERY_ORDER_DETAIL objDetailsUpdate = delOrder.DELIVERY_ORDER_DETAIL.FirstOrDefault(x => x.DELORDER_DETAIL_ID == Details.DELORDER_DETAIL_ID);
                        if (objDetailsUpdate != null)
                        {
                            objDetailsUpdate.PRODUCT_SENT_QTY = Details.PRODUCT_SENT_QTY;
                            objDetailsUpdate.PRODUCT_SENT_REMAIN = Details.PRODUCT_SENT_REMAIN;
                            objDetailsUpdate.PRODUCT_SENT_WEIGHT_TOTAL = Details.PRODUCT_SENT_WEIGHT_TOTAL;
                            objDetailsUpdate.PRODUCT_SENT_PRICE_TOTAL = Details.PRODUCT_SENT_PRICE_TOTAL;
                        }
                        
                    }
                    objDelOrderUpdate.DELORDER_PRICE_TOTAL = objDelOrderUpdate.DELIVERY_ORDER_DETAIL.Sum(x => x.PRODUCT_SENT_PRICE_TOTAL);
                    objDelOrderUpdate.DELORDER_WEIGHT_TOTAL = objDelOrderUpdate.DELIVERY_ORDER_DETAIL.Sum(x => x.PRODUCT_SENT_WEIGHT_TOTAL);
                    objDelOrderUpdate.DELORDER_STEP = "50";
                }
            }
            this._Datacontext.SaveChanges();
        }



        #endregion

    }
}
