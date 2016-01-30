using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SPW.DataService;
using SPW.Model;

namespace SPW.UI.Web.Page
{
    public partial class ManageOrderHQEdit : System.Web.UI.Page
    {
        #region Declare variable
        private DataServiceEngine _dataServiceEngine;
        public class DATAGRID
        {
            public DATAGRID()
            {

            }
            public int ORDER_ID { get; set; }
            public int ORDER_DETAIL_ID { get; set; }
            public string PRODUCT_NAME { get; set; }
            public Nullable<int> PRODUCT_SEQ { get; set; }
            public string COLOR_TYPE_NAME { get; set; }
            public string COLOR_NAME { get; set; }
            public Nullable<int> PRODUCT_QTY { get; set; }
            public Nullable<int> FLEE { get; set; }
            public Nullable<decimal> PRODUCT_PRICE { get; set; }
            public Nullable<decimal> PRODUCT_PRICE_TOTAL { get; set; }
            public Nullable<decimal> PRODUCT_WEIGHT { get; set; }
            public Nullable<int> PRODUCT_SEND_REMAIN_QTY { get; set; }
            public Nullable<int> PRODUCT_SEND_REMAIN_FLEE { get; set; }
        }
        #endregion

        #region Sevice control
        private ColorService _colorService;
        private ColorTypeService _colorTypeService;
        private OrderService _orderService;
        private OrderDetailService _orderDetailService;

        private void InitialPage()
        {
            CreatePageEngine();
            ReloadDatasource();
            InitialData();
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

        private void CreatePageEngine()
        {
            _dataServiceEngine = new DataServiceEngine();
            Session["DataServiceEngine"] = _dataServiceEngine;
            InitialDataService();
        }

        private void InitialDataService()
        {
            _colorService = (ColorService)_dataServiceEngine.GetDataService(typeof(ColorService));
            _colorTypeService = (ColorTypeService)_dataServiceEngine.GetDataService(typeof(ColorTypeService));
            _orderService = (OrderService)_dataServiceEngine.GetDataService(typeof(OrderService));
            _orderDetailService = (OrderDetailService)_dataServiceEngine.GetDataService(typeof(OrderDetailService));
        }

        private void ReloadDatasource()
        {

        }

        private void InitialData()
        {
            txtOrderId.Text = Request.QueryString["ORDER_ID"];
            txtStoreCode.Text = Request.QueryString["STORE_CODE"];
            txtStoreName.Text = Request.QueryString["STORE_NAME"];
            BindGridview();
        }
        private void BindGridview()
        {
            List<DATAGRID> query = new List<DATAGRID>();
            List<ORDER_DETAIL> listOrderDetail = _orderDetailService.GetAllIncludeByOrder(int.Parse(Request.QueryString["ORDER_ID"]));
            int sumQty = 0, sumFlee = 0;
            decimal totalPrice = 0;

            var normal = listOrderDetail
                        .Where(f => f.IS_FREE == "N")
                        .Select(e => new DATAGRID
                        {
                            ORDER_ID = e.ORDER_ID,
                            ORDER_DETAIL_ID = e.ORDER_DETAIL_ID,
                            PRODUCT_NAME = e.PRODUCT.PRODUCT_NAME,
                            PRODUCT_SEQ = e.PRODUCT_SEQ,
                            COLOR_TYPE_NAME = e.COLOR_TYPE.COLOR_TYPE_NAME,
                            COLOR_NAME = e.COLOR.COLOR_NAME,
                            PRODUCT_QTY = e.PRODUCT_QTY,
                            FLEE = e.PRODUCT_QTY,
                            PRODUCT_PRICE = e.PRODUCT_PRICE,
                            PRODUCT_PRICE_TOTAL = e.PRODUCT_PRICE_TOTAL,
                            PRODUCT_WEIGHT = e.PRODUCT_WEIGHT,
                            PRODUCT_SEND_REMAIN_QTY = e.PRODUCT_SEND_REMAIN
                        }).ToList();
            var flee = listOrderDetail
                        .Where(f => f.IS_FREE == "F")
                        .Select(e => new DATAGRID
                        {
                            ORDER_ID = e.ORDER_ID,
                            ORDER_DETAIL_ID = e.ORDER_DETAIL_ID,
                            PRODUCT_NAME = e.PRODUCT.PRODUCT_NAME,
                            PRODUCT_SEQ = e.PRODUCT_SEQ,
                            COLOR_TYPE_NAME = e.COLOR_TYPE.COLOR_TYPE_NAME,
                            COLOR_NAME = e.COLOR.COLOR_NAME,
                            PRODUCT_QTY = e.PRODUCT_QTY,
                            FLEE = e.PRODUCT_QTY,
                            PRODUCT_PRICE = e.PRODUCT_PRICE,
                            PRODUCT_PRICE_TOTAL = e.PRODUCT_PRICE_TOTAL,
                            PRODUCT_WEIGHT = e.PRODUCT_WEIGHT,
                            PRODUCT_SEND_REMAIN_FLEE = e.PRODUCT_SEND_REMAIN
                        }).ToList();
            normal.ForEach(i =>
            {
                flee.ForEach(j =>
                {
                    if (i.PRODUCT_SEQ == j.PRODUCT_SEQ && i.ORDER_ID == j.ORDER_ID)
                    {
                        DATAGRID data = new DATAGRID
                        {
                            ORDER_ID = i.ORDER_ID,
                            ORDER_DETAIL_ID = i.ORDER_DETAIL_ID,
                            PRODUCT_NAME = i.PRODUCT_NAME,
                            PRODUCT_SEQ = i.PRODUCT_SEQ,
                            COLOR_TYPE_NAME = i.COLOR_TYPE_NAME,
                            COLOR_NAME = i.COLOR_NAME,
                            PRODUCT_QTY = i.PRODUCT_QTY,
                            FLEE = j.PRODUCT_QTY,
                            PRODUCT_PRICE = i.PRODUCT_PRICE,
                            PRODUCT_PRICE_TOTAL = i.PRODUCT_PRICE_TOTAL,
                            PRODUCT_WEIGHT = i.PRODUCT_WEIGHT,
                            PRODUCT_SEND_REMAIN_QTY = i.PRODUCT_SEND_REMAIN_QTY,
                            PRODUCT_SEND_REMAIN_FLEE = j.PRODUCT_SEND_REMAIN_FLEE
                        };
                        query.Add(data);
                        if (data.PRODUCT_SEND_REMAIN_QTY > 0 || data.PRODUCT_SEND_REMAIN_FLEE > 0)
                        {
                            sumQty = sumQty + (int)data.PRODUCT_SEND_REMAIN_QTY;
                            sumFlee = sumFlee + (int)data.PRODUCT_SEND_REMAIN_FLEE;
                            totalPrice = totalPrice + (decimal)data.PRODUCT_PRICE_TOTAL;
                        }
                    }
                });
            });
            lblSumQty.Text = sumQty.ToString();
            lblFlee.Text = sumFlee.ToString();
            lblTotalPrice.Text = totalPrice.ToString();
            hfSumQty.Value = sumQty.ToString();
            hfFlee.Value = sumFlee.ToString();
            hfTotalPrice.Value = totalPrice.ToString();

            gdvManageOrderHQDetail.DataSource = query;
            gdvManageOrderHQDetail.DataBind();
        }
        bool UpdateOrderDetail()
        {
            try
            {
                decimal oTotal = 0;
                int orderID = 0;
                List<DATAGRID> query = new List<DATAGRID>();
                foreach (GridViewRow row in gdvManageOrderHQDetail.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        DATAGRID data = new DATAGRID();
                        data.ORDER_ID = int.Parse(Request.QueryString["ORDER_ID"]);
                        data.PRODUCT_SEQ = int.Parse(((HiddenField)row.FindControl("hfPRODUCT_SEQ")).Value);
                        data.PRODUCT_QTY = int.Parse(((TextBox)row.FindControl("txtQty")).Text == "" ? "0" :((TextBox)row.FindControl("txtQty")).Text);
                        data.FLEE = int.Parse(((TextBox)row.FindControl("txtFleeQty")).Text == "" ? "0" : ((TextBox)row.FindControl("txtFleeQty")).Text);
                        data.COLOR_NAME = ((DropDownList)row.FindControl("ddlCOLOR_NAME")).SelectedItem.Value;
                        data.COLOR_TYPE_NAME = ((DropDownList)row.FindControl("ddlCOLOR_TYPE_NAME")).SelectedItem.Value;
                        data.PRODUCT_WEIGHT = decimal.Parse(row.Cells[4].Text);
                        data.PRODUCT_PRICE = decimal.Parse(((Label)row.FindControl("lblPRODUCT_PRICE")).Text);
                        data.PRODUCT_SEND_REMAIN_QTY = int.Parse(((HiddenField)row.FindControl("hfRemainQty")).Value);
                        data.PRODUCT_SEND_REMAIN_FLEE = int.Parse(((HiddenField)row.FindControl("hfRemainFlee")).Value);
                        int oQty = int.Parse(((HiddenField)row.FindControl("hfOldQty")).Value);
                        int oFlee = int.Parse(((HiddenField)row.FindControl("hfOldFlee")).Value);
                        data.PRODUCT_QTY = data.PRODUCT_QTY + (oQty - data.PRODUCT_SEND_REMAIN_QTY);
                        data.FLEE = data.FLEE + (oFlee - data.PRODUCT_SEND_REMAIN_FLEE);
                        if (oQty != data.PRODUCT_SEND_REMAIN_QTY || oFlee != data.PRODUCT_SEND_REMAIN_FLEE)
                        {
                            if (data.PRODUCT_QTY < oQty - data.PRODUCT_SEND_REMAIN_QTY || data.FLEE < oFlee - data.PRODUCT_SEND_REMAIN_FLEE)
                            {
                                return false;
                            }
                            else
                            {
                                data.PRODUCT_SEND_REMAIN_QTY = data.PRODUCT_SEND_REMAIN_QTY + data.PRODUCT_QTY - oQty;
                                data.PRODUCT_SEND_REMAIN_FLEE = data.PRODUCT_SEND_REMAIN_FLEE + data.FLEE - oFlee;
                                query.Add(data);
                            }
                        }
                        else
                        {
                            data.PRODUCT_SEND_REMAIN_QTY = data.PRODUCT_SEND_REMAIN_QTY + data.PRODUCT_QTY - oQty;
                            data.PRODUCT_SEND_REMAIN_FLEE = data.PRODUCT_SEND_REMAIN_FLEE + data.FLEE - oFlee;
                            query.Add(data);
                        }
                    }
                }

                List<ORDER_DETAIL> listOrderDetail = _orderDetailService.GetAllIncludeByOrder(int.Parse(Request.QueryString["ORDER_ID"]));
                listOrderDetail.ForEach(i => 
                    {
                        query.ForEach(j =>
                            {
                                if (i.PRODUCT_SEQ == j.PRODUCT_SEQ && i.ORDER_ID == j.ORDER_ID)
                                {
                                    if (i.IS_FREE == "N")
                                    {
                                        i.PRODUCT_QTY = j.PRODUCT_QTY;
                                        i.PRODUCT_SEND_REMAIN = j.PRODUCT_SEND_REMAIN_QTY;
                                        orderID = j.ORDER_ID;
                                        oTotal += (decimal)(j.PRODUCT_QTY * j.PRODUCT_PRICE);
                                    }
                                    else
                                    {
                                        i.PRODUCT_QTY = j.FLEE;
                                        i.PRODUCT_SEND_REMAIN = j.PRODUCT_SEND_REMAIN_FLEE;
                                    }
                                    i.COLOR_ID = int.Parse(j.COLOR_NAME);
                                    i.COLOR_TYPE_ID = int.Parse(j.COLOR_TYPE_NAME);
                                    i.PRODUCT_WEIGHT = j.PRODUCT_WEIGHT;
                                }
                            });
                    });

                _orderDetailService.EditOrderDetailList(listOrderDetail);
                _orderService.EditOrderTotal(orderID, oTotal);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        #endregion

        #region ASP control
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

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            int ORDER_ID = int.Parse(Request.QueryString["ORDER_ID"]);
            _orderService.EditOrderStepCancel(ORDER_ID);
            Response.Redirect("ManageOrderHQ.aspx");
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            string ORDER_ID = Request.QueryString["ORDER_ID"];
            string STORE_CODE = Request.QueryString["STORE_CODE"];
            string STORE_NAME = Request.QueryString["STORE_NAME"];

            if (UpdateOrderDetail())
                Response.Redirect("ManageOrderHQPreview.aspx?ORDER_ID=" + ORDER_ID + "&STORE_CODE=" + STORE_CODE + "&STORE_NAME=" + STORE_NAME);
            else
                lblError.Text = "เกิดข้อผิดพลาดในการงาน กรุณาตรวจสอบจำนวนให้ถูกต้อง";
        }
        protected void gdvManageOrderHQDetail_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DropDownList ddlCOLOR_TYPE_NAME = e.Row.FindControl("ddlCOLOR_TYPE_NAME") as DropDownList;
                HiddenField hfCOLOR_TYPE_NAME = e.Row.FindControl("hfCOLOR_TYPE_NAME") as HiddenField;
                
                ddlCOLOR_TYPE_NAME.DataSource = _colorTypeService.GetAll();
                ddlCOLOR_TYPE_NAME.DataTextField = "COLOR_TYPE_NAME";
                ddlCOLOR_TYPE_NAME.DataValueField = "COLOR_TYPE_ID";
                ddlCOLOR_TYPE_NAME.DataBind();
                ddlCOLOR_TYPE_NAME.SelectedIndex = ddlCOLOR_TYPE_NAME.Items.IndexOf(ddlCOLOR_TYPE_NAME.Items.FindByText(hfCOLOR_TYPE_NAME.Value));


                DropDownList ddlCOLOR_NAME = e.Row.FindControl("ddlCOLOR_NAME") as DropDownList;
                HiddenField hfCOLOR_NAME = e.Row.FindControl("hfCOLOR_NAME") as HiddenField;
                ddlCOLOR_NAME.DataSource = _colorService.GetAll();
                ddlCOLOR_NAME.DataTextField = "COLOR_NAME";
                ddlCOLOR_NAME.DataValueField = "COLOR_ID";
                ddlCOLOR_NAME.DataBind();
                ddlCOLOR_NAME.SelectedIndex = ddlCOLOR_NAME.Items.IndexOf(ddlCOLOR_NAME.Items.FindByText(hfCOLOR_NAME.Value));
            }
        }
        protected void gdvManageOrderHQDetail_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int ORDER_ID = Convert.ToInt32(Request.QueryString["ORDER_ID"]);
            int ORDER_DETAIL_ID = int.Parse(gdvManageOrderHQDetail.DataKeys[e.RowIndex].Value.ToString());
            if (_orderDetailService.IsSend(ORDER_ID))
                _orderDetailService.EditOrderDetailCancelBySend(ORDER_DETAIL_ID);
            else
                _orderDetailService.EditOrderDetailCancel(ORDER_DETAIL_ID);

            BindGridview();
        }
        #endregion
    }
}