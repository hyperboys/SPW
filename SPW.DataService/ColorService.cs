using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SPW.DAL;
using SPW.Model;

namespace SPW.DataService
{
    public class ColorService
    {
        private COLOR _item = new COLOR();
        private List<COLOR> _lstItem = new List<COLOR>();

        public ColorService()
        {

        }

        public ColorService(COLOR item)
        {
            _item = item;
        }

        public ColorService(List<COLOR> lstItem)
        {
            _lstItem = lstItem;
        }

        public List<COLOR> GetALL()
        {
            using (var ctx = new SPWEntities())
            {
                var list = ctx.COLOR.Where(x => x.SYE_DEL == true).ToList();
                return list;
            }
        }

        //public List<COLOR> GetALLInclude()
        //{
        //    using (var ctx = new SPWEntities())
        //    {
        //        var list = ctx.COLOR.Include("COLOR_TYPE").Where(x => x.SYE_DEL == true).ToList();
        //        return list;
        //    }
        //}

        public List<COLOR> GetALL(string subname, string name)
        {
            using (var ctx = new SPWEntities())
            {
                var list = ctx.COLOR.Where(x => x.COLOR_SUBNAME.Contains(subname)
                    && x.COLOR_NAME.Contains(name)).ToList();
                return list;
            }
        }

        public COLOR Select(int ID)
        {
            using (var ctx = new SPWEntities())
            {
                var list = ctx.COLOR.Where(x => x.COLOR_ID == ID).FirstOrDefault();
                return list;
            }
        }

        public COLOR SelectInclude(int ID)
        {
            using (var ctx = new SPWEntities())
            {
                var list = ctx.COLOR.Include("COLOR_TYPE").Where(x => x.COLOR_ID == ID).FirstOrDefault();
                return list;
            }
        }

        public void Add()
        {
            using (var ctx = new SPWEntities())
            {
                ctx.COLOR.Add(_item);
                ctx.SaveChanges();
            }
        }

        public void Edit()
        {
            using (var ctx = new SPWEntities())
            {
                var obj = ctx.COLOR.Where(x => x.COLOR_ID == _item.COLOR_ID).FirstOrDefault();
                obj.COLOR_NAME = _item.COLOR_NAME;
                obj.COLOR_SUBNAME = _item.COLOR_SUBNAME;
                obj.UPDATE_DATE = _item.UPDATE_DATE;
                obj.UPDATE_EMPLOYEE_ID = _item.UPDATE_EMPLOYEE_ID;
                ctx.SaveChanges();
            }
        }
    }
}
