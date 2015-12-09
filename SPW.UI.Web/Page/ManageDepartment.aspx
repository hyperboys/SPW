<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPageMainAdmin.Master" AutoEventWireup="true" CodeBehind="ManageDepartment.aspx.cs" Inherits="SPW.UI.Web.Page.ManageDepartment" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1 class="page-header">
        <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Page/SearchDepartment.aspx">แผนก</asp:HyperLink> 
        - <asp:Label ID="lblName" runat="server" Text="เพิ่มแผนกใหม่"></asp:Label></h1>
    <div class="alert alert-info" id="alert" runat="server" visible="false">
        <strong>บันทึกข้อมูลสำเร็จ Save Success</strong>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <style type="text/css">
        .auto-style1 {
            width: 18px;
        }
        .auto-style2 {
            width: 160px;
        }
        .auto-style3 {
            width: 103px;
        }
    </style>
    <div class="panel panel-primary">
        <div class="panel-heading">
            ข้อมูลแผนก             
        </div>
        <div class="panel-body">
            <div class="row">
                <div class="col-lg-6">
                    <div role="form">
                        <div class="form-group">
                            <table style="width: 729px; height: 80px;">
                                <tr>
                                    <td>รหัสแผนก</td>
                                    <td class="auto-style1" style="text-align: center">:</td>
                                    <td>
                                        <table>
                                            <tr>
                                                <td>
                                                    <asp:TextBox ID="popTxtDepartmentCode" class="form-control" runat="server" Height="30px" Width="125px"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="กรุณากรอกรหัสแผนก"
                                                           ValidationGroup="group" ControlToValidate="popTxtDepartmentCode" Style="color: #FF0000; font-size: large;">*</asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td></td>
                                    <td>ชื่อแผนก</td>
                                    <td class="auto-style1" style="text-align: center">:</td>
                                    <td>
                                        <table>
                                            <tr>
                                                <td>
                                                    <asp:TextBox ID="txtDepartmentName" class="form-control" runat="server" Height="30px" Width="125px"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="กรุณากรอกชื่อแผนก"
                                                           ValidationGroup="group" ControlToValidate="txtDepartmentName" Style="color: #FF0000; font-size: large;">*</asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td></td>
                                    <td>
                                        <asp:Button ID="btnSave" runat="server" Text="บันทึก"  ValidationGroup="group" class="btn btn-primary" OnClick="btnSave_Click"   />
                                    </td>
                                    <td></td>
                                    <td>
                                        <asp:Button ID="btnCancel" class="btn btn-primary" Text="ยกเลิก" runat="server" OnClick="btnCancel_Click" />
                                    </td>
                                </tr>
                            </table>
                            <asp:Label ID="flag" runat="server" Text="Add" Visible="false"></asp:Label>
                        </div>
                        <div>
                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" Style="color: #FF0000; font-size: large;"
                                   ValidationGroup="group" ShowMessageBox="True" ShowSummary="False" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <ajax:ConfirmButtonExtender ID="btnSave_ConfirmButtonExtender" runat="server"
        ConfirmText="ต้องการจะบันทึกหรือไม่" Enabled="True" TargetControlID="btnSave">
    </ajax:ConfirmButtonExtender>
</asp:Content>
