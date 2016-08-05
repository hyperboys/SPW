using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SPW.DAL;
using SPW.Model;

namespace SPW.DataService
{
    public class EmpSkillService : ServiceBase, IDataService<EMP_SKILL>, IService
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

        #region IDataService<EMP_SKILL> Members

        public void Add(EMP_SKILL obj)
        {
            this.Datacontext.EMP_SKILL.Add(obj);
            this.Datacontext.SaveChanges();
        }

        public void AddList(List<EMP_SKILL> obj)
        {
            throw new NotImplementedException();
        }

        public void Edit(EMP_SKILL obj)
        {
            var item = this.Datacontext.EMP_SKILL.Where(x => x.SKILL_ID == obj.SKILL_ID).FirstOrDefault();
            item.SKILL_NAME = obj.SKILL_NAME;
            item.UPDATE_DATE = obj.UPDATE_DATE;
            item.UPDATE_EMPLOYEE_ID = obj.UPDATE_EMPLOYEE_ID;
            this.Datacontext.SaveChanges();
        }

        public void EditList(List<EMP_SKILL> obj)
        {
            throw new NotImplementedException();
        }

        public EMP_SKILL Select()
        {
            throw new NotImplementedException();
        }

        public EMP_SKILL Select(int ID)
        {
            return this.Datacontext.EMP_SKILL.Where(x => x.SKILL_ID == ID).FirstOrDefault();
        }

        public List<EMP_SKILL> GetAll()
        {
            return this.Datacontext.EMP_SKILL.Where(x => x.SYE_DEL == false).ToList();
        }

        public void Delete(int ID)
        {
            var obj = this.Datacontext.EMP_SKILL.Where(x => x.SKILL_ID == ID).FirstOrDefault();
            this.Datacontext.EMP_SKILL.Remove(obj);
            this.Datacontext.SaveChanges();
        }
        #endregion

    }
}
