using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SPW.DataService;
using SPW.Model;

namespace SPW.UI.Web.Page
{
    public partial class ManageProduct : System.Web.UI.Page
    {
        private DataServiceEngine _dataServiceEngine;
        private ProductPriceListService cmdProductPriceListService;
        private CategoryService cmdCategoryService;
        private ProductService cmdProductService;
        private ZoneService cmdZoneService;

        private PRODUCT _product;
        private List<PRODUCT_PRICELIST> DataSouceList
        {
            get
            {
                var list = (List<PRODUCT_PRICELIST>)ViewState["listProductDetail"];
                return list;
            }
            set
            {
                ViewState["listProductDetail"] = value;
            }
        }

        private List<PRODUCT_PROMOTION> DataSoucePromotion
        {
            get
            {

                var list = (List<PRODUCT_PROMOTION>)ViewState["DataSoucePromotion"];
                return list;
            }
            set
            {
                ViewState["DataSoucePromotion"] = value;
            }
        }

        private List<PRODUCT_PROMOTION> listPromotion
        {
            get
            {
                if (Session["listPromotion"] == null)
                {
                    Session["listPromotion"] = new List<PRODUCT_PROMOTION>();
                }
                var list = (List<PRODUCT_PROMOTION>)Session["listPromotion"];
                return list;
            }
            set
            {
                ViewState["listPromotion"] = value;
            }
        }

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
            //ReloadDatasource();
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
            cmdCategoryService = (CategoryService)_dataServiceEngine.GetDataService(typeof(CategoryService));
            cmdProductService = (ProductService)_dataServiceEngine.GetDataService(typeof(ProductService));
            cmdZoneService = (ZoneService)_dataServiceEngine.GetDataService(typeof(ZoneService));
            cmdProductPriceListService = (ProductPriceListService)_dataServiceEngine.GetDataService(typeof(ProductPriceListService));
        }

        private void ReloadDatasource()
        {
            //DataSouce = _categoryService.GetAll();
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

        private void PrepareObjectScreen()
        {
            var list = cmdCategoryService.GetAll();
            foreach (var item in list)
            {
                ddlCategory.Items.Add(new ListItem(item.CATEGORY_NAME, item.CATEGORY_ID.ToString()));
            }

            var listddlPakUDesc = cmdProductService.GetUDescPacking();
            foreach (var item in listddlPakUDesc)
            {
                ddlPakUDesc.Items.Add(new ListItem(item, item));
            }

            var listddlPakPDesc = cmdProductService.GetPDescPacking();
            foreach (var item in listddlPakPDesc)
            {
                ddlPakPDesc.Items.Add(new ListItem(item, item));
            }

            var listZone = cmdZoneService.GetAll();
            DataSouceList = new List<PRODUCT_PRICELIST>();
            foreach (var item in listZone)
            {
                PRODUCT_PRICELIST pd = new PRODUCT_PRICELIST();
                pd.ZONE_ID = item.ZONE_ID;
                pd.ZONE = new ZONE();
                pd.ZONE.ZONE_CODE = item.ZONE_CODE;
                pd.ZONE.ZONE_NAME = item.ZONE_NAME;
                DataSouceList.Add(pd);
            }

            if (Request.QueryString["id"] != null)
            {
                _product = cmdProductService.Select(Convert.ToInt32(Request.QueryString["id"].ToString()));
                popTxtProductCode.Text = _product.PRODUCT_CODE;
                popTxtProductCode.Enabled = false;
                poptxtProductName.Text = _product.PRODUCT_NAME;
                txtPacking.Text = _product.PRODUCT_PACKING_QTY.ToString();
                txtWeight.Text = _product.PRODUCT_WEIGHT.ToString();
                txtUnit.Text = _product.PRODUCT_WEIGHT_DEFINE;
                ddlCategory.SelectedValue = _product.CATEGORY_ID.ToString();
                ddlkind.SelectedValue = _product.PRODUCT_TYPE_CODE.ToString();
                ddlPakUDesc.SelectedValue = _product.PRODUCT_PACKING_PER_UDESC;
                ddlPakPDesc.SelectedValue = _product.PRODUCT_PACKING_PER_PDESC;
                lblName.Text = poptxtProductName.Text;
                var listPD = cmdProductPriceListService.GetAll(Convert.ToInt32(Request.QueryString["id"].ToString()));
                foreach (var itemPD in listPD)
                {
                    foreach (var itemDST in DataSouceList)
                    {
                        if (itemDST.ZONE_ID == itemPD.ZONE_ID)
                        {
                            itemDST.PRODUCT_PRICE = itemPD.PRODUCT_PRICE;
                            itemDST.PRODUCT_ID = itemPD.PRODUCT_ID;
                            break;
                        }
                    }
                }

                flag.Text = "Edit";
            }
            gridProductDetail.DataSource = DataSouceList;
            gridProductDetail.DataBind();
        }

       

        protected void btnSave_Click(object sender, EventArgs e)
        {
            var obj = new PRODUCT();
            obj.PRODUCT_CODE = popTxtProductCode.Text;
            obj.PRODUCT_NAME = poptxtProductName.Text;
            obj.PRODUCT_PACKING_QTY = Convert.ToInt32(txtPacking.Text);
            if (ddlPakUDesc.SelectedValue != "กรุณาเลือก")
                obj.PRODUCT_PACKING_PER_UDESC = ddlPakUDesc.SelectedValue;

            if (ddlPakPDesc.SelectedValue != "กรุณาเลือก")
                obj.PRODUCT_PACKING_PER_PDESC = ddlPakPDesc.SelectedValue;

            obj.PRODUCT_PACKING_DESC = "(" + obj.PRODUCT_PACKING_QTY + " " + obj.PRODUCT_PACKING_PER_UDESC + "/" + obj.PRODUCT_PACKING_PER_PDESC + ")";
            obj.PRODUCT_WEIGHT = Convert.ToDecimal(txtWeight.Text);
            obj.PRODUCT_WEIGHT_DEFINE = txtUnit.Text;
            obj.CATEGORY_ID = Convert.ToInt32(ddlCategory.SelectedValue);
            obj.PRODUCT_TYPE_CODE = Convert.ToInt32(ddlkind.SelectedValue);
            obj.STOCK_TYPE_ID = Convert.ToInt32(ddlkind.SelectedValue);
            if (flag.Text.Equals("Add"))
            {
                obj.Action = ActionEnum.Create;
                obj.CREATE_DATE = DateTime.Now;
                obj.CREATE_EMPLOYEE_ID = 0;
                obj.UPDATE_DATE = DateTime.Now;
                obj.UPDATE_EMPLOYEE_ID = 0;
                obj.SYE_DEL = false;
                cmdProductService.Add(obj);
            }
            else
            {
                obj.Action = ActionEnum.Update;
                obj.PRODUCT_ID = Convert.ToInt32(Request.QueryString["id"].ToString());
                obj.UPDATE_DATE = DateTime.Now;
                obj.UPDATE_EMPLOYEE_ID = 0;
                obj.SYE_DEL = false;
                cmdProductService.Edit(obj);
            }

            if (FileUpload1.HasFile)
            {
                Stream fs = FileUpload1.PostedFile.InputStream;
                if (!System.IO.Directory.Exists(HttpContext.Current.Server.MapPath("~") + "ImageProduct"))
                    System.IO.Directory.CreateDirectory(HttpContext.Current.Server.MapPath("~") + "ImageProduct");
                obj.PRODUCT_IMAGE_PATH = "~/ImageProduct/" + obj.PRODUCT_ID + "." + FileUpload1.FileName.Split('.').ToArray()[1];
                FileUpload1.PostedFile.SaveAs(HttpContext.Current.Server.MapPath("~") + "ImageProduct\\" + obj.PRODUCT_ID + "." + FileUpload1.FileName.Split('.').ToArray()[1]);
            }
            cmdProductService.Edit(obj);

            var listDetail = new List<PRODUCT_PRICELIST>();
            int i = 0;
            foreach (var item in DataSouceList)
            {
                var objDetail = new PRODUCT_PRICELIST();
                objDetail.ZONE_ID = item.ZONE_ID;
                objDetail.PRODUCT_PRICE = Convert.ToDecimal(((TextBox)(gridProductDetail.Rows[i++].Cells[2].FindControl("txtPrice"))).Text);
                objDetail.SYE_DEL = false;
                objDetail.UPDATE_DATE = DateTime.Now;
                objDetail.UPDATE_EMPLOYEE_ID = 0;
                if (item.PRODUCT_ID == 0)
                {
                    objDetail.Action = ActionEnum.Create;
                    objDetail.PRODUCT_ID = obj.PRODUCT_ID;
                    objDetail.CREATE_DATE = DateTime.Now;
                    objDetail.CREATE_EMPLOYEE_ID = 0;
                }
                else
                {
                    objDetail.Action = ActionEnum.Update;
                    objDetail.PRODUCT_ID = item.PRODUCT_ID;
                }
                listDetail.Add(objDetail);
            }

            cmdProductPriceListService.AddUpdateList(listDetail);
            alert.Visible = true;
            Response.AppendHeader("Refresh", "2; url=SearchProduct.aspx");
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.RedirectPermanent("SearchProduct.aspx");
        }

        protected void gridProductDetail_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (flag.Text.Equals("Edit"))
                {
                    TextBox t = (TextBox)e.Row.FindControl("txtPrice");
                    if (DataSouceList[e.Row.RowIndex].PRODUCT_PRICE != null)
                        t.Text = DataSouceList[e.Row.RowIndex].PRODUCT_PRICE.ToString();
                    else
                        t.Text = "0";
                }
                else
                {
                    TextBox t = (TextBox)e.Row.FindControl("txtPrice");
                    t.Text = "0";
                }
            }
        }
    }
}