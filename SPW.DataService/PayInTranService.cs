using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SPW.DAL;
using SPW.Model;
using SPW.Common;

namespace SPW.DataService
{
    public class PayInTranService : ServiceBase, IDataService<PAYIN_TRANS>, IService
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

        #region IDataService<PAYIN_TRANS> Members

        public void Add(PAYIN_TRANS obj)
        {
            try
            {
                this.Datacontext.PAYIN_TRANS.Add(obj);
                this.Datacontext.SaveChanges();
            }
            catch (Exception ex)
            {
                DebugLog.WriteLog(ex.ToString());
            }
        }

        public void AddList(List<PAYIN_TRANS> obj)
        {
            try
            {
                foreach (PAYIN_TRANS tmp in obj)
                {
                    this.Datacontext.PAYIN_TRANS.Add(tmp);
                }
                this.Datacontext.SaveChanges();
            }
            catch (Exception ex)
            {
                DebugLog.WriteLog(ex.ToString());
            }
        }

        public void Edit(PAYIN_TRANS obj)
        {
            throw new NotImplementedException();
        }

        public void EditList(List<PAYIN_TRANS> obj)
        {
            throw new NotImplementedException();
        }

        public PAYIN_TRANS Select()
        {
            throw new NotImplementedException();
        }

        public PAYIN_TRANS Select(string ID)
        {
            throw new NotImplementedException();
        }

        public List<PAYIN_TRANS> GetAll()
        {
            throw new NotImplementedException();
        }

        public void Delete(string ID)
        {
            throw new NotImplementedException();
        }
        #endregion

    }
}
