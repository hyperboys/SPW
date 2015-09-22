<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPageMainAdmin.Master" AutoEventWireup="true" CodeBehind="SearchCategory.aspx.cs" Inherits="SPW.UI.Web.Page.SearchCategory" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1 class="page-header">หมวดหมู่สินค้า</h1>
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
            ข้อมูลหมวดหมู่สินค้า         
        </div>
        <div class="panel-body">
            <div class="row">
                <div class="col-lg-6">
                    <form role="form">
                        <div class="form-group">
                            <table style="width: 611px">
                                <tr>
                                    <td>รหัสหมวดหมู่สินค้า</td>
                                    <td class="auto-style1" style="text-align: center">:</td>
                                    <td>
                                        <asp:TextBox ID="txtCategoryCode" class="form-control" runat="server"></asp:TextBox>
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
                    </form>
                </div>
                <!-- /.col-lg-6 (nested) -->
            </div>
            <!-- /.row (nested) -->
            <div class="panel panel-primary">
                <asp:GridView ID="gridCategory" runat="server" ForeColor="#507CD1" AutoGenerateColumns="False"
                    DataKeyNames="CATEGORY_ID" PageSize="20" Width="100%" EmptyDataText="ไม่พบข้อมูลหมวดหมู่สินค้า"
                    OnRowEditing="gridCategory_EditCommand" OnPageIndexChanging="gridCategory_PageIndexChanging"
                    Style="text-align: center" CssClass="grid">
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:CommandField ButtonType="Image" EditImageUrl="~/Image/Icon/find.png" HeaderText="ดูข้อมล"
                            ShowCancelButton="False" ShowEditButton="True" ItemStyle-Width="10%" HeaderStyle-HorizontalAlign="Center">
                            <ItemStyle Width="10%"></ItemStyle>
                        </asp:CommandField>
                        <asp:BoundField DataField="CATEGORY_CODE" HeaderText="รหัสหมวดหมู่สินค้า" ItemStyle-Width="45%" ItemStyle-HorizontalAlign="Center">
                            <ItemStyle Width="45%"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="CATEGORY_NAME" HeaderText="ชื่อหมวดหมู่สินค้า" ItemStyle-Width="45%" ItemStyle-HorizontalAlign="Center">
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
                ข้อมูลหมวดหมู่สินค้า
            </div>
            <div class="panel-body">
                <div class="row">
                    <div class="col-lg-6">
                        <div class="form">
                            <div class="form-group">
                                <table style="width: 797px; height: 80px;">
                                    <tr>
                                        <td class="auto-style5">รหัสหมวดหมู่สินค้า</td>
                                        <td class="auto-style1" style="text-align: center">:</td>
                                        <td class="auto-style2">
                                            <asp:TextBox ID="popTxtCategoryCode" class="form-control" runat="server" Height="30px" Width="125px"></asp:TextBox>
                                        </td>
                                        <td class="auto-style3">ชื่อหมวดหมู่สินค้า</td>
                                        <td class="auto-style1" style="text-align: center">:</td>
                                        <td class="auto-style2">
                                            <asp:TextBox ID="txtCategoryName" class="form-control" runat="server" Height="30px" Width="147px"></asp:TextBox>
                                        </td>
                                        <td class="auto-style4"></td>
                                        <td>
                                            <asp:Button ID="btnSave" runat="server" Text="บันทึก" class="btn btn-primary" OnClick="btnSave_Click" />
                                        </td>
                                        <td class="auto-style6"></td>
                                        <td>
                                            <asp:Button ID="btnCancel" class="btn btn-primary" Text="ยกเลิก" runat="server" OnClick="btnCancel_Click"/>
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
