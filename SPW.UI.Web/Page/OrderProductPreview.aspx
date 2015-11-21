<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterPageMainAdmin.Master" AutoEventWireup="true" CodeBehind="OrderProductPreview.aspx.cs" Inherits="SPW.UI.Web.Page.OrderProductPreview" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br />
    <br />
    <div class="alert alert-success" id="alert" style="height: 50px">
        <b>
            <div class='glyphicon glyphicon-ok'></div>
            <asp:Label ID="lblName" runat="server" Text="  สั่งซื้อสินค้าสำเร็จ"></asp:Label></b>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <style type="text/css">
        .right
        {
            text-align: right;
        }

        .grid td, .grid th
        {
            text-align: center;
        }

        .auto-style11
        {
            width: 5px;
        }

        .auto-style12
        {
            width: 81px;
        }

        .auto-style13
        {
            width: 429px;
        }

        .auto-style17
        {
            width: 81px;
            height: 48px;
        }

        .auto-style18
        {
            width: 5px;
            height: 48px;
        }

        .auto-style19
        {
            width: 429px;
            height: 48px;
        }

        .auto-style20
        {
            height: 40px;
        }

        .auto-style21
        {
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
                    </div>
                    <div class="panel panel-primary">
                        <asp:GridView ID="gridProduct" runat="server" ForeColor="#507CD1" AutoGenerateColumns="False"
                            DataKeyNames="ORDER_DETAIL_ID" PageSize="20" Width="100%" EmptyDataText="ไม่พบข้อมูลหมวดหมู่สินค้า"
                            Style="text-align: center" CssClass="grid">
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
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

