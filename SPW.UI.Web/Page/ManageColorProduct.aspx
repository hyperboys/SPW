<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPageMainAdmin.Master"
     AutoEventWireup="true" CodeBehind="ManageColorProduct.aspx.cs" Inherits="SPW.UI.Web.Page.ManageColorProduct" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1 class="page-header">
        <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Page/SearchColorProduct.aspx">สีของสินค้า</asp:HyperLink> 
        - <asp:Label ID="lblName" runat="server" Text="เพิ่มสีของสินค้าใหม่"></asp:Label></h1>
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
            width: 85px;
        }

        .auto-style5 {
            width: 287px;
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

        .auto-style12 {
            width: 229px;
        }

        .auto-style13 {
            width: 63px;
        }

        .auto-style14 {
            width: 29px;
        }

        .auto-style15 {
            width: 76px;
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
                            <table style="width: 823px; height: 80px;">
                                <tr>
                                    <td class="auto-style5">ชื่อย่อสีของสินค้า</td>
                                    <td class="auto-style1" style="text-align: center">:</td>
                                    <td class="auto-style2">
                                        <table>
                                            <tr>
                                                <td>
                                                    <asp:TextBox ID="popTxtColorTypeSubName" class="form-control" runat="server" Height="30px" Width="145px"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="กรุณากรอกชื่อย่อสีของสินค้า"
                                                       ControlToValidate="popTxtColorTypeSubName" ValidationGroup="group" Style="color: #FF0000; font-size: large;">*</asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td class="auto-style14"></td>
                                    <td class="auto-style12">ชื่อสีของสินค้า</td>
                                    <td class="auto-style1" style="text-align: center">:</td>
                                    <td class="auto-style2">
                                        <table>
                                            <tr>
                                                <td>
                                                    <asp:TextBox ID="popTxtColorTypeName" class="form-control" runat="server" Height="30px" Width="145px"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="กรุณากรอกชื่อสีของสินค้า"
                                                        ControlToValidate="popTxtColorTypeName" ValidationGroup="group" Style="color: #FF0000; font-size: large;">*</asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td class="auto-style15"></td>
                                    <td>
                                        <asp:Button ID="btnSave" runat="server" Text="บันทึก" class="btn btn-primary" OnClick="btnSave_Click" ValidationGroup="group"/>
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
