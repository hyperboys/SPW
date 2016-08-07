<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPageMainAdmin.Master"
    AutoEventWireup="true" CodeBehind="ManageHDTemplate.aspx.cs" Inherits="SPW.UI.Web.Page.ManageHDTemplate" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1 class="page-header">
        <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Page/SearchEmpSkillType.aspx">ประเภททักษะพนักงาน</asp:HyperLink>
        -
        <asp:Label ID="lblName" runat="server" Text="เพิ่มประเภททักษะพนักงานใหม่"></asp:Label></h1>
    <div class="alert alert-info" id="alert" runat="server" visible="false">
        <strong>บันทึกข้อมูลสำเร็จ Save Success</strong>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <style type="text/css">
        .right {
            text-align: right;
        }

        .grid td, .grid th {
            text-align: center;
        }
    </style>
    <div class="panel panel-primary">
        <div class="panel-heading">
            ข้อมูลประเภททักษะพนักงาน
        </div>
        <div class="panel-body">
            <div class="row">
                <div class="col-lg-12">
                    <div class="form">
                        <div class="form-group">
                            <div class="row">
                                <div class="col-md-2">ชื่อแผนก</div>
                                <div class="col-md-3">
                                    <asp:DropDownList ID="ddlDepartment" class="form-control" runat="server" Height="35px" Width="200px" SelectedValue='<%# Eval("Key") %>'>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-2">ชื่อประเภททักษะ</div>
                                <div class="col-md-3">
                                    <asp:DropDownList ID="ddlSkillType" class="form-control" runat="server" Height="35px" Width="200px" SelectedValue='<%# Eval("Key") %>'>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-2"></div>
                            </div>
                            <div class="row">
                                <div class="col-md-2">&nbsp;</div>
                                <div class="col-md-3">&nbsp;</div>
                                <div class="col-md-2">&nbsp;</div>
                                <div class="col-md-3">&nbsp;</div>
                                <div class="col-md-2">&nbsp;</div>
                            </div>
                            <div class="row">
                                <div class="col-md-2">เปอร์เซ็น</div>
                                <div class="col-md-3">
                                    <asp:TextBox ID="txtPercen" class="form-control" runat="server" Height="35px" placeholder="เปอร์เซ็น"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="กรุณากรอกเปอร์เซ็น"
                                        ControlToValidate="txtPercen" ValidationGroup="group" Style="color: #FF0000; font-size: large;">*</asp:RequiredFieldValidator>
                                </div>
                                <div class="col-md-2"></div>
                                <div class="col-md-3">
                                    <asp:Button ID="btnSave" runat="server" class="btn btn-primary" Text="บันทึก" OnClick="btnSave_Click" ValidationGroup="group" />
                                    <asp:Button ID="btnCancel" runat="server" class="btn btn-primary" Text="ยกเลิก" PostBackUrl="~/Page/SearchEmpHdTemplate.aspx" />
                                </div>
                                <div class="col-md-2">
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <!-- /.col-lg-6 (nested) -->
            </div>
            <div>
                <asp:ValidationSummary ID="ValidationSummary1" runat="server" Style="color: #FF0000; font-size: large;"
                    ValidationGroup="group" ShowMessageBox="True" ShowSummary="False" />
            </div>
        </div>
    </div>
    <ajax:ConfirmButtonExtender ID="btnSave_ConfirmButtonExtender" runat="server"
        ConfirmText="ต้องการจะบันทึกหรือไม่" Enabled="True" TargetControlID="btnSave">
    </ajax:ConfirmButtonExtender>
</asp:Content>
