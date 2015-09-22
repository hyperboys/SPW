using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SPW.DAL;
using SPW.Model;

namespace SPW.DataService
{
    public class TraceryService
    {
        private COLOR_TYPE _item = new COLOR_TYPE();
        private List<COLOR_TYPE> _lstItem = new List<COLOR_TYPE>();

        public TraceryService()
        {

        }

        public TraceryService(COLOR_TYPE item)
        {
            _item = item;
        }

        public TraceryService(List<COLOR_TYPE> lstItem)
        {
            _lstItem = lstItem;
        }

        public List<COLOR_TYPE> GetALL()
        {
            using (var ctx = new SPWEntities())
            {
                var list = ctx.COLOR_TYPE.Where(x => x.SYE_DEL == true).ToList();
                return list;
            }
        }

        public List<COLOR_TYPE> GetALL(string subname,string name)
        {
            using (var ctx = new SPWEntities())
            {
                var list = ctx.COLOR_TYPE.Where(x => x.COLOR_TYPE_SUBNAME.Contains(subname)
                    && x.COLOR_TYPE_NAME.Contains(name)).ToList();
                return list;
            }
        }

        public COLOR_TYPE Select(int ID)
        {
            using (var ctx = new SPWEntities())
            {
                var list = ctx.COLOR_TYPE.Where(x => x.COLOR_TYPE_ID == ID).FirstOrDefault();
                return list;
            }
        }

        public void Add()
        {
            using (var ctx = new SPWEntities())
            {
                ctx.COLOR_TYPE.Add(_item);
                ctx.SaveChanges();
            }
        }

        public void Edit()
        {
            using (var ctx = new SPWEntities())
            {
                var obj = ctx.COLOR_TYPE.Where(x => x.COLOR_TYPE_ID == _item.COLOR_TYPE_ID).FirstOrDefault();
                obj.COLOR_TYPE_NAME = _item.COLOR_TYPE_NAME;
                obj.COLOR_TYPE_SUBNAME = _item.COLOR_TYPE_SUBNAME;
                obj.UPDATE_DATE = _item.UPDATE_DATE;
                obj.UPDATE_EMPLOYEE_ID = _item.UPDATE_EMPLOYEE_ID;
                ctx.SaveChanges();
            }
        }
    }
}
