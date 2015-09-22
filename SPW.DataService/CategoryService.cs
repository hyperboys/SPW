using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SPW.DAL;
using SPW.Model;

namespace SPW.DataService
{
    public class CategoryService
    {
        private CATEGORY _item = new CATEGORY();
        private List<CATEGORY> _lstItem = new List<CATEGORY>();

        public CategoryService()
        {

        }

        public CategoryService(CATEGORY item)
        {
            _item = item;
        }

        public CategoryService(List<CATEGORY> lstItem)
        {
            _lstItem = lstItem;
        }

        public List<CATEGORY> GetALL()
        {
            using (var ctx = new SPWEntities())
            {
                var list = ctx.CATEGORY.Where(x => x.SYE_DEL == true).ToList();
                return list;
            }
        }

        public List<CATEGORY> GetALL(CATEGORY item)
        {
            using (var ctx = new SPWEntities())
            {
                var list = ctx.CATEGORY.Where(x => x.CATEGORY_CODE.Contains(item.CATEGORY_CODE)).ToList();
                return list;
            }
        }

        public CATEGORY Select(int ID)
        {
            using (var ctx = new SPWEntities())
            {
                var list = ctx.CATEGORY.Where(x => x.CATEGORY_ID == ID).FirstOrDefault();
                return list;
            }
        }

        public void Add()
        {
            using (var ctx = new SPWEntities())
            {
                ctx.CATEGORY.Add(_item);
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
                        ctx.CATEGORY.Add(item);
                    }
                }
                ctx.SaveChanges();
            }
        }

        public void Edit()
        {
            using (var ctx = new SPWEntities())
            {
                var obj = ctx.CATEGORY.Where(x => x.CATEGORY_ID == _item.CATEGORY_ID).FirstOrDefault();
                obj.CATEGORY_CODE = _item.CATEGORY_CODE;
                obj.CATEGORY_NAME = _item.CATEGORY_NAME;
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
                        var obj = ctx.CATEGORY.Where(x => x.CATEGORY_ID == item.CATEGORY_ID).FirstOrDefault();
                        if (obj != null)
                        {
                            obj.CATEGORY_CODE = item.CATEGORY_CODE;
                            obj.CATEGORY_NAME = item.CATEGORY_NAME;
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
