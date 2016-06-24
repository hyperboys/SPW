﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SPW.DAL;
using SPW.Model;
using SPW.Common;

namespace SPW.DataService
{
    public class RoleFunctionService : ServiceBase, IDataService<ROLE_FUNCTION>, IService
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

        #region IDataService<ROLE_FUNCTION> Members

        public void Add(ROLE_FUNCTION obj)
        {
            this.Datacontext.ROLE_FUNCTION.Add(obj);
            this.Datacontext.SaveChanges();
        }

        public void AddList(List<ROLE_FUNCTION> obj)
        {
            foreach (var item in obj)
            {
                if (item.Action == ActionEnum.Create)
                {
                    this.Datacontext.ROLE_FUNCTION.Add(item);
                }
            }
            this.Datacontext.SaveChanges();
        }

        public void Edit(ROLE_FUNCTION obj)
        {
            throw new NotImplementedException();
        }

        public void EditList(List<ROLE_FUNCTION> obj)
        {
            throw new NotImplementedException();
        }

        public ROLE_FUNCTION Select()
        {
            throw new NotImplementedException();
        }

        public ROLE_FUNCTION Select(int ID)
        {
            //return this.Datacontext.ROLE_FUNCTION.Where(x => x.ROLE_FUNCTION_ID == ID).FirstOrDefault();
            return null;
        }

        public List<ROLE_FUNCTION> GetAll()
        {
            return this.Datacontext.ROLE_FUNCTION.Where(x => x.SYE_DEL == false).ToList();
        }

        public List<ROLE_FUNCTION> GetAllIncludeFunction(int roleId)
        {
            return this.Datacontext.ROLE_FUNCTION.Include("FUNCTION").Where(x => x.ROLE_ID == roleId && x.SYE_DEL == false).ToList();
        }

        public List<ROLE_FUNCTION> SelectByRole(int ID, int functionID)
        {
            return this.Datacontext.ROLE_FUNCTION.Include("SUB_FUNCTION").Where(x => x.ROLE_ID == ID && x.FUNCTION_ID == functionID && x.SYE_DEL == false).ToList();
        }

        public void RemoveAll(int RoleID)
        {
            try
            {
                List<ROLE_FUNCTION> lstRoleFunc = this.Datacontext.ROLE_FUNCTION.Where(x => x.ROLE_ID == RoleID).ToList();
                foreach (ROLE_FUNCTION rf in lstRoleFunc)
                {
                    this.Datacontext.ROLE_FUNCTION.Remove(rf);
                }
                this.Datacontext.SaveChanges();
            }
            catch (Exception ex)
            {
                DebugLog.WriteLog(ex.ToString());
                throw ex;
            }
        }

        #endregion
    }
}
