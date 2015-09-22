<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPageMainAdmin.Master" AutoEventWireup="true" CodeBehind="OrderProduct.aspx.cs" Inherits="SPW.UI.Web.Page.OrderProduct" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1 class="page-header">สั่งสินค้า</h1>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <style type="text/css">
        .right
        {
            text-align: right;
        }

        .auto-style5
        {
            width: 13px;
        }

        .auto-style10
        {
            width: 92px;
        }

        .auto-style15
        {
            width: 241px;
        }

        .auto-style16
        {
            width: 16px;
        }

        .auto-style17
        {
            width: 98px;
        }

        .auto-style18
        {
            width: 10px;
        }

        .auto-style19
        {
            width: 655px;
        }

        .auto-style20
        {
            width: 84px;
            height: 52px;
        }

        .auto-style22
        {
            width: 72px;
            height: 52px;
        }

        .auto-style23
        {
            width: 19px;
            height: 52px;
        }

        .auto-style24
        {
            width: 33px;
        }

        .auto-style25
        {
            width: 473px;
        }

        .auto-style26
        {
            width: 21px;
            height: 52px;
        }

        .auto-style27
        {
            width: 84px;
            height: 48px;
        }

        .auto-style28
        {
            width: 21px;
            height: 48px;
        }

        .auto-style30
        {
            width: 19px;
            height: 48px;
        }

        .auto-style31
        {
            width: 72px;
            height: 48px;
        }

        .auto-style32
        {
            width: 84px;
            height: 47px;
        }

        .auto-style33
        {
            width: 21px;
            height: 47px;
        }

        .auto-style35
        {
            width: 19px;
            height: 47px;
        }

        .auto-style36
        {
            width: 72px;
            height: 47px;
        }

        .auto-style37
        {
            height: 47px;
        }

        .auto-style39
        {
            height: 48px;
        }

        .auto-style40
        {
            height: 52px;
        }

        .auto-style41
        {
            width: 16px;
            height: 47px;
        }

        .auto-style42
        {
            width: 16px;
            height: 48px;
        }

        .auto-style43
        {
            width: 16px;
            height: 52px;
        }
    </style>
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
                                        <asp:LinkButton ID="linkToOrder" runat="server" Text="รายการสินค้าที่สั่งซื้อ" Visible="false"></asp:LinkButton>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="auto-style10">ชื่อร้านค้า</td>
                                    <td style="text-align: center" class="auto-style5">:</td>
                                    <td class="auto-style19">
                                        <asp:TextBox ID="txtStoreName" class="form-control" runat="server" Height="30px" Width="373px" Enabled="false"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label1" runat="server" Text="ราคารวม "></asp:Label>
                                        <asp:Label ID="lblPrice" runat="server" Text="0 "></asp:Label>
                                        <asp:Label ID="Label3" runat="server" Text=" บาท"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-lg-6">
                    <div class="form">
                        <div class="form-group">
                            <table style="width: 867px">
                                <tr>
                                    <td class="auto-style17">ประเภทสินค้า</td>
                                    <td class="auto-style1" style="text-align: center">:</td>
                                    <td class="auto-style15">
                                        <asp:DropDownList ID="ddlCategory" class="form-control" runat="server" Height="35px" Width="200px">
                                            <asp:ListItem Value="0">กรุณาเลือก</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td class="auto-style18" style="text-align: center">&nbsp;</td>
                                    <td>รหัสสินค้า
                                    </td>
                                    <td class="auto-style1" style="text-align: center">:</td>
                                    <td>
                                        <asp:TextBox ID="txtProductCode" class="form-control" runat="server"></asp:TextBox>
                                    </td>
                                    <td class="auto-style16" style="text-align: center"></td>
                                    <td>
                                        <asp:Button ID="btnSearch" class="btn btn-primary" runat="server" Text="ค้นหา" OnClick="btnSearch_Click" Height="30px" Width="70px" />
                                    </td>
                                    <td class="auto-style16"></td>
                                    <td>
                                        <asp:Button ID="btnSave" runat="server" Text="สั่งสินค้า" class="btn btn-primary" Height="30px" Width="100px" OnClick="btnSave_Click" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </div>
                <!-- /.col-lg-6 (nested) -->
            </div>
            <div class="panel panel-primary">
                <asp:GridView ID="gridProduct" runat="server" ForeColor="#507CD1" AutoGenerateColumns="False"
                    DataKeyNames="PRODUCT_ID" PageSize="20" Width="100%" EmptyDataText="ไม่พบข้อมูลสินค้า"
                    OnRowEditing="gridProduct_EditCommand"
                    Style="text-align: center" CssClass="grid">
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:CommandField ButtonType="Image" EditImageUrl="~/Image/Icon/Product.gif" HeaderText="สั่งซื้อ"
                            ShowCancelButton="False" ShowEditButton="True" ItemStyle-Width="10%" HeaderStyle-HorizontalAlign="Center">
                            <ItemStyle Width="10%"></ItemStyle>
                        </asp:CommandField>
                        <asp:BoundField DataField="PRODUCT_CODE" HeaderText="รหัสสินค้า" ItemStyle-Width="35%" ItemStyle-HorizontalAlign="Center">
                            <ItemStyle Width="35%"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="PRODUCT_NAME" HeaderText="ชื่อสินค้า" ItemStyle-Width="35%" ItemStyle-HorizontalAlign="Center">
                            <ItemStyle Width="35%"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="PRICE" HeaderText="ราคาสินค้า" ItemStyle-Width="20%" ItemStyle-HorizontalAlign="Center">
                            <ItemStyle Width="20%"></ItemStyle>
                        </asp:BoundField>
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
        </div>
        <!-- /.panel-body -->
    </div>
    <!-- /.panel -->
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
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
                                <table style="width: 688px; height: 141px;">
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
                                            <asp:DropDownList ID="ddlColorType" class="form-control" runat="server" Height="35px" Width="200px">
                                                <asp:ListItem Value="0">กรุณาเลือก</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td class="auto-style30"></td>
                                        <td class="auto-style31">สีของสินค้า</td>
                                        <td style="text-align: center" class="auto-style42">:</td>
                                        <td class="auto-style39">
                                            <asp:DropDownList ID="ddlColor" class="form-control" runat="server" Height="35px" Width="200px">
                                                <asp:ListItem Value="0">กรุณาเลือก</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td class="auto-style39"></td>
                                    </tr>
                                    <tr>
                                        <td class="auto-style20">จำนวน</td>
                                        <td style="text-align: center" class="auto-style43">:</td>
                                        <td class="auto-style40">
                                            <asp:TextBox ID="txtQty" class="form-control" runat="server" Height="30px" Width="125px"></asp:TextBox>
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

</asp:Content>
