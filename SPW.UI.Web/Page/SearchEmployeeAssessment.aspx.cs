using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SPW.DataService;
using SPW.Model;

namespace SPW.UI.Web.Page
{
    public partial class SearchEmployeeAssessment : System.Web.UI.Page
    {
        private DataServiceEngine _dataServiceEngine;
        private EmployeeService cmdEmp;
        public List<EMPLOYEE> DataSouce
        {
            get
            {
                var list = (List<EMPLOYEE>)ViewState["listEmployee"];
                return list;
            }
            set
            {
                ViewState["listEmployee"] = value;
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

        private void InitialDataService()
        {
            cmdEmp = (EmployeeService)_dataServiceEngine.GetDataService(typeof(EmployeeService));
        }


        private void CreatePageEngine()
        {
            _dataServiceEngine = new DataServiceEngine();
            Session["DataServiceEngine"] = _dataServiceEngine;
            InitialDataService();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                CreatePageEngine();
                InitialData();
            }
            else
            {
                ReloadPageEngine();
            }
        }

        private void BlindGrid()
        {
            gridEmployee.DataSource = DataSouce;
            gridEmployee.DataBind();
        }

        private void InitialData()
        {
            DataSouce = cmdEmp.GetAll();
            gridEmployee.DataSource = DataSouce;
            gridEmployee.DataBind();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            SearchGrid();
        }

        private void SearchGrid()
        {
            if (txtEmployeeCode.Text.Equals(""))
            {
                gridEmployee.DataSource = DataSouce;
            }
            else
            {
                gridEmployee.DataSource = DataSouce.Where(x => x.EMPLOYEE_CODE.Contains(txtEmployeeCode.Text) && x.SYE_DEL == false).ToList();
            }
            gridEmployee.DataBind();
        }

        protected void gridEmployee_EditCommand(object sender, System.Web.UI.WebControls.GridViewEditEventArgs e)
        {
            Response.RedirectPermanent("KeyInEmpAssessment.aspx?id=" + gridEmployee.DataKeys[e.NewEditIndex].Values[0].ToString());
        }

        protected void gridEmployee_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gridEmployee.PageIndex = e.NewPageIndex;
            gridEmployee.DataBind();
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            txtEmployeeCode.Text = "";
            SearchGrid();
        }
    }
}