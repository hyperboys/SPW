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
            var item = this.Datacontext.EMP_MEASURE_DT_TEMPLATE.Where(x => x.TEMPLATE_ID == obj.TEMPLATE_ID && x.SEQ_NO == obj.SEQ_NO).FirstOrDefault();
            item.SKILL_TARGET_SCORE = obj.SKILL_TARGET_SCORE;
            item.SKILL_ID = obj.SKILL_ID;
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

        public EMP_MEASURE_DT_TEMPLATE Select(string ID,int seq)
        {
            return this.Datacontext.EMP_MEASURE_DT_TEMPLATE.Where(x => x.TEMPLATE_ID == ID && x.SEQ_NO == seq).FirstOrDefault();
        }


        public List<EMP_MEASURE_DT_TEMPLATE> GetAll()
        {
            return this.Datacontext.EMP_MEASURE_DT_TEMPLATE.Where(x => x.SYE_DEL == false).ToList();
        }

        public List<EMP_MEASURE_DT_TEMPLATE> GetList(string ID)
        {
            return this.Datacontext.EMP_MEASURE_DT_TEMPLATE.Where(x => x.SYE_DEL == false && x.TEMPLATE_ID == ID).ToList();
        }

        public List<EMP_MEASURE_DT_TEMPLATE> GetList(string ID,int typeId)
        {
            return this.Datacontext.EMP_MEASURE_DT_TEMPLATE.Where(x => x.SYE_DEL == false && x.TEMPLATE_ID == ID && x.EMP_SKILL_TYPE_ID == typeId).ToList();
        }

        public int GetSeqNo(string TEM_ID)
        {
            try
            {
                return this.Datacontext.EMP_MEASURE_DT_TEMPLATE.Where(x => x.SYE_DEL == false && x.TEMPLATE_ID == TEM_ID).ToList().Count();
            }
            catch (Exception ex)
            {
                DebugLog.WriteLog(ex.ToString());
                return 0;
            }
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
