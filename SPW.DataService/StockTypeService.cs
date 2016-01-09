using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SPW.DAL;
using SPW.Model;

namespace SPW.DataService
{
    public class StockTypeService : ServiceBase, IDataService<STOCK_TYPE>, IService
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

        private STOCK_TYPE _item = new STOCK_TYPE();
        private List<STOCK_TYPE> _lstItem = new List<STOCK_TYPE>();

        public StockTypeService()
        {

        }

        public StockTypeService(STOCK_TYPE item)
        {
            _item = item;
        }

        public StockTypeService(List<STOCK_TYPE> lstItem)
        {
            _lstItem = lstItem;
        }

        public List<STOCK_TYPE> GetAll()
        {
            return Datacontext.STOCK_TYPE.Where(x => x.SYE_DEL == false).ToList();
        }

        public STOCK_TYPE Select()
        {
            return Datacontext.STOCK_TYPE.Where(x => x.SYE_DEL == false).FirstOrDefault();
        }

        public STOCK_TYPE Select(int ID)
        {
            return Datacontext.STOCK_TYPE.Where(x => x.STOCK_TYPE_ID == ID).FirstOrDefault();
        }

        public void Add(STOCK_TYPE item)
        {
            Datacontext.STOCK_TYPE.Add(item);
            Datacontext.SaveChanges();
        }

        public void AddList(List<STOCK_TYPE> listItem)
        {
            foreach (var item in listItem)
            {
                if (item.Action == ActionEnum.Create)
                {
                    Datacontext.STOCK_TYPE.Add(item);
                }
            }
            Datacontext.SaveChanges();
        }

        public void Edit(STOCK_TYPE item)
        {
            var obj = Datacontext.STOCK_TYPE.Where(x => x.STOCK_TYPE_ID == item.STOCK_TYPE_ID).FirstOrDefault();
            obj.STOCK_TYPE_NAME = item.STOCK_TYPE_NAME;
            obj.UPDATE_DATE = item.UPDATE_DATE;
            obj.UPDATE_EMPLOYEE_ID = item.UPDATE_EMPLOYEE_ID;
            Datacontext.SaveChanges();
        }

        public void EditList(List<STOCK_TYPE> listItem)
        {
            foreach (var item in listItem)
            {
                if (item.Action == ActionEnum.Update)
                {
                    var obj = Datacontext.STOCK_TYPE.Where(x => x.STOCK_TYPE_ID == item.STOCK_TYPE_ID).FirstOrDefault();
                    if (obj != null)
                    {
                        obj.STOCK_TYPE_NAME = item.STOCK_TYPE_NAME;
                        obj.UPDATE_DATE = item.UPDATE_DATE;
                        obj.UPDATE_EMPLOYEE_ID = item.UPDATE_EMPLOYEE_ID;
                    }
                }
            }
            Datacontext.SaveChanges();
        }
    }
}
