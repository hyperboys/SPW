using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SPW.DAL;
using SPW.Model;

namespace SPW.DataService
{
    public class FunctionService
    {
        private FUNCTION _item = new FUNCTION();
        private List<FUNCTION> _lstItem = new List<FUNCTION>();

        public FunctionService()
        {

        }

        public FunctionService(FUNCTION item)
        {
            _item = item;
        }

        public FunctionService(List<FUNCTION> lstItem)
        {
            _lstItem = lstItem;
        }

        public List<FUNCTION> GetALL()
        {
            using (var ctx = new SPWEntities())
            {
                var list = ctx.FUNCTION.Where(x => x.SYE_DEL == true).ToList();
                return list;
            }
        }

        public List<FUNCTION> GetALLInclude()
        {
            using (var ctx = new SPWEntities())
            {
                var list = ctx.FUNCTION.Include("SUB_FUNCTION").Where(x => x.SYE_DEL == true).ToList();
                return list;
            }
        }

        public List<FUNCTION> GetALL(string name)
        {
            using (var ctx = new SPWEntities())
            {
                var list = ctx.FUNCTION.Where(x => x.FUNCTION_NAME.Contains(name)).ToList();
                return list;
            }
        }

        public FUNCTION Select(int ID)
        {
            using (var ctx = new SPWEntities())
            {
                var list = ctx.FUNCTION.Where(x => x.FUNCTION_ID == ID).FirstOrDefault();
                return list;
            }
        }

        public void Add()
        {
            using (var ctx = new SPWEntities())
            {
                ctx.FUNCTION.Add(_item);
                ctx.SaveChanges();
            }
        }

        public void Edit()
        {
            using (var ctx = new SPWEntities())
            {
                var obj = ctx.FUNCTION.Where(x => x.FUNCTION_ID == _item.FUNCTION_ID).FirstOrDefault();
                obj.FUNCTION_NAME = _item.FUNCTION_NAME;
                obj.UPDATE_DATE = _item.UPDATE_DATE;
                obj.UPDATE_EMPLOYEE_ID = _item.UPDATE_EMPLOYEE_ID;
                ctx.SaveChanges();
            }
        }
    }
}
