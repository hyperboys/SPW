﻿<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterPageMainAdmin.Master" AutoEventWireup="true" CodeBehind="OrderProductDetailSummary.aspx.cs" Inherits="SPW.UI.Web.Page.OrderProductDetailSummary" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1 class="page-header">
        <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Page/Order.aspx">สั่งสินค้า</asp:HyperLink>
        /
        <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl="~/Page/OrderProduct.aspx?id=17548">เลือกสินค้า</asp:HyperLink>
        /
        <asp:HyperLink ID="HyperLink3" runat="server" NavigateUrl="~/Page/OrderProductDetail.aspx">จัดการสินค้า</asp:HyperLink>
        /
        <asp:Label ID="lblName" runat="server" Text="ตรวจสอบสินค้า"></asp:Label></h1>
    <div class="alert alert-info" id="alert" runat="server" visible="false">
        <strong>สั่งซื้อสินค้าสำเร็จ Order Success</strong>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <style type="text/css">
        .right {
            text-align: right;
        }

        .grid td, .grid th {
            text-align: center;
        }

        .auto-style11 {
            width: 5px;
        }

        .auto-style12 {
            width: 81px;
        }

        .auto-style13 {
            width: 429px;
        }

        .auto-style17 {
            width: 81px;
            height: 48px;
        }

        .auto-style18 {
            width: 5px;
            height: 48px;
        }

        .auto-style19 {
            width: 429px;
            height: 48px;
        }

        .auto-style20 {
            height: 40px;
        }

        .auto-style21 {
            height: 48px;
        }
    </style>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="panel panel-primary">
                <div class="panel-heading">
                    ข้อมูลสั่งสินค้า          
                </div>
                <div class="panel-body">
                    <div class="row">
                        <div class="col-lg-6">
                            <div role="form">
                                <div class="form-group">
                                    <table style="width: 893px">
                                        <tr>
                                            <td class="auto-style17">รหัสร้าน</td>
                                            <td style="text-align: center" class="auto-style18">:</td>
                                            <td class="auto-style19">
                                                <asp:TextBox ID="txtStoreCode" class="form-control" runat="server" Height="30px" Width="125px" Enabled="false"></asp:TextBox>
                                            </td>
                                            <td class="auto-style21"></td>
                                            <td class="auto-style21"></td>
                                            <td class="auto-style21"></td>
                                        </tr>
                                        <tr>
                                            <td class="auto-style12">ชื่อร้านค้า</td>
                                            <td style="text-align: center" class="auto-style11">:</td>
                                            <td class="auto-style13">
                                                <asp:TextBox ID="txtStoreName" class="form-control" runat="server" Height="30px" Width="373px" Enabled="false"></asp:TextBox>
                                            </td>
                                            <td></td>
                                            <td></td>
                                            <td></td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                        </div>
                        <!-- /.col-lg-6 (nested) -->
                    </div>
                    <!-- /.row (nested) -->
                    <div class="panel panel-primary">
                        <asp:GridView ID="gridProduct" runat="server" ForeColor="#507CD1" AutoGenerateColumns="False"
                            DataKeyNames="ORDER_DETAIL_ID" PageSize="20" Width="100%" EmptyDataText="ไม่พบข้อมูลหมวดหมู่สินค้า"
                            Style="text-align: center" CssClass="grid" OnRowEditing="gridProduct_EditCommand">
                            <AlternatingRowStyle BackColor="White" />
                            <Columns>
                                <asp:BoundField DataField="ProductName" HeaderText="ชื่อสินค้า" ItemStyle-Width="40%" ItemStyle-HorizontalAlign="Center">
                                    <ItemStyle Width="40%"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="PRODUCT_PRICE" HeaderText="ราคาต่อหน่วย" ItemStyle-Width="15%" ItemStyle-HorizontalAlign="Center">
                                    <ItemStyle Width="15%"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="PRODUCT_QTY" HeaderText="จำนวน" ItemStyle-Width="15%" ItemStyle-HorizontalAlign="Center">
                                    <ItemStyle Width="15%"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="QTYFree" HeaderText="จำนวนฟรี" ItemStyle-Width="15%" ItemStyle-HorizontalAlign="Center">
                                    <ItemStyle Width="15%"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="PRODUCT_PRICE_TOTAL" HeaderText="ราคารวม" ItemStyle-Width="15%" ItemStyle-HorizontalAlign="Center">
                                    <ItemStyle Width="15%"></ItemStyle>
                                </asp:BoundField>
                                <asp:CommandField HeaderText="รายละเอียด" ControlStyle-CssClass="glyphicon glyphicon-list"
                                    ShowCancelButton="False" ShowEditButton="True" ItemStyle-Width="10%" HeaderStyle-HorizontalAlign="Center" CancelText="" DeleteText="" EditText="" InsertText="" NewText="" SelectText="" UpdateText="">
                                    <ControlStyle CssClass="glyphicon glyphicon-list" />
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle Width="10%"></ItemStyle>
                                </asp:CommandField>
                            </Columns>
                            <EditRowStyle BackColor="#2461BF" />
                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="#507CD1" ForeColor="White" />
                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                            <RowStyle BackColor="#EFF3FB" HorizontalAlign="Center" Height="20px" />
                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                            <SortedAscendingCellStyle BackColor="#F5F7FB" />
                            <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                            <SortedDescendingCellStyle BackColor="#E9EBEF" />
                            <SortedDescendingHeaderStyle BackColor="#4870BE" />
                            <PagerSettings Mode="NumericFirstLast" />
                        </asp:GridView>
                    </div>
                    <div class="row">
                        <table style="margin-right: 15px;" align="right">
                            <tr>
                                <td class="auto-style21" style="text-align: right">
                                    <h4>จำนวนชิ้นทั้งหมด :
                                        <asp:Label ID="sumQty" runat="server"></asp:Label>
                                        ชิ้น </h4>
                                </td>
                            </tr>
                            <tr>
                                <td class="auto-style21" style="text-align: right">
                                    <h4>ของแถมทั้งหมด :
                                        <asp:Label ID="sumQtyFree" runat="server"></asp:Label>
                                        ชิ้น </h4>
                                </td>
                            </tr>
                            <tr>
                                <td class="auto-style21" style="text-align: right">
                                    <h4>ราคารวมทั้งหมด :
                                        <asp:Label ID="sumPrice" runat="server"></asp:Label>
                                        บาท</h4>
                                </td>
                            </tr>
                        </table>
                        <asp:HiddenField ID="hfSumQty" runat="server" />
                        <asp:HiddenField ID="hfFlee" runat="server" />
                        <asp:HiddenField ID="hfTotalPrice" runat="server" />
                    </div>
                    <div style="margin-top: 10px;">
                        <table style="float: left">
                            <tr>
                                <td>
                                    <asp:LinkButton ID="btnNew" Text="เพิ่มการสั่งซื้อสินค้า" class="btn btn-primary" Height="30px" Width="125px" runat="server" PostBackUrl="~/Page/OrderProduct.aspx"></asp:LinkButton>
                                </td>
                                <td>&nbsp;&nbsp;</td>
                                <td></td>
                            </tr>
                        </table>
                        <table style="float: right">
                            <tr>
                                <td>&nbsp;</td>
                                <td>&nbsp;&nbsp;</td>
                                <td>
                                    <asp:Button ID="btnSave" runat="server" Text="ยืนยันการสั่งซื้อ" class="btn btn-primary" Height="30px" Width="125px" OnClick="btnSave_Click" />

                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </div>
            <asp:Panel ID="Panel1" runat="server" Visible="True">
                <div class="panel panel-primary">
                    <div class="panel-heading">
                        <asp:Label ID="lblProduct" runat="server" Text="XXX"></asp:Label>
                    </div>
                    <div class="panel-body">
                        <div class="row">
                            <div class="col-lg-6">
                                <div class="form">
                                    <div class="form-group">
                                        <table style="width: 688px; height: 130px;">
                                            <tr>
                                                <td class="auto-style32">รหัสสินค้า</td>
                                                <td style="text-align: center" class="auto-style41">:</td>
                                                <td class="auto-style37">
                                                    <asp:Label ID="lblTxtProductCode" runat="server" Text="XXXXX"></asp:Label>
                                                </td>
                                                <td class="auto-style35"></td>
                                                <td class="auto-style36">ราคา</td>
                                                <td style="text-align: center" class="auto-style41">:</td>
                                                <td class="auto-style37">
                                                    <asp:Label ID="lblPriceProduct" runat="server" Text="XXXX"></asp:Label>
                                                </td>
                                                <td class="auto-style37"></td>
                                            </tr>
                                            <tr>
                                                <td class="auto-style27">ลวดลายสินค้า</td>
                                                <td style="text-align: center" class="auto-style42">:</td>
                                                <td class="auto-style39">
                                                    <asp:DropDownList ID="ddlColorType" class="form-control" runat="server" Height="35px" Width="200px" Enabled="False" SelectedValue='<%# Eval("Key") %>'>
                                                    </asp:DropDownList>
                                                </td>
                                                <td class="auto-style30"></td>
                                                <td class="auto-style31">สีของสินค้า</td>
                                                <td style="text-align: center" class="auto-style42">:</td>
                                                <td class="auto-style39">
                                                    <asp:DropDownList ID="ddlColor" class="form-control" runat="server" Height="35px" Width="200px" Enabled="False" SelectedValue='<%# Eval("Key") %>'>
                                                    </asp:DropDownList>
                                                </td>
                                                <td class="auto-style39"></td>
                                            </tr>
                                            <tr>
                                                <td class="auto-style20">จำนวน</td>
                                                <td style="text-align: center" class="auto-style43">:</td>
                                                <td class="auto-style40">
                                                    <table>
                                                        <tr>
                                                            <td>
                                                                <asp:TextBox ID="txtQty" class="form-control" runat="server" Height="30px" Width="125px" Enabled="False"></asp:TextBox>

                                                            </td>
                                                            <td>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="กรุณากรอกจำนวน"
                                                                    ValidationGroup="group" ControlToValidate="txtQty" Style="color: #FF0000; font-size: large;">*</asp:RequiredFieldValidator>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td class="auto-style23"></td>
                                                <td class="auto-style22">
                                                    <asp:Label ID="lblPacking" runat="server" Text="XXXX"></asp:Label>
                                                </td>
                                                <td style="text-align: center" class="auto-style43"></td>
                                                <td class="auto-style40"></td>
                                                <td class="auto-style40"></td>
                                            </tr>
                                        </table>
                                        <table style="width: 200px; height: 38px; margin-top: 20px; margin-left: 480px;">
                                            <tr>
                                                <td style="width: 200px;"></td>
                                                <td>
                                                    <asp:Button ID="btnCancelD" runat="server" Text="ยกเลิก" class="btn btn-primary" Height="30px" Width="100px" OnClick="btnCancelD_Click" />
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                    <div>
                                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" Style="color: #FF0000; font-size: large;"
                                            ValidationGroup="group" ShowMessageBox="True" ShowSummary="False" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </asp:Panel>
            <asp:LinkButton ID="lnkFake" runat="server"></asp:LinkButton>
            <ajax:ModalPopupExtender ID="popup" runat="server" BackgroundCssClass="modalBackground" DropShadow="false" PopupControlID="Panel1" TargetControlID="lnkFake" Enabled="True">
            </ajax:ModalPopupExtender>
            <ajax:ConfirmButtonExtender ID="btnSave_ConfirmButtonExtender" runat="server"
                ConfirmText="ต้องการจะบันทึกหรือไม่" Enabled="True" TargetControlID="btnSave">
            </ajax:ConfirmButtonExtender>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
