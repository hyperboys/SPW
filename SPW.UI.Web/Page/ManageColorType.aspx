<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPageMainAdmin.Master"
    AutoEventWireup="true" CodeBehind="ManageColorType.aspx.cs" Inherits="SPW.UI.Web.Page.ManageColorType" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1 class="page-header">
        <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Page/SearchColorType.aspx">ลวดลายสินค้า</asp:HyperLink>
        -
        <asp:Label ID="lblName" runat="server" Text="เพิ่มลวดลายสินค้าใหม่"></asp:Label></h1>
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
    <div class="panel panel-primary">
        <div class="panel-heading">
            ข้อมูลลวดลายสินค้า
        </div>
        <div class="panel-body">
            <div class="row">
                <div class="col-lg-6">
                    <div class="form">
                        <div class="form-group">
                            <table style="width: 835px; height: 80px;">
                                <tr>
                                    <td class="auto-style5">ชื่อย่อลวดลายสินค้า</td>
                                    <td class="auto-style1" style="text-align: center">:</td>
                                    <td class="auto-style2">
                                        <asp:TextBox ID="popTxtColorTypeSubName" class="form-control" runat="server" Height="30px" Width="125px"  ></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="กรุณากรอกชื่อย่อลวดลายสินค้า"
                                               ValidationGroup="group" ControlToValidate="popTxtColorTypeSubName" Style="color: #FF0000; font-size: large;">*</asp:RequiredFieldValidator>
                                    </td>
                                    <td></td>
                                    <td class="auto-style12">ชื่อลวดลายสินค้า</td>
                                    <td class="auto-style1" style="text-align: center">:</td>
                                    <td class="auto-style2">
                                        <asp:TextBox ID="popTxtColorTypeName" class="form-control" runat="server"   Height="30px" Width="147px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="กรุณากรอกชื่อลวดลายสินค้า"
                                               ValidationGroup="group" ControlToValidate="popTxtColorTypeName" Style="color: #FF0000; font-size: large;">*</asp:RequiredFieldValidator>
                                    </td>
                                    <td></td>
                                    <td class="auto-style4"></td>
                                    <td>
                                        <asp:Button ID="btnSave" runat="server" Text="บันทึก"  ValidationGroup="group" class="btn btn-primary" OnClick="btnSave_Click" />
                                    </td>
                                    <td class="auto-style13"></td>
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
