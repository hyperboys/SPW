using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SPW.DAL;
using SPW.Model;

namespace SPW.DataService
{
    public class EmployeeService
    {
        private EMPLOYEE _item = new EMPLOYEE();
        private List<EMPLOYEE> _lstItem = new List<EMPLOYEE>();

        public EmployeeService()
        {
        }

        public EmployeeService(EMPLOYEE item)
        {
            _item = item;
        }

        public EmployeeService(List<EMPLOYEE> lstItem)
        {
            _lstItem = lstItem;
        }

        public EMPLOYEE Select(int ID)
        {
            using (var ctx = new SPWEntities())
            {
                var list = ctx.EMPLOYEE.Where(x => x.EMPLOYEE_ID == ID).FirstOrDefault();
                return list;
            }
        }

        public List<EMPLOYEE> GetALL()
        {
            using (var ctx = new SPWEntities())
            {
                var list = ctx.EMPLOYEE.Where(x => x.SYE_DEL == true).ToList();
                return list;
            }
        }

        public List<EMPLOYEE> GetALLInclude()
        {
            using (var ctx = new SPWEntities())
            {
                var list = ctx.EMPLOYEE.Include("DEPARTMENT").Where(x => x.SYE_DEL == true).ToList();
                return list;
            }
        }


        public List<EMPLOYEE> GetALL(EMPLOYEE item)
        {
            using (var ctx = new SPWEntities())
            {
                var list = ctx.EMPLOYEE.Where(x => x.EMPLOYEE_CODE.Contains(item.EMPLOYEE_CODE) && x.SYE_DEL == true).ToList();
                return list;
            }
        }

        public void Add()
        {
            using (var ctx = new SPWEntities())
            {
                ctx.EMPLOYEE.Add(_item);
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
                        ctx.EMPLOYEE.Add(item);
                    }
                }
                ctx.SaveChanges();
            }
        }

        public void Edit()
        {
            using (var ctx = new SPWEntities())
            {
                var obj = ctx.EMPLOYEE.Where(x => x.EMPLOYEE_ID == _item.EMPLOYEE_ID).FirstOrDefault();
                obj.EMPLOYEE_NAME = _item.EMPLOYEE_NAME;
                obj.EMPLOYEE_SURNAME = _item.EMPLOYEE_SURNAME;
                obj.EMPLOYEE_CODE = _item.EMPLOYEE_CODE;
                obj.DEPARTMENT_ID = _item.DEPARTMENT_ID;
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
                        var obj = ctx.EMPLOYEE.Where(x => x.EMPLOYEE_ID == item.EMPLOYEE_ID).FirstOrDefault();
                        if (obj != null)
                        {
                            obj.EMPLOYEE_NAME = item.EMPLOYEE_NAME;
                            obj.EMPLOYEE_SURNAME = item.EMPLOYEE_SURNAME;
                            obj.EMPLOYEE_CODE = item.EMPLOYEE_CODE;
                            obj.DEPARTMENT_ID = item.DEPARTMENT_ID;
                            obj.UPDATE_DATE = item.UPDATE_DATE;
                            obj.UPDATE_EMPLOYEE_ID = item.UPDATE_EMPLOYEE_ID;
                        }
                    }
                }
                ctx.SaveChanges();
            }
        }

        public void Delete()
        {
            using (var ctx = new SPWEntities())
            {
                foreach (var item in _lstItem)
                {
                    if (item.Action == ActionEnum.Delete)
                    {
                        var obj = ctx.EMPLOYEE.Where(x => x.EMPLOYEE_ID == item.EMPLOYEE_ID).FirstOrDefault();
                        if (obj != null)
                        {
                            ctx.EMPLOYEE.Remove(obj);
                        }
                    }
                }
                ctx.SaveChanges();
            }
        }
    }
}
