<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPageMainAdmin.Master" AutoEventWireup="true"
    CodeBehind="InternalOrder.aspx.cs" Inherits="SPW.UI.Web.Page.InternalOrder" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1 class="page-header">Order ภายใน</h1>
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
            ข้อมูลร้านค้า        
        </div>
        <div class="panel-body">
            <div class="row">
                <div class="col-lg-6">
                    <div class="form">
                        <div class="form-group">
                            <table style="width: 748px">
                                <tr>
                                    <td>จังหวัด</td>
                                    <td class="auto-style1" style="text-align: center">:</td>
                                    <td>
                                        <asp:DropDownList ID="ddlProvince" class="form-control" runat="server" Height="35px" Width="200px" AutoPostBack="True" OnSelectedIndexChanged="ddlProvince_SelectedIndexChanged">
                                            <asp:ListItem Value="0">เลือกทั้งหมด</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                     <td class="auto-style1" style="text-align: center">&nbsp;</td>
                                    <td>ร้านค้า</td>
                                    <td class="auto-style1" style="text-align: center">:</td>
                                    <td>
                                         <asp:DropDownList ID="ddlStore" class="form-control" runat="server" Height="35px" Width="200px" Enabled="False">
                                            <asp:ListItem Value="0">เลือกทั้งหมด</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td class="auto-style1" style="text-align: center">&nbsp;</td>
                                    <td>
                                        <asp:Button ID="btnSerch" class="btn btn-primary" runat="server" Text="ค้นหา" OnClick="btnSearch_Click" Height="30px" Width="70px" />
                                    </td>
                                    <td class="auto-style1" style="text-align: center">&nbsp;</td>
                                    <td>
                                        <asp:Button ID="btnPrint" class="btn btn-primary" runat="server" Text="พิมพ์" OnClick="btnPrint_Click" Height="30px" Width="70px" />
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
                <asp:GridView ID="grideInOrder" runat="server" ForeColor="#507CD1" AutoGenerateColumns="False"
                    DataKeyNames="ORDER_ID,STORE_ID" PageSize="20" Width="100%" EmptyDataText="ไม่พบข้อมูลร้านค้า"
                    OnPageIndexChanging="grideInOrder_PageIndexChanging"
                    Style="text-align: center" CssClass="grid">
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:BoundField DataField="ORDER_CODE" HeaderText="รหัส Order" ItemStyle-Width="30%" ItemStyle-HorizontalAlign="Center">
                            <ItemStyle Width="30%"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="STORE.STORE_NAME" HeaderText="ชื่อร้านค้า" ItemStyle-Width="70%" ItemStyle-HorizontalAlign="Center">
                            <ItemStyle Width="70%"></ItemStyle>
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
