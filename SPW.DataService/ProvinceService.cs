using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SPW.DAL;
using SPW.Model;

namespace SPW.DataService
{
    public class ProvinceService
    {
        private PROVINCE _item = new PROVINCE();
        private List<PROVINCE> _lstItem = new List<PROVINCE>();

        public ProvinceService()
        {

        }

        public List<PROVINCE> GetALL()
        {
            using (var ctx = new SPWEntities())
            {
                var list = ctx.PROVINCE.Where(x => x.SYE_DEL == true).ToList();
                return list;
            }
        }

        public PROVINCE Select(int ID)
        {
            using (var ctx = new SPWEntities())
            {
                var list = ctx.PROVINCE.Where(x => x.PROVINCE_ID == ID).FirstOrDefault();
                return list;
            }
        }
    }
}
