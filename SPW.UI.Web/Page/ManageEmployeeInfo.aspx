<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterPageMainAdmin.Master" AutoEventWireup="true" CodeBehind="ManageEmployeeInfo.aspx.cs" Inherits="SPW.UI.Web.Page.ManageEmployeeInfo" %>

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
        <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Page/SearchEmployee.aspx">พนักงาน</asp:HyperLink>
        -
        <asp:Label ID="lblName" runat="server" Text="เพิ่มพนักงานใหม่"></asp:Label></h1>
    <div class="alert alert-info" id="alert" runat="server" visible="false">
        <strong><asp:Label ID="lblAlert" runat="server" Text="บันทึกข้อมูลสำเร็จ Save Success"></asp:Label></strong>
    </div>
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
   
            <div class="panel panel-primary">
                <div class="panel-heading">
                    ข้อมูลพนักงาน        
                </div>
                <div class="panel-body">
                    <div class="row">
                        <div class="col-lg-12">
                            <div class="form">
                                <div class="form-group">
                                    <div class="row">
                                        <div class="col-md-2">รหัสพนักงาน</div>
                                        <div class="col-md-3">
                                            <asp:TextBox ID="popTxtEmployeeCode" class="form-control" runat="server" Height="35px" Width="200px" placeholder="รหัสพนักงาน"></asp:TextBox>
                                        </div>
                                        <div class="col-md-2"></div>
                                        <div class="col-md-3">
                                        </div>
                                        <div class="col-md-2"></div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-2">ชื่อ</div>
                                        <div class="col-md-3">
                                            <asp:TextBox ID="txtName" class="form-control" runat="server" Height="35px" Width="200px" placeholder="ชื่อ"></asp:TextBox>
                                        </div>
                                        <div class="col-md-2">นามสกุล</div>
                                        <div class="col-md-3">
                                            <asp:TextBox ID="txtLastName" class="form-control" runat="server" Height="35px" Width="200px" placeholder="นามสกุล"></asp:TextBox>
                                        </div>
                                        <div class="col-md-2"></div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-2">เพศ</div>
                                        <div class="col-md-3">
                                            <asp:DropDownList ID="ddlSex" class="form-control" runat="server" Height="35px" Width="200px" AutoPostBack="True" SelectedValue='<%# Eval("Key") %>'>
                                                <asp:ListItem Value="1" Selected="True">ชาย</asp:ListItem>
                                                <asp:ListItem Value="2">หญิง</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-md-2">วันเกิด</div>
                                        <div class="col-md-3">
                                            <div class='input-group date' id='datetimepicker1'>
                                                <asp:TextBox ID="txtBDate" class="form-control datetimepicker" runat="server" Height="35px" placeholder="วันเกิด"></asp:TextBox>
                                                <span class="input-group-addon">
                                                    <span class="glyphicon glyphicon-calendar"></span>
                                                </span>
                                            </div>
                                        </div>
                                        <div class="col-md-2"></div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-2">สัญชาติ</div>
                                        <div class="col-md-3">
                                            <asp:TextBox ID="txtNationality" class="form-control" runat="server" Height="35px" Width="200px" placeholder="สัญชาติ"></asp:TextBox>

                                        </div>
                                        <div class="col-md-2">ศาสนา</div>
                                        <div class="col-md-3">
                                            <asp:TextBox ID="txtReligion" class="form-control" runat="server" Height="35px" Width="200px" placeholder="ศาสนา"></asp:TextBox>
                                        </div>
                                        <div class="col-md-2"></div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-2">ที่อยู่ที่ 1</div>
                                        <div class="col-md-3">
                                            <asp:TextBox ID="txtAddress1" class="form-control" runat="server" Height="100px" Width="620px" placeholder="ที่อยู่ที่ 1" TextMode="MultiLine"></asp:TextBox>

                                        </div>
                                        <div class="col-md-2"></div>
                                        <div class="col-md-3">
                                        </div>
                                        <div class="col-md-2"></div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-2">เบอร์โทรที่ 1</div>
                                        <div class="col-md-3">
                                            <asp:TextBox ID="txtPhone1" class="form-control" runat="server" Height="35px" Width="200px" placeholder="เบอร์โทรที่ 1"></asp:TextBox>
                                        </div>
                                        <div class="col-md-2">เบอร์โทรที่ 2</div>
                                        <div class="col-md-3">
                                            <asp:TextBox ID="txtPhone2" class="form-control" runat="server" Height="35px" Width="200px" placeholder="เบอร์โทรที่ 2"></asp:TextBox>

                                        </div>
                                        <div class="col-md-2"></div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-2">สถานภาพทหาร</div>
                                        <div class="col-md-3">
                                            <asp:TextBox ID="txtSoldier" class="form-control" runat="server" Height="35px" Width="200px" placeholder="สถานภาพทหาร"></asp:TextBox>
                                        </div>
                                        <div class="col-md-2">สถานภาพสมรส</div>
                                        <div class="col-md-3">
                                            <asp:TextBox ID="txtMarry" class="form-control" runat="server" Height="35px" Width="200px" placeholder="สถานภาพสมรส"></asp:TextBox>
                                        </div>
                                        <div class="col-md-2"></div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-2">ระดับการศึกษาสูงสุด</div>
                                        <div class="col-md-3">
                                            <asp:TextBox ID="txtGrade" class="form-control" runat="server" Height="35px" Width="200px" placeholder="ระดับการศึกษาสูงสุด"></asp:TextBox>
                                        </div>
                                        <div class="col-md-2">สถานศึกษาที่จบ</div>
                                        <div class="col-md-3">
                                            <asp:TextBox ID="txtUniversity" class="form-control" runat="server" Height="35px" Width="200px" placeholder="สถานศึกษาที่จบ"></asp:TextBox>
                                        </div>
                                        <div class="col-md-2"></div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <!-- /.col-lg-6 (nested) -->
                    </div>
                    <!-- /.row (nested) -->
                    <div style="float: right; margin-top: 20px;">
                        <table>
                            <tr>
                                <td>
                                    <asp:Button ID="btnSave" runat="server" Text="บันทึก" class="btn btn-primary" ValidationGroup="group" OnClick="btnSave_Click" />
                                </td>
                                <td>&nbsp;&nbsp;</td>
                                <td>
                                    <asp:Button ID="btnCancel" class="btn btn-primary" Text="ยกเลิก" runat="server" OnClick="btnCancel_Click" />
                                </td>
                            </tr>
                        </table>
                    </div>
                    <asp:PlaceHolder ID="PlaceHolder1" runat="server"></asp:PlaceHolder>
                </div>
                <!-- /.panel-body -->
            </div>
            <!-- /.panel -->
</asp:Content>
