using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SPW.DataService;
using SPW.Model;
using SPW.DAL;
using SPW.Common;

namespace SPW.UI.Web.Page
{
    public partial class ManageMeasureWeight : System.Web.UI.Page
    {
        private DataServiceEngine _dataServiceEngine;
        private EmpMeasureWeightService cmdEmpMeasureWeightService;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                InitialPage();
            }
            else
            {
                ReloadPageEngine();
            }
        }

        private void InitialPage()
        {
            CreatePageEngine();
            PrepareObjectScreen();
        }

        private void CreatePageEngine()
        {
            _dataServiceEngine = new DataServiceEngine();
            Session["DataServiceEngine"] = _dataServiceEngine;
            InitialDataService();
        }

        private void InitialDataService()
        {
            cmdEmpMeasureWeightService = (EmpMeasureWeightService)_dataServiceEngine.GetDataService(typeof(EmpMeasureWeightService));
        }

        private void PrepareObjectScreen()
        {
            List<EMP_MEASURE_WEIGHT> items = cmdEmpMeasureWeightService.GetAll();
            if (items != null && items.Count() >= 2)
            {
                txtPercenA.Text = items.Where(x => x.WEIGHT_NAME == "ผู้บริหาร").FirstOrDefault().WEIGHT_VALUE.ToString();
                txtPercenB.Text = items.Where(x => x.WEIGHT_NAME == "Supervisor").FirstOrDefault().WEIGHT_VALUE.ToString();

                lblDate1.Text = items[0].EFFECTIVE_DATE.ToString("dd/MM/yyyy");
                lblDate2.Text = items[1].EFFECTIVE_DATE.ToString("dd/MM/yyyy");
            }
            else
            {
                DateTime date = DateTime.Now;
                lblDate1.Text = date.ToString("dd/MM/yyyy");
                lblDate2.Text = date.ToString("dd/MM/yyyy");
            }
        }

        private void ReloadPageEngine()
        {
            if (Session["DataServiceEngine"] != null)
            {
                _dataServiceEngine = (DataServiceEngine)Session["DataServiceEngine"];
                InitialDataService();
            }
            else
            {
                CreatePageEngine();
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                USER userItem = Session["user"] as USER;
                List<EMP_MEASURE_WEIGHT> items = cmdEmpMeasureWeightService.GetAll();
                if (items != null && items.Count() > 0)
                {
                    foreach (EMP_MEASURE_WEIGHT tmp in items)
                    {
                        if (tmp.WEIGHT_NAME == "ผู้บริหาร")
                        {
                            tmp.WEIGHT_VALUE = Convert.ToDecimal(txtPercenA.Text);
                        }
                        else
                        {
                            tmp.WEIGHT_VALUE = Convert.ToDecimal(txtPercenB.Text);
                        }
                        tmp.SYE_DEL = false;
                        tmp.EFFECTIVE_DATE = DateTime.Now;
                        tmp.UPDATE_EMPLOYEE_ID = userItem.EMPLOYEE_ID;
                        tmp.UPDATE_DATE = DateTime.Now;
                        cmdEmpMeasureWeightService.Edit(tmp);
                    }
                }
                else
                {
                    // ผู้บริหาร
                    EMP_MEASURE_WEIGHT item = new EMP_MEASURE_WEIGHT();
                    item.CREATE_DATE = DateTime.Now;
                    item.CREATE_EMPLOYEE_ID = userItem.EMPLOYEE_ID;
                    item.EFFECTIVE_DATE = DateTime.Now;
                    item.WEIGHT_NAME = "ผู้บริหาร";
                    item.WEIGHT_VALUE = Convert.ToDecimal(txtPercenA.Text);
                    item.WEIGHT_ID = 1;
                    item.SYE_DEL = false;
                    item.UPDATE_DATE = DateTime.Now;
                    item.UPDATE_EMPLOYEE_ID = userItem.EMPLOYEE_ID;
                    cmdEmpMeasureWeightService.Add(item);

                    // Supervisor
                    item = new EMP_MEASURE_WEIGHT();
                    item.CREATE_DATE = DateTime.Now;
                    item.CREATE_EMPLOYEE_ID = userItem.EMPLOYEE_ID;
                    item.EFFECTIVE_DATE = DateTime.Now;
                    item.WEIGHT_NAME = "Supervisor";
                    item.WEIGHT_VALUE = Convert.ToDecimal(txtPercenB.Text);
                    item.WEIGHT_ID = 2;
                    item.SYE_DEL = false;
                    item.UPDATE_DATE = DateTime.Now;
                    item.UPDATE_EMPLOYEE_ID = userItem.EMPLOYEE_ID;
                    cmdEmpMeasureWeightService.Add(item);

                }

                btnSave.Enabled = false;
                btnSave.Visible = false;
                btnCancel.Visible = false;
                alert.Visible = true;
                Response.AppendHeader("Refresh", "2; url=MainAdmin.aspx");
            }
            catch (Exception ex) 
            {
                DebugLog.WriteLog(ex.ToString());
            }
        }
    }
}