using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SPW.DAL;
using SPW.Model;

namespace SPW.DataService
{
    public class DepartmentService
    {
        private DEPARTMENT _item = new DEPARTMENT();
        private List<DEPARTMENT> _lstItem = new List<DEPARTMENT>();

        public DepartmentService()
        {

        }

        public DepartmentService(DEPARTMENT item)
        {
            _item = item;
        }

        public DepartmentService(List<DEPARTMENT> lstItem)
        {
            _lstItem = lstItem;
        }

        public List<DEPARTMENT> GetALL()
        {
            using (var ctx = new SPWEntities())
            {
                var list = ctx.DEPARTMENT.Where(x => x.SYE_DEL == true).ToList();
                return list;
            }
        }

        public List<DEPARTMENT> GetALL(DEPARTMENT item)
        {
            using (var ctx = new SPWEntities())
            {
                var list = ctx.DEPARTMENT.Where(x => x.DEPARTMENT_CODE.Contains(item.DEPARTMENT_CODE)).ToList();
                return list;
            }
        }

        public DEPARTMENT Select(int ID)
        {
            using (var ctx = new SPWEntities())
            {
                var list = ctx.DEPARTMENT.Where(x => x.DEPARTMENT_ID == ID).FirstOrDefault();
                return list;
            }
        }

        public void Add()
        {
            using (var ctx = new SPWEntities())
            {
                ctx.DEPARTMENT.Add(_item);
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
                        ctx.DEPARTMENT.Add(item);
                    }
                }
                ctx.SaveChanges();
            }
        }

        public void Edit()
        {
            using (var ctx = new SPWEntities())
            {
                var obj = ctx.DEPARTMENT.Where(x => x.DEPARTMENT_ID == _item.DEPARTMENT_ID).FirstOrDefault();
                obj.DEPARTMENT_CODE = _item.DEPARTMENT_CODE;
                obj.DEPARTMENT_NAME = _item.DEPARTMENT_NAME;
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
                        var obj = ctx.DEPARTMENT.Where(x => x.DEPARTMENT_ID == item.DEPARTMENT_ID).FirstOrDefault();
                        if (obj != null)
                        {
                            obj.DEPARTMENT_CODE = item.DEPARTMENT_CODE;
                            obj.DEPARTMENT_NAME = item.DEPARTMENT_NAME;
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
