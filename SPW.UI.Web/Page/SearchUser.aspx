<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPageMainAdmin.Master" AutoEventWireup="true" CodeBehind="SearchUser.aspx.cs" Inherits="SPW.UI.Web.Page.SearchUser" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1 class="page-header">ผู้ใช้งาน</h1>
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
            ข้อมูลผู้ใช้งาน          
        </div>
        <div class="panel-body">
            <div class="row">
                <div class="col-lg-6">
                    <div class="form">
                        <div class="form-group">
                            <table style="width: 567px">
                                <tr>
                                    <td>ชื่อผู้ใช้งาน</td>
                                    <td class="auto-style1" style="text-align: center">:</td>
                                    <td>
                                        <asp:TextBox ID="txtUsername" class="form-control" runat="server"></asp:TextBox>
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
                <asp:GridView ID="gridUsername" runat="server" ForeColor="#507CD1" AutoGenerateColumns="False"
                    DataKeyNames="USER_ID" PageSize="20" Width="100%" EmptyDataText="ไม่พบข้อมูลผู้ใช้งาน"
                    OnRowEditing="gridDepartment_EditCommand" OnPageIndexChanging="gridDepartment_PageIndexChanging"
                    Style="text-align: center" CssClass="grid">
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:CommandField ButtonType="Image" EditImageUrl="~/Image/Icon/find.png" HeaderText="ดูข้อมล"
                            ShowCancelButton="False" ShowEditButton="True" ItemStyle-Width="10%" HeaderStyle-HorizontalAlign="Center">
                            <ItemStyle Width="10%"></ItemStyle>
                        </asp:CommandField>
                        <asp:BoundField DataField="USER_NAME" HeaderText="ชื่อผู้ใช้งาน" ItemStyle-Width="45%" ItemStyle-HorizontalAlign="Center">
                            <ItemStyle Width="45%"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="EMPLOYEE.EMPLOYEE_NAME" HeaderText="ชื่อพนักงาน" ItemStyle-Width="45%" ItemStyle-HorizontalAlign="Center">
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
                ข้อมูลผู้ใช้งาน             
            </div>
            <div class="panel-body">
                <div class="row">
                    <div class="col-lg-6">
                        <div role="form">
                            <div class="form-group">
                                <table style="width: 630px; height: 80px;">
                                    <tr>
                                        <td>ชื่อผู้ใช้งาน</td>
                                        <td class="auto-style1" style="text-align: center">:</td>
                                        <td>
                                            <asp:TextBox ID="popTxtUsername" class="form-control" runat="server" Height="30px" Width="125px"></asp:TextBox>
                                        </td>
                                        <td >รหัสหัสผู้ใช้งาน</td>
                                        <td class="auto-style1" style="text-align: center">:</td>
                                        <td>
                                            <asp:TextBox ID="popTxtPassword" class="form-control" runat="server" Height="30px" Width="125px" TextMode="Password"></asp:TextBox>
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
                                    <tr>
                                        <td>Role</td>
                                        <td class="auto-style1" style="text-align: center">:</td>
                                        <td>
                                             <asp:DropDownList ID="dllRole" class="form-control" runat="server" Height="35px" Width="145px">
                                            </asp:DropDownList>
                                        </td>
                                        <td>พนักงาน</td>
                                        <td class="auto-style1" style="text-align: center">:</td>
                                        <td>
                                             <asp:DropDownList ID="dllEmp" class="form-control" runat="server" Height="35px" Width="145px">
                                            </asp:DropDownList>
                                        </td>
                                        <td></td>
                                        <td>
                                           
                                        </td>
                                        <td></td>
                                        <td>
                                           
                                        </td>
                                    </tr>
                                </table>
                                <asp:Label ID="flag" runat="server" Text="Add" Visible="false"></asp:Label>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </asp:Panel>
    <asp:LinkButton ID="lnkFake" runat="server"></asp:LinkButton>
    <ajax:ModalPopupExtender ID="popup" runat="server" BackgroundCssClass="modalBackground" DropShadow="false" PopupControlID="Panel1" TargetControlID="lnkFake" Enabled="True">
    </ajax:ModalPopupExtender>
</asp:Content>
