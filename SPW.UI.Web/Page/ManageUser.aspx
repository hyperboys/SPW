<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPageMainAdmin.Master"
    AutoEventWireup="true" CodeBehind="ManageUser.aspx.cs" Inherits="SPW.UI.Web.Page.ManageUser" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
    <style type="text/css">
        .auto-style3 {
            width: 148px;
        }

        .auto-style4 {
            width: 22px;
        }

        .auto-style5 {
            width: 23px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1 class="page-header">
        <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Page/SearchUser.aspx">ข้อมูลผู้ใช้งาน</asp:HyperLink>
        -
        <asp:Label ID="lblName" runat="server" Text="เพิ่มผู้ใช้งานใหม่"></asp:Label></h1>
    <div class="alert alert-info" id="alert" runat="server" visible="false">
        <strong>
            <asp:Label ID="lblAlert" runat="server" Text="บันทึกข้อมูลสำเร็จ Save Success"></asp:Label>
        </strong>
    </div>
    <div class="alert alert-warning" id="warning" runat="server" visible="false">
        <strong>
            <asp:Label ID="lblWarning" runat="server" Text=""></asp:Label></strong>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="panel panel-primary">
        <div class="panel-heading">
            ข้อมูลผู้ใช้งาน             
        </div>
        <div class="panel-body">
            <div class="row">
                <div class="col-lg-12">
                    <div class="form">
                        <div class="form-group">
                            <div class="row">
                                <div class="col-md-2">รหัสผู้ใช้งาน</div>
                                <div class="col-md-3">
                                    <asp:TextBox ID="txtUsername" class="form-control" runat="server" Height="35px" Width="200px" placeholder="รหัสผู้ใช้งาน" Enabled="false"></asp:TextBox>
                                </div>
                                <div class="col-md-2"></div>
                                <div class="col-md-3">
                                </div>
                                <div class="col-md-2"></div>
                            </div>
                            <div class="row">
                                <div class="col-md-2">รหัสผ่านใหม่</div>
                                <div class="col-md-3">
                                    <asp:TextBox ID="txtPassword1" class="form-control" runat="server" Height="35px" Width="200px" placeholder="รหัสผู้ใช้งาน" TextMode="Password"></asp:TextBox>
                                </div>
                                <div class="col-md-2"></div>
                                <div class="col-md-3">
                                </div>
                                <div class="col-md-2"></div>
                            </div>
                            <div class="row">
                                <div class="col-md-2">รหัสผ่านใหม่อีกครั้ง</div>
                                <div class="col-md-3">
                                    <asp:TextBox ID="txtPassword2" class="form-control" runat="server" Height="35px" Width="200px" placeholder="รหัสผู้ใช้งาน" TextMode="Password"></asp:TextBox>
                                </div>
                                <div class="col-md-2"></div>
                                <div class="col-md-3">
                                </div>
                                <div class="col-md-2"></div>
                            </div>
                            <div class="row">
                                <div class="col-md-2"></div>
                                <div class="col-md-3">
                                    <asp:Button ID="btnSave" runat="server" Text="บันทึก" class="btn btn-primary" ValidationGroup="group" OnClick="btnSave_Click" />
                                    <asp:Button ID="btnCancel" class="btn btn-primary" Text="ยกเลิก" runat="server" OnClick="btnCancel_Click" />
                                </div>
                                <div class="col-md-2">
                                </div>
                                <div class="col-md-3">
                                </div>
                                <div class="col-md-2"></div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <ajax:ConfirmButtonExtender ID="btnSave_ConfirmButtonExtender" runat="server"
            ConfirmText="ต้องการจะบันทึกหรือไม่" Enabled="True" TargetControlID="btnSave">
        </ajax:ConfirmButtonExtender>
    </div>
</asp:Content>
