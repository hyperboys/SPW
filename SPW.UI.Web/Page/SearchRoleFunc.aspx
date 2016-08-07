<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPageMainAdmin.Master" 
    AutoEventWireup="true" CodeBehind="SearchRoleFunc.aspx.cs" Inherits="SPW.UI.Web.Page.SearchRoleFunc" %>

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
            </div>
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
    </div>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
</asp:Content>
