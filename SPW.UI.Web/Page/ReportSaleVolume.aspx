<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterPageMainAdmin.Master" AutoEventWireup="true"
    CodeBehind="ReportSaleVolume.aspx.cs" Inherits="SPW.UI.Web.Page.ReportSaleVolume" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1 class="page-header">รายงานสรุปยอดขายราย Item (ต่อภาค / ต่อเซลล์)</h1>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <style type="text/css">
        .right {
            text-align: right;
        }

        .row {
            margin-top: 5px;
        }
    </style>

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="panel panel-primary">
                <div class="panel-heading">
                    ข้อมูลการค้นหา        
                </div>
                <div class="panel-body">
                    <div class="row">
                        <div class="col-lg-12">
                            <div class="form">
                                <div class="form-group">
                                    <table>
                                        <tr>
                                            <td class="col-md-2">รหัสสินค้า</td>
                                            <td class="col-md-3">
                                                <asp:TextBox ID="txtProductCode" class="form-control" runat="server" Width="200px" placeholder="รหัสสินค้า"></asp:TextBox>
                                            </td>
                                            <td class="col-md-2">ชื่อสินค้า</td>
                                            <td class="col-md-3">
                                                <asp:TextBox ID="txtProductName" class="form-control" runat="server" Width="200px" placeholder="ชื่อสินค้า"></asp:TextBox>
                                            </td>
                                            <td></td>
                                            <td class="col-md-3">
                                                <asp:Button ID="btnSearch" class="btn btn-primary" runat="server" Text="ค้นหา" Height="30px" Width="70px" OnClick="btnSearch_Click" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="col-md-2">ภาค</td>
                                            <td class="col-md-3">
                                                <asp:DropDownList ID="ddlSector" class="form-control" runat="server" Height="35px" Width="200px" CssClass="form-control" SelectedValue='<%# Eval("Key") %>'>
                                                    <asp:ListItem Value="0">กรุณาเลือก</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td class="col-md-2">เซล์ล</td>
                                            <td class="col-md-3">
                                                <asp:DropDownList ID="ddlSale" class="form-control" runat="server" Height="35px" Width="200px" CssClass="form-control" SelectedValue='<%# Eval("Key") %>'>
                                                    <asp:ListItem Value="0">กรุณาเลือก</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td></td>
                                            <td class="col-md-3">
                                                <asp:Button ID="btnPrint" class="btn btn-primary" runat="server" Text="พิมพ์" Height="30px" Width="70px" OnClick="btnPrint_Click" />

                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                        </div>
                    </div>
                    <asp:GridView ID="gridProduct" runat="server" ForeColor="#507CD1" AutoGenerateColumns="False"
                        DataKeyNames="" PageSize="20" Width="100%" EmptyDataText="ไม่พบข้อมูลสินค้า" PageIndex="10"
                        Style="text-align: center" CssClass="grid">
                        <AlternatingRowStyle BackColor="White" />
                        <Columns>
                            <asp:BoundField DataField="PRODUCT_CODE" HeaderText="รหัสสินค้า" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center">
                                <ItemStyle Width="10%"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="PRODUCT_NAME" HeaderText="ชื่อสินค้า" ItemStyle-Width="20%" ItemStyle-HorizontalAlign="Center">
                                <ItemStyle Width="20%"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="SECTOR_NAME" HeaderText="ภาค" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center">
                                <ItemStyle Width="10%"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="EMPLOYEE_NAME" HeaderText="เซลล์" ItemStyle-Width="20%" ItemStyle-HorizontalAlign="Center">
                                <ItemStyle Width="20%"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="SUMQTY" HeaderText="จำนวนชิ้น" ItemStyle-Width="20%" ItemStyle-HorizontalAlign="Center">
                                <ItemStyle Width="20%"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="SUMAMT" HeaderText="จำนวนเงิน" ItemStyle-Width="20%" ItemStyle-HorizontalAlign="Center">
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
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
