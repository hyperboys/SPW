<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterPageMainAdmin.Master" AutoEventWireup="true" CodeBehind="ManageOrderDeliveryDetail.aspx.cs" Inherits="SPW.UI.Web.Page.ManageOrderDeliveryDetail" %>

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
        <asp:Label ID="lblName" runat="server" Text="รายละเอียดใบขึ้นของ"></asp:Label></h1>
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

        .auto-style5 {
            width: 13px;
        }

        .auto-style10 {
            width: 67px;
        }

        .row {
            margin-top: 5px;
        }
    </style>
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

    <div class="panel panel-primary">
        <div class="panel-heading">
            <asp:Label ID="lb_Vehicle" runat="server" Text="ทะเบียนรถยนต์ : "></asp:Label>
        </div>
        <div class="panel-body">
            <!-- /.row (nested) -->
            <div class="panel panel-primary">
                <asp:GridView ID="gdvManageOrderHQ" runat="server" ForeColor="#507CD1" AutoGenerateColumns="False"
                    DataKeyNames="DELORDER_ID" PageSize="20" Width="100%" EmptyDataText="ไม่พบข้อมูลสั่งซื้อ"
                    Style="text-align: center" CssClass="grid" OnRowCommand="gdvManageOrderHQ_RowCommand">
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:TemplateField HeaderText="ลำดับ" ItemStyle-Width="5%">
                            <ItemTemplate>
                                <%# Container.DataItemIndex + 1 %>
                            </ItemTemplate>
                            <ItemStyle BorderStyle="Solid" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="STORE.STORE_CODE" HeaderText="รหัสร้าน" ItemStyle-Width="15%">
                            <ItemStyle Width="15%"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="STORE.STORE_NAME" HeaderText="ชื่อร้าน" ItemStyle-Width="35%">
                            <ItemStyle Width="35%"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="DELORDER_PRICE_TOTAL" HeaderText="ราคา" ItemStyle-Width="20%">
                            <ItemStyle Width="20%"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="DELORDER_WEIGHT_TOTAL" HeaderText="น้ำหนัก" ItemStyle-Width="20%">
                            <ItemStyle Width="20%"></ItemStyle>
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="ตรวจสอบ" ItemStyle-Width="5%">
                            <ItemTemplate>
                                <asp:LinkButton ID="lbtnDetail" runat="server" CommandArgument="<%#((GridViewRow)Container).RowIndex%>" CommandName="ViewDeliveryOrder">
                                    <div class='glyphicon glyphicon-search'></div>
                                </asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
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
                        <asp:Button ID="btnConfirmDelOrderIndex" runat="server" Text="ยืนยัน" class="btn btn-primary" Height="30px" Width="125px" OnClientClick="return confirm();" AlternateText="Yes" OnClick="btnConfirmDelOrder_Click" Enabled="False" />
                    </td>
                </tr>
            </table>
        </div>
        <!-- /.panel-body -->
    </div>
    <!-- /.panel -->
</asp:Content>
