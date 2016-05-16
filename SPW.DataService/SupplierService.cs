using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SPW.DAL;
using SPW.Model;


namespace SPW.DataService
{
    public class SupplierService : ServiceBase, IDataService<VENDOR>, IService
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

        #region IDataService<VENDOR> Members

        public void Add(VENDOR obj)
        {
            this.Datacontext.VENDOR.Add(obj);
            this.Datacontext.SaveChanges();
        }

        public void AddList(List<VENDOR> obj)
        {
            throw new NotImplementedException();
        }

        public void Edit(VENDOR obj)
        {
            var item = this.Datacontext.VENDOR.Where(x => x.VENDOR_ID == obj.VENDOR_ID).FirstOrDefault();
            item.VENDOR_CODE = obj.VENDOR_CODE;
            item.VENDOR_NAME = obj.VENDOR_NAME;
            item.PROVINCE_ID = obj.PROVINCE_ID;
            //item.SECTOR_ID = obj.SECTOR_ID;
            item.ROAD_ID = obj.ROAD_ID;
            //item.ZONE_ID = obj.ZONE_ID;
            item.VENDOR_ADDR1 = obj.VENDOR_ADDR1;
            item.VENDOR_CODE = obj.VENDOR_CODE;
            item.VENDOR_DISTRICT = obj.VENDOR_DISTRICT;
            item.VENDOR_FAX = obj.VENDOR_FAX;
            item.VENDOR_MOBILE = obj.VENDOR_MOBILE;
            item.VENDOR_NAME = obj.VENDOR_NAME;
            item.VENDOR_POSTCODE = obj.VENDOR_POSTCODE;
            item.VENDOR_STREET = obj.VENDOR_STREET;
            item.VENDOR_SUBDISTRICT = obj.VENDOR_SUBDISTRICT;
            item.VENDOR_TEL1 = obj.VENDOR_TEL1;
            item.VENDOR_TEL2 = obj.VENDOR_TEL2;
            item.UPDATE_DATE = obj.UPDATE_DATE;
            item.UPDATE_EMPLOYEE_ID = obj.UPDATE_EMPLOYEE_ID;
            //item.VENDOR_EMAIL = obj.VENDOR_EMAIL;
            //item.VENDOR_CONTACT_PERSON = obj.VENDOR_CONTACT_PERSON;
            //item.VENDOR_CREDIT_INTERVAL = obj.VENDOR_CREDIT_INTERVAL;
            //item.VENDOR_CREDIT_VALUE = obj.VENDOR_CREDIT_VALUE;
            //item.VAT_TYPE = obj.VAT_TYPE;
            //item.VAT_RATE = obj.VAT_RATE;
            this.Datacontext.SaveChanges();
        }

        public void Delete(int ID)
        {
            var obj = this.Datacontext.VENDOR.Where(x => x.VENDOR_ID == ID).FirstOrDefault();
            this.Datacontext.VENDOR.Remove(obj);
            this.Datacontext.SaveChanges();
        }

        public void EditList(List<VENDOR> obj)
        {
            throw new NotImplementedException();
        }

        public VENDOR Select()
        {
            throw new NotImplementedException();
        }

        public VENDOR Select(int ID)
        {
            return this.Datacontext.VENDOR.Where(x => x.VENDOR_ID == ID).FirstOrDefault();
        }

        public VENDOR Select(string vendorName)
        {
            return this.Datacontext.VENDOR.Where(x => x.VENDOR_NAME == vendorName).FirstOrDefault();
        }

        public VENDOR SelectByVendorCode(string vendorCode)
        {
            return this.Datacontext.VENDOR.Where(x => x.VENDOR_CODE == vendorCode).FirstOrDefault();
        }

        public VENDOR SelectInclude(int ID)
        {
            return this.Datacontext.VENDOR.Include("ORDER").Include("PROVINCE").Where(x => x.VENDOR_ID == ID).FirstOrDefault();
        }

        public List<VENDOR> GetAll()
        {
            return this.Datacontext.VENDOR.Where(x => x.SYE_DEL == false).ToList();
        }
        public List<VENDOR> GetAll(string VENDOR_CODE, string VENDOR_NAME)
        {
            return this.Datacontext.VENDOR.Where(x => x.SYE_DEL == false && x.VENDOR_CODE.Contains(VENDOR_CODE) && x.VENDOR_NAME.Contains(VENDOR_NAME)).ToList();
        }

        public List<VENDOR> GetAll(VENDOR item)
        {
            return this.Datacontext.VENDOR.Where(x => x.VENDOR_CODE.ToUpper().Contains(item.VENDOR_CODE.ToUpper())).ToList();
        }

        public List<VENDOR> GetAll(int SectorID, int ProvinceID)
        {
            return this.Datacontext.VENDOR.Where(x => x.PROVINCE_ID == ProvinceID).ToList();
        }

        public List<VENDOR> GetAllIncludeOrder()
        {
            return this.Datacontext.VENDOR.Include("ORDER").ToList();
        }

        public List<string> GetAllStreet()
        {
            return this.Datacontext.VENDOR.Where(x => x.PROVINCE.PROVINCE_NAME == "กรุงเทพมหานคร").Select(y => y.VENDOR_STREET).Distinct().ToList();
        }

        //public List<VENDOR> GetAllIncludeNotCompleteOrder()
        //{
        //    // Not Complete Status 12,30,50
        //    return this.Datacontext.VENDOR.Include("ORDER").Where(x => x.ORDER.Count > 0 && x.ORDER.Any(y => y.ORDER_STEP != "12" && y.ORDER_STEP != "30" && y.ORDER_STEP != "50")).ToList();
        //}

        public int GetAllCount()
        {
            return GetAll().Count();
        }

        public List<VENDOR> GetAllByFilter(int PageIndex, int PageLimit)
        {
            return this.Datacontext.VENDOR.Where(x => x.SYE_DEL == false).OrderBy(x => x.VENDOR_ID).Skip(PageLimit * (PageIndex - 1)).Take(PageLimit).ToList();
        }

        public List<VENDOR> GetAllByFilterCondition(string StoreCode, string StoreName, int PageIndex, int PageLimit, ref int ItemsCount)
        {
            List<VENDOR> SourceItems = GetAll();
            if (StoreCode.Trim() != "")
            {
                SourceItems = SourceItems.Where(x => x.VENDOR_CODE.ToUpper().Contains(StoreCode.ToUpper())).ToList();
            }
            if (StoreName.Trim() != "")
            {
                SourceItems = SourceItems.Where(x => x.VENDOR_NAME.ToUpper().Contains(StoreName.ToUpper())).ToList();
            }
            ItemsCount = SourceItems.Count();
            return SourceItems.Skip(PageLimit * (PageIndex - 1)).Take(PageLimit).ToList();
        }

        public List<VENDOR> GetAllByFilterConditionDropdown(string StoreCode, string StoreName, int ProvinceID, string StoreStreet, int PageIndex, int PageLimit, ref int ItemsCount)
        {
            List<VENDOR> SourceItems = GetAll();
            if (ProvinceID != 0)
            {
                SourceItems = SourceItems.Where(x => x.PROVINCE_ID == ProvinceID).ToList();
            }
            if (StoreStreet.Trim() != "0")
            {
                SourceItems = SourceItems.Where(x => x.VENDOR_STREET.ToUpper() == StoreStreet.ToUpper()).ToList();
            }

            if (StoreCode.Trim() != "")
            {
                SourceItems = SourceItems.Where(x => x.VENDOR_CODE.ToUpper().Contains(StoreCode.ToUpper())).ToList();
            }
            if (StoreName.Trim() != "")
            {
                SourceItems = SourceItems.Where(x => x.VENDOR_NAME.ToUpper().Contains(StoreName.ToUpper())).ToList();
            }
            ItemsCount = SourceItems.Count();
            return SourceItems.Skip(PageLimit * (PageIndex - 1)).Take(PageLimit).ToList();
        }

        public List<VENDOR> GetAllByCondition(string StoreCode, string StoreName)
        {
            List<VENDOR> SourceItems = GetAll();
            if (StoreCode.Trim() != "")
            {
                SourceItems = SourceItems.Where(x => x.VENDOR_CODE.ToUpper().Contains(StoreCode.ToUpper())).ToList();
            }
            if (StoreName.Trim() != "")
            {
                SourceItems = SourceItems.Where(x => x.VENDOR_NAME.ToUpper().Contains(StoreName.ToUpper())).ToList();
            }
            return SourceItems.ToList();
        }

        public List<VENDOR> GetAllByProvinceID(int ProvinceID)
        {
            return this.Datacontext.VENDOR.Where(x => x.PROVINCE_ID == ProvinceID).ToList();
        }

        public List<VENDOR> GetAllByStoreID(int StoreID)
        {
            return this.Datacontext.VENDOR.Where(x => x.VENDOR_ID == StoreID).ToList();
        }

        public VENDOR GetStoreIndex(int StoreID)
        {
            return this._Datacontext.VENDOR.Include("PROVINCE").FirstOrDefault(x => x.VENDOR_ID == StoreID);
        }

        public int GetVendorID(string VENDOR_CODE)
        {
            return this.Datacontext.VENDOR.Where(x => x.SYE_DEL == false && x.VENDOR_CODE == VENDOR_CODE).FirstOrDefault().VENDOR_ID;
        }
        public int CountVendorCode(string VENDOR_CODE)
        {
            return this.Datacontext.VENDOR.Where(x => x.SYE_DEL == false && x.VENDOR_CODE == VENDOR_CODE).Count();
        }
        #endregion

    }
}
