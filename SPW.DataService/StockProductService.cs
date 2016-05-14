using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SPW.DAL;
using SPW.Model;

namespace SPW.DataService
{
    public class StockProductService : ServiceBase, IDataService<STOCK_PRODUCT_STOCK>, IService
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

        public void Add(STOCK_PRODUCT_STOCK item)
        {
            Datacontext.STOCK_PRODUCT_STOCK.Add(item);
            Datacontext.SaveChanges();
        }

        public void AddList(List<STOCK_PRODUCT_STOCK> lstItem)
        {
            foreach (var item in lstItem)
            {
                if (item.Action == ActionEnum.Create)
                {
                    Datacontext.STOCK_PRODUCT_STOCK.Add(item);
                }
            }
            Datacontext.SaveChanges();
        }

        public void Edit(STOCK_PRODUCT_STOCK item)
        {
            STOCK_PRODUCT_STOCK obj = Datacontext.STOCK_PRODUCT_STOCK.Where(x => x.PRODUCT_CODE.Contains(item.PRODUCT_CODE)).FirstOrDefault();
            obj.STOCK_REMAIN = item.STOCK_REMAIN;
            obj.UPDATE_DATE = item.UPDATE_DATE;
            obj.UPDATE_EMPLOYEE_ID = item.UPDATE_EMPLOYEE_ID;
            Datacontext.SaveChanges();
        }

        public void CutStock(STOCK_PRODUCT_STOCK item)
        {
            STOCK_PRODUCT_STOCK obj = Datacontext.STOCK_PRODUCT_STOCK.Where(x => x.PRODUCT_ID == item.PRODUCT_ID).FirstOrDefault();
            if (obj != null)
            {
                obj.STOCK_REMAIN -= item.STOCK_REMAIN;
                obj.UPDATE_DATE = item.UPDATE_DATE;
                obj.UPDATE_EMPLOYEE_ID = item.UPDATE_EMPLOYEE_ID;
                Datacontext.SaveChanges();
            }
        }

        public void EditList(List<STOCK_PRODUCT_STOCK> lstItem)
        {
            //foreach (var item in lstItem)
            //{
            //    if (item.Action == ActionEnum.Update)
            //    {
            //        STOCK_PRODUCT_STOCK obj = Datacontext.STOCK_PRODUCT_STOCK.Where(x => x.PRODUCT_CODE.Contains(item.PRODUCT_CODE) && x.COLOR_ID == item.COLOR_ID && x.COLOR_TYPE_ID == item.COLOR_TYPE_ID).FirstOrDefault();
            //        if (obj != null)
            //        {
            //            obj.STOCK_REMAIN = item.STOCK_REMAIN;
            //            obj.STOCK_MINIMUM = item.STOCK_MINIMUM;
            //            obj.UPDATE_DATE = item.UPDATE_DATE;
            //            obj.UPDATE_EMPLOYEE_ID = item.UPDATE_EMPLOYEE_ID;
            //            Datacontext.SaveChanges();
            //        }
            //    }
            //}
        }

        public STOCK_PRODUCT_STOCK Select()
        {
            throw new NotImplementedException();
        }

        public STOCK_PRODUCT_STOCK Select(int ID)
        {
            return Datacontext.STOCK_PRODUCT_STOCK.Include("COLOR").Where(x => x.PRODUCT_ID == ID).FirstOrDefault();
        }

        public int SelectForCutStock(int ID)
        {
            return Datacontext.STOCK_PRODUCT_STOCK.Where(x => x.PRODUCT_ID == ID).FirstOrDefault().STOCK_REMAIN.Value;
        }

        public List<STOCK_PRODUCT_STOCK> GetAll()
        {
            return this.Datacontext.STOCK_PRODUCT_STOCK.Include("COLOR").Where(x => x.SYE_DEL == false).ToList();
        }

        public List<STOCK_PRODUCT_STOCK> GetAllColorDetail()
        {
            return this.Datacontext.STOCK_PRODUCT_STOCK.Include("COLOR").Include("COLOR_TYPE").Where(x => x.SYE_DEL == false).ToList();
        }

        public List<STOCK_PRODUCT_STOCK> GetAll(int CATEGORY)
        {
            var a = Datacontext.STOCK_PRODUCT_STOCK.Include("COLOR")
                //.Join(Datacontext.PRODUCT,
                //    s => s.PRODUCT_ID,
                //    p => p.PRODUCT_ID,
                //    (s, p) => new { STOCK_PRODUCT_STOCK = s, PRODUCT = p })
                //.Where(x => x.STOCK_PRODUCT_STOCK.SYE_DEL == false && x.PRODUCT.CATEGORY.CATEGORY_ID.Equals(CATEGORY))
                //.Select(e => new STOCK_PRODUCT_STOCK
                //{
                //    PRODUCT_ID = e.PRODUCT.PRODUCT_ID,
                //    PRODUCT_CODE = e.PRODUCT.PRODUCT_CODE,
                //    COLOR_ID = e.STOCK_PRODUCT_STOCK.COLOR_ID,
                //    STOCK_REMAIN = e.STOCK_PRODUCT_STOCK.STOCK_REMAIN,
                //    SYE_DEL = e.STOCK_PRODUCT_STOCK.SYE_DEL,
                //    CREATE_DATE = e.STOCK_PRODUCT_STOCK.CREATE_DATE,
                //    UPDATE_DATE = e.STOCK_PRODUCT_STOCK.UPDATE_DATE,
                //    CREATE_EMPLOYEE_ID = e.STOCK_PRODUCT_STOCK.CREATE_EMPLOYEE_ID,
                //    UPDATE_EMPLOYEE_ID = e.STOCK_PRODUCT_STOCK.UPDATE_EMPLOYEE_ID
                //}).ToList();
                //.Select(e => new STOCK_PRODUCT_STOCK
                //{
                //    PRODUCT_ID = e.PRODUCT_ID,
                //    PRODUCT_CODE = e.PRODUCT_CODE,
                //    COLOR_ID = e.COLOR_ID,
                //    STOCK_REMAIN = e.STOCK_REMAIN,
                //    SYE_DEL = e.SYE_DEL,
                //    CREATE_DATE = e.CREATE_DATE,
                //    UPDATE_DATE = e.UPDATE_DATE,
                //    CREATE_EMPLOYEE_ID = e.CREATE_EMPLOYEE_ID,
                //    UPDATE_EMPLOYEE_ID = e.UPDATE_EMPLOYEE_ID
                //})
                .ToList();
            return null;
        }

        public List<STOCK_PRODUCT_STOCK> GetAll(STOCK_PRODUCT_STOCK item)
        {
            return Datacontext.STOCK_PRODUCT_STOCK.Where(x => x.PRODUCT_CODE.Contains(item.PRODUCT_CODE)).ToList();
        }

        //public List<STOCK_PRODUCT_STOCK> CheckNewItem()
        //{
        //    ProductService pService = new ProductService();
        //    ColorService cService = new ColorService();
        //    List<PRODUCT> product = pService.GetALL();
        //    List<COLOR> color = cService.GetALL();
        //    var stock = Datacontext.STOCK_PRODUCT_STOCK.Where(x => x.SYE_DEL == 0).Select(e => new { e.PRODUCT_ID, e.COLOR_ID });
        //    var listNewItem = (from p in product from c in color select new { p.PRODUCT_ID, c.COLOR_ID }).Except(stock);
        //    List<STOCK_PRODUCT_STOCK> listMasterProduct = (List<STOCK_PRODUCT_STOCK>)(from p in product
        //                                                                              from c in color
        //                                                                              join n in listNewItem on new { p.PRODUCT_ID, c.COLOR_ID } equals new { n.PRODUCT_ID, n.COLOR_ID }
        //                                                                              select new { c, p })
        //                                                        .Select(e => new STOCK_PRODUCT_STOCK
        //                                                        {
        //                                                            Action = ActionEnum.Create,
        //                                                            PRODUCT_ID = e.p.PRODUCT_ID,
        //                                                            PRODUCT_CODE = e.p.PRODUCT_CODE,
        //                                                            COLOR_ID = e.c.COLOR_ID,
        //                                                            STOCK_REMAIN = 0,
        //                                                            SYE_DEL = 0,
        //                                                            CREATE_DATE = DateTime.Now,
        //                                                            UPDATE_DATE = DateTime.Now,
        //                                                            CREATE_EMPLOYEE_ID = 8,
        //                                                            UPDATE_EMPLOYEE_ID = 8
        //                                                        }).ToList();
        //    return listMasterProduct;
        //}
    }
}
