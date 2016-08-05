using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SPW.DAL;
using SPW.Model;

namespace SPW.DataService
{
    public class EmpSkillTypeService : ServiceBase, IDataService<EMP_SKILL_TYPE>, IService
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

        #region IDataService<EMP_SKILL_TYPE> Members

        public void Add(EMP_SKILL_TYPE obj)
        {
            this.Datacontext.EMP_SKILL_TYPE.Add(obj);
            this.Datacontext.SaveChanges();
        }

        public void AddList(List<EMP_SKILL_TYPE> obj)
        {
            throw new NotImplementedException();
        }

        public void Edit(EMP_SKILL_TYPE obj)
        {
            var item = this.Datacontext.EMP_SKILL_TYPE.Where(x => x.EMP_SKILL_TYPE_ID == obj.EMP_SKILL_TYPE_ID).FirstOrDefault();
            item.EMP_SKILL_TYPE_DEFAULT = obj.EMP_SKILL_TYPE_DEFAULT;
            item.EMP_SKILL_TYPE_NA = obj.EMP_SKILL_TYPE_NA;
            item.UPDATE_DATE = obj.UPDATE_DATE;
            item.UPDATE_EMPLOYEE_ID = obj.UPDATE_EMPLOYEE_ID;
            this.Datacontext.SaveChanges();
        }

        public void EditList(List<EMP_SKILL_TYPE> obj)
        {
            throw new NotImplementedException();
        }

        public EMP_SKILL_TYPE Select()
        {
            throw new NotImplementedException();
        }

        public EMP_SKILL_TYPE Select(int ID)
        {
            return this.Datacontext.EMP_SKILL_TYPE.Where(x => x.EMP_SKILL_TYPE_ID == ID).FirstOrDefault();
        }

        public List<EMP_SKILL_TYPE> GetAll()
        {
            return this.Datacontext.EMP_SKILL_TYPE.Where(x => x.SYE_DEL == false).ToList();
        }

        public void Delete(int ID)
        {
            var obj = this.Datacontext.EMP_SKILL_TYPE.Where(x => x.EMP_SKILL_TYPE_ID == ID).FirstOrDefault();
            this.Datacontext.EMP_SKILL_TYPE.Remove(obj);
            this.Datacontext.SaveChanges();
        }
        #endregion

    }
}
