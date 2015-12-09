<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPageMainAdmin.Master"
    AutoEventWireup="true" CodeBehind="ManageVehicle.aspx.cs" Inherits="SPW.UI.Web.Page.ManageVehicle" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1 class="page-header">
        <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Page/SearchVehicle.aspx">รถบรรทุกสินค้า</asp:HyperLink>
        -
        <asp:Label ID="lblName" runat="server" Text="เพิ่มรถบรรทุกสินค้าใหม่"></asp:Label></h1>
    <div class="alert alert-info" id="alert" runat="server" visible="false">
        <strong>บันทึกข้อมูลสำเร็จ Save Success</strong>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <style type="text/css">
        .auto-style1 {
            width: 18px;
        }

        .right {
            text-align: right;
        }

        .auto-style2 {
            width: 160px;
        }

        .grid td, .grid th {
            text-align: center;
        }

        .auto-style4 {
            width: 108px;
        }

        .auto-style5 {
            width: 287px;
        }

        .auto-style6 {
            width: 84px;
        }

        .auto-style7 {
            width: 80px;
        }

        .auto-style8 {
            width: 83px;
        }

        .auto-style9 {
            width: 215px;
        }

        .auto-style10 {
            width: 184px;
        }

        .auto-style11 {
            width: 183px;
        }

        .auto-style12 {
            width: 229px;
        }

        .auto-style13 {
            width: 63px;
        }
    </style>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="panel panel-primary">
        <div class="panel-heading">
            ข้อมูลรถ
        </div>
        <div class="panel-body">
            <div class="row">
                <div class="col-lg-6">
                    <div class="form">
                        <div class="form-group">
                            <table style="width: 835px; height: 80px;">
                                <tr>
                                    <td class="auto-style5">รหัสรถ</td>
                                    <td class="auto-style1" style="text-align: center">:</td>
                                    <td class="auto-style2">
                                        <asp:TextBox ID="popTxtCode" class="form-control" runat="server" Height="30px" Width="145px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="กรุณากรอกรหัสรถ"
                                            ValidationGroup="group" ControlToValidate="popTxtCode" Style="color: #FF0000; font-size: large;">*</asp:RequiredFieldValidator>
                                    </td>
                                    <td></td>
                                    <td class="auto-style12">เลขทะเบียนรถ</td>
                                    <td class="auto-style1" style="text-align: center">:</td>
                                    <td class="auto-style2">
                                        <asp:TextBox ID="popTxtReg" class="form-control" runat="server" Height="30px" Width="145px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="กรุณากรอกเลขทะเบียนรถ"
                                            ValidationGroup="group" ControlToValidate="popTxtReg" Style="color: #FF0000; font-size: large;">*</asp:RequiredFieldValidator>
                                    </td>
                                    <td class="auto-style4"></td>
                                    <td>
                                        <asp:Button ID="btnSave" runat="server" Text="บันทึก" ValidationGroup="group" class="btn btn-primary" OnClick="btnSave_Click" />
                                    </td>
                                    <td class="auto-style13"></td>
                                    <td>
                                        <asp:Button ID="btnCancel" class="btn btn-primary" Text="ยกเลิก" runat="server" OnClick="btnCancel_Click" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="auto-style12">ชนิดรถ</td>
                                    <td class="auto-style1" style="text-align: center">:</td>
                                    <td class="auto-style2">
                                        <asp:TextBox ID="popTxtTypNO" class="form-control" runat="server" Height="30px" Width="145px" MaxLength="2"></asp:TextBox>
                                    </td>
                                    <td></td>
                                    <td class="auto-style12"></td>
                                    <td class="auto-style1" style="text-align: center"></td>
                                    <td class="auto-style2"></td>
                                    <td class="auto-style4"></td>
                                    <td></td>
                                    <td class="auto-style13"></td>
                                    <td></td>
                                </tr>
                            </table>
                            <asp:Label ID="flag" runat="server" Text="Add" Visible="false"></asp:Label>
                        </div>
                    </div>
                </div>
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
