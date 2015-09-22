using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SPW.DAL;
using SPW.Model;

namespace SPW.DataService
{
    public class RoleFunctionService
    {
        private ROLE_FUNCTION _item = new ROLE_FUNCTION();
        private List<ROLE_FUNCTION> _lstItem = new List<ROLE_FUNCTION>();

        public RoleFunctionService()
        {

        }

        public RoleFunctionService(ROLE_FUNCTION item)
        {
            _item = item;
        }

        public RoleFunctionService(List<ROLE_FUNCTION> lstItem)
        {
            _lstItem = lstItem;
        }

        public List<ROLE_FUNCTION> GetALL()
        {
            using (var ctx = new SPWEntities())
            {
                var list = ctx.ROLE_FUNCTION.Where(x => x.SYE_DEL == true).ToList();
                return list;
            }
        }

        public List<ROLE_FUNCTION> GetALLIncludeFunction(int roleId)
        {
            using (var ctx = new SPWEntities())
            {
                var list = ctx.ROLE_FUNCTION.Include("FUNCTION").Where(x => x.ROLE_ID == roleId && x.SYE_DEL == true).ToList();
                return list;
            }
        }

        public ROLE_FUNCTION Select(int ID)
        {
            using (var ctx = new SPWEntities())
            {
                var list = ctx.ROLE_FUNCTION.Where(x => x.ROLE_FUNCTION_ID == ID).FirstOrDefault();
                return list;
            }
        }

        public List<ROLE_FUNCTION> SelectByRole(int ID)
        {
            using (var ctx = new SPWEntities())
            {
                var list = ctx.ROLE_FUNCTION.Where(x => x.ROLE_ID == ID).ToList();
                return list;
            }
        }

        public void Delete(int ID)
        {
            using (var ctx = new SPWEntities())
            {
                var obj = ctx.ROLE_FUNCTION.Where(x => x.ROLE_FUNCTION_ID == ID).FirstOrDefault();
                ctx.ROLE_FUNCTION.Remove(obj);
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
                        ctx.ROLE_FUNCTION.Add(item);
                    }
                }
                ctx.SaveChanges();
            }
        }
    }
}
