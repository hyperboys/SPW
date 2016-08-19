using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SPW.DAL;
using SPW.Model;

namespace SPW.DataService
{
    public class EmployeeService : ServiceBase, IDataService<EMPLOYEE>, IService
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

        public void Add(EMPLOYEE obj)
        {
            this.Datacontext.EMPLOYEE.Add(obj);
            this.Datacontext.SaveChanges();
        }

        public void AddList(List<EMPLOYEE> obj)
        {
            throw new NotImplementedException();
        }

        public void Edit(EMPLOYEE o)
        {
            var i = this.Datacontext.EMPLOYEE.Where(x => x.EMPLOYEE_ID == o.EMPLOYEE_ID).FirstOrDefault();
            i.EMPLOYEE_NAME = o.EMPLOYEE_NAME;
            i.EMPLOYEE_SURNAME = o.EMPLOYEE_SURNAME;
            i.EMPLOYEE_CODE = o.EMPLOYEE_CODE;
            i.ADDRESS1 = o.ADDRESS1;
            i.ADDRESS2 = o.ADDRESS2;
            i.ADDRESS3 = o.ADDRESS3;
            i.BIR_DATE = o.BIR_DATE;
            i.EDUCATION_GRADE = o.EDUCATION_GRADE;
            i.EDUCATION_NAME = o.EDUCATION_NAME;
            i.EMPLOYEE_MIDNAME = o.EMPLOYEE_MIDNAME;
            i.GEND = o.GEND;
            i.MARI_STT = o.MARI_STT;
            i.MILI_STT = o.MILI_STT;
            i.NAT = o.NAT;
            i.RELI = o.RELI;
            i.TEL1 = o.TEL1;
            i.TEL2 = o.TEL2;
            i.UPDATE_DATE = o.UPDATE_DATE;
            i.UPDATE_EMPLOYEE_ID = o.UPDATE_EMPLOYEE_ID;
            i.SYE_DEL = o.SYE_DEL;
            this.Datacontext.SaveChanges();
        }

        public void EditList(List<EMPLOYEE> obj)
        {
            throw new NotImplementedException();
        }

        public EMPLOYEE Select()
        {
            throw new NotImplementedException();
        }

        public EMPLOYEE Select(int ID)
        {
            return this.Datacontext.EMPLOYEE.Where(x => x.EMPLOYEE_ID == ID).FirstOrDefault();
        }

        public EMPLOYEE SelectIncludeTrans(int ID)
        {
            return this.Datacontext.EMPLOYEE.Include("EMP_MEASURE_TRANS").Where(x => x.EMPLOYEE_ID == ID).FirstOrDefault();
        }


        public EMPLOYEE SelectIncludeHits(int ID)
        {
            EMPLOYEE item = this.Datacontext.EMPLOYEE.Where(x => x.SYE_DEL == false && x.EMPLOYEE_ID == ID).FirstOrDefault();
            item.EMPLOYEE_HIST = this.Datacontext.EMPLOYEE_HIST.Where(x => x.EMPLOYEE_ID == item.EMPLOYEE_ID).OrderByDescending(y => y.EFF_DATE).FirstOrDefault();
            return item;
        }

        public List<EMPLOYEE> GetAll()
        {
            return this.Datacontext.EMPLOYEE.Where(x => x.SYE_DEL == false).ToList();
        }

        public List<EMPLOYEE> GetAllInclude()
        {
            List<EMPLOYEE> listItem = this.Datacontext.EMPLOYEE.Where(x => x.SYE_DEL == false).ToList();
            foreach (EMPLOYEE item in listItem)
            {
                item.EMPLOYEE_HIST = this.Datacontext.EMPLOYEE_HIST.Where(x => x.EMPLOYEE_ID == item.EMPLOYEE_ID).OrderByDescending(y => y.EFF_DATE).FirstOrDefault();
            }
            return listItem;
        }

        public List<EMPLOYEE> GetAll(EMPLOYEE item)
        {
            return this.Datacontext.EMPLOYEE.Where(x => x.EMPLOYEE_CODE.Contains(item.EMPLOYEE_CODE) && x.SYE_DEL == false).ToList();
        }

        #endregion
    }
}
