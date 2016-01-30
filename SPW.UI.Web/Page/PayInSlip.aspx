<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPageMainAdmin.Master" AutoEventWireup="true"
    CodeBehind="PayInSlip.aspx.cs" Inherits="SPW.UI.Web.Page.PayInSlip" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1 class="page-header">
        <asp:Label ID="lblName" runat="server" Text="PayIn Slip"></asp:Label></h1>
    <div class="alert alert-info" id="alert" runat="server" visible="false">
        <strong>บันทึกข้อมูลสำเร็จ Save Success</strong>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <style type="text/css">
        .auto-style1
        {
            width: 18px;
        }

        .right
        {
            text-align: right;
        }

        .auto-style2
        {
            width: 160px;
        }

        .grid td, .grid th
        {
            text-align: center;
        }

        .auto-style4
        {
            width: 108px;
        }

        .auto-style5
        {
            width: 147px;
        }

        .auto-style12
        {
            width: 229px;
        }

        .auto-style13
        {
            width: 60px;
        }
    </style>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="panel panel-primary">
        <div class="panel-heading">
            จัดการใบ Payin
        </div>
        <div class="panel-body">
            <div class="row">
                <div class="col-lg-6">
                    <div class="form">
                        <div class="form-group">
                            <table style="width: 835px; height: 80px;">
                                <tr>
                                    <td class="auto-style5">
                                        
                                    </td>
                                    <td class="auto-style1" style="text-align: center"></td>
                                    <td class="auto-style2">
                                        <asp:RadioButton ID="RadioButton1" runat="server" Text="ธ.ทหารไทย" TextAlign="Right" />
                                    </td>
                                    <td class="auto-style13">

                                    </td>
                                    <td class="auto-style12">
                                        <asp:RadioButton ID="RadioButton2" runat="server" Text="ธ.กรุงศรีอยุธยา"  TextAlign="Right" />
                                    </td>
                                    <td class="auto-style1" style="text-align: center">&nbsp;</td>
                                    <td class="auto-style2">
                                        <asp:Label ID="lblDateTime" runat="server" Text="xxxx"></asp:Label>
                                    </td>
                                    <td class="auto-style4"></td>
                                </tr>
                                <tr>
                                    <td class="auto-style5"></td>
                                    <td class="auto-style1" style="text-align: center"></td>
                                    <td class="auto-style2">
                                        &nbsp;</td>
                                    <td class="auto-style13"></td>
                                    <td class="auto-style12"></td>
                                    <td class="auto-style1" style="text-align: center"></td>
                                    <td class="auto-style2"></td>
                                    <td class="auto-style4"></td>
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
</asp:Content>
