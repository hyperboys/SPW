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
    public class EmpMeasureWeightService : ServiceBase, IDataService<EMP_MEASURE_WEIGHT>, IService
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

        #region IDataService<EMP_MEASURE_WEIGHT> Members

        public void Add(EMP_MEASURE_WEIGHT obj)
        {
            try
            {
                this.Datacontext.EMP_MEASURE_WEIGHT.Add(obj);
                this.Datacontext.SaveChanges();
            }
            catch (Exception ex)
            {
                DebugLog.WriteLog(ex.ToString());
                throw ex;
            }
        }

        public void AddList(List<EMP_MEASURE_WEIGHT> obj)
        {
            throw new NotImplementedException();
        }

        public void Edit(EMP_MEASURE_WEIGHT obj)
        {
            var item = this.Datacontext.EMP_MEASURE_WEIGHT.Where(x => x.WEIGHT_ID == obj.WEIGHT_ID).FirstOrDefault();
             item.EFFECTIVE_DATE = obj.EFFECTIVE_DATE;
            item.EXPIRE_DATE = obj.EXPIRE_DATE;
            item.SYE_DEL = obj.SYE_DEL;
            item.WEIGHT_NAME = obj.WEIGHT_NAME;
            item.WEIGHT_VALUE = obj.WEIGHT_VALUE;
            item.UPDATE_DATE = obj.UPDATE_DATE;
            item.UPDATE_EMPLOYEE_ID = obj.UPDATE_EMPLOYEE_ID;
            this.Datacontext.SaveChanges();
        }

        public void EditList(List<EMP_MEASURE_WEIGHT> obj)
        {
            throw new NotImplementedException();
        }

        public EMP_MEASURE_WEIGHT Select()
        {
            throw new NotImplementedException();
        }

        public EMP_MEASURE_WEIGHT Select(int ID)
        {
            return this.Datacontext.EMP_MEASURE_WEIGHT.Where(x => x.WEIGHT_ID == ID).FirstOrDefault();
        }

        public EMP_MEASURE_WEIGHT Select(string PositionName)
        {
            return this.Datacontext.EMP_MEASURE_WEIGHT.Where(x => x.WEIGHT_NAME == PositionName).FirstOrDefault();
        }

        public List<EMP_MEASURE_WEIGHT> GetAll()
        {
            return this.Datacontext.EMP_MEASURE_WEIGHT.Where(x => x.SYE_DEL == false).ToList();
        }

        public void Delete(int ID)
        {
            var obj = this.Datacontext.EMP_MEASURE_WEIGHT.Where(x => x.WEIGHT_ID == ID).FirstOrDefault();
            this.Datacontext.EMP_MEASURE_WEIGHT.Remove(obj);
            this.Datacontext.SaveChanges();
        }
        #endregion

    }
}
