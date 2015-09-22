using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SPW.DAL;
using SPW.Model;

namespace SPW.DataService
{
    public class RoleService
    {
        private ROLE _item = new ROLE();
        private List<ROLE> _lstItem = new List<ROLE>();

        public RoleService()
        {

        }

        public RoleService(ROLE item)
        {
            _item = item;
        }

        public RoleService(List<ROLE> lstItem)
        {
            _lstItem = lstItem;
        }

        public List<ROLE> GetALL()
        {
            using (var ctx = new SPWEntities())
            {
                var list = ctx.ROLE.Where(x => x.SYE_DEL == true).ToList();
                return list;
            }
        }

        //public List<ROLE> GetALLInclude()
        //{
        //    using (var ctx = new SPWEntities())
        //    {
        //        var list = ctx.ROLE.Include("EMPLOYEE").Where(x => x.SYE_DEL == true).ToList();
        //        return list;
        //    }
        //}

        public List<ROLE> GetALL(string name)
        {
            using (var ctx = new SPWEntities())
            {
                var list = ctx.ROLE.Where(x => x.ROLE_NAME.Contains(name)).ToList();
                return list;
            }
        }

        public ROLE Select(int ID)
        {
            using (var ctx = new SPWEntities())
            {
                var list = ctx.ROLE.Where(x => x.ROLE_ID == ID).FirstOrDefault();
                return list;
            }
        }

        public ROLE SelectIncludeEmployee(int ID)
        {
            using (var ctx = new SPWEntities())
            {
                var list = ctx.ROLE.Include("ROLE_FUNCTION").Where(x => x.ROLE_ID == ID).FirstOrDefault();
                return list;
            }
        }

        //public ROLE SelectIncludeUserRole(int ID)
        //{
        //    using (var ctx = new SPWEntities())
        //    {
        //        var list = ctx.ROLE.Include("USER_ROLE").Where(x => x.USER_ID == ID).FirstOrDefault();
        //        return list;
        //    }
        //}

        public void Add()
        {
            using (var ctx = new SPWEntities())
            {
                ctx.ROLE.Add(_item);
                ctx.SaveChanges();
            }
        }

        public void Edit()
        {
            using (var ctx = new SPWEntities())
            {
                var obj = ctx.ROLE.Where(x => x.ROLE_ID == _item.ROLE_ID).FirstOrDefault();
                obj.ROLE_CODE = _item.ROLE_CODE;
                obj.ROLE_NAME = _item.ROLE_NAME;
                obj.UPDATE_DATE = _item.UPDATE_DATE;
                obj.UPDATE_EMPLOYEE_ID = _item.UPDATE_EMPLOYEE_ID;
                ctx.SaveChanges();
            }
        }
    }
}
