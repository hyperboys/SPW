﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPageMainAdmin.Master"
     AutoEventWireup="true" CodeBehind="ManageCategory.aspx.cs" Inherits="SPW.UI.Web.Page.ManageCategory" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1 class="page-header">
        <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Page/SearchCategory.aspx">หมวดหมู่สินค้า</asp:HyperLink>
         - <asp:Label ID="lblName" runat="server" Text="เพิ่มหมวดหมู่สินค้าใหม่"></asp:Label>
    </h1>
     <div class="alert alert-info" id="alert" runat="server" visible="false">
        <strong>บันทึกข้อมูลสำเร็จ Save Success</strong>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="panel panel-primary">
        <div class="panel-heading">
            ข้อมูลหมวดหมู่สินค้า         
        </div>
        <div class="panel-body">
            <div class="row">
                <div class="col-lg-6">
                    <div role="form">
                        <div class="form-group">
                            <table style="width: 742px; height: 80px;">
                                <tr>
                                    <td class="auto-style5">รหัสหมวดหมู่สินค้า</td>
                                    <td class="auto-style1" style="text-align: center">:</td>
                                    <td class="auto-style2">
                                        <table>
                                            <tr>
                                                <td>
                                                    <asp:TextBox ID="popTxtCategoryCode" class="form-control" runat="server" Height="30px" Width="125px" />
                                                </td>
                                                <td>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="กรุณากรอกรหัสหมวดหมู่สินค้า"
                                                         ValidationGroup="group" ControlToValidate="popTxtCategoryCode" Style="color: #FF0000; font-size: large;">*</asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td class="auto-style3">ชื่อหมวดหมู่สินค้า</td>
                                    <td class="auto-style1" style="text-align: center">:</td>
                                    <td class="auto-style2">
                                        <table>
                                            <tr>
                                                <td>
                                                    <asp:TextBox ID="txtCategoryName" class="form-control" runat="server" Height="30px" Width="147px"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="กรุณากรอกชื่อหมวดหมู่สินค้า"
                                                         ValidationGroup="group" ControlToValidate="txtCategoryName" Style="color: #FF0000; font-size: large;">*</asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td class="auto-style7"></td>
                                    <td class="auto-style9">
                                        <asp:Button ID="btnSave" runat="server" Text="บันทึก" class="btn btn-primary"  ValidationGroup="group"  OnClick="btnSave_Click"   />
                                    </td>
                                    <td class="auto-style8"></td>
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