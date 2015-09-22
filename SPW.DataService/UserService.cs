using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SPW.DAL;
using SPW.Model;

namespace SPW.DataService
{
    public class UserService
    {
        private USER _item = new USER();
        private List<USER> _lstItem = new List<USER>();

        public UserService()
        {

        }

        public UserService(USER item)
        {
            _item = item;
        }

        public UserService(List<USER> lstItem)
        {
            _lstItem = lstItem;
        }

        public List<USER> GetALL()
        {
            using (var ctx = new SPWEntities())
            {
                var list = ctx.USER.Where(x => x.SYE_DEL == true).ToList();
                return list;
            }
        }

        public List<USER> GetALLInclude()
        {
            using (var ctx = new SPWEntities())
            {
                var list = ctx.USER.Include("EMPLOYEE").Where(x => x.SYE_DEL == true).ToList();
                return list;
            }
        }

        public List<USER> GetALL(string name)
        {
            using (var ctx = new SPWEntities())
            {
                var list = ctx.USER.Where(x => x.USER_NAME.Contains(name)).ToList();
                return list;
            }
        }

        public USER Select(int ID)
        {
            using (var ctx = new SPWEntities())
            {
                var list = ctx.USER.Where(x => x.USER_ID == ID).FirstOrDefault();
                return list;
            }
        }

        public USER SelectIncludeEmployee(int ID)
        {
            using (var ctx = new SPWEntities())
            {
                var list = ctx.USER.Include("EMPLOYEE").Where(x => x.USER_ID == ID).FirstOrDefault();
                return list;
            }
        }

        public USER SelectIncludeUserRole(int ID)
        {
            using (var ctx = new SPWEntities())
            {
                var list = ctx.USER.Include("ROLE").Where(x => x.USER_ID == ID).FirstOrDefault();
                return list;
            }
        }

        public USER SelectInclude(string username, string password)
        {
            using (var ctx = new SPWEntities())
            {
                var list = ctx.USER.Include("EMPLOYEE").Include("ROLE").Where(x => x.USER_NAME == username && x.PASSWORD == password).FirstOrDefault();
                return list;
            }
        }

        public void Add()
        {
            using (var ctx = new SPWEntities())
            {
                ctx.USER.Add(_item);
                ctx.SaveChanges();
            }
        }

        public void Edit()
        {
            using (var ctx = new SPWEntities())
            {
                var obj = ctx.USER.Where(x => x.USER_ID == _item.USER_ID).FirstOrDefault();
                obj.USER_NAME = _item.USER_NAME;
                obj.PASSWORD = _item.PASSWORD;
                obj.EMPLOYEE_ID = _item.EMPLOYEE_ID;
                obj.UPDATE_DATE = _item.UPDATE_DATE;
                obj.UPDATE_EMPLOYEE_ID = _item.UPDATE_EMPLOYEE_ID;
                ctx.SaveChanges();
            }
        }
    }
}
