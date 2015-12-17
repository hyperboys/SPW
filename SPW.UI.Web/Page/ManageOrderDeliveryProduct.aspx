<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterPageMainAdmin.Master" AutoEventWireup="true" CodeBehind="ManageOrderDeliveryProduct.aspx.cs" Inherits="SPW.UI.Web.Page.ManageOrderDeliveryProduct" %>

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
    <h1 class="page-header">
        <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl="~/Page/ManageOrderDelivery.aspx">ตรวจสอบใบขึ้นของ</asp:HyperLink>
        /
        <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Page/ManageOrderDeliveryDetail.aspx">รายละเอียดใบขึ้นของ</asp:HyperLink>
        /
        <asp:Label ID="lblName" runat="server" Text="รายละเอียดสินค้า"></asp:Label></h1>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <script type="text/javascript">
        function Confirm() {
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            if (confirm("คุณต้องการลบข้อมูลหรือไม่ ?")) {
                confirm_value.value = "Yes";
            } else {
                confirm_value.value = "No";
            }
            return confirm_value;
        }
    </script>
    <style type="text/css">
        .right {
            text-align: right;
        }

        .row {
            margin-top: 5px;
        }
    </style>
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="panel panel-primary">
                <div class="panel-heading">
                    จัดการใบขึ้นของ        
                </div>
                <div class="panel-body">
                    <div class="row">
                        <div class="col-lg-12">
                            <div class="form">
                                <div class="form-group">
                                    <%--first row--%>
                                    <div class="row">
                                        <div class="col-md-2">ทะเบียนรถยนต์</div>
                                        <div class="col-md-3">
                                            <asp:Label ID="lb_Vehicle" runat="server" Text="Label"></asp:Label>
                                        </div>
                                        <br></br>
                                        <div class="col-md-2">
                                            ชื่อร้าน
                                        </div>
                                        <div class="col-md-3">
                                            <asp:Label ID="lb_Store" runat="server" Text="Label"></asp:Label>
                                        </div>
                                        <div class="col-md-2">
                                        </div>
                                        <br></br>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <!-- /.col-lg-6 (nested) -->
                    </div>
                    <!-- /.row (nested) -->
                    <div class="panel panel-primary">
                        <asp:GridView ID="gdvManageOrderHQ" runat="server" ForeColor="#507CD1" AutoGenerateColumns="False"
                            DataKeyNames="DELORDER_DETAIL_ID" PageSize="20" Width="100%" EmptyDataText="ไม่พบข้อมูลสั่งซื้อ"
                            Style="text-align: center" CssClass="grid">
                            <AlternatingRowStyle BackColor="White" />
                            <Columns>
                                <asp:TemplateField HeaderText="ลำดับ" ItemStyle-Width="3%">
                                    <ItemTemplate>
                                        <%# Container.DataItemIndex + 1 %>
                                    </ItemTemplate>
                                    <ItemStyle BorderStyle="Solid" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="ORDER_CODE" HeaderText="รหัสใบสั่งซื้อ" ItemStyle-Width="10%">
                                    <ItemStyle Width="10%"></ItemStyle>
                                </asp:BoundField>
                                <%--<asp:BoundField DataField="PRODUCT.PRODUCT_NAME" HeaderText="ชื่อสินค้า" ItemStyle-Width="20%">
                                    <ItemStyle Width="20%"></ItemStyle>
                                </asp:BoundField>--%>
                                <asp:BoundField DataField="ORDER_DETAIL.ColorDesc" HeaderText="ชื่อสินค้า" ItemStyle-Width="20%">
                                    <ItemStyle Width="20%"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="PRODUCT_PRICE" HeaderText="ราคาหน่วย" ItemStyle-Width="15%">
                                    <ItemStyle Width="15%"></ItemStyle>
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="จำนวน (สั่ง)" ItemStyle-Width="15%">
                                    <ItemTemplate>
                                        <%#String.Format("{0}",((Int32)Eval("PRODUCT_SENT_REMAIN"))+((Int32)Eval("PRODUCT_SENT_QTY")))%>
                                    </ItemTemplate>
                                    <ItemStyle BorderStyle="Solid" />
                                    <ItemStyle Width="15%"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="จำนวน (ส่ง)" ItemStyle-Width="15%">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtQty" class="form-control" ReadOnly='<%#ISEnabled()%>' runat="server" Height="30px" placeholder="จำนวน" Text='<%#Eval("PRODUCT_SENT_QTY")%>'></asp:TextBox>
                                        <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="txtQty" ErrorMessage="Digit Only and Not MoreThan Send Qty" ForeColor="Red" Operator="LessThanEqual" SetFocusOnError="True" Type="Integer" ValueToCompare='<%#String.Format("{0}",((Int32)Eval("PRODUCT_SENT_REMAIN"))+((Int32)Eval("PRODUCT_SENT_QTY")))%>'></asp:CompareValidator>
                                    </ItemTemplate>
                                    <ItemStyle Width="15%"></ItemStyle>
                                </asp:TemplateField>
                                <asp:BoundField DataField="PRODUCT_SENT_PRICE_TOTAL" HeaderText="ราคารวม" ItemStyle-Width="20%">
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
                    <table style="float: right">
                        <tr>
                            <td>&nbsp;</td>
                            <td>&nbsp;&nbsp;</td>
                            <td>
                                <asp:Label ID="lb_Exception" runat="server" Text="Label" ForeColor="Red" Visible="False"></asp:Label>
                                <asp:Button ID="btnConfirmDelOrder" runat="server" Text="ยืนยันจัดของ" class="btn btn-primary" Height="30px" Width="125px" OnClick="btnConfirmDelOrder_Click" />

                            </td>
                        </tr>
                    </table>
                    <asp:Button ID="btnConfirmDelOrder0" runat="server" class="btn btn-primary" Height="30px" PostBackUrl="~/Page/ManageOrderDeliveryDetail.aspx" Text="ย้อนกลับ" Width="125px" />
                    <asp:Button ID="btnConfirmZeroDelOrder" runat="server" class="btn btn-primary" Height="30px" OnClientClick="return confirm();" AlternateText="Yes" OnClick="btnConfirmZeroDelOrder_Click" Text="ไม่ทำการส่ง" Width="125px" />
                </div>
                <!-- /.panel-body -->
            </div>
            <!-- /.panel -->
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
