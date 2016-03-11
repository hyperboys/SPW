<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterPageMainAdmin.Master" AutoEventWireup="true" CodeBehind="ManageOrderHQPrint.aspx.cs" Inherits="SPW.UI.Web.Page.ManageOrderHQPrint" %>

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
    <h1 class="page-header">พิมพ์ใบสั่งซื้อ</h1>
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
            width: 67px;
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
                    จัดการใบสั่งซื้อ        
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
                                        <div class="col-md-2"></div>
                                    </div>
                                    <%--second row--%>
                                    <div class="row">
                                        <div class="col-md-2">จังหวัด</div>
                                        <div class="col-md-3">
                                            <asp:DropDownList ID="ddlProvince" class="form-control" runat="server" Height="35px" Width="200px" AutoPostBack="True" SelectedValue='<%# Eval("Key") %>'>
                                                <asp:ListItem Value="0">กรุณาเลือก</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-md-2">สายจัดรถ</div>
                                        <div class="col-md-3">
                                            <asp:DropDownList ID="ddlTranspot" class="form-control" runat="server" Height="35px" Width="200px" SelectedValue='<%# Eval("Key") %>'>
                                                <asp:ListItem Value="0">กรุณาเลือก</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-md-2">
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-2">รหัสร้าน</div>
                                        <div class="col-md-3">
                                            <asp:TextBox ID="txtStoreCode" class="form-control" runat="server" Height="35px" placeholder="รหัสร้าน"></asp:TextBox>
                                        </div>
                                        <div class="col-md-2">
                                            <asp:Button ID="btnPrint" class="btn btn-primary" runat="server" Text="พิมพ์" OnClick="btnPrint_Click" Height="30px" Width="70px" />
                                        </div>
                                        <div class="col-md-3">
                                        </div>
                                        <div class="col-md-2">
                                        </div>
                                    </div>
                                    <%--second row--%>
                                    <div class="row">
                                        <div class="col-md-2">
                                            <asp:Label ID="lblError" runat="server" ForeColor="Red"></asp:Label>
                                        </div>
                                        <div class="col-md-3"></div>
                                        <div class="col-md-2"></div>
                                        <div class="col-md-3"></div>
                                        <div class="col-md-2"></div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <!-- /.col-lg-6 (nested) -->
                    </div>
                </div>
                <!-- /.panel-body -->
            </div>
            <!-- /.panel -->
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
