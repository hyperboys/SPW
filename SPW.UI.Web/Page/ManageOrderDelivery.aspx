<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterPageMainAdmin.Master" AutoEventWireup="true" CodeBehind="ManageOrderDelivery.aspx.cs" Inherits="SPW.UI.Web.Page.ManageOrderDelivery" %>

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
    <h1 class="page-header">ตรวจสอบใบขึ้นของ</h1>
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
                    ข้อมูลใบแปะหน้า        
                </div>
                <div class="panel-body">
                    <div class="row">
                        <div class="col-lg-12">
                            <div class="form">
                                <div class="form-group">
                                    <%--first row--%>
                                    <div class="row">
                                        <div class="col-md-2">วันที่สั่งซื้อ</div>
                                        <div class="col-md-3">
                                            <div class='input-group date' id='datetimepicker1'>
                                                <asp:TextBox ID="txtStartDate" class="form-control datetimepicker" runat="server" Height="35px" placeholder="วันที่สิ้นสุด"></asp:TextBox>
                                                <span class="input-group-addon">
                                                    <span class="glyphicon glyphicon-calendar"></span>
                                                </span>
                                            </div>
                                        </div>
                                        <div class="col-md-2">ถึง</div>
                                        <div class="col-md-3">
                                            <div class='input-group date' id='Div1'>
                                                <asp:TextBox ID="txtEndDate" class="form-control datetimepicker" runat="server" Height="35px" placeholder="วันที่สิ้นสุด"></asp:TextBox>
                                                <span class="input-group-addon">
                                                    <span class="glyphicon glyphicon-calendar"></span>
                                                </span>
                                            </div>
                                        </div>
                                        <div class="col-md-2">
                                            <asp:Button ID="btnSearch" class="btn btn-primary" runat="server" Text="ค้นหา" Height="30px" Width="100px" OnClick="btnSearch_Click" />
                                        </div>
                                    </div>
                                    <%--second row--%>
                                    <div class="row">
                                        <div class="col-md-2">รถ</div>
                                        <div class="col-md-3">
                                            <asp:DropDownList ID="ddlVehicle" class="form-control" runat="server" Height="35px" Width="200px" AutoPostBack="True">
                                                <asp:ListItem Value="0">กรุณาเลือก</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-md-2">เลขที่ใบแปะหน้า</div>
                                        <div class="col-md-3">
                                            <asp:TextBox ID="txtVehicle" class="form-control" runat="server" Height="35px" placeholder="เลขที่ใบแปะหน้า"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-2">สถานะ</div>
                                        <div class="col-md-3">
                                            <asp:DropDownList ID="ddlStatus" class="form-control" runat="server" Height="35px" Width="200px" AutoPostBack="True">
                                                <asp:ListItem Value="30">ไม่สำเร็จ</asp:ListItem>
                                                <asp:ListItem Value="50">สำเร็จ</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-md-2"></div>
                                        <div class="col-md-3"></div>
                                    </div>
                                </div>
                            </div>
                            <!-- /.col-lg-6 (nested) -->
                        </div>
                        <!-- /.row (nested) -->
                    </div>
                    <div class="panel panel-primary">
                        <asp:GridView ID="gdvManageOrderHQ" runat="server" ForeColor="#507CD1" AutoGenerateColumns="False"
                            DataKeyNames="DELIND_ID" PageSize="20" Width="100%" EmptyDataText="ไม่พบข้อมูลใบแปะหน้า"
                            Style="text-align: center" CssClass="grid" OnRowCommand="gdvManageOrderHQ_RowCommand">
                            <AlternatingRowStyle BackColor="White" />
                            <Columns>
                                <asp:TemplateField HeaderText="ลำดับ" ItemStyle-Width="5%">
                                    <ItemTemplate>
                                        <%# Container.DataItemIndex + 1 %>
                                    </ItemTemplate>
                                    <ItemStyle BorderStyle="Solid" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="DELIND_CODE" HeaderText="เลขใบแปะหน้า" ItemStyle-Width="10%">
                                    <ItemStyle Width="10%"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="DELIND_DATE" DataFormatString="{0:dd/MM/yy}" ItemStyle-Width="10%" HeaderText="วันที่ทำรายการ">
                                    <ItemStyle Width="10%"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="VEHICLE.VEHICLE_REGNO" HeaderText="รถ" ItemStyle-Width="10%">
                                    <ItemStyle Width="10%"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="PROVINCE_NAME" HeaderText="จังหัวด" ItemStyle-Width="10%">
                                    <ItemStyle Width="10%"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField HeaderText="ราคารวม" DataField="TOTAL_PRICE" ItemStyle-Width="15%" ItemStyle-HorizontalAlign="Center">
                                    <ItemStyle Width="15%"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField HeaderText="น้ำหนักรวม" DataField="TOTAL_WEIGHT" ItemStyle-Width="15%" ItemStyle-HorizontalAlign="Center">
                                    <ItemStyle Width="15%"></ItemStyle>
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
                    <asp:PlaceHolder ID="PlaceHolder1" runat="server"></asp:PlaceHolder>

                    <!-- /.panel-body -->
                </div>
                <!-- /.panel -->
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
