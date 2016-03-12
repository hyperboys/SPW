using SPW.DAL;
using SPW.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPW.DataService
{
    public class TransportLineService : ServiceBase, IDataService<TRANSPORT_LINE>, IService
    {
        #region IService Members
        public DAL.SPWEntities Datacontext
        {
            get
            {
                return this._Datacontext;
            }
            set
            {
                this._Datacontext = value;
            }
        }
        #endregion

        public TransportLineService()
        {

        }

        public List<TRANSPORT_LINE> GetALL()
        {
            using (var ctx = new SPWEntities())
            {
                var list = ctx.TRANSPORT_LINE.Where(x => x.SYE_DEL == true).ToList();
                return list;
            }
        }

        public List<TRANSPORT_LINE> GetALLDistince()
        {
            using (var ctx = new SPWEntities())
            {
                var list = ctx.TRANSPORT_LINE.Where(x => x.SYE_DEL == true).Distinct().ToList();
                return list;
            }
        }

        public List<TRANSPORT_LINE> SelectAll(int ID)
        {
            using (var ctx = new SPWEntities())
            {
                var list = ctx.TRANSPORT_LINE.Include("STORE").Where(x => x.TRANS_LINE_ID == ID).ToList();
                return list;
            }
        }

        public TRANSPORT_LINE Select(int ID)
        {
            using (var ctx = new SPWEntities())
            {
                var list = ctx.TRANSPORT_LINE.Where(x => x.TRANS_LINE_ID == ID).FirstOrDefault();
                return list;
            }
        }

        public void Delete(int TRANS_LINE_ID, int STORE_ID)
        {
            using (var ctx = new SPWEntities())
            {
                TRANSPORT_LINE item = ctx.TRANSPORT_LINE.Where(x => x.TRANS_LINE_ID == TRANS_LINE_ID && x.STORE_ID == STORE_ID).FirstOrDefault();
                ctx.TRANSPORT_LINE.Remove(item);
                ctx.SaveChanges();
            }
        }

        public List<int> SelectListStoreID(int ID)
        {
            using (var ctx = new SPWEntities())
            {
                var list = ctx.TRANSPORT_LINE.Where(x => x.TRANS_LINE_ID == ID).Select(y => y.STORE_ID).ToList();
                return list;
            }
        }

        public void Add(TRANSPORT_LINE obj)
        {
            throw new NotImplementedException();
        }

        public void AddList(List<TRANSPORT_LINE> obj)
        {
            throw new NotImplementedException();
        }

        public void Edit(TRANSPORT_LINE obj)
        {
            throw new NotImplementedException();
        }

        public void EditList(List<TRANSPORT_LINE> obj)
        {
            throw new NotImplementedException();
        }

        public TRANSPORT_LINE Select()
        {
            throw new NotImplementedException();
        }

        public List<TRANSPORT_LINE> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}