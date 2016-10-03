<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterPageMainAdmin.Master" AutoEventWireup="true" CodeBehind="SearchRawProduct.aspx.cs" Inherits="SPW.UI.Web.Page.SearchRawProduct" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1 class="page-header">วัตถุดิบ</h1>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
      <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <style type="text/css">
        .right
        {
            text-align: right;
        }

        .auto-style6
        {
            width: 10px;
        }

        </style>
    <div class="panel panel-primary">
        <div class="panel-heading">
            วัตถุดิบ        
        </div>
        <div class="panel-body">
            <div class="row">
                <div class="col-lg-6">
                    <div class="form">
                        <div class="form-group">
                            <table style="width: 630px">
                                <tr>
                                    <td>รหัส</td>
                                    <td class="auto-style6" style="text-align: center"></td>
                                    <td>
                                        <asp:TextBox ID="txtRawProductCode" class="form-control" runat="server" Width="115px"></asp:TextBox>
                                    </td>
                                    <td style="text-align: center">&nbsp;</td>
                                    <td>ชื่อ</td>
                                    <td class="auto-style6" style="text-align: center"></td>
                                    <td>
                                        <asp:TextBox ID="txtRawProductName" class="form-control" runat="server" Width="115px"></asp:TextBox>
                                    </td>
                                    <td style="text-align: center">&nbsp;</td>
                                    <td>
                                        <asp:Button ID="btnSearch" class="btn btn-primary" runat="server" Text="ค้นหา" OnClick="btnSearch_Click" />
                                    </td>
                                    <td style="text-align: center">&nbsp;</td>
                                    <td>
                                        <asp:Button ID="btnReset" class="btn btn-primary" Text="รีเซท" runat="server" OnClick="btnReset_Click" />
                                    </td>
                                    <td style="text-align: center">&nbsp;</td>
                                    <td>
                                        <asp:Button ID="btnAdd" class="btn btn-primary" Text="เพิ่ม" runat="server" OnClick="btnAdd_Click" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </div>
            </div>
            <div class="panel panel-primary">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                <asp:GridView ID="gridRawProduct" runat="server" ForeColor="#507CD1" AutoGenerateColumns="False"
                    DataKeyNames="RAW_ID" PageSize="20" Width="100%" EmptyDataText="ไม่พบข้อมูลวัตถุดิบ"
                    OnRowEditing="gridRawProduct_EditCommand" OnPageIndexChanging="gridRawProduct_PageIndexChanging"
                    OnRowDeleting="gridRawProduct_RowDeleting" OnRowDataBound="gridRawProduct_RowDataBound"
                    Style="text-align: center" CssClass="grid">
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:CommandField ButtonType="Image" EditImageUrl="~/Image/Icon/find.png" HeaderText="ดูข้อมล"
                            ShowCancelButton="False" ShowEditButton="True" ItemStyle-Width="10%" HeaderStyle-HorizontalAlign="Center">
                            <ItemStyle Width="10%"></ItemStyle>
                        </asp:CommandField>
                        <asp:BoundField DataField="RAW_NAME1" HeaderText="ชื่อสินค้า(ปกติ)" ItemStyle-Width="40%" ItemStyle-HorizontalAlign="Center">
                            <ItemStyle Width="40%"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="RAW_NAME2" HeaderText="ชื่อสินค้า(สำหรับโรงงาน)" ItemStyle-Width="40%" ItemStyle-HorizontalAlign="Center">
                            <ItemStyle Width="40%"></ItemStyle>
                        </asp:BoundField>
                         <asp:TemplateField HeaderText="ลบ" ShowHeader="False">
                        <ItemTemplate>
                            <asp:ImageButton ID="ImageButton1" runat="server"  ItemStyle-Width="10%" CausesValidation="False" CommandName="Delete"
                                ImageUrl="~/Image/Icon/close.png" Text="ลบ" />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
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
                    <asp:PlaceHolder ID="PlaceHolder1" runat="server"></asp:PlaceHolder>
                        </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
</asp:Content>

