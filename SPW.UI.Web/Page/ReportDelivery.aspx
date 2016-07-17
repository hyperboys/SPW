<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPageMainAdmin.Master"
    AutoEventWireup="true" CodeBehind="ReportDelivery.aspx.cs" Inherits="SPW.UI.Web.Page.ReportDelivery" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
    <script type="text/javascript" src="../JQuery/bootstrap-datepicker.js"></script>
    <link href="../CSS/datepicker.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        //แก้ไขตอนที่ใส่ update panel แล้ว datetimepicker ไม่ทำงาน
        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);

            function EndRequestHandler(sender, args) {
                $('.datetimepicker').datepicker({
                    format: 'dd/mm/yyyy'
                });
            }
        });
        $(function () {
            $('.datetimepicker').datepicker({
                format: 'dd/mm/yyyy'
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1 class="page-header">รายงานคงค้างส่ง</h1>
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
                                            <td class="col-md-2">วันที่เริ่มต้น</td>
                                            <td class="col-md-3">
                                                <div class='input-group date' id='datetimepicker1'>
                                                    <asp:TextBox ID="txtStartDate" class="form-control datetimepicker" runat="server" Height="35px" placeholder="วันที่เริ่มต้น"></asp:TextBox>
                                                    <span class="input-group-addon">
                                                        <span class="glyphicon glyphicon-calendar"></span>
                                                    </span>
                                                </div>
                                            </td>
                                            <td class="col-md-2">วันที่สิ้นสุด</td>
                                            <td class="col-md-3">
                                                <div class='input-group date' id='Div1'>
                                                    <asp:TextBox ID="txtEndDate" class="form-control datetimepicker" runat="server" Height="35px" placeholder="วันที่สิ้นสุด"></asp:TextBox>
                                                    <span class="input-group-addon">
                                                        <span class="glyphicon glyphicon-calendar"></span>
                                                    </span>
                                                </div>
                                            </td>
                                            <td></td>
                                            <td class="col-md-3"></td>
                                            <td style="width: 5px"></td>
                                            <td class="col-md-3"></td>
                                        </tr>
                                        <tr>
                                            <td class="col-md-2">รหัสสั่งซื้อสินค้า</td>
                                            <td class="col-md-3">
                                                <asp:TextBox ID="txtOrderCode" class="form-control" runat="server" Height="35px" placeholder="รหัสสั่งซื้อสินค้า"></asp:TextBox>
                                            </td>
                                            <td class="col-md-2">รหัสสินค้า</td>
                                            <td class="col-md-3">
                                                <asp:TextBox ID="txtProductCode" class="form-control" runat="server" Height="35px" placeholder="รหัสสินค้า"></asp:TextBox>
                                            </td>
                                            <td></td>
                                            <td class="col-md-3"></td>
                                            <td style="width: 5px"></td>
                                            <td class="col-md-3"></td>
                                        </tr>
                                        <tr>
                                            <td class="col-md-2">รหัสร้านค้า</td>
                                            <td class="col-md-3">
                                                <asp:TextBox ID="txtStoreCode" class="form-control" runat="server" Height="35px" placeholder="รหัสสั่งซื้อสินค้า"></asp:TextBox>
                                            </td>
                                            <td class="col-md-2">สถานะ</td>
                                            <td class="col-md-3">
                                                <asp:DropDownList ID="ddlStatus" class="form-control" runat="server" Height="35px" Width="200px" SelectedValue='<%# Eval("Key") %>'>
                                                    <asp:ListItem Value="0">กรุณาเลือก</asp:ListItem>
                                                    <asp:ListItem Value="12">ยกเลิก</asp:ListItem>
                                                    <asp:ListItem Value="11">สำเร็จ</asp:ListItem>
                                                    <asp:ListItem Value="10" Selected="True">ไม่สำเร็จ</asp:ListItem>
                                                </asp:DropDownList></td>
                                            <td></td>
                                            <td class="col-md-3"></td>
                                            <td style="width: 5px"></td>
                                            <td class="col-md-3">
                                                <asp:Button ID="btnSearch" class="btn btn-primary" runat="server" Text="ค้นหา" Height="30px" Width="70px" OnClick="btnSearch_Click" />
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                        </div>
                    </div>
                    <asp:GridView ID="gridProduct" runat="server" ForeColor="#507CD1" AutoGenerateColumns="False"
                        DataKeyNames="" PageSize="20" Width="100%" EmptyDataText="ไม่พบข้อมูลการสั่งซื้อสินค้า" PageIndex="10"
                        Style="text-align: center" CssClass="grid">
                        <AlternatingRowStyle BackColor="White" />
                        <Columns>
                            <asp:BoundField DataField="ORDER_CODE" HeaderText="รหัสสั่งซื้อสินค้า" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center">
                                <ItemStyle Width="10%"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="ORDER_DATE" HeaderText="วันที่สั่งสินค้า" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center">
                                <ItemStyle Width="10%"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="STORE_CODE" HeaderText="รหัสร้านค้า" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center">
                                <ItemStyle Width="10%"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="PRODUCT_NAME" HeaderText="ชื่อสินค้า" ItemStyle-Width="20%" ItemStyle-HorizontalAlign="Center">
                                <ItemStyle Width="20%"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="COLOR_TYPE_NAME" HeaderText="ลาย" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center">
                                <ItemStyle Width="10%"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="COLOR_NAME" HeaderText="สี" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center">
                                <ItemStyle Width="10%"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="PRODUCT_QTY" HeaderText="ยอดสั่ง" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center">
                                <ItemStyle Width="10%"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="PRODUCT_SEND_QTY" HeaderText="ยอดส่ง" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center">
                                <ItemStyle Width="10%"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="PRODUCT_SEND_REMAIN" HeaderText="ยอดสั่งคงเหลือ" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center">
                                <ItemStyle Width="10%"></ItemStyle>
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
