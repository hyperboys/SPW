<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPageMainAdmin.Master"
    AutoEventWireup="true" CodeBehind="ManageZone.aspx.cs" Inherits="SPW.UI.Web.Page.ManageZone" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1 class="page-header">
        <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Page/SearchZone.aspx">ราคาขายสินค้า</asp:HyperLink>
        -
        <asp:Label ID="lblName" runat="server" Text="เพิ่มราคาขายสินค้าใหม่"></asp:Label></h1>
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

        .auto-style3 {
            width: 15px;
        }

        .auto-style4 {
            width: 137px;
        }
    </style>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="panel panel-primary">
        <div class="panel-heading">
            ข้อมูลราคาขายสินค้า
        </div>
        <div class="panel-body">
            <div class="row">
                <div class="col-lg-6">
                    <div class="form">
                        <div class="form-group">
                            <table style="width: 803px; height: 51px;">
                                <tr>
                                    <td class="auto-style7">รหัสราคาขายสินค้า</td>
                                    <td class="auto-style1" style="text-align: center">:</td>
                                    <td class="auto-style2">
                                        <asp:TextBox ID="popTxtZoneCode" class="form-control" runat="server" Height="35px" Width="150px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="กรุณากรอกเลขรหัสราคาขายสินค้า"
                                            ValidationGroup="group" ControlToValidate="popTxtZoneCode" Style="color: #FF0000; font-size: large;">*</asp:RequiredFieldValidator>
                                    </td>
                                    <td class="auto-style6"></td>
                                    <td class="auto-style8">ชื่อราคาขายสินค้า</td>
                                    <td class="auto-style1" style="text-align: center">:</td>
                                    <td class="auto-style2">
                                        <asp:TextBox ID="popTxtZoneName" class="form-control" runat="server" Height="35px" Width="150px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="กรุณากรอกชื่อราคาขายสินค้า"
                                            ValidationGroup="group" ControlToValidate="popTxtZoneName" Style="color: #FF0000; font-size: large;">*</asp:RequiredFieldValidator>
                                    </td>
                                    <td class="auto-style3"></td>
                                    <td>
                                        <asp:Button ID="btnSave" class="btn btn-primary" runat="server" Text="บันทึก" ValidationGroup="group" OnClick="btnSave_Click" />
                                    </td>
                                    <td class="auto-style3"></td>
                                    <td>
                                        <asp:Button ID="btnCancel" class="btn btn-primary" Text="ยกเลิก" runat="server" OnClick="btnCancel_Click" />
                                    </td>
                                </tr>
                            </table>
                            <table style="width: 803px; height: 51px;">
                                <tr>
                                    <td class="auto-style4">พนักงานขาย</td>
                                    <td class="auto-style1" style="text-align: center">:</td>
                                    <td>
                                        <asp:DropDownList ID="ddlSell" class="form-control" runat="server" Height="35px" Width="456px" SelectedValue='<%# Eval("Key") %>'>
                                            <asp:ListItem Value="0">กรุณาเลือก</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <asp:Label ID="flag" runat="server" Text="Add" Visible="false"></asp:Label>
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
