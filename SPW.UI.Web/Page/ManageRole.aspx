<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPageMainAdmin.Master" AutoEventWireup="true" CodeBehind="ManageRole.aspx.cs" Inherits="SPW.UI.Web.Page.ManageRole" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1 class="page-header">
        <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Page/SearchRole.aspx">Role</asp:HyperLink>
        -
        <asp:Label ID="lblName" runat="server" Text="เพิ่ม Role ใหม่"></asp:Label></h1>
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

        .grid td, .grid th {
            text-align: center;
        }
        .auto-style2 {
            width: 30px;
        }
    </style>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>

    <div class="panel panel-primary">
        <div class="panel-heading">
            ข้อมูล Role            
        </div>
        <div class="panel-body">
            <div class="row">
                <div class="col-lg-6">
                    <div role="form">
                        <div class="form-group">
                            <table style="width: 479px; height: 80px;">
                                <tr>
                                    <td>Role Code</td>
                                    <td class="auto-style1" style="text-align: center">:</td>
                                    <td>
                                        <asp:TextBox ID="popTxtRoleCode" class="form-control" runat="server" Height="30px" Width="125px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="กรุณากรอก Role Code" 
                                           ValidationGroup="group" ControlToValidate="popTxtRoleCode" Style="color: #FF0000; font-size: large;" SetFocusOnError="True">*</asp:RequiredFieldValidator>
                                    </td>
                                    <td class="auto-style2"></td>
                                    <td>Role Name</td>
                                    <td class="auto-style1" style="text-align: center">:</td>
                                    <td>
                                        <asp:TextBox ID="popTxtRoleName" class="form-control" runat="server" Height="30px" Width="125px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="กรุณากรอก Role Name" 
                                            ValidationGroup="group" ControlToValidate="popTxtRoleName" Style="color: #FF0000; font-size: large;" SetFocusOnError="True">*</asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                            </table>
                            <asp:Label ID="flag" runat="server" Text="Add" Visible="false"></asp:Label>
                            <div>
                                <asp:ValidationSummary ID="ValidationSummary1" runat="server"
                                    ValidationGroup="group" Style="color: #FF0000; font-size:large;" ShowMessageBox="True" ShowSummary="False" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <table style="margin-bottom: 10px; height: 36px;">
                <tr>
                    <td>
                        <asp:Button ID="AddFunction" runat="server" Text="เพิ่มฟังก์ชัน" class="btn btn-primary" />
                    </td>
                </tr>
            </table>
            <asp:GridView ID="gridFunction" runat="server" ForeColor="#507CD1" AutoGenerateColumns="False"
                DataKeyNames="ROLE_FUNCTION_ID" PageSize="20" Width="100%" EmptyDataText="ไม่พบข้อมูลฟังก์ชัน"
                Style="text-align: center;" CssClass="grid" OnRowDeleting="gridFunction_RowDeleting" OnRowDataBound="gridFunction_RowDataBound">
                <AlternatingRowStyle BackColor="White" />
                <Columns>
                    <asp:TemplateField HeaderText="ลบ" ShowHeader="False">
                        <ItemTemplate>
                            <asp:ImageButton ID="ImageButton1" runat="server" CausesValidation="False" CommandName="Delete"
                                ImageUrl="~/Image/Icon/close.png" Text="ลบ" />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:BoundField DataField="FUNCTION.FUNCTION_NAME" HeaderText="ชื่อฟังก์ชัน" ItemStyle-Width="90%" ItemStyle-HorizontalAlign="Center">
                        <ItemStyle Width="90%"></ItemStyle>
                    </asp:BoundField>
                </Columns>
                <EditRowStyle BackColor="#2461BF" />
                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#507CD1" ForeColor="White" />
                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                <RowStyle BackColor="#EFF3FB" HorizontalAlign="Center" Height="20px" />
                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                <SortedAscendingCellStyle BackColor="#F5F7FB" />
                <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                <SortedDescendingCellStyle BackColor="#E9EBEF" />
                <SortedDescendingHeaderStyle BackColor="#4870BE" />
                <PagerSettings Mode="NumericFirstLast" />
            </asp:GridView>
            <div style="margin-top: 20px; float: right;">
                <table>
                    <tr>
                        <td>
                            <asp:Button ID="btnSave" runat="server" Text="บันทึก" class="btn btn-primary"  ValidationGroup="group" OnClick="btnSave_Click" />
                        </td>
                        <td>&nbsp;&nbsp;</td>
                        <td>
                            <asp:Button ID="btnCancel" class="btn btn-primary" Text="ยกเลิก" runat="server" OnClick="btnCancel_Click" />
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
    <asp:Panel ID="Panel2" runat="server" Visible="True" Height="272px">
        <div class="panel panel-primary">
            <div class="panel-heading">
                ข้อมูลฟังก์ชัน         
            </div>
            <div class="panel-body">
                <asp:GridView ID="gridSelectFunction" runat="server" ForeColor="#507CD1" AutoGenerateColumns="False"
                    DataKeyNames="FUNCTION_ID" PageSize="20" Width="100%" EmptyDataText="ไม่พบข้อมูลฟังก์ชัน"
                    OnPageIndexChanging="gridSelectFunction_PageIndexChanging"
                    Style="text-align: center" CssClass="grid">
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:TemplateField HeaderText="เลือก" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:CheckBox ID="check" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="FUNCTION_NAME" HeaderText="ชื่อฟังก์ชัน" ItemStyle-Width="90%" ItemStyle-HorizontalAlign="Center">
                            <ItemStyle Width="90%"></ItemStyle>
                        </asp:BoundField>
                    </Columns>
                    <EditRowStyle BackColor="#2461BF" />
                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#507CD1" ForeColor="White" />
                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                    <RowStyle BackColor="#EFF3FB" HorizontalAlign="Center" Height="20px" />
                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                    <SortedAscendingCellStyle BackColor="#F5F7FB" />
                    <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                    <SortedDescendingCellStyle BackColor="#E9EBEF" />
                    <SortedDescendingHeaderStyle BackColor="#4870BE" />
                    <PagerSettings Mode="NumericFirstLast" />
                </asp:GridView>
                <div style="margin-top: 20px; float: right;">
                    <table>
                        <tr>
                            <td></td>
                            <td>
                                <asp:Button ID="btnAddColor" runat="server" Text="บันทึก" class="btn btn-primary" OnClick="btnAddFunction_Click" />
                            </td>
                            <td>
                                <asp:Button ID="btnCancelColor" class="btn btn-primary" Text="ยกเลิก" runat="server" OnClick="btnCancelFunction_Click" />
                            </td>
                        </tr>
                    </table>
                </div>
                <asp:Label ID="flag2" runat="server" Text="Add" Visible="false"></asp:Label>
            </div>
        </div>
    </asp:Panel>
    <asp:LinkButton ID="lnkFake" runat="server"></asp:LinkButton>
    <ajax:ModalPopupExtender ID="popup2" runat="server" BackgroundCssClass="modalBackground" DropShadow="false" PopupControlID="Panel2" TargetControlID="AddFunction" Enabled="True">
    </ajax:ModalPopupExtender>
    <ajax:ConfirmButtonExtender ID="btnSave_ConfirmButtonExtender" runat="server"
        ConfirmText="ต้องการจะบันทึกหรือไม่" Enabled="True" TargetControlID="btnSave">
    </ajax:ConfirmButtonExtender>
</asp:Content>
