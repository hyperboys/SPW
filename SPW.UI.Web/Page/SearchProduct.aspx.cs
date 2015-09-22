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
    public partial class SearchProduct : System.Web.UI.Page
    {
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

        private List<PRODUCT> DataSouce
        {
            get
            {
                var list = (List<PRODUCT>)ViewState["listProduct"];
                return list;
            }
            set
            {
                ViewState["listProduct"] = value;
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
                if (ViewState["listPromotion"] == null)
                {
                    ViewState["listPromotion"] = new List<PRODUCT_PROMOTION>();
                }
                var list = (List<PRODUCT_PROMOTION>)ViewState["listPromotion"];
                return list;
            }
            set
            {
                ViewState["listPromotion"] = value;
            }
        }

        private void BlindGrid()
        {
            gridProduct.DataSource = DataSouce;
            gridProduct.DataBind();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                string appPath = HttpContext.Current.Server.MapPath("~");
                InitialData();
            }
        }

        private void InitialData()
        {
            var cmd = new ProductService();
            DataSouce = cmd.GetALL();
            gridProduct.DataSource = DataSouce;
            gridProduct.DataBind();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            SearchGrid();
        }

        private void SearchGrid()
        {
            if (txtProductCode.Text.Equals(""))
            {
                gridProduct.DataSource = DataSouce;
            }
            else
            {
                gridProduct.DataSource = DataSouce.Where(x => x.PRODUCT_CODE.Contains(txtProductCode.Text)).ToList();
            }
            gridProduct.DataBind();
        }

        protected void gridProduct_EditCommand(object sender, System.Web.UI.WebControls.GridViewEditEventArgs e)
        {
            ViewState["proId"] = gridProduct.DataKeys[e.NewEditIndex].Values[0].ToString();
            InitialDataPopup();
            this.popup.Show();
        }

        protected void gridProduct_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gridProduct.PageIndex = e.NewPageIndex;
            gridProduct.DataBind();
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            txtProductCode.Text = "";
            SearchGrid();
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            ViewState["proId"] = null;
            InitialDataPopup();
            this.popup.Show();
        }

        private void InitialDataPopup()
        {
            var cmdCat = new CategoryService();
            var list = cmdCat.GetALL();
            foreach (var item in list)
            {
                ddlCategory.Items.Add(new ListItem(item.CATEGORY_NAME, item.CATEGORY_ID.ToString()));
            }
            var cmdPro = new ProductService();
            var listddlPakUDesc = cmdPro.GetUDescPacking();
            foreach (var item in listddlPakUDesc)
            {
                ddlPakUDesc.Items.Add(new ListItem(item, item));
            }

            var listddlPakPDesc = cmdPro.GetPDescPacking();
            foreach (var item in listddlPakPDesc)
            {
                ddlPakPDesc.Items.Add(new ListItem(item, item));
            }

            var cmd = new ZoneService();
            var listZone = cmd.GetALL();
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

            if (ViewState["proId"] != null)
            {
                _product = cmdPro.Select(Convert.ToInt32(ViewState["proId"].ToString()));
                popTxtProductCode.Text = _product.PRODUCT_CODE;
                poptxtProductName.Text = _product.PRODUCT_NAME;
                txtPacking.Text = _product.PRODUCT_PACKING_QTY.ToString();
                //txtPackingDesc.Text = _product.PRODUCT_PACKING_DESC;
                txtWeight.Text = _product.PRODUCT_WEIGHT.ToString();
                txtUnit.Text = _product.PRODUCT_WEIGHT_DEFINE;
                ddlCategory.SelectedValue = _product.CATEGORY_ID.ToString();
                ddlkind.SelectedValue = _product.PRODUCT_TYPE_CODE.ToString();
                //txtSize.Text = _product.PRODUCT_SIZE;
                ddlPakUDesc.SelectedValue = _product.PRODUCT_PACKING_PER_UDESC;
                ddlPakPDesc.SelectedValue = _product.PRODUCT_PACKING_PER_PDESC;
                var cmdPD = new ProductPriceListService();
                var listPD = cmdPD.Select(Convert.ToInt32(ViewState["proId"].ToString()));
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

                List<PRODUCT_PROMOTION> listPromotionItem = new List<PRODUCT_PROMOTION>();
                var cmdPromotion = new ProductPromotionService();
                DataSoucePromotion = cmdPromotion.GetALLIncludeZone(Convert.ToInt32(ViewState["proId"].ToString()));
                listPromotionItem.AddRange(DataSoucePromotion);

                listPromotionItem.AddRange(listPromotion);

                gridPromotion.DataSource = listPromotionItem;
                gridPromotion.DataBind();

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

           

            var cmd = new ProductService(obj);

            if (flag.Text.Equals("Add"))
            {
                obj.Action = ActionEnum.Create;
                obj.CREATE_DATE = DateTime.Now;
                obj.CREATE_EMPLOYEE_ID = 0;
                obj.UPDATE_DATE = DateTime.Now;
                obj.UPDATE_EMPLOYEE_ID = 0;
                obj.SYE_DEL = true;
                cmd.Add();
            }
            else
            {
                obj.Action = ActionEnum.Update;
                obj.PRODUCT_ID = Convert.ToInt32(ViewState["proId"].ToString());
                obj.UPDATE_DATE = DateTime.Now;
                obj.UPDATE_EMPLOYEE_ID = 0;
                obj.SYE_DEL = true;
                cmd.Edit();
            }

            if (FileUpload1.HasFile)
            {
                Stream fs = FileUpload1.PostedFile.InputStream;
                if (!System.IO.Directory.Exists(HttpContext.Current.Server.MapPath("~") + "ImageProduct"))
                    System.IO.Directory.CreateDirectory(HttpContext.Current.Server.MapPath("~") + "ImageProduct");
                obj.PRODUCT_IMAGE_PATH = "~/ImageProduct/" + obj.PRODUCT_ID + "." + FileUpload1.FileName.Split('.').ToArray()[1];
                FileUpload1.PostedFile.SaveAs(HttpContext.Current.Server.MapPath("~") + "ImageProduct\\" + obj.PRODUCT_ID + "." + FileUpload1.FileName.Split('.').ToArray()[1]);
            }
            cmd = new ProductService(obj);
            cmd.Edit();

            var listDetail = new List<PRODUCT_PRICELIST>();
            int i = 0;
            foreach (var item in DataSouceList)
            {
                var objDetail = new PRODUCT_PRICELIST();
                objDetail.ZONE_ID = item.ZONE_ID;
                objDetail.PRODUCT_PRICE = Convert.ToDecimal(((TextBox)(gridProductDetail.Rows[i++].Cells[2].FindControl("txtPrice"))).Text);
                objDetail.SYE_DEL = true;
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

            if (listPromotion.Count > 0)
            {
                foreach (PRODUCT_PROMOTION item in listPromotion)
                {
                    item.PRODUCT_ID = obj.PRODUCT_ID;
                }
                var cmdPromotion = new ProductPromotionService(listPromotion);
                cmdPromotion.AddList();
            }

            var cmdDetail = new ProductPriceListService(listDetail);
            cmdDetail.AddUpdateList();
            ViewState["proId"] = null;
            Response.Redirect("SearchProduct.aspx");
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("SearchProduct.aspx");
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

        private void InitialDataPopupPromotion()
        {
            var cmdCat = new ZoneService();
            var list = cmdCat.GetALL();
            ddlZonePromotion.Items.Clear();
            foreach (var item in list)
            {
                ddlZonePromotion.Items.Add(new ListItem(item.ZONE_NAME, item.ZONE_ID.ToString()));
            }

            if (ViewState["PromotionId"] != null)
            {
                flag2.Text = "Edit";
                var cmdPro = new ProductPromotionService();
                PRODUCT_PROMOTION promotion = cmdPro.Select(Convert.ToInt32(ViewState["PromotionId"].ToString()));
                txtQty.Text = promotion.PRODUCT_CONDITION_QTY.ToString();
                txtFreeQty.Text = promotion.PRODUCT_FREE_QTY.ToString();
                ddlZonePromotion.SelectedValue = promotion.ZONE_ID.ToString();
            }
            else
            {
                txtQty.Text = "";
                txtFreeQty.Text = "";
                flag2.Text = "Add";
            }
        }

        protected void AddPromotion_Click(object sender, EventArgs e)
        {
            InitialDataPopupPromotion();
            this.popup2.Show();
        }

        protected void btnAddPromotion_Click(object sender, EventArgs e)
        {
            if (ViewState["PromotionId"] != null)
            {
                var cmd = new ProductPromotionService();
                PRODUCT_PROMOTION item = new PRODUCT_PROMOTION();
                item.PROMOTION_ID = Convert.ToInt32(ViewState["PromotionId"].ToString());
                item.PRODUCT_CONDITION_QTY = Convert.ToInt32(txtQty.Text);
                item.PRODUCT_FREE_QTY = Convert.ToInt32(txtFreeQty.Text);
                item.ZONE_ID = Convert.ToInt32(ddlZonePromotion.SelectedValue);
                item.UPDATE_DATE = DateTime.Now;
                item.UPDATE_EMPLOYEE_ID = 0;
                cmd.Edit(item);
            }
            else
            {
                var cmdZonePromotion = new ZoneService();
                PRODUCT_PROMOTION item = new PRODUCT_PROMOTION();
                item.Action = ActionEnum.Create;
                item.PRODUCT_CONDITION_QTY = Convert.ToInt32(txtQty.Text);
                item.PRODUCT_FREE_QTY = Convert.ToInt32(txtFreeQty.Text);
                item.ZONE_ID = Convert.ToInt32(ddlZonePromotion.SelectedValue);
                item.CREATE_DATE = DateTime.Now;
                item.CREATE_EMPLOYEE_ID = 0;
                item.UPDATE_DATE = DateTime.Now;
                item.UPDATE_EMPLOYEE_ID = 0;
                item.SYE_DEL = true;
                listPromotion.Add(item);
            }
            InitialDataPopup();
            this.popup.Show();
        }

        protected void btnCancelPromotion_Click(object sender, EventArgs e)
        {
            InitialDataPopup();
            this.popup.Show();
        }

        protected void gridPromotion_EditCommand(object sender, GridViewEditEventArgs e)
        {
            ViewState["PromotionId"] = gridPromotion.DataKeys[e.NewEditIndex].Value.ToString();
            InitialDataPopupPromotion();
            this.popup.Show();
        }
    }
}