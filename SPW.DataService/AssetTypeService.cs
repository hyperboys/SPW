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
    public class AssetTypeService : ServiceBase, IDataService<ASSET_TYPE>, IService
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

        #region IDataService<ASSET_TYPE> Members

        public void Add(ASSET_TYPE obj)
        {
            try
            {
                this.Datacontext.ASSET_TYPE.Add(obj);
                this.Datacontext.SaveChanges();
            }
            catch (Exception ex)
            {
                DebugLog.WriteLog(ex.ToString());
            }
        }

        public void AddList(List<ASSET_TYPE> obj)
        {
            throw new NotImplementedException();
        }

        public void Edit(ASSET_TYPE obj)
        {
            try
            {
                var item = this.Datacontext.ASSET_TYPE.Where(x => x.ASSET_TYPE_ID == obj.ASSET_TYPE_ID).FirstOrDefault();
                item.ASSET_TYPE_NAME = obj.ASSET_TYPE_NAME;
                item.UPDATE_DATE = obj.UPDATE_DATE;
                item.UPDATE_EMPLOYEE_ID = obj.UPDATE_EMPLOYEE_ID;
                this.Datacontext.SaveChanges();
            }
            catch (Exception ex)
            {
                DebugLog.WriteLog(ex.ToString());
            }
        }

        public void EditList(List<ASSET_TYPE> obj)
        {
            throw new NotImplementedException();
        }

        public ASSET_TYPE Select()
        {
            throw new NotImplementedException();
        }

        public ASSET_TYPE Select(int ID)
        {
            try
            {
                return this.Datacontext.ASSET_TYPE.Where(x => x.ASSET_TYPE_ID == ID).FirstOrDefault();
            }
            catch (Exception ex)
            {
                DebugLog.WriteLog(ex.ToString());
                return null;
            }
        }

        public ASSET_TYPE Select(string username)
        {
            try
            {
                return this.Datacontext.ASSET_TYPE.Where(x => x.ASSET_TYPE_NAME.ToUpper() == username.ToUpper()).FirstOrDefault();
            }
            catch (Exception ex)
            {
                DebugLog.WriteLog(ex.ToString());
                return null;
            }
        }

        public ASSET_TYPE SelectIncludeEmployee(int ID)
        {
            return this.Datacontext.ASSET_TYPE.Include("EMPLOYEE").Where(x => x.ASSET_TYPE_ID == ID).FirstOrDefault();
        }

        public ASSET_TYPE SelectIncludeUserRole(int ID)
        {
            return this.Datacontext.ASSET_TYPE.Include("ROLE").Where(x => x.ASSET_TYPE_ID == ID).FirstOrDefault();
        }

        public ASSET_TYPE SelectInclude(string username, string password)
        {
            return this.Datacontext.ASSET_TYPE.Include("EMPLOYEE").Include("ROLE").Where(x => x.ASSET_TYPE_NAME == username).FirstOrDefault();
        }

        public List<ASSET_TYPE> GetAll()
        {
            return this.Datacontext.ASSET_TYPE.Where(x => x.SYE_DEL == false).ToList();
        }

        public List<ASSET_TYPE> GetAllInclude()
        {
            return this.Datacontext.ASSET_TYPE.Include("EMPLOYEE").Where(x => x.SYE_DEL == false).ToList();
        }

        public List<ASSET_TYPE> GetAll(string name)
        {
            return this.Datacontext.ASSET_TYPE.Where(x => x.ASSET_TYPE_NAME.Contains(name)).ToList();
        }

        #endregion

    }
}
