<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPageMainAdmin.Master" AutoEventWireup="true" CodeBehind="Order.aspx.cs" Inherits="SPW.UI.Web.Page.Order" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1 class="page-header">สั่งสินค้า</h1>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <style type="text/css">
        .right
        {
            text-align: right;
        }

        .auto-style5
        {
            width: 13px;
        }

        .auto-style10
        {
            width: 67px;
        }
    </style>
    <div class="panel panel-primary">
        <div class="panel-heading">
            ข้อมูลสั่งสินค้า        
        </div>
        <div class="panel-body">
            <div class="row">
                <div class="col-lg-6">
                    <div class="form">
                        <div class="form-group">
                            <table style="width: 724px; height: 80px;">
                                <tr>
                                    <td class="auto-style10">ภาค</td>
                                    <td style="text-align: center" class="auto-style5">:</td>
                                    <td>
                                        <asp:DropDownList ID="ddlSector" class="form-control" runat="server" Height="35px" Width="200px" AutoPostBack="True" OnSelectedIndexChanged="ddlSector_SelectedIndexChanged">
                                            <asp:ListItem Value="0">กรุณาเลือก</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td></td>
                                    <td>จังหวัด</td>
                                    <td style="text-align: center" class="auto-style5">:</td>
                                    <td>
                                        <asp:DropDownList ID="ddlProvince" class="form-control" runat="server" Height="35px" Width="200px" Enabled="False" OnSelectedIndexChanged="ddlProvince_SelectedIndexChanged" AutoPostBack="True">
                                            <asp:ListItem Value ="0">กรุณาเลือก</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td style="text-align: center" class="auto-style5"></td>
                                    <td>
                                         <asp:Button ID="btnSerch" runat="server" Text="ค้นหา" class="btn btn-primary" OnClick="btnSerch_Click" Width="95px"/>
                                    </td>
                                </tr>
                                 <tr>
                                    <td class="auto-style10">
                                        <asp:Label ID="lblRoad" runat="server" Text="ถนน" Visible="False"></asp:Label></td>
                                    <td style="text-align: center" class="auto-style5"><asp:Label ID="lblComma" runat="server" Text=":" Visible="False"></asp:Label></td>
                                    <td>
                                        <asp:DropDownList ID="ddlRoad" class="form-control" runat="server" Height="35px" Width="200px" Visible="False">
                                            <asp:ListItem Value="0">กรุณาเลือก</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                     <td></td>
                                    <td></td>
                                    <td style="text-align: center" class="auto-style5"></td>
                                    <td></td>
                                    <td style="text-align: center" class="auto-style5"></td>
                                    <td></td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </div>
                <!-- /.col-lg-6 (nested) -->
            </div>
            <!-- /.row (nested) -->
            <div class="panel panel-primary">
                <asp:GridView ID="gridStore" runat="server" ForeColor="#507CD1" AutoGenerateColumns="False"
                    DataKeyNames="STORE_ID" PageSize="20" Width="100%" EmptyDataText="ไม่พบข้อมูลร้านค้า"
                    OnRowEditing="gridProduct_EditCommand" OnPageIndexChanging="gridProduct_PageIndexChanging"
                    Style="text-align: center" CssClass="grid" Visible="False">
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:CommandField ButtonType="Image" EditImageUrl="~/Image/Icon/find.png" HeaderText="ดูข้อมล"
                            ShowCancelButton="False" ShowEditButton="True" ItemStyle-Width="10%" HeaderStyle-HorizontalAlign="Center">
                            <ItemStyle Width="10%"></ItemStyle>
                        </asp:CommandField>
                        <asp:BoundField DataField="STORE_CODE" HeaderText="รหัสร้านค้า" ItemStyle-Width="45%" ItemStyle-HorizontalAlign="Center">
                            <ItemStyle Width="45%"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="STORE_NAME" HeaderText="ชื่อร้านค้า" ItemStyle-Width="45%" ItemStyle-HorizontalAlign="Center">
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
</asp:Content>
