<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPageMainAdmin.Master" AutoEventWireup="true" CodeBehind="SearchRole.aspx.cs" Inherits="SPW.UI.Web.Page.SearchRole" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1 class="page-header">Role</h1>
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

        .auto-style3
        {
            width: 103px;
        }

        .grid td, .grid th
        {
            text-align: center;
        }
    </style>
    <div class="panel panel-primary">
        <div class="panel-heading">
            ข้อมูล Role        
        </div>
        <div class="panel-body">
            <div class="row">
                <div class="col-lg-6">
                    <div class="form">
                        <div class="form-group">
                            <table style="width: 567px">
                                <tr>
                                    <td>Role Code</td>
                                    <td class="auto-style1" style="text-align: center">:</td>
                                    <td>
                                        <asp:TextBox ID="txtRoleCode" class="form-control" runat="server"></asp:TextBox>
                                    </td>
                                    <td class="auto-style1" style="text-align: center">&nbsp;</td>
                                    <td>
                                        <asp:Button ID="btnSearch" class="btn btn-primary" runat="server" Text="ค้นหา" OnClick="btnSearch_Click" Height="30px" Width="70px" />
                                    </td>
                                    <td class="auto-style1" style="text-align: center">&nbsp;</td>
                                    <td>
                                        <asp:Button ID="btnReset" class="btn btn-primary" Text="รีเซท" runat="server" Height="30px" Width="70px" OnClick="btnReset_Click" />
                                    </td>
                                    <td class="auto-style1" style="text-align: center">&nbsp;</td>
                                    <td>
                                        <asp:Button ID="btnAdd" class="btn btn-primary" Text="เพิ่ม" runat="server" Height="30px" Width="70px" OnClick="btnAdd_Click" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </div>
                <!-- /.col-lg-6 (nested) -->
            </div>
            <!-- /.row (nested) -->
            <div class="panel panel-primary">
                <asp:GridView ID="gridRole" runat="server" ForeColor="#507CD1" AutoGenerateColumns="False"
                    DataKeyNames="ROLE_ID" PageSize="20" Width="100%" EmptyDataText="ไม่พบข้อมูลผู้ใช้งาน"
                    OnRowEditing="gridDepartment_EditCommand" OnPageIndexChanging="gridDepartment_PageIndexChanging"
                    Style="text-align: center" CssClass="grid">
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:CommandField ButtonType="Image" EditImageUrl="~/Image/Icon/find.png" HeaderText="ดูข้อมล"
                            ShowCancelButton="False" ShowEditButton="True" ItemStyle-Width="10%" HeaderStyle-HorizontalAlign="Center">
                            <ItemStyle Width="10%"></ItemStyle>
                        </asp:CommandField>
                        <asp:BoundField DataField="ROLE_CODE" HeaderText="Role Code" ItemStyle-Width="45%" ItemStyle-HorizontalAlign="Center">
                            <ItemStyle Width="45%"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="ROLE_NAME" HeaderText="Role Name" ItemStyle-Width="45%" ItemStyle-HorizontalAlign="Center">
                            <ItemStyle Width="45%"></ItemStyle>
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
            </div>
        </div>
        <!-- /.panel-body -->
    </div>
    <!-- /.panel -->
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>

    <asp:Panel ID="Panel1" runat="server" Visible="True">
        <div class="panel panel-primary">
            <div class="panel-heading">
                ข้อมูล Role            
            </div>
            <div class="panel-body">
                <div class="row">
                    <div class="col-lg-6">
                        <div role="form">
                            <div class="form-group">
                                <table style="width: 630px; height: 80px;">
                                    <tr>
                                        <td>Role Code</td>
                                        <td class="auto-style1" style="text-align: center">:</td>
                                        <td>
                                            <asp:TextBox ID="popTxtRoleCode" class="form-control" runat="server" Height="30px" Width="125px"></asp:TextBox>
                                        </td>
                                        <td>Role Name</td>
                                        <td class="auto-style1" style="text-align: center">:</td>
                                        <td>
                                            <asp:TextBox ID="popTxtRoleName" class="form-control" runat="server" Height="30px" Width="125px"></asp:TextBox>
                                        </td>
                                        <td></td>
                                        <td>
                                            <asp:Button ID="btnSave" runat="server" Text="บันทึก" class="btn btn-primary" OnClick="btnSave_Click" />
                                        </td>
                                        <td></td>
                                        <td>
                                            <asp:Button ID="btnCancel" class="btn btn-primary" Text="ยกเลิก" runat="server" OnClick="btnCancel_Click" />
                                        </td>
                                    </tr>
                                </table>
                                <asp:Label ID="flag" runat="server" Text="Add" Visible="false"></asp:Label>
                            </div>
                        </div>
                        <table style="width: 204px; margin-top: 10px; margin-bottom: 10px; height: 36px;">
                            <tr>
                                <td></td>
                                <td>
                                    <asp:Button ID="AddFunction" runat="server" Text="เพิ่มฟังก์ชัน" class="btn btn-primary" OnClick="AddFunction_Click" />
                                </td>
                            </tr>
                        </table>

                        <asp:GridView ID="gridFunction" runat="server" ForeColor="#507CD1" AutoGenerateColumns="False"
                            DataKeyNames="ROLE_FUNCTION_ID" PageSize="20" Width="640px" EmptyDataText="ไม่พบข้อมูลสีของสินค้า"
                            Style="text-align: center;" CssClass="grid" OnRowDeleting="gridFunction_RowDeleting">
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
                    </div>
                </div>
            </div>
        </div>
    </asp:Panel>
    <asp:Panel ID="Panel2" runat="server" Visible="True" Height="272px">
        <div class="panel panel-primary">
            <div class="panel-heading">
                ข้อมูลฟังก์ชัน         
            </div>
            <div class="panel-body">
                <asp:GridView ID="gridSelectFunction" runat="server" ForeColor="#507CD1" AutoGenerateColumns="False"
                    DataKeyNames="FUNCTION_ID" PageSize="20" Width="640px" EmptyDataText="ไม่พบข้อมูลฟังก์ชัน"
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

                <table style="width: 189px; margin-top: 20px; height: 36px; margin-left: 450px;">
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
                <asp:Label ID="flag2" runat="server" Text="Add" Visible="false"></asp:Label>
            </div>
        </div>
    </asp:Panel>
    <asp:LinkButton ID="lnkFake" runat="server"></asp:LinkButton>
    <ajax:ModalPopupExtender ID="popup" runat="server" BackgroundCssClass="modalBackground" DropShadow="false" PopupControlID="Panel1" TargetControlID="lnkFake" Enabled="True">
    </ajax:ModalPopupExtender>
    <ajax:ModalPopupExtender ID="popup2" runat="server" BackgroundCssClass="modalBackground" DropShadow="false" PopupControlID="Panel2" TargetControlID="lnkFake" Enabled="True">
    </ajax:ModalPopupExtender>
</asp:Content>
