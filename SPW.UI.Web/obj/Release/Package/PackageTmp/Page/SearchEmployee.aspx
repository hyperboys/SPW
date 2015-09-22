<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPageMainAdmin.Master" AutoEventWireup="true" CodeBehind="SearchEmployee.aspx.cs" Inherits="SPW.UI.Web.Page.SearchEmployee" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1 class="page-header">พนักงาน</h1>
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
    </style>
    <div class="panel panel-primary">
        <div class="panel-heading">
            ข้อมูลพนักงาน             
        </div>
        <div class="panel-body">
            <div class="row">
                <div class="col-lg-6">
                    <div class="form">
                        <div class="form-group">
                            <table style="width: 567px">
                                <tr>
                                    <td>รหัสผนักงาน</td>
                                    <td class="auto-style1" style="text-align: center">:</td>
                                    <td>
                                        <asp:TextBox ID="txtEmployeeCode" class="form-control" runat="server"></asp:TextBox>
                                    </td>
                                    <td class="auto-style1" style="text-align: center">&nbsp;</td>
                                    <td>
                                        <asp:Button ID="btnSearch" class="btn btn-primary" runat="server" Text="ค้นหา" OnClick="btnSearch_Click" Height="30px" Width="70px" />
                                    </td>
                                    <td class="auto-style1" style="text-align: center">&nbsp;</td>
                                    <td>
                                        <asp:Button ID="btnReset" class="btn btn-primary" runat="server" Height="30px" Width="70px" Text="รีเซท" OnClick="btnReset_Click" />
                                    </td>
                                    <td class="auto-style1" style="text-align: center">&nbsp;</td>
                                    <td>
                                        <asp:Button ID="btnAdd" class="btn btn-primary" runat="server" Height="30px" Width="70px" Text="เพิ่ม" OnClick="btnAdd_Click" />
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
                <asp:GridView ID="gridEmployee" runat="server" ForeColor="#507CD1" AutoGenerateColumns="False"
                    DataKeyNames="EMPLOYEE_ID" PageSize="20" Width="100%" EmptyDataText="ไม่พบข้อมูลพนักงาน"
                    OnRowEditing="gridEmployee_EditCommand" OnPageIndexChanging="gridEmployee_PageIndexChanging"
                    Style="text-align: center" CssClass="grid">
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:CommandField ButtonType="Image" EditImageUrl="~/Image/Icon/find.png" HeaderText="ดูข้อมล"
                            ShowCancelButton="False" ShowEditButton="True" ItemStyle-Width="10%" HeaderStyle-HorizontalAlign="Center">
                            <ItemStyle Width="10%"></ItemStyle>
                        </asp:CommandField>
                        <asp:BoundField DataField="EMPLOYEE_CODE" HeaderText="รหัสพนักงาน" ItemStyle-Width="30%">
                            <ItemStyle Width="30%"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="EMPLOYEE_NAME" HeaderText="ชื่อ" ItemStyle-Width="30%">
                            <ItemStyle Width="30%"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="EMPLOYEE_SURNAME" HeaderText="นามสกุล" ItemStyle-Width="30%">
                            <ItemStyle Width="30%"></ItemStyle>
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
                ข้อมูลพนักงาน              
            </div>
            <div class="panel-body">
                <div class="row">
                    <div class="col-lg-6">
                        <div class="form">
                            <div class="form-group">
                                <table style="width: 803px; height: 80px;">
                                    <tr>
                                        <td>รหัสผนักงาน</td>
                                        <td style="text-align: center">:</td>
                                        <td>
                                            <asp:TextBox ID="popTxtEmployeeCode" class="form-control" runat="server" Height="35px" Width="200px"></asp:TextBox>
                                        </td>
                                        <td></td>
                                        <td>แผนก</td>
                                        <td style="text-align: center">:</td>
                                        <td>
                                            <asp:DropDownList ID="ddlDepartment" class="form-control" runat="server" Height="35px" Width="200px">
                                                <asp:ListItem Value="0">กรุณาเลือก</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td></td>
                                        <td>
                                            <asp:Button ID="btnSave" runat="server" Text="บันทึก" class="btn btn-primary" OnClick="btnSave_Click" />

                                        </td>
                                    </tr>
                                    <tr>
                                        <td>ชื่อ</td>
                                        <td style="text-align: center">:</td>
                                        <td>
                                            <asp:TextBox ID="txtName" class="form-control" runat="server" Height="35px" Width="200px"></asp:TextBox>
                                        </td>
                                        <td></td>
                                        <td>นามสกุล</td>
                                        <td style="text-align: center">:</td>
                                        <td>
                                            <asp:TextBox ID="txtLastName" class="form-control" runat="server" Height="35px" Width="200px"></asp:TextBox>
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
                                    <asp:Button ID="AddZone" runat="server" Text="เพิ่มการคุมโซนราคา" class="btn btn-primary" OnClick="AddZone_Click" />
                                </td>
                            </tr>
                        </table>

                        <asp:GridView ID="gridZone" runat="server" ForeColor="#507CD1" AutoGenerateColumns="False"
                            DataKeyNames="ZONE_DETAIL_ID" PageSize="20" Width="640px" EmptyDataText="ไม่พบข้อมูลสีของสินค้า"
                            Style="text-align: center;" CssClass="grid" OnRowDeleting="gridZone_RowDeleting">
                            <AlternatingRowStyle BackColor="White" />
                            <Columns>
                                <asp:TemplateField HeaderText="ลบ" ShowHeader="False">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="ImageButton1" runat="server" CausesValidation="False" CommandName="Delete"
                                            ImageUrl="~/Image/Icon/close.png" Text="ลบ" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="ZONE.ZONE_CODE" HeaderText="รหัสราคาขายสินค้า" ItemStyle-Width="45%" ItemStyle-HorizontalAlign="Center">
                                    <ItemStyle Width="45%"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="ZONE.ZONE_NAME" HeaderText="ชื่อราคาขายสินค้า" ItemStyle-Width="45%" ItemStyle-HorizontalAlign="Center">
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
        </div>
    </asp:Panel>
    <asp:Panel ID="Panel2" runat="server" Visible="True" Height="272px">
        <div class="panel panel-primary">
            <div class="panel-heading">
                ข้อมูลราคาขายสินค้า      
            </div>
            <div class="panel-body">
                <asp:GridView ID="gridSelectZone" runat="server" ForeColor="#507CD1" AutoGenerateColumns="False"
                    DataKeyNames="ZONE_ID" PageSize="20" Width="640px" EmptyDataText="ไม่พบข้อมูลฟังก์ชัน"
                    OnPageIndexChanging="gridSelectZone_PageIndexChanging"
                    Style="text-align: center" CssClass="grid">
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:TemplateField HeaderText="เลือก" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:CheckBox ID="check" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="ZONE_CODE" HeaderText="รหัสราคาขายสินค้า" ItemStyle-Width="45%" ItemStyle-HorizontalAlign="Center">
                            <ItemStyle Width="45%"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="ZONE_NAME" HeaderText="ชื่อราคาขายสินค้า" ItemStyle-Width="45%" ItemStyle-HorizontalAlign="Center">
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

                <table style="width: 189px; margin-top: 20px; height: 36px; margin-left: 450px;">
                    <tr>
                        <td></td>
                        <td>
                            <asp:Button ID="btnAddZone" runat="server" Text="บันทึก" class="btn btn-primary" OnClick="btnAddZone_Click" />
                        </td>
                        <td>
                            <asp:Button ID="btnCancelZone" class="btn btn-primary" Text="ยกเลิก" runat="server" OnClick="btnCancelZone_Click" />
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
