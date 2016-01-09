using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SPW.DAL;
using SPW.Model;

namespace SPW.DataService
{
    public class OrderService : ServiceBase, IDataService<ORDER>, IService
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

        #region IDataService<ORDER> Members

        public void Add(ORDER obj)
        {
            this.Datacontext.ORDER.Add(obj);
            this.Datacontext.SaveChanges();
        }

        public void AddList(List<ORDER> obj)
        {
            throw new NotImplementedException();
        }

        public void Edit(ORDER obj)
        {
            var item = this.Datacontext.ORDER.Where(x => x.ORDER_ID == obj.ORDER_ID).FirstOrDefault();
            item.ORDER_CODE = obj.ORDER_CODE;
            item.UPDATE_DATE = obj.UPDATE_DATE;
            item.UPDATE_EMPLOYEE_ID = obj.UPDATE_EMPLOYEE_ID;
            this.Datacontext.SaveChanges();
        }

        public void EditOrderTotal(int ORDER_ID,decimal ORDER_TOTAL)
        {
            var item = this.Datacontext.ORDER.Where(x => x.ORDER_ID == ORDER_ID).FirstOrDefault();
            item.ORDER_TOTAL = ORDER_TOTAL;
            this.Datacontext.SaveChanges();
        }

        public void EditList(List<ORDER> objList)
        {
            foreach (var item in objList)
            {
                var obj = this.Datacontext.ORDER.Where(x => x.ORDER_ID == item.ORDER_ID).FirstOrDefault();
                if (obj != null)
                {
                    //obj.ORDER_APPROVE = item.ORDER_APPROVE;
                    obj.UPDATE_DATE = item.UPDATE_DATE;
                    obj.UPDATE_EMPLOYEE_ID = item.UPDATE_EMPLOYEE_ID;
                }
            }
            this.Datacontext.SaveChanges();
        }

        public void EditOrderStepCancel(int EditSysDel)
        {
            var item = this.Datacontext.ORDER.Where(x => x.ORDER_ID == EditSysDel).FirstOrDefault();
            item.ORDER_STEP = "12"; //Cancel
            item.UPDATE_DATE = DateTime.Now;
            //item.UPDATE_EMPLOYEE_ID = obj.UPDATE_EMPLOYEE_ID;
            this.Datacontext.SaveChanges();
        }

        public void EditOrderStepHQApprove(int EditSysDel)
        {
            var item = this.Datacontext.ORDER.Where(x => x.ORDER_ID == EditSysDel).FirstOrDefault();
            item.ORDER_STEP = "11"; //HQ Approve
            item.UPDATE_DATE = DateTime.Now;
            //item.UPDATE_EMPLOYEE_ID = obj.UPDATE_EMPLOYEE_ID;
            this.Datacontext.SaveChanges();
        }

        public void EditSysDel(int EditSysDel)
        {
            var item = this.Datacontext.ORDER.Where(x => x.ORDER_ID == EditSysDel).FirstOrDefault();
            item.SYE_DEL = true;
            item.UPDATE_DATE = DateTime.Now;
            //item.UPDATE_EMPLOYEE_ID = obj.UPDATE_EMPLOYEE_ID;
            this.Datacontext.SaveChanges();
        }

        public ORDER Select()
        {
            throw new NotImplementedException();
        }

        public ORDER Select(int ID)
        {
            return this.Datacontext.ORDER.Include("ORDER_DETAIL").Where(x => x.ORDER_ID == ID).FirstOrDefault();
        }

        public List<ORDER> GetAllIncludeStore()
        {
            return this.Datacontext.ORDER.Include("STORE").Where(x => x.SYE_DEL == false).ToList();
        }

        public ORDER SelectIncludeStore(int ID)
        {
            return this.Datacontext.ORDER.Include("STORE").Where(x => x.ORDER_ID == ID).FirstOrDefault();
        }

        public List<ORDER> GetAll()
        {
            return this.Datacontext.ORDER.Where(x => x.SYE_DEL == false).ToList();
        }

        public List<ORDER> GetAllInclude()
        {
            return this.Datacontext.ORDER.Include("ORDER_DETAIL").Where(x => x.SYE_DEL == false).ToList();
        }

        public List<ORDER> GetStoreInOrder()
        {
            return this.Datacontext.ORDER.Include("STORE").Where(x => x.SYE_DEL == false && x.ORDER_STEP != "30" && x.ORDER_STEP != "40" && x.ORDER_STEP != "50" && x.ORDER_STEP != "12").ToList();
        }

        public List<ORDER> GetAllIncludeByStore(int ID)
        {
            return this.Datacontext.ORDER.Include("ORDER_DETAIL").Where(x => x.SYE_DEL == false && x.STORE_ID == ID).ToList();
        }

        public List<ORDER> GetAllNotCompleteIncludeByStore(int ID)
        {
            // Not Complete Status 10,12,30,50
            return this.Datacontext.ORDER.Include("ORDER_DETAIL").Where(x => x.SYE_DEL == false && x.STORE_ID == ID && x.ORDER_STEP != "10" && x.ORDER_STEP != "12" && x.ORDER_STEP != "30" && x.ORDER_STEP != "50").ToList();
        }

        public int GetOrderCode(string date)
        {
            return this.Datacontext.ORDER.Where(x=>x.ORDER_CODE.Contains(date)).Count() + 1;
        }

        public List<ORDER> GetAllByStore(List<STORE> StoreList)
        {
            List<ORDER> SourceItems = new List<ORDER>();
            foreach (var item in StoreList)
            {
                SourceItems.AddRange(GetAllNotCompleteIncludeByStore(item.STORE_ID));
            }

            return SourceItems.ToList();
        }

        public void FlagStatus(List<int> objOrderItems, string Flag, int UpdateID)
        {
            foreach (var item in objOrderItems)
            {
                ORDER objOrder = Select(item);
                objOrder.ORDER_STEP = Flag;
                objOrder.UPDATE_EMPLOYEE_ID = UpdateID;
                objOrder.UPDATE_DATE = DateTime.Now;
            }
            this.Datacontext.SaveChanges();
        }

        public List<ORDER> GetAllByID(List<int> KeyItems)
        {
            return (this._Datacontext.ORDER.Include("STORE").Where(x => KeyItems.Any(y => y == x.ORDER_ID)).ToList());
        }

        #endregion
    }
}
