using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SPW.DAL;
using SPW.Model;

namespace SPW.DataService
{
    public class EmpHdTemplateService : ServiceBase, IDataService<EMP_MEASURE_HD_TEMPLATE>, IService
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

        #region IDataService<EMP_MEASURE_HD_TEMPLATE> Members

        public void Add(EMP_MEASURE_HD_TEMPLATE obj)
        {
            this.Datacontext.EMP_MEASURE_HD_TEMPLATE.Add(obj);
            this.Datacontext.SaveChanges();
        }

        public void AddList(List<EMP_MEASURE_HD_TEMPLATE> obj)
        {
            throw new NotImplementedException();
        }

        public void Edit(EMP_MEASURE_HD_TEMPLATE obj)
        {
            var item = this.Datacontext.EMP_MEASURE_HD_TEMPLATE.Where(x => x.TEMPLATE_ID == obj.TEMPLATE_ID).FirstOrDefault();
            item.EMP_SKILL_TYPE_PERCENTAGE = obj.EMP_SKILL_TYPE_PERCENTAGE;
            item.UPDATE_DATE = obj.UPDATE_DATE;
            item.UPDATE_EMPLOYEE_ID = obj.UPDATE_EMPLOYEE_ID;
            this.Datacontext.SaveChanges();
        }

        public void EditList(List<EMP_MEASURE_HD_TEMPLATE> obj)
        {
            throw new NotImplementedException();
        }

        public EMP_MEASURE_HD_TEMPLATE Select()
        {
            throw new NotImplementedException();
        }

        public EMP_MEASURE_HD_TEMPLATE Select(string ID)
        {
            return this.Datacontext.EMP_MEASURE_HD_TEMPLATE.Include("DEPARTMENT").Include("EMP_SKILL_TYPE").Where(x => x.TEMPLATE_ID == ID && x.SYE_DEL == false).FirstOrDefault();
        }

        public int GetCount(string tmpId)
        {
            return this.Datacontext.EMP_MEASURE_HD_TEMPLATE.Where(x => x.TEMPLATE_ID.Contains(tmpId) && x.SYE_DEL == false).Count();
        }

        public List<EMP_MEASURE_HD_TEMPLATE> GetAll()
        {
            return this.Datacontext.EMP_MEASURE_HD_TEMPLATE.Include("DEPARTMENT").Include("EMP_SKILL_TYPE").Where(x => x.SYE_DEL == false).ToList();
        }

        public void Delete(string ID)
        {
            var obj = this.Datacontext.EMP_MEASURE_HD_TEMPLATE.Where(x => x.TEMPLATE_ID == ID).FirstOrDefault();
            this.Datacontext.EMP_MEASURE_HD_TEMPLATE.Remove(obj);
            this.Datacontext.SaveChanges();
        }
        #endregion
    }
}
