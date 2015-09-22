using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SPW.DAL;
using SPW.Model;

namespace SPW.DataService
{
    public class StoreService
    {
        private STORE _item = new STORE();
        private List<STORE> _lstItem = new List<STORE>();

        public StoreService()
        {

        }

        public StoreService(STORE item)
        {
            _item = item;
        }

        public StoreService(List<STORE> lstItem)
        {
            _lstItem = lstItem;
        }

        public List<STORE> GetALL()
        {
            using (var ctx = new SPWEntities())
            {
                var list = ctx.STORE.Where(x => x.SYE_DEL == true).ToList();
                return list;
            }
        }

        public List<STORE> GetALL(STORE item)
        {
            using (var ctx = new SPWEntities())
            {
                var list = ctx.STORE.Where(x => x.STORE_CODE.Contains(item.STORE_CODE)).ToList();
                return list;
            }
        }

        public List<STORE> GetALL(int SectorID,int ProvinceID)
        {
            using (var ctx = new SPWEntities())
            {
                var list = ctx.STORE.Where(x => x.SECTOR_ID==SectorID && x.PROVINCE_ID == ProvinceID).ToList();
                return list;
            }
        }

        public STORE Select(int ID)
        {
            using (var ctx = new SPWEntities())
            {
                var list = ctx.STORE.Include("ZONE").Where(x => x.STORE_ID == ID).FirstOrDefault();
                return list;
            }
        }

        public void Add()
        {
            using (var ctx = new SPWEntities())
            {
                ctx.STORE.Add(_item);
                ctx.SaveChanges();
            }
        }

        public void AddList()
        {
            using (var ctx = new SPWEntities())
            {
                foreach (var item in _lstItem)
                {
                    if (item.Action == ActionEnum.Create)
                    {
                        ctx.STORE.Add(item);
                    }
                }
                ctx.SaveChanges();
            }
        }

        public void Edit()
        {
            using (var ctx = new SPWEntities())
            {
                var obj = ctx.STORE.Where(x => x.STORE_ID ==  _item.STORE_ID).FirstOrDefault();
                obj.STORE_CODE = _item.STORE_CODE;
                obj.STORE_NAME = _item.STORE_NAME;
                obj.PROVINCE_ID = _item.PROVINCE_ID;
                obj.SECTOR_ID = _item.SECTOR_ID;
                obj.ROAD_ID = _item.ROAD_ID;
                obj.ZONE_ID = _item.ZONE_ID;
                obj.STORE_ADDR1 = _item.STORE_ADDR1;
                obj.STORE_CODE = _item.STORE_CODE;
                obj.STORE_DISTRICT = _item.STORE_DISTRICT;
                obj.STORE_FAX = _item.STORE_FAX;
                obj.STORE_MOBILE = _item.STORE_MOBILE;
                obj.STORE_NAME = _item.STORE_NAME;
                obj.STORE_POSTCODE = _item.STORE_POSTCODE;
                obj.STORE_STREET = _item.STORE_STREET;
                obj.STORE_SUBDISTRICT = _item.STORE_SUBDISTRICT;
                obj.STORE_TEL1 = _item.STORE_TEL1;
                obj.STORE_TEL2 = _item.STORE_TEL2;
                obj.UPDATE_DATE = _item.UPDATE_DATE;
                obj.UPDATE_EMPLOYEE_ID = _item.UPDATE_EMPLOYEE_ID;
                ctx.SaveChanges();
            }
        }

        public void EditList()
        {
            using (var ctx = new SPWEntities())
            {
                foreach (var item in _lstItem)
                {
                    if (item.Action == ActionEnum.Update)
                    {
                        var obj = ctx.STORE.Where(x => x.STORE_ID == item.STORE_ID).FirstOrDefault();
                        if (obj != null)
                        {
                            obj.STORE_CODE = _item.STORE_CODE;
                            obj.STORE_NAME = _item.STORE_NAME;
                            obj.PROVINCE_ID = _item.PROVINCE_ID;
                            obj.SECTOR_ID = _item.SECTOR_ID;
                            obj.ROAD_ID = _item.ROAD_ID;
                            obj.STORE_ADDR1 = _item.STORE_ADDR1;
                            obj.STORE_CODE = _item.STORE_CODE;
                            obj.STORE_DISTRICT = _item.STORE_DISTRICT;
                            obj.STORE_FAX = _item.STORE_FAX;
                            obj.STORE_MOBILE = _item.STORE_MOBILE;
                            obj.STORE_NAME = _item.STORE_NAME;
                            obj.STORE_POSTCODE = _item.STORE_POSTCODE;
                            obj.STORE_STREET = _item.STORE_STREET;
                            obj.STORE_SUBDISTRICT = _item.STORE_SUBDISTRICT;
                            obj.STORE_TEL1 = _item.STORE_TEL1;
                            obj.STORE_TEL2 = _item.STORE_TEL2;
                            obj.UPDATE_DATE = item.UPDATE_DATE;
                            obj.UPDATE_EMPLOYEE_ID = item.UPDATE_EMPLOYEE_ID;
                        }
                    }
                }
                ctx.SaveChanges();
            }
        }
    }
}
