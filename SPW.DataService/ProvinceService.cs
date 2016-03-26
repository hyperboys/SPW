﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SPW.DAL;
using SPW.Model;

namespace SPW.DataService
{
    public class ProvinceService : ServiceBase, IDataService<PROVINCE>, IService 
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

        #region IDataService<PROVINCE> Members

        public void Add(PROVINCE obj)
        {
            this.Datacontext.PROVINCE.Add(obj);
            this.Datacontext.SaveChanges();
        }

        public void AddList(List<PROVINCE> obj)
        {
            throw new NotImplementedException();
        }

        public void Edit(PROVINCE obj)
        {
            throw new NotImplementedException();
        }

        public void EditList(List<PROVINCE> obj)
        {
            throw new NotImplementedException();
        }

        public PROVINCE Select()
        {
            throw new NotImplementedException();
        }

        public PROVINCE Select(int ID)
        {
            return this.Datacontext.PROVINCE.Where(x => x.PROVINCE_ID == ID).FirstOrDefault();
        }

        public PROVINCE Select(string name)
        {
            return this.Datacontext.PROVINCE.Where(x => x.PROVINCE_NAME == name).FirstOrDefault();
        }

        public int GetID(string provinceName)
        {
            return this.Datacontext.PROVINCE.Where(x => x.PROVINCE_NAME == provinceName).FirstOrDefault().PROVINCE_ID;
        }


        public List<PROVINCE> GetAllLike(string provinceName) 
        {
            List<PROVINCE> list = this.Datacontext.PROVINCE.Where(x => x.SYE_DEL == false && x.PROVINCE_NAME.Contains(provinceName)).OrderBy(y => y.PROVINCE_NAME).ToList();
            return list.Count() > 5 ? list.Skip(list.Count() - 5).ToList() : list;
        }

        public List<PROVINCE> GetAll()
        {
            return this.Datacontext.PROVINCE.Where(x => x.SYE_DEL == false).OrderBy(y=>y.PROVINCE_NAME).ToList();
        }
        #endregion
    
    }
}
