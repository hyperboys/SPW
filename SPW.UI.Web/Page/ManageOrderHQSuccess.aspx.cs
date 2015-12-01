﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SPW.DataService;
using SPW.Model;

namespace SPW.UI.Web.Page
{
    public partial class ManageOrderHQSuccess : System.Web.UI.Page
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
        }
        public List<DATAGRID> DataSouce
        {
            get
            {
                return (List<DATAGRID>)ViewState["DATAGRID"];
            }
            set
            {
                ViewState["DATAGRID"] = value;
            }
        }
        #endregion

        #region Sevice control
        private OrderDetailService _orderDetailService;
        private OrderService _orderService;

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
            _orderService = (OrderService)_dataServiceEngine.GetDataService(typeof(OrderService));
            _orderDetailService = (OrderDetailService)_dataServiceEngine.GetDataService(typeof(OrderDetailService));
        }

        private void ReloadDatasource()
        {

        }

        private void InitialData()
        {
            txtStoreCode.Text = Request.QueryString["STORE_CODE"];
            txtStoreName.Text = Request.QueryString["STORE_NAME"];
            BindGridview();
        }
        private void BindGridview()
        {
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
                            PRODUCT_PRICE_TOTAL = e.PRODUCT_PRICE_TOTAL
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
                            PRODUCT_PRICE_TOTAL = e.PRODUCT_PRICE_TOTAL
                        }).ToList();
            List<DATAGRID> query = new List<DATAGRID>();
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
                            PRODUCT_QTY = i.PRODUCT_QTY + j.PRODUCT_QTY,
                            FLEE = j.PRODUCT_QTY,
                            PRODUCT_PRICE = i.PRODUCT_PRICE,
                            PRODUCT_PRICE_TOTAL = i.PRODUCT_PRICE_TOTAL
                        };
                        query.Add(data);
                        sumQty = sumQty + (int)data.PRODUCT_QTY;
                        sumFlee = sumFlee + (int)data.FLEE;
                        totalPrice = totalPrice + (decimal)data.PRODUCT_PRICE_TOTAL;
                    }
                });
            });
            lblSumQty.Text = sumQty.ToString();
            //lblFlee.Text = sumFlee.ToString();
            lblTotalPrice.Text = totalPrice.ToString();
            hfSumQty.Value = sumQty.ToString();
            hfFlee.Value = sumFlee.ToString();
            hfTotalPrice.Value = totalPrice.ToString();

            gdvPreview.DataSource = query;
            gdvPreview.DataBind();
        }
        void SetOrderStep(int ORDER_ID)
        {
            _orderService.EditOrderStepHQApprove(ORDER_ID);
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
            string ORDER_ID = Request.QueryString["ORDER_ID"];
            string STORE_CODE = Request.QueryString["STORE_CODE"];
            string STORE_NAME = Request.QueryString["STORE_NAME"];

            Response.Redirect("ManageOrderHQEdit.aspx?ORDER_ID=" + ORDER_ID + "&STORE_CODE=" + STORE_CODE + "&STORE_NAME=" + STORE_NAME);

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            string ORDER_ID = Request.QueryString["ORDER_ID"];
            string STORE_CODE = Request.QueryString["STORE_CODE"];
            string STORE_NAME = Request.QueryString["STORE_NAME"];

            SetOrderStep(int.Parse(ORDER_ID));
            Response.Redirect("ManageOrderHQPreview.aspx?ORDER_ID=" + ORDER_ID + "&STORE_CODE=" + STORE_CODE + "&STORE_NAME=" + STORE_NAME);
        }
        #endregion
    }
}