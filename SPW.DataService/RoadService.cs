using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SPW.DAL;
using SPW.Model;

namespace SPW.DataService
{
    public class RoadService
    {
        private ROAD _item = new ROAD();
        private List<ROAD> _lstItem = new List<ROAD>();

        public RoadService()
        {

        }

        public List<ROAD> GetALL()
        {
            using (var ctx = new SPWEntities())
            {
                var list = ctx.ROAD.ToList();
                return list;
            }
        }

        public ROAD Select(int ID)
        {
            using (var ctx = new SPWEntities())
            {
                var list = ctx.ROAD.Where(x => x.ROAD_ID == ID).FirstOrDefault();
                return list;
            }
        }
    }
}
