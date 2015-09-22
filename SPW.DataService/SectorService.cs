using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SPW.DAL;
using SPW.Model;

namespace SPW.DataService
{
    public class SectorService
    {
        private SECTOR _item = new SECTOR();
        private List<SECTOR> _lstItem = new List<SECTOR>();

        public SectorService()
        {

        }

        public List<SECTOR> GetALL()
        {
            using (var ctx = new SPWEntities())
            {
                var list = ctx.SECTOR.Where(x => x.SYE_DEL == true).ToList();
                return list;
            }
        }

        public List<SECTOR> GetALLInclude()
        {
            using (var ctx = new SPWEntities())
            {
                var list = ctx.SECTOR.Include("PROVINCE").ToList();
                return list;
            }
        }

        public SECTOR Select(int ID)
        {
            using (var ctx = new SPWEntities())
            {
                var list = ctx.SECTOR.Where(x => x.SECTOR_ID == ID).FirstOrDefault();
                return list;
            }
        }
    }
}
