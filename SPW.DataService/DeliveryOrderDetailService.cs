using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SPW.DAL;
using SPW.Model;

namespace SPW.DataService
{
    public class DeliveryOrderDetailService : ServiceBase, IDataService<DELIVERY_ORDER_DETAIL>, IService
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

        #region IDataService<COLOR_TYPE> Members

        public void Add(DELIVERY_ORDER_DETAIL obj)
        {
            this.Datacontext.DELIVERY_ORDER_DETAIL.Add(obj);
            this.Datacontext.SaveChanges();
        }

        public void AddList(List<DELIVERY_ORDER_DETAIL> obj)
        {
            throw new NotImplementedException();
        }

        public void Edit(DELIVERY_ORDER_DETAIL obj)
        {
            var item = this.Datacontext.DELIVERY_ORDER_DETAIL.Where(x => x.DELORDER_DETAIL_ID == obj.DELORDER_DETAIL_ID).FirstOrDefault();
            item.COLOR_TYPE_ID = obj.COLOR_TYPE_ID;
            item.COLOR_ID = obj.COLOR_ID;
            item.PRODUCT_ID = obj.PRODUCT_ID;
            //item.PRODUCT_TOTAL = obj.PRODUCT_TOTAL;
            //item.PRODUCT_SEND_QTY = obj.PRODUCT_SEND_QTY;
            item.PRODUCT_QTY = obj.PRODUCT_QTY;
            item.PRODUCT_PRICE = obj.PRODUCT_PRICE;
            item.UPDATE_DATE = obj.UPDATE_DATE;
            item.UPDATE_EMPLOYEE_ID = obj.UPDATE_EMPLOYEE_ID;
            this.Datacontext.SaveChanges();
        }

        public void EditList(List<DELIVERY_ORDER_DETAIL> objlist)
        {
            foreach (var obj in objlist)
            {
                var item = this.Datacontext.DELIVERY_ORDER_DETAIL.Where(x => x.DELORDER_DETAIL_ID == obj.DELORDER_DETAIL_ID).FirstOrDefault();
                item.COLOR_TYPE_ID = obj.COLOR_TYPE_ID;
                item.COLOR_ID = obj.COLOR_ID;
                item.PRODUCT_ID = obj.PRODUCT_ID;
                //item.PRODUCT_TOTAL = obj.PRODUCT_TOTAL;
                //item.PRODUCT_SEND_QTY = obj.PRODUCT_SEND_QTY;
                item.PRODUCT_QTY = obj.PRODUCT_QTY;
                item.PRODUCT_PRICE = obj.PRODUCT_PRICE;
                item.UPDATE_DATE = obj.UPDATE_DATE;
                item.UPDATE_EMPLOYEE_ID = obj.UPDATE_EMPLOYEE_ID;
            }
            this.Datacontext.SaveChanges();
        }

        public DELIVERY_ORDER_DETAIL Select()
        {
            throw new NotImplementedException();
        }

        public DELIVERY_ORDER_DETAIL Select(int ID)
        {
            return this.Datacontext.DELIVERY_ORDER_DETAIL.Where(x => x.DELORDER_DETAIL_ID == ID).FirstOrDefault();
        }

        public List<DELIVERY_ORDER_DETAIL> GetAll()
        {
            return this.Datacontext.DELIVERY_ORDER_DETAIL.Where(x => x.SYE_DEL == false).ToList();
        }

        public List<DELIVERY_ORDER_DETAIL> GetAllIncludeByDeliveryOrder(int ID)
        {
            return this.Datacontext.DELIVERY_ORDER_DETAIL.Include("PRODUCT").Include("ORDER_DETAIL").Include("COLOR").Include("COLOR_TYPE").Where(x => x.SYE_DEL == false && x.DELORDER_ID == ID).ToList();
        }

        public List<DELIVERY_ORDER_DETAIL> GetAllInclude()
        {
            return this.Datacontext.DELIVERY_ORDER_DETAIL.Include("DELIVERY_ORDER").Where(x => x.SYE_DEL == false).ToList();
        }

        public List<DELIVERY_ORDER_DETAIL> GetAllInclude(int ID)
        {
            return this.Datacontext.DELIVERY_ORDER_DETAIL.Include("PRODUCT").Where(x => x.SYE_DEL == false && x.DELORDER_ID == ID).ToList();
        }

        public List<DELIVERY_ORDER_DETAIL> GetAllIncludeProduct()
        {
            return this.Datacontext.DELIVERY_ORDER_DETAIL.Include("PRODUCT").Where(x => x.SYE_DEL == false).ToList();
        }

        public List<DELIVERY_ORDER_DETAIL> GetAllIncludeOrderByDeliveryOrder(int ID)
        {
            return this.Datacontext.DELIVERY_ORDER_DETAIL.Include("ORDER_DETAIL").Where(x => x.SYE_DEL == false && x.DELORDER_ID == ID).ToList();
        }

        #endregion

    }
}
