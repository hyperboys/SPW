using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SPW.Model;
using SPW.DAL;

namespace SPW.DataService
{
    public class DeliveryIndexService : ServiceBase, IDataService<DELIVERY_INDEX>, IService
    {
        #region IDataService<DELIVERY_INDEX> Members

        public void Add(DELIVERY_INDEX obj)
        {
            this.Datacontext.DELIVERY_INDEX.Add(obj);
            this._Datacontext.SaveChanges();
        }

        public void AddList(List<DELIVERY_INDEX> obj)
        {
            throw new NotImplementedException();
        }

        public void Edit(DELIVERY_INDEX obj)
        {
            throw new NotImplementedException();
        }

        public void EditList(List<DELIVERY_INDEX> obj)
        {
            throw new NotImplementedException();
        }

        public DELIVERY_INDEX Select(int ID)
        {
            return this._Datacontext.DELIVERY_INDEX.Include("VEHICLE").Include("DELIVERY_ORDER").Include("DELIVERY_INDEX_DETAIL").FirstOrDefault(x => x.DELIND_ID == ID);
        }
        public DELIVERY_INDEX Select()
        {
            throw new NotImplementedException();
        }
        public List<DELIVERY_INDEX> GetAll()
        {
            return this.Datacontext.DELIVERY_INDEX.Include("DELIVERY_ORDER").Include("DELIVERY_INDEX_DETAIL").Where(x => x.SYE_DEL == false).ToList();
        }


        public int GetAllCount()
        {
            return GetAll().Count();
        }

        public int GetID(DateTime date)
        {
            return this._Datacontext.DELIVERY_INDEX.Where(x => x.CREATE_DATE == date.Date).Count();
        }

        public List<DELIVERY_INDEX> GetAllByFilter(int PageIndex, int PageLimit)
        {
            List<DELIVERY_INDEX> SourceItems = this.Datacontext.DELIVERY_INDEX.Include("DELIVERY_ORDER").Include("VEHICLE").Where(x => x.SYE_DEL == false).OrderBy(x => x.CREATE_DATE).Skip(PageLimit * (PageIndex - 1)).Take(PageLimit).ToList();
            foreach (DELIVERY_INDEX tmp in SourceItems)
            {
                List<DELIVERY_ORDER> dlo = this.Datacontext.DELIVERY_ORDER.Include("STORE").Where(x => x.DELIND_ID == tmp.DELIND_ID && x.SYE_DEL == false).ToList();
                List<int> listPovinceID = dlo.Select(x => x.STORE.PROVINCE_ID).ToList().Distinct().ToList();
                tmp.PROVINCE_NAME = getProvinceName(listPovinceID);
            }
            return SourceItems;
        }

        public List<DELIVERY_INDEX> GetAllByFilter(int PageIndex, int PageLimit, string status)
        {
            List<DELIVERY_INDEX> SourceItems = this.Datacontext.DELIVERY_INDEX.Include("DELIVERY_ORDER").Include("VEHICLE").Where(x => x.SYE_DEL == false).OrderBy(x => x.CREATE_DATE).ToList();
            SourceItems = SourceItems.Where(x => x.ISDELETE == (status == "30" ? true : false)).Skip(PageLimit * (PageIndex - 1)).Take(PageLimit).ToList();
            foreach (DELIVERY_INDEX tmp in SourceItems)
            {
                List<DELIVERY_ORDER> dlo = this.Datacontext.DELIVERY_ORDER.Include("STORE").Where(x => x.DELIND_ID == tmp.DELIND_ID && x.DELORDER_STEP == status && x.SYE_DEL == false).ToList();
                List<int> listPovinceID = dlo.Select(x => x.STORE.PROVINCE_ID).ToList().Distinct().ToList();
                tmp.PROVINCE_NAME = getProvinceName(listPovinceID);
            }
            return SourceItems;
        }

        public List<DELIVERY_INDEX> GetAllByFilterCondition(string DelOrderCode, int VehicleID, DateTime? BeginDate, DateTime? EndDate, int PageIndex, int PageLimit, ref int ItemsCount)
        {
            List<DELIVERY_INDEX> SourceItems = GetAll();
            if (DelOrderCode.Trim() != "")
            {
                SourceItems = SourceItems.Where(x => x.DELIND_CODE.ToUpper().Contains(DelOrderCode.ToUpper())).ToList();
            }
            if (VehicleID > 0)
            {
                SourceItems = SourceItems.Where(x => x.VEHICLE_ID == VehicleID).ToList();
            }
            if (BeginDate != null && EndDate != null)
            {
                System.Threading.Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.CreateSpecificCulture("en-US");
                SourceItems = SourceItems.Where(x => x.CREATE_DATE.Value.Date >= BeginDate.Value.Date && x.CREATE_DATE.Value.Date <= EndDate.Value.Date).ToList();
            }

            foreach (DELIVERY_INDEX tmp in SourceItems)
            {
                List<DELIVERY_ORDER> dlo = this.Datacontext.DELIVERY_ORDER.Include("STORE").Where(x => x.DELIND_ID == tmp.DELIND_ID && x.SYE_DEL == false).ToList();
                List<int> listPovinceID = dlo.Select(x => x.STORE.PROVINCE_ID).ToList().Distinct().ToList();
                tmp.PROVINCE_NAME = getProvinceName(listPovinceID);
            }

            ItemsCount = SourceItems.Count();
            return SourceItems.Skip(PageLimit * (PageIndex - 1)).Take(PageLimit).ToList();
        }

        public List<DELIVERY_INDEX> GetAllByFilterCondition(string DelOrderCode, int VehicleID, DateTime? BeginDate, DateTime? EndDate, int PageIndex, int PageLimit, ref int ItemsCount, string status)
        {
            List<DELIVERY_INDEX> SourceItems = GetAll().Where(x => x.ISDELETE == (status == "30" ? true : false)).ToList();
            if (DelOrderCode.Trim() != "")
            {
                SourceItems = SourceItems.Where(x => x.DELIND_CODE.ToUpper().Contains(DelOrderCode.ToUpper())).ToList();
            }
            if (VehicleID > 0)
            {
                SourceItems = SourceItems.Where(x => x.VEHICLE_ID == VehicleID).ToList();
            }
            if (BeginDate != null && EndDate != null)
            {
                System.Threading.Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.CreateSpecificCulture("en-US");
                SourceItems = SourceItems.Where(x => x.CREATE_DATE.Value.Date >= BeginDate.Value.Date && x.CREATE_DATE.Value.Date <= EndDate.Value.Date).ToList();
            }

            foreach (DELIVERY_INDEX tmp in SourceItems)
            {
                List<DELIVERY_ORDER> dlo = this.Datacontext.DELIVERY_ORDER.Include("STORE").Where(x => x.DELIND_ID == tmp.DELIND_ID && x.DELORDER_STEP == status && x.SYE_DEL == false).ToList();
                List<int> listPovinceID = dlo.Select(x => x.STORE.PROVINCE_ID).ToList().Distinct().ToList();
                tmp.PROVINCE_NAME = getProvinceName(listPovinceID);
            }

            ItemsCount = SourceItems.Count();
            return SourceItems.Skip(PageLimit * (PageIndex - 1)).Take(PageLimit).ToList();
        }

        public string getProvinceName(List<int> listPovinceID)
        {
            string province = "";
            foreach (int index in listPovinceID)
            {
                province += this.Datacontext.PROVINCE.Where(x => x.PROVINCE_ID == index).FirstOrDefault().PROVINCE_NAME + " ";
            }
            return province;
        }

        public void Delete(int IndexID)
        {
            DELIVERY_INDEX objDel = Select(IndexID);
            objDel.SYE_DEL = true;
            foreach (var item in objDel.DELIVERY_INDEX_DETAIL)
            {
                item.SYE_DEL = true;
            }
            foreach (var item in objDel.DELIVERY_ORDER)
            {
                item.SYE_DEL = true;
            }
            this._Datacontext.SaveChanges();
        }

        #endregion

        public void ConfirmDelOrderIndex(List<DELIVERY_ORDER> DelOrderItems, int delIndexId, int UpdateID)
        {
            DELIVERY_INDEX objdelIndex = Select(delIndexId);
            if (objdelIndex != null)
            {
                //Update DeliveryOrderIndex
                foreach (var indexDetail in objdelIndex.DELIVERY_INDEX_DETAIL)
                {
                    int SENT_QTY = 0;
                    decimal SENT_TOTAL_PRICE = 0;
                    decimal SENT_TOTAL_WEIGHT = 0;
                    foreach (var delOrder in DelOrderItems)
                    {
                        List<DELIVERY_ORDER_DETAIL> tempList = delOrder.DELIVERY_ORDER_DETAIL.Where(x => x.PRODUCT_ID == indexDetail.PRODUCT_ID && x.COLOR_ID == indexDetail.COLOR_ID
                                                               && x.COLOR_TYPE_ID == indexDetail.COLOR_TYPE_ID && x.IS_FREE == indexDetail.IS_FREE).ToList();
                        SENT_QTY += (int)tempList.Sum(x => x.PRODUCT_SENT_QTY);
                        SENT_TOTAL_PRICE += (decimal)tempList.Sum(x => x.PRODUCT_SENT_PRICE_TOTAL);
                        SENT_TOTAL_WEIGHT += (decimal)tempList.Sum(x => x.PRODUCT_SENT_WEIGHT_TOTAL);
                    }
                    indexDetail.PRODUCT_SENT_QTY = SENT_QTY;
                    indexDetail.PRODUCT_PRICE_TOTAL = SENT_TOTAL_PRICE;
                    indexDetail.PRODUCT_WEIGHT_TOTAL = SENT_TOTAL_WEIGHT;
                    indexDetail.UPDATE_EMPLOYEE_ID = UpdateID;
                    indexDetail.UPDATE_DATE = DateTime.Now;
                }
                objdelIndex.UPDATE_EMPLOYEE_ID = UpdateID;
                objdelIndex.UPDATE_DATE = DateTime.Now;

                //Update DeliveryOrder
                foreach (var delOrder in DelOrderItems)
                {
                    DELIVERY_ORDER objDelOrderUpdate = (new DeliveryOrderService() { Datacontext = this._Datacontext }).Select(delOrder.DELORDER_ID);
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
                                objDetailsUpdate.UPDATE_EMPLOYEE_ID = UpdateID;
                                objDetailsUpdate.UPDATE_DATE = DateTime.Now;
                            }

                        }
                        objDelOrderUpdate.DELORDER_PRICE_TOTAL = objDelOrderUpdate.DELIVERY_ORDER_DETAIL.Sum(x => x.PRODUCT_SENT_PRICE_TOTAL);
                        objDelOrderUpdate.DELORDER_WEIGHT_TOTAL = objDelOrderUpdate.DELIVERY_ORDER_DETAIL.Sum(x => x.PRODUCT_SENT_WEIGHT_TOTAL);
                        objDelOrderUpdate.DELORDER_STEP = "50";
                        objDelOrderUpdate.UPDATE_EMPLOYEE_ID = UpdateID;
                        objDelOrderUpdate.UPDATE_DATE = DateTime.Now;
                    }
                }

                //Update Order
                foreach (var delOrder in DelOrderItems)
                {
                    foreach (var Details in delOrder.DELIVERY_ORDER_DETAIL)
                    {
                        ORDER_DETAIL objtempUpdate = (new OrderDetailService() { Datacontext = this._Datacontext }).Select(Details.ORDER_DETAIL_ID);
                        if (objtempUpdate != null)
                        {
                            if (Details.PRODUCT_SENT_QTY > 0)
                            {
                                objtempUpdate.PRODUCT_SEND_ROUND = Details.PRODUCT_SENT_ROUND;
                                objtempUpdate.PRODUCT_SEND_QTY += Details.PRODUCT_SENT_QTY;
                                objtempUpdate.PRODUCT_SEND_REMAIN = Details.PRODUCT_SENT_REMAIN;
                                if (objtempUpdate.PRODUCT_SEND_REMAIN == 0)
                                {
                                    objtempUpdate.PRODUCT_SEND_COMPLETE = "1";
                                }
                                else
                                {
                                    objtempUpdate.PRODUCT_SEND_COMPLETE = "0";
                                }
                                objtempUpdate.UPDATE_EMPLOYEE_ID = UpdateID;
                                objtempUpdate.UPDATE_DATE = DateTime.Now;
                            }
                        }
                    }
                }

                this._Datacontext.SaveChanges();
            }
        }


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
    }
}
