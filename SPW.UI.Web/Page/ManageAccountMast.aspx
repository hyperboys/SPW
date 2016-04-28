<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPageMainAdmin.Master" AutoEventWireup="true"
    CodeBehind="ManageAccountMast.aspx.cs" Inherits="SPW.UI.Web.Page.ManageAccountMast" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1 class="page-header">
        <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Page/SearchTranspotLine.aspx">บัญชี</asp:HyperLink>
        -
        <asp:Label ID="lblName" runat="server" Text="จัดการบัญชี"></asp:Label></h1>
    <div class="alert alert-info" id="alert" runat="server" visible="false">
        <strong>บันทึกข้อมูลสำเร็จ Save Success</strong>
    </div>
    <div class="alert alert-warning" id="warning" runat="server" visible="false">
        <strong>
            <asp:Label ID="lblWarning" runat="server" Text=""></asp:Label></strong>
    </div>
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
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

    <div class="panel panel-primary">
        <div class="panel-heading">
            จัดการบัญชี        
        </div>
        <div class="panel-body">

            <div class="row">
                <div class="col-lg-12">
                    <div class="form">
                        <div class="form-group">
                            <div class="row">
                                <div class="col-md-2">เลขที่บัญชี</div>
                                <div class="col-md-3">
                                    <asp:TextBox ID="txtAccountNo" class="form-control" runat="server" Height="35px" placeholder="เลขที่บัญชี" BackColor="White"></asp:TextBox>
                                </div>
                                <div class="col-md-2">ชื่อบัญชี</div>
                                <div class="col-md-3">
                                    <asp:TextBox ID="txtAccountName" class="form-control" runat="server" Height="35px" placeholder="ชื่อบัญชี"></asp:TextBox>
                                </div>
                                <div class="col-md-2">
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-2">ธนาคาร</div>
                                <div class="col-md-3">
                                    <asp:DropDownList ID="ddlType" class="form-control" runat="server" Height="35px">
                                        <asp:ListItem Value="1" Selected="True">ธ.ทหารไทย จำกัด(มหาชน)</asp:ListItem>
                                        <asp:ListItem Value="2">ธ.กรุงศรีอยุธยา จำกัด (มหาชน) </asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-2">สาขา</div>
                                <div class="col-md-3">
                                    <asp:TextBox ID="txtBrhName" class="form-control" runat="server" Height="35px" placeholder="สาขา"></asp:TextBox>
                                </div>
                                <div class="col-md-2">
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-2">
                                </div>
                                <div class="col-md-3">
                                </div>
                                <div class="col-md-2"></div>
                                <div class="col-md-3">
                                    <asp:Button ID="btnSave" class="btn btn-primary" runat="server" Text="บันทึก" Height="30px" Width="70px" OnClick="btnSave_Click" />
                                    <asp:Button ID="btnCancel" class="btn btn-primary" runat="server" Text="ยกเลิก" Height="30px" Width="70px" PostBackUrl="~/Page/SearchAccountMast.aspx" />
                                </div>
                                <div class="col-md-2">
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <!-- /.col-lg-6 (nested) -->
            </div>
            <!-- /.row (nested) -->
            <div class="row">
                <div class="col-lg-12">
                    <div class="form">
                        <div class="form-group">
                            <div class="row">
                                <div class="col-md-2"></div>
                                <div class="col-md-3">
                                </div>
                                <div class="col-md-2"></div>
                                <div class="col-md-3">
                                </div>
                                <div class="col-md-2">
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <!-- /.col-lg-6 (nested) -->
            </div>
            <asp:PlaceHolder ID="PlaceHolder1" runat="server"></asp:PlaceHolder>
        </div>
        <!-- /.panel-body -->
    </div>
    <!-- /.panel -->

</asp:Content>
