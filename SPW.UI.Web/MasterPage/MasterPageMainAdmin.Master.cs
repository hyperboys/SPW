using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SPW.Model;
using SPW.DataService;

namespace SPW.UI.Web.MasterPage
{
    public partial class MasterPageMainAdmin : System.Web.UI.MasterPage
    {
        private DataServiceEngine _dataServiceEngine;
        private FunctionService _functionService;
        private SubFunctionService _subFunctionService;

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
            _functionService = (FunctionService)_dataServiceEngine.GetDataService(typeof(FunctionService));
            _subFunctionService = (SubFunctionService)_dataServiceEngine.GetDataService(typeof(SubFunctionService));
        }

        private void CreatePageEngine()
        {
            _dataServiceEngine = new DataServiceEngine();
            Session["DataServiceEngine"] = _dataServiceEngine;
            InitialDataService();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    CreatePageEngine();

                    USER userItem = Session["user"] as USER;

                    //  ข้อมูลระบบ
                    listSystem.DataSource = _subFunctionService.GetAll(2).Where(x => (userItem.ROLE.ROLE_FUNCTION.Where(y => y.FUNCTION_ID == 2 && y.SYE_DEL == false).Select(z => z.SUB_FUNCTION_ID).ToList()).Contains(x.SUB_FUNCTION_ID)).ToList();
                    listSystem.DataBind();
                    if (listSystem.Items.Count() == 0) 
                    {
                        SystemData.Visible = false;
                    }

                    //  ข้อมูลพื้นฐาน
                    listStandard.DataSource = _subFunctionService.GetAll(3).Where(x => (userItem.ROLE.ROLE_FUNCTION.Where(y => y.FUNCTION_ID == 3 && y.SYE_DEL == false).Select(z => z.SUB_FUNCTION_ID).ToList()).Contains(x.SUB_FUNCTION_ID)).ToList();
                    listStandard.DataBind();
                    if (listStandard.Items.Count() == 0)
                    {
                        StandardData.Visible = false;
                    }

                    //  การสั่งซื้อสินค้า
                    listOrder.DataSource = _subFunctionService.GetAll(4).Where(x => (userItem.ROLE.ROLE_FUNCTION.Where(y => y.FUNCTION_ID == 4 && y.SYE_DEL == false).Select(z => z.SUB_FUNCTION_ID).ToList()).Contains(x.SUB_FUNCTION_ID)).ToList();
                    listOrder.DataBind();
                    if (listOrder.Items.Count() == 0)
                    {
                        OrderData.Visible = false;
                    }

                    //  รายงาน
                    listReport.DataSource = _subFunctionService.GetAll(5).Where(x => (userItem.ROLE.ROLE_FUNCTION.Where(y => y.FUNCTION_ID == 5 && y.SYE_DEL == false).Select(z => z.SUB_FUNCTION_ID).ToList()).Contains(x.SUB_FUNCTION_ID)).ToList();
                    listReport.DataBind();
                    if (listReport.Items.Count() == 0)
                    {
                        Report.Visible = false;
                    }

                    //  คลัง
                    listStock.DataSource = _subFunctionService.GetAll(6).Where(x => (userItem.ROLE.ROLE_FUNCTION.Where(y => y.FUNCTION_ID == 6 && y.SYE_DEL == false).Select(z => z.SUB_FUNCTION_ID).ToList()).Contains(x.SUB_FUNCTION_ID)).ToList();
                    listStock.DataBind();
                    if (listStock.Items.Count() == 0)
                    {
                        Stock.Visible = false;
                    }

                    //  ทรัพย์สิน
                    listInvoice.DataSource = _subFunctionService.GetAll(7).Where(x => (userItem.ROLE.ROLE_FUNCTION.Where(y => y.FUNCTION_ID == 7 && y.SYE_DEL == false).Select(z => z.SUB_FUNCTION_ID).ToList()).Contains(x.SUB_FUNCTION_ID)).ToList();
                    listInvoice.DataBind();
                    if (listInvoice.Items.Count() == 0)
                    {
                        Asset.Visible = false;
                    }

                    //  Purchase Order
                    listPO.DataSource = _subFunctionService.GetAll(8).Where(x => (userItem.ROLE.ROLE_FUNCTION.Where(y => y.FUNCTION_ID == 8 && y.SYE_DEL == false).Select(z => z.SUB_FUNCTION_ID).ToList()).Contains(x.SUB_FUNCTION_ID)).ToList();
                    listPO.DataBind();
                    if (listPO.Items.Count() == 0)
                    {
                        PO.Visible = false;
                    }
                }
                catch
                {
                    Response.RedirectPermanent("../PageLogin/Login.aspx");
                }
            }
            else
            {
                ReloadPageEngine();
            }
        }
    }
}