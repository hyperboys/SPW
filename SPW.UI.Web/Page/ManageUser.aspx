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
        <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Page/SearchUser.aspx">ผู้ใช้งาน</asp:HyperLink>
        -
        <asp:Label ID="lblName" runat="server" Text="เพิ่มผู้ใช้งานใหม่"></asp:Label></h1>
    <div class="alert alert-info" id="alert" runat="server" visible="false">
        <strong>บันทึกข้อมูลสำเร็จ Save Success</strong>
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
                <div class="col-lg-6">
                    <div role="form">
                        <div class="form-group">
                            <table style="width: 766px; height: 97px;">
                                <tr>
                                    <td>ชื่อผู้ใช้งาน</td>
                                    <td style="text-align: center">:</td>
                                    <td class="auto-style3">
                                        <asp:TextBox ID="popTxt1" class="form-control" runat="server" Height="30px" Width="145px"></asp:TextBox>
                                    </td>
                                     <td>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="กรุณากรอกชื่อผู้ใช้งาน"
                                            ValidationGroup="group" ControlToValidate="popTxt1" Style="color: #FF0000; font-size: large;">*</asp:RequiredFieldValidator>
                                    </td>
                                    <td class="auto-style4"></td>
                                    <td>รหัสหัสผู้ใช้งาน</td>
                                    <td style="text-align: center">:</td>
                                    <td class="auto-style3">
                                        <asp:TextBox ID="popTxt2" class="form-control" runat="server" Height="30px" Width="145px" TextMode="Password"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="กรุณากรอกรหัสหัสผู้ใช้งาน"
                                             ValidationGroup="group" ControlToValidate="popTxt2" Style="color: #FF0000; font-size: large;">*</asp:RequiredFieldValidator>
                                    </td>
                                    <td class="auto-style5"></td>
                                    <td>
                                        <asp:Button ID="btnSave" runat="server" ValidationGroup="group" Text="บันทึก" class="btn btn-primary" OnClick="btnSave_Click" />
                                    </td>
                                    <td></td>
                                    <td>
                                        <asp:Button ID="btnCancel" class="btn btn-primary" Text="ยกเลิก" runat="server" OnClick="btnCancel_Click" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>Role</td>
                                    <td style="text-align: center">:</td>
                                    <td class="auto-style3">
                                        <asp:DropDownList ID="dllRole" class="form-control" runat="server" Height="35px" Width="145px" SelectedValue='<%# Eval("Key") %>'>
                                        </asp:DropDownList>
                                    </td>
                                    <td></td>
                                    <td class="auto-style4"></td>
                                    <td>พนักงาน</td>
                                    <td style="text-align: center">:</td>
                                    <td class="auto-style3">
                                        <asp:DropDownList ID="dllEmp" class="form-control" runat="server" Height="35px" Width="145px" SelectedValue='<%# Eval("Key") %>'>
                                        </asp:DropDownList>
                                    </td>
                                    <td></td>
                                    <td class="auto-style5"></td>
                                    <td></td>
                                    <td></td>
                                </tr>
                            </table>
                            <asp:Label ID="flag" runat="server" Text="Add" Visible="false"></asp:Label>
                        </div>
                    </div>
                </div>
            </div>
            <div>
                <asp:ValidationSummary ID="ValidationSummary1" runat="server" style="color:#FF0000; font-size: large;"
                    ValidationGroup="group" ShowMessageBox="True" ShowSummary="False" />
            </div>
        </div>
    </div>
    <ajax:ConfirmButtonExtender ID="btnSave_ConfirmButtonExtender" runat="server"
        ConfirmText="ต้องการจะบันทึกหรือไม่" Enabled="True" TargetControlID="btnSave">
    </ajax:ConfirmButtonExtender>
</asp:Content>
