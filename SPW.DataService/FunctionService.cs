﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SPW.DAL;
using SPW.Model;

namespace SPW.DataService
{
    public class FunctionService : ServiceBase, IDataService<FUNCTION>, IService 
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

        #region IDataService<EMPLOYEE> Members

        public void Add(FUNCTION obj)
        {
            this.Datacontext.FUNCTION.Add(obj);
            this.Datacontext.SaveChanges();
        }

        public void AddList(List<FUNCTION> obj)
        {
            throw new NotImplementedException();
        }

        public void Edit(FUNCTION obj)
        {
            var item = this.Datacontext.FUNCTION.Where(x => x.FUNCTION_ID == obj.FUNCTION_ID).FirstOrDefault();
            item.FUNCTION_NAME = obj.FUNCTION_NAME;
            item.UPDATE_DATE = obj.UPDATE_DATE;
            item.UPDATE_EMPLOYEE_ID = obj.UPDATE_EMPLOYEE_ID;
            this.Datacontext.SaveChanges();
        }

        public void EditList(List<FUNCTION> obj)
        {
            throw new NotImplementedException();
        }

        public FUNCTION Select()
        {
            throw new NotImplementedException();
        }

        public FUNCTION Select(int ID)
        {
            return this.Datacontext.FUNCTION.Where(x => x.FUNCTION_ID == ID).FirstOrDefault();
        }

        public List<FUNCTION> GetAll()
        {
            return this.Datacontext.FUNCTION.Where(x => x.SYE_DEL == false).ToList();
        }

        public List<FUNCTION> GetAllInclude()
        {
            return this.Datacontext.FUNCTION.Include("SUB_FUNCTION").Where(x => x.SYE_DEL == false).ToList();
        }

        public List<FUNCTION> GetAll(string name)
        {
            return this.Datacontext.FUNCTION.Where(x => x.FUNCTION_NAME.ToUpper().Contains(name.ToUpper())).ToList();
        }

        #endregion
    }
}
