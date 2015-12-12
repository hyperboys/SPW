<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterPageMainAdmin.Master" AutoEventWireup="true" CodeBehind="ManageOrderDeliveryPreview.aspx.cs" Inherits="SPW.UI.Web.Page.ManageOrderDeliveryPreview" %>

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
    <br />
    <br />
    <div class="alert alert-success" id="alert" style="height: 50px">
        <b>
            <div class='glyphicon glyphicon-ok'></div>
            <asp:Label ID="lblName" runat="server" Text="  ตรวจสอบการสั่งซื้อสินค้าสำเร็จ"></asp:Label></b>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <style type="text/css">
        .auto-style1 {
            width: 18px;
        }

        .right {
            text-align: right;
        }

        .grid td, .grid th {
            text-align: center;
        }
    </style>
    <div class="panel panel-primary">
        <div class="panel-heading">
            ข้อมูลรายการสินค้า          
        </div>
        <div class="panel-body">
            <div class="row">
                <div class="col-lg-6">
                    <div class="form">
                        <div class="form-group">
                            <table style="width: 466px">
                                <tr>
                                    <td>ทะเบียนรถยนต์</td>
                                    <td class="auto-style1" style="text-align: center">:</td>
                                    <td>
                                        <asp:TextBox ID="txtVehicle" class="form-control" runat="server" Text="XX-0000" Height="30px" Width="200px" Enabled="False" />
                                    </td>
                                    <td></td>
                                    <td>&nbsp;</td>
                                    <td></td>
                                    <td>&nbsp;</td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </div>
                <!-- /.col-lg-6 (nested) -->
            </div>
            <div class="panel panel-primary">
                <asp:GridView ID="grideInOrder" runat="server" ForeColor="#507CD1" AutoGenerateColumns="False"
                    DataKeyNames="PRODUCT_ID" PageSize="20" Width="100%" EmptyDataText="ไม่พบข้อมูลร้านค้า"
                    Style="text-align: center" CssClass="grid">
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:BoundField DataField="PRODUCT.PRODUCT_NAME" HeaderText="ชื่อสินค้า" ItemStyle-Width="30%" ItemStyle-HorizontalAlign="Center">
                            <ItemStyle Width="30%"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="PRODUCT_SENT_QTY" HeaderText="จำนวน" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center">
                            <ItemStyle Width="10%"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="PRODUCT_PRICE" HeaderText="ราคาต่อหน่วย" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center">
                            <ItemStyle Width="10%"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="PRODUCT_WEIGHT" HeaderText="น้ำหนักต่อหน่วย" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center">
                            <ItemStyle Width="10%"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="PRODUCT_PRICE_TOTAL" HeaderText="ราคารวม" ItemStyle-Width="20%" ItemStyle-HorizontalAlign="Center">
                            <ItemStyle Width="20%"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="PRODUCT_WEIGHT_TOTAL" HeaderText="น้ำหนักรวม" ItemStyle-Width="20%" ItemStyle-HorizontalAlign="Center">
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
            <%--<div class="row" style="margin-top: 20px;">
                <div class="col-lg-6">
                    <div role="form">
                        <div class="form-group">
                            <table style="width: 893px">
                                <tr>
                                    <td class="auto-style21" style="text-align: right">
                                        <asp:Label ID="lb_TotalWeight" runat="server" Text="น้ำหนักรวม 0 กก." Font-Bold="True"></asp:Label>
                                        <b>&nbsp;</b></td>
                                </tr>
                                <tr>
                                    <td class="auto-style21" style="text-align: right">
                                        <asp:Label ID="lb_TotalAmount" runat="server" Font-Bold="True" Text="ราคารวม 0 บาท"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </div>
            </div>--%>
            <div class="row">
                <table style="margin-right: 15px;" align="right">
                    <tr>
                        <td class="auto-style21" style="text-align: right">
                            <h4>น้ำหนักรวม :
                                        <asp:Label ID="lb_TotalWeight" runat="server"></asp:Label>
                                กก. </h4>
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style21" style="text-align: right">
                            <h4>ราคารวม :
                                        <asp:Label ID="lb_TotalAmount" runat="server"></asp:Label>
                                บาท </h4>
                        </td>
                    </tr>
                </table>
                <asp:HiddenField ID="hfSumQty" runat="server" />
                <asp:HiddenField ID="hfFlee" runat="server" />
                <asp:HiddenField ID="hfTotalPrice" runat="server" />
            </div>
            <div style="margin-top: 10px;">
                <table style="float: left;">
                    <tr>
                        <td></td>
                    </tr>
                </table>
                <asp:Button ID="btn_Return" class="btn btn-primary" runat="server" Text="ย้อนกลับ" Height="30px" Width="120px" OnClick="btnNext_Click1" />
            </div>
        </div>
    </div>
</asp:Content>
