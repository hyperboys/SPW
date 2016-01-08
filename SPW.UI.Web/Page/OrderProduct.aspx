﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPageMainAdmin.Master"
    EnableEventValidation="false" AutoEventWireup="true" CodeBehind="OrderProduct.aspx.cs" Inherits="SPW.UI.Web.Page.OrderProduct" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1 class="page-header">
        <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Page/Order.aspx">สั่งสินค้า</asp:HyperLink>
        /
        <asp:Label ID="lblName" runat="server" Text="เลือกสินค้า"></asp:Label></h1>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">

    <style type="text/css">
        .right {
            text-align: right;
        }

        .auto-style5 {
            width: 13px;
        }

        .auto-style10 {
            width: 92px;
        }

        .auto-style15 {
            width: 230px;
        }

        .auto-style16 {
            width: 16px;
        }

        .auto-style17 {
            width: 98px;
        }

        .auto-style18 {
            width: 10px;
        }

        .auto-style19 {
            width: 506px;
        }

        .auto-style22 {
            width: 72px;
            height: 52px;
        }

        .auto-style23 {
            width: 19px;
            height: 52px;
        }

        .auto-style24 {
            width: 33px;
        }

        .auto-style25 {
            width: 473px;
        }

        .auto-style27 {
            width: 84px;
            height: 48px;
        }

        .auto-style30 {
            width: 19px;
            height: 48px;
        }

        .auto-style31 {
            width: 72px;
            height: 48px;
        }

        .auto-style32 {
            width: 84px;
            height: 47px;
        }

        .auto-style35 {
            width: 19px;
            height: 47px;
        }

        .auto-style36 {
            width: 72px;
            height: 47px;
        }

        .auto-style37 {
            height: 47px;
        }

        .auto-style39 {
            height: 48px;
        }

        .auto-style41 {
            width: 16px;
            height: 47px;
        }

        .auto-style42 {
            width: 16px;
            height: 48px;
        }

        .auto-style43 {
            width: 16px;
            height: 52px;
        }

        .auto-style44 {
            width: 87px;
        }

        .auto-style45 {
            width: 190px;
        }

        .auto-style47 {
            width: 41px;
        }

        .auto-style48 {
            width: 80px;
            height: 52px;
        }

        .auto-style49 {
            height: 47px;
            width: 226px;
        }

        .auto-style50 {
            height: 48px;
            width: 226px;
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
                            <div class="form">
                                <div class="form-group">
                                    <table style="width: 888px; height: 80px;">
                                        <tr>
                                            <td class="auto-style10">รหัสร้าน</td>
                                            <td style="text-align: center" class="auto-style5">:</td>
                                            <td class="auto-style19">
                                                <asp:TextBox ID="txtStoreCode" class="form-control" runat="server" Height="30px" Width="125px" Enabled="false"></asp:TextBox>
                                            </td>
                                            <td>
                                                <%--<asp:ImageButton ID="btnSave" runat="server"  Height="24px" ImageUrl="~/Image/Icon/order.jpg" Width="41px" Enabled="False" PostBackUrl="~/Page/OrderProductDetail.aspx" />--%>
                                                <%--<asp:Button ID="btnSave" runat="server" Text="ตรวจสอบรายการ" class="btn btn-primary" Height="30px" Width="125px" OnClick="btnSave_Click" Visible="False" />--%>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label2" runat="server" Text="จำนวนรวม "></asp:Label>
                                                <asp:Label ID="lblAmount" runat="server" Text="0 "></asp:Label>
                                                <asp:Label ID="Label5" runat="server" Text=" รายการ"></asp:Label>
                                            </td>
                                            <td>แสดงรายการสั่งซื้อ
                                        <asp:LinkButton ID="lbtnCart" Enabled="false" runat="server" PostBackUrl="~/Page/OrderProductDetail.aspx"><div class='glyphicon glyphicon-shopping-cart'></div></asp:LinkButton>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="auto-style10">ชื่อร้านค้า</td>
                                            <td style="text-align: center" class="auto-style5">:</td>
                                            <td class="auto-style19">
                                                <asp:TextBox ID="txtStoreName" class="form-control" runat="server" Height="30px" Width="373px" Enabled="false"></asp:TextBox>
                                            </td>
                                            <td></td>
                                            <td>
                                                <asp:Label ID="Label1" runat="server" Text="ราคารวม "></asp:Label>
                                                <asp:Label ID="lblPrice" runat="server" Text="0 "></asp:Label>
                                                <asp:Label ID="Label3" runat="server" Text=" บาท"></asp:Label>
                                            </td>
                                            <td></td>
                                        </tr>
                                    </table>
                                    <table style="width: 867px">
                                        <tr>
                                            <td class="auto-style17">ประเภทสินค้า</td>
                                            <td class="auto-style1" style="text-align: center">:</td>
                                            <td class="auto-style15">
                                                <asp:DropDownList ID="ddlCategory" class="form-control" runat="server" Height="35px" Width="200px" SelectedValue='<%# Eval("Key") %>'>
                                                    <asp:ListItem Value="0">กรุณาเลือก</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td class="auto-style18" style="text-align: center">&nbsp;</td>
                                            <td>รหัสสินค้า
                                            </td>
                                            <td class="auto-style1" style="text-align: center">:</td>
                                            <td>
                                                <asp:TextBox ID="txtProductCode" Height="30px" Width="200px" class="form-control" runat="server"></asp:TextBox>
                                            </td>
                                            <td class="auto-style16" style="text-align: center">
                                                <asp:Button ID="btnSearch" class="btn btn-primary" runat="server" Text="ค้นหา" OnClick="btnSearch_Click" Height="30px" Width="70px" />
                                            </td>
                                            <td class="auto-style44"></td>
                                            <td class="auto-style16"></td>
                                            <td>
                                                <%--<asp:Button ID="btnSave" runat="server" Text="ตรวจสอบรายการ" class="btn btn-primary" Height="30px" Width="125px" OnClick="btnSave_Click" Visible="False" />--%>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="panel panel-primary">
                        <asp:GridView ID="gridProduct" runat="server" ForeColor="#507CD1" AutoGenerateColumns="False"
                            DataKeyNames="PRODUCT_ID" PageSize="20" Width="100%" EmptyDataText="ไม่พบข้อมูลสินค้า" Style="text-align: center" CssClass="grid" OnRowEditing="gridProduct_EditCommand">
                            <AlternatingRowStyle BackColor="White" />
                            <Columns>
                                <asp:TemplateField HeaderText="No" ItemStyle-Width="50px">
                                    <ItemTemplate>
                                        <%# Container.DataItemIndex + 1 %>
                                    </ItemTemplate>
                                    <ItemStyle Width="50px" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="PRODUCT_CODE" HeaderText="รหัสสินค้า" ItemStyle-Width="35%" ItemStyle-HorizontalAlign="Center" ItemStyle-Height="30px" HeaderStyle-Height="35px">
                                    <HeaderStyle Height="35px" />
                                    <ItemStyle Width="35%"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="PRODUCT_NAME" HeaderText="ชื่อสินค้า" ItemStyle-Width="35%" ItemStyle-HorizontalAlign="Center">
                                    <ItemStyle Width="35%"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="PRICE" HeaderText="ราคาสินค้า" ItemStyle-Width="20%" ItemStyle-HorizontalAlign="Center">
                                    <ItemStyle Width="20%"></ItemStyle>
                                </asp:BoundField>
                                <%--<asp:TemplateField HeaderText="สั่งซื้อ">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lbtnCart" runat="server">
                                    <div class='glyphicon glyphicon-shopping-cart'></div>
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
                                <asp:CommandField HeaderText="สั่งซื้อ" ControlStyle-CssClass="glyphicon glyphicon-shopping-cart"
                                    ShowCancelButton="False" ShowEditButton="True" ItemStyle-Width="10%" HeaderStyle-HorizontalAlign="Center" CancelText="" DeleteText="" EditText="" InsertText="" NewText="" SelectText="" UpdateText="">
                                    <ControlStyle CssClass="glyphicon glyphicon-shopping-cart" />
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
                    <div>
                        <asp:PlaceHolder ID="PlaceHolder1" runat="server"></asp:PlaceHolder>
                    </div>
                </div>
            </div>

            <asp:Panel ID="Panel1" runat="server" Visible="True" DefaultButton="btnAdd">
                <div class="panel panel-primary">
                    <div class="panel-heading">
                        <asp:Label ID="lblProduct" runat="server" Text="XXX"></asp:Label>
                    </div>
                    <div class="panel-body">
                        <div class="row">
                            <div class="col-lg-6">
                                <div class="form">
                                    <div class="form-group">
                                        <table style="width: 783px; height: 120px;">
                                            <tr>
                                                <td class="auto-style32">รหัสสินค้า</td>
                                                <td style="text-align: center" class="auto-style41">:</td>
                                                <td class="auto-style49">
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
                                                <td class="auto-style50">
                                                    <asp:DropDownList ID="ddlColorType" class="form-control" runat="server" Height="35px" Width="200px" SelectedValue='<%# Eval("Key") %>'>
                                                    </asp:DropDownList>
                                                </td>
                                                <td class="auto-style30"></td>
                                                <td class="auto-style31">สีของสินค้า</td>
                                                <td style="text-align: center" class="auto-style42">:</td>
                                                <td class="auto-style39">
                                                    <asp:DropDownList ID="ddlColor" class="form-control" runat="server" Height="35px" Width="200px" SelectedValue='<%# Eval("Key") %>'>
                                                    </asp:DropDownList>
                                                </td>
                                                <td class="auto-style39"></td>
                                            </tr>
                                        </table>
                                        <table style="width: 783px; height: 60px;">
                                            <tr>
                                                <td class="auto-style48">จำนวน</td>
                                                <td class="auto-style43" style="text-align: center">:</td>
                                                <td class="auto-style45">
                                                    <asp:TextBox ID="txtQty" runat="server" class="form-control" Height="28px" ondrop="return false;" onkeypress="return IsNumeric(event);" onpaste="return false;" Width="125px" MaxLength="4"></asp:TextBox>
                                                    <span id="error" style="color: Red; display: none">* กรุณากรอก (0 - 9)</span>
                                                    <script type="text/javascript">
                                                        var specialKeys = new Array();
                                                        specialKeys.push(8); //Backspace
                                                        function IsNumeric(e) {
                                                            var keyCode = e.which ? e.which : e.keyCode
                                                             var ret = ((keyCode >= 48 && keyCode <= 57) || specialKeys.indexOf(keyCode) != -1);
                                                             document.getElementById("error").style.display = ret ? "none" : "inline";
                                                             return ret;
                                                         }
                                                    </script>
                                                </td>
                                                <td class="auto-style47"></td>
                                                <td class="auto-style23"></td>
                                                <td class="auto-style22">
                                                    <asp:Label ID="lblPacking" runat="server" Text="XXXX"></asp:Label>
                                                </td>
                                                <td></td>
                                            </tr>
                                            <tr>
                                                <td></td>
                                                <td></td>
                                                <td>
                                                      <asp:Label ID="lblError" runat="server" Text="" style="color: #FF0000"></asp:Label>
                                                </td>
                                                <td>&nbsp;</td>
                                                <td></td>
                                                <td></td>
                                                <td></td>
                                            </tr>
                                        </table>
                                        <table style="width: 697px; height: 38px; margin-top: 20px;">
                                            <tr>
                                                <td></td>
                                                <td class="auto-style25"></td>
                                                <td>
                                                    <asp:Button ID="btnAdd" runat="server" Text="ตกลง" class="btn btn-primary" Height="30px" Width="100px" OnClick="btnAdd_Click" />
                                                </td>
                                                <td class="auto-style24"></td>
                                                <td>
                                                    <asp:Button ID="btnCancel" runat="server" Text="ยกเลิก" class="btn btn-primary" Height="30px" Width="100px" OnClick="btnCancel_Click" />
                                                </td>
                                            </tr>
                                        </table>
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
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
