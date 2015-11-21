using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SPW.DAL;
using SPW.Model;

namespace SPW.DataService
{
    public class ProductService : ServiceBase, IDataService<PRODUCT>, IService 
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

        #region IDataService<PRODUCT> Members

        public void Add(PRODUCT obj)
        {
            this.Datacontext.PRODUCT.Add(obj);
            this.Datacontext.SaveChanges();
        }

        public void AddList(List<PRODUCT> obj)
        {
            throw new NotImplementedException();
        }

        public void Edit(PRODUCT obj)
        {
            var item = this.Datacontext.PRODUCT.Where(x => x.PRODUCT_ID == obj.PRODUCT_ID).FirstOrDefault();
            item.PRODUCT_CODE = obj.PRODUCT_CODE;
            item.PRODUCT_NAME = obj.PRODUCT_NAME;
            item.PRODUCT_SIZE = obj.PRODUCT_SIZE;
            item.PRODUCT_WEIGHT = obj.PRODUCT_WEIGHT;
            item.PRODUCT_PACKING_DESC = obj.PRODUCT_PACKING_DESC;
            item.PRODUCT_PACKING_PER_PDESC = obj.PRODUCT_PACKING_PER_PDESC;
            item.PRODUCT_PACKING_PER_UDESC = obj.PRODUCT_PACKING_PER_UDESC;
            item.PRODUCT_PACKING_QTY = obj.PRODUCT_PACKING_QTY;
            item.PRODUCT_TYPE_CODE = obj.PRODUCT_TYPE_CODE;
            item.PRODUCT_WEIGHT_DEFINE = obj.PRODUCT_WEIGHT_DEFINE;
            if (obj.PRODUCT_IMAGE_PATH != null)
            {
                item.PRODUCT_IMAGE_PATH = obj.PRODUCT_IMAGE_PATH;
            }
            item.CATEGORY_ID = obj.CATEGORY_ID;
            item.UPDATE_DATE = obj.UPDATE_DATE;
            item.UPDATE_EMPLOYEE_ID = obj.UPDATE_EMPLOYEE_ID;
            this.Datacontext.SaveChanges();
        }

        public void EditList(List<PRODUCT> obj)
        {
            throw new NotImplementedException();
        }

        public PRODUCT Select()
        {
            throw new NotImplementedException();
        }

        public PRODUCT Select(int ID)
        {
            return this.Datacontext.PRODUCT.Include("PRODUCT_PRICELIST").Where(x => x.PRODUCT_ID == ID).FirstOrDefault();
        }

        public PRODUCT SelectNotInclude(int ID)
        {
            return this.Datacontext.PRODUCT.Where(x => x.PRODUCT_ID == ID).FirstOrDefault();
        }

        public List<PRODUCT> GetAll()
        {
            return this.Datacontext.PRODUCT.Where(x => x.SYE_DEL == false).ToList();
        }

        public List<string> GetUDescPacking()
        {
            return this.Datacontext.PRODUCT.Select(x => x.PRODUCT_PACKING_PER_UDESC).Distinct().ToList();
        }

        public List<string> GetPDescPacking()
        {
            return this.Datacontext.PRODUCT.Select(x => x.PRODUCT_PACKING_PER_PDESC).Distinct().ToList();
        }

        public List<PRODUCT> GetAll(PRODUCT item)
        {
            return this.Datacontext.PRODUCT.Where(x => x.PRODUCT_CODE.Contains(item.PRODUCT_CODE)).ToList();
        }

        public List<PRODUCT> GetAllInclude()
        {
            return this.Datacontext.PRODUCT.Include("PRODUCT_PRICELIST").Where(x => x.SYE_DEL == false).ToList();
        }

        public List<PRODUCT> GetAllIncludeBySale()
        {
            return this.Datacontext.PRODUCT.Include("PRODUCT_PRICELIST").Where(x => x.SYE_DEL == false).ToList();
        }

        public List<PRODUCT> GetAllIncludeOrder()
        {
            return this.Datacontext.PRODUCT.Include("ORDER_DETAIL").Where(x => x.SYE_DEL == false).ToList();
        }
        #endregion
    
    }
}
