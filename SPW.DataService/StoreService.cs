using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SPW.DAL;
using SPW.Model;

namespace SPW.DataService
{
    public class StoreService : ServiceBase, IDataService<STORE>, IService
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

        #region IDataService<STORE> Members

        public void Add(STORE obj)
        {
            this.Datacontext.STORE.Add(obj);
            this.Datacontext.SaveChanges();
        }

        public void AddList(List<STORE> obj)
        {
            throw new NotImplementedException();
        }

        public void Edit(STORE obj)
        {
            var item = this.Datacontext.STORE.Where(x => x.STORE_ID == obj.STORE_ID).FirstOrDefault();
            item.STORE_CODE = obj.STORE_CODE;
            item.STORE_NAME = obj.STORE_NAME;
            item.PROVINCE_ID = obj.PROVINCE_ID;
            item.SECTOR_ID = obj.SECTOR_ID;
            item.ROAD_ID = obj.ROAD_ID;
            item.ZONE_ID = obj.ZONE_ID;
            item.STORE_ADDR1 = obj.STORE_ADDR1;
            item.STORE_CODE = obj.STORE_CODE;
            item.STORE_DISTRICT = obj.STORE_DISTRICT;
            item.STORE_FAX = obj.STORE_FAX;
            item.STORE_MOBILE = obj.STORE_MOBILE;
            item.STORE_NAME = obj.STORE_NAME;
            item.STORE_POSTCODE = obj.STORE_POSTCODE;
            item.STORE_STREET = obj.STORE_STREET;
            item.STORE_SUBDISTRICT = obj.STORE_SUBDISTRICT;
            item.STORE_TEL1 = obj.STORE_TEL1;
            item.STORE_TEL2 = obj.STORE_TEL2;
            item.UPDATE_DATE = obj.UPDATE_DATE;
            item.UPDATE_EMPLOYEE_ID = obj.UPDATE_EMPLOYEE_ID;
            this.Datacontext.SaveChanges();
        }

        public void Delete(int ID)
        {
            var obj = this.Datacontext.STORE.Where(x => x.SECTOR_ID == ID).FirstOrDefault();
            this.Datacontext.STORE.Remove(obj);
            this.Datacontext.SaveChanges();
        }

        public void EditList(List<STORE> obj)
        {
            throw new NotImplementedException();
        }

        public STORE Select()
        {
            throw new NotImplementedException();
        }

        public STORE Select(int ID)
        {
            return this.Datacontext.STORE.Include("ZONE").Include("ZONE_DETAIL").Where(x => x.STORE_ID == ID).FirstOrDefault();
        }

        public STORE Select(string code, string name)
        {
            if (code != "")
            {
                return this.Datacontext.STORE.Where(x => x.STORE_CODE == code).FirstOrDefault();
            }
            else
            {
                return this.Datacontext.STORE.Where(x => x.STORE_NAME == name).FirstOrDefault();
            }
        }

        public STORE SelectInclude(int ID)
        {
            return this.Datacontext.STORE.Include("ORDER").Include("ZONE").Include("PROVINCE").Where(x => x.STORE_ID == ID).FirstOrDefault();
        }

        public List<STORE> GetAll()
        {
            return this.Datacontext.STORE.Where(x => x.SYE_DEL == false).ToList();
        }

        public List<STORE> GetAll(STORE item)
        {
            return this.Datacontext.STORE.Where(x => x.STORE_CODE.ToUpper().Contains(item.STORE_CODE.ToUpper())).ToList();
        }

        public int GetStoreID(string store_name)
        {
            return this.Datacontext.STORE.Where(x => x.STORE_NAME == store_name).FirstOrDefault().STORE_ID;
        }

        public List<STORE> GetAll(int SectorID, int ProvinceID)
        {
            return this.Datacontext.STORE.Where(x => x.SECTOR_ID == SectorID && x.PROVINCE_ID == ProvinceID).ToList();
        }

        public List<STORE> GetAllIncludeOrder()
        {
            return this.Datacontext.STORE.Include("ORDER").ToList();
        }

        public List<string> GetAllStreet()
        {
            return this.Datacontext.STORE.Where(x => x.PROVINCE.PROVINCE_NAME == "กรุงเทพมหานคร").Select(y => y.STORE_STREET).Distinct().ToList();
        }

        public List<STORE> GetAllIncludeNotCompleteOrder()
        {
            // Not Complete Status 12,30,50
            return this.Datacontext.STORE.Include("ORDER").Where(x => x.ORDER.Count > 0 && x.ORDER.Any(y => y.ORDER_STEP != "12" && y.ORDER_STEP != "30" && y.ORDER_STEP != "50")).ToList();
        }

        public int GetAllCount()
        {
            return GetAll().Count();
        }

        public List<STORE> GetAllByFilter(int PageIndex, int PageLimit)
        {
            return this.Datacontext.STORE.Where(x => x.SYE_DEL == false).OrderBy(x => x.STORE_ID).Skip(PageLimit * (PageIndex - 1)).Take(PageLimit).ToList();
        }

        public List<STORE> GetAllByFilterCondition(string StoreCode, string StoreName, int PageIndex, int PageLimit, ref int ItemsCount)
        {
            List<STORE> SourceItems = GetAll();
            if (StoreCode.Trim() != "")
            {
                SourceItems = SourceItems.Where(x => x.STORE_CODE.ToUpper().Contains(StoreCode.ToUpper())).ToList();
            }
            if (StoreName.Trim() != "")
            {
                SourceItems = SourceItems.Where(x => x.STORE_NAME.ToUpper().Contains(StoreName.ToUpper())).ToList();
            }
            ItemsCount = SourceItems.Count();
            return SourceItems.Skip(PageLimit * (PageIndex - 1)).Take(PageLimit).ToList();
        }

        public List<STORE> GetAllByFilterConditionDropdown(string StoreCode, string StoreName, int ProvinceID, string StoreStreet, int PageIndex, int PageLimit, ref int ItemsCount)
        {
            List<STORE> SourceItems = GetAll();
            if (ProvinceID != 0)
            {
                SourceItems = SourceItems.Where(x => x.PROVINCE_ID == ProvinceID).ToList();
            }
            if (StoreStreet.Trim() != "0")
            {
                SourceItems = SourceItems.Where(x => x.STORE_STREET.ToUpper() == StoreStreet.ToUpper()).ToList();
            }

            if (StoreCode.Trim() != "")
            {
                SourceItems = SourceItems.Where(x => x.STORE_CODE.ToUpper().Contains(StoreCode.ToUpper())).ToList();
            }
            if (StoreName.Trim() != "")
            {
                SourceItems = SourceItems.Where(x => x.STORE_NAME.ToUpper().Contains(StoreName.ToUpper())).ToList();
            }
            ItemsCount = SourceItems.Count();
            return SourceItems.Skip(PageLimit * (PageIndex - 1)).Take(PageLimit).ToList();
        }

        public List<STORE> GetAllByCondition(string StoreCode, string StoreName)
        {
            List<STORE> SourceItems = GetAll();
            if (StoreCode.Trim() != "")
            {
                SourceItems = SourceItems.Where(x => x.STORE_CODE.ToUpper().Contains(StoreCode.ToUpper())).ToList();
            }
            if (StoreName.Trim() != "")
            {
                SourceItems = SourceItems.Where(x => x.STORE_NAME.ToUpper().Contains(StoreName.ToUpper())).ToList();
            }
            return SourceItems.ToList();
        }

        public List<STORE> GetAllByProvinceID(int ProvinceID)
        {
            return this.Datacontext.STORE.Where(x => x.PROVINCE_ID == ProvinceID).ToList();
        }

        public List<STORE> GetAllByStoreID(int StoreID)
        {
            return this.Datacontext.STORE.Where(x => x.STORE_ID == StoreID).ToList();
        }

        public STORE GetStoreIndex(int StoreID)
        {
            return this._Datacontext.STORE.Include("SECTOR").Include("PROVINCE").FirstOrDefault(x => x.STORE_ID == StoreID);
        }
        #endregion

    }
}
