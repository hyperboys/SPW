using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SPW.DAL;
using SPW.Model;

namespace SPW.DataService
{
    public class EmpDtTemplateService : ServiceBase, IDataService<EMP_MEASURE_DT_TEMPLATE>, IService
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

        #region IDataService<EMP_MEASURE_DT_TEMPLATE> Members

        public void Add(EMP_MEASURE_DT_TEMPLATE obj)
        {
            this.Datacontext.EMP_MEASURE_DT_TEMPLATE.Add(obj);
            this.Datacontext.SaveChanges();
        }

        public void AddList(List<EMP_MEASURE_DT_TEMPLATE> obj)
        {
            throw new NotImplementedException();
        }

        public void Edit(EMP_MEASURE_DT_TEMPLATE obj)
        {
            var item = this.Datacontext.EMP_MEASURE_DT_TEMPLATE.Where(x => x.TEMPLATE_ID == obj.TEMPLATE_ID).FirstOrDefault();
            item.SKILL_TARGET_SCORE = obj.SKILL_TARGET_SCORE;
            item.UPDATE_DATE = obj.UPDATE_DATE;
            item.UPDATE_EMPLOYEE_ID = obj.UPDATE_EMPLOYEE_ID;
            this.Datacontext.SaveChanges();
        }

        public void EditList(List<EMP_MEASURE_DT_TEMPLATE> obj)
        {
            throw new NotImplementedException();
        }

        public EMP_MEASURE_DT_TEMPLATE Select()
        {
            throw new NotImplementedException();
        }

        public EMP_MEASURE_DT_TEMPLATE Select(string ID)
        {
            return this.Datacontext.EMP_MEASURE_DT_TEMPLATE.Where(x => x.TEMPLATE_ID == ID).FirstOrDefault();
        }

        public List<EMP_MEASURE_DT_TEMPLATE> GetAll()
        {
            return this.Datacontext.EMP_MEASURE_DT_TEMPLATE.Where(x => x.SYE_DEL == false).ToList();
        }

        public void Delete(string ID)
        {
            var obj = this.Datacontext.EMP_MEASURE_DT_TEMPLATE.Where(x => x.TEMPLATE_ID == ID).FirstOrDefault();
            this.Datacontext.EMP_MEASURE_DT_TEMPLATE.Remove(obj);
            this.Datacontext.SaveChanges();
        }
        #endregion
    }
}
