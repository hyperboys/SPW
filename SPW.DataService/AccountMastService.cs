using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SPW.DAL;
using SPW.Model;

namespace SPW.DataService
{
    public class AccountMastService : ServiceBase, IDataService<ACCOUNT_MAST>, IService 
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

        #region IDataService<ACCOUNT_MAST> Members

        public void Add(ACCOUNT_MAST obj)
        {
            try
            {
                this.Datacontext.ACCOUNT_MAST.Add(obj);
                this.Datacontext.SaveChanges();
            }
            catch (Exception ex) 
            {
            
            }
        }

        public void AddList(List<ACCOUNT_MAST> obj)
        {
            throw new NotImplementedException();
        }

        public void Edit(ACCOUNT_MAST obj)
        {
            try
            {
                var item = this.Datacontext.ACCOUNT_MAST.Where(x => x.ACCOUNT_ID == obj.ACCOUNT_ID).FirstOrDefault();
                item.ACCOUNT_NAME = obj.ACCOUNT_NAME;
                item.BANK_NAME = obj.BANK_NAME;
                item.BANK_SH_NAME = obj.BANK_SH_NAME;
                item.PAYIN_FORMAT = obj.PAYIN_FORMAT;
                item.UPDATE_DATE = obj.UPDATE_DATE;
                item.UPDATE_EMPLOYEE_ID = obj.UPDATE_EMPLOYEE_ID;
                this.Datacontext.SaveChanges();
            }
            catch (Exception ex) 
            {
            
            }
        }

        public void EditList(List<ACCOUNT_MAST> obj)
        {
            throw new NotImplementedException();
        }

        public ACCOUNT_MAST Select()
        {
            throw new NotImplementedException();
        }

        public ACCOUNT_MAST Select(string ID)
        {
            return this.Datacontext.ACCOUNT_MAST.Where(x => x.ACCOUNT_ID == ID).FirstOrDefault();
        }

        public List<ACCOUNT_MAST> GetAll()
        {
            return this.Datacontext.ACCOUNT_MAST.Where(x => x.SYE_DEL == false).ToList();
        }

        public void Delete(string ID)
        {
            var obj = this.Datacontext.ACCOUNT_MAST.Where(x => x.ACCOUNT_ID == ID).FirstOrDefault();
            //obj.SYE_DEL = true;
            this.Datacontext.ACCOUNT_MAST.Remove(obj);
            this.Datacontext.SaveChanges();
        }
        #endregion
    
    }
}
