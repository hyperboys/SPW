using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SPW.DAL;
using SPW.Model;

namespace SPW.DataService
{
    public class OrderDetailService : ServiceBase, IDataService<ORDER_DETAIL>, IService 
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

        #region IDataService<ORDER_DETAIL> Members

        public void Add(ORDER_DETAIL obj)
        {
            this.Datacontext.ORDER_DETAIL.Add(obj);
            this.Datacontext.SaveChanges();
        }

        public void AddList(List<ORDER_DETAIL> obj)
        {
            throw new NotImplementedException();
        }

        public void Edit(ORDER_DETAIL obj)
        {
            var item = this.Datacontext.ORDER_DETAIL.Where(x => x.ORDER_DETAIL_ID == obj.ORDER_DETAIL_ID).FirstOrDefault();
            item.UPDATE_DATE = obj.UPDATE_DATE;
            item.UPDATE_EMPLOYEE_ID = obj.UPDATE_EMPLOYEE_ID;
            this.Datacontext.SaveChanges();
        }
        public void EditOrderDetailCancel(int ORDER_DETAIL_ID)
        {
            var item = this.Datacontext.ORDER_DETAIL.Where(x => x.ORDER_DETAIL_ID == ORDER_DETAIL_ID).FirstOrDefault();
            item.UPDATE_DATE = DateTime.Now;
            item.SYE_DEL = true;
            this.Datacontext.SaveChanges();
        }

        public void EditList(List<ORDER_DETAIL> objList)
        {
            foreach (var item in objList)
            {
                var obj = this.Datacontext.ORDER_DETAIL.Where(x => x.ORDER_DETAIL_ID == item.ORDER_DETAIL_ID).FirstOrDefault();
                if (obj != null)
                {
                    obj.PRODUCT_SEND_QTY = item.PRODUCT_SEND_QTY;
                    obj.UPDATE_DATE = item.UPDATE_DATE;
                    obj.UPDATE_EMPLOYEE_ID = item.UPDATE_EMPLOYEE_ID;
                }
            }
            this.Datacontext.SaveChanges();
        }

        public void EditOrderDetailList(List<ORDER_DETAIL> objList)
        {
            foreach (var item in objList)
            {
                var obj = this.Datacontext.ORDER_DETAIL.Where(x => x.ORDER_DETAIL_ID == item.ORDER_DETAIL_ID).FirstOrDefault();
                if (obj != null)
                {
                    obj.COLOR_ID = item.COLOR_ID;
                    obj.COLOR_TYPE_ID = item.COLOR_TYPE_ID;
                    obj.PRODUCT_QTY = item.PRODUCT_QTY;
                    obj.PRODUCT_PRICE_TOTAL = item.PRODUCT_QTY * obj.PRODUCT_PRICE;
                    obj.PRODUCT_WEIGHT_TOTAL = item.PRODUCT_WEIGHT * item.PRODUCT_QTY;
                    obj.UPDATE_DATE = DateTime.Now;
                    obj.PRODUCT_SEND_REMAIN = item.PRODUCT_SEND_REMAIN;
                }
            }
            this.Datacontext.SaveChanges();
        }

        public ORDER_DETAIL Select()
        {
            throw new NotImplementedException();
        }

        public ORDER_DETAIL Select(int ID)
        {
            return this.Datacontext.ORDER_DETAIL.Where(x => x.ORDER_DETAIL_ID == ID).FirstOrDefault();
        }

        public List<ORDER_DETAIL> GetAll()
        {
            return this.Datacontext.ORDER_DETAIL.Where(x => x.SYE_DEL == false).ToList();
        }

        public List<ORDER_DETAIL> GetAllInclude()
        {
            return this.Datacontext.ORDER_DETAIL.Include("PRODUCT").Where(x => x.SYE_DEL == false).ToList();
        }

        public List<ORDER_DETAIL> GetAllInclude(int ID)
        {
            return this.Datacontext.ORDER_DETAIL.Include("PRODUCT").Where(x => x.SYE_DEL == false && x.ORDER_ID == ID).ToList();
        }

        public List<ORDER_DETAIL> GetAllIncludeByOrder(int ID)
        {
            return this.Datacontext.ORDER_DETAIL.Include("PRODUCT").Include("COLOR").Include("COLOR_TYPE").Where(x => x.SYE_DEL == false && x.ORDER_ID == ID).ToList();
        }

        public void ConfirmOrder(List<DELIVERY_ORDER> DelOrderItems)
        {
            foreach (var delOrder in DelOrderItems)
            {
                foreach (var Details in delOrder.DELIVERY_ORDER_DETAIL)
                {
                    ORDER_DETAIL objtempUpdate = Select(Details.ORDER_DETAIL_ID);
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
                        }
                    }
                }
            }
            this._Datacontext.SaveChanges();
        }
        #endregion
    }
}
