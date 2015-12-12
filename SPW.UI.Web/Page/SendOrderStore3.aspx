<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPageMainAdmin.Master" AutoEventWireup="true"
    CodeBehind="SendOrderStore3.aspx.cs" Inherits="SPW.UI.Web.Page.SendOrderStore3" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1 class="page-header">
    <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Page/SendOrderStore.aspx">เลือกร้านค้า</asp:HyperLink>
    /
    <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl="~/Page/SendOrderStore2.aspx">จัดการร้านค้า</asp:HyperLink>
    /
    เลือกรถ</h1>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
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
                            <table style="width: 565px">
                                <tr>
                                    <td>ทะเบียนรถยนต์</td>
                                    <td class="auto-style1" style="text-align: center">:</td>
                                    <td>
                                        <asp:DropDownList ID="ddlVehicle" class="form-control" runat="server" Height="35px" Width="200px">
                                        </asp:DropDownList>
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
                <asp:GridView ID="grideInOrder" runat="server" ForeColor="#507CD1" AutoGenerateColumns="False"
                    DataKeyNames="STORE_ID" PageSize="20" Width="100%" EmptyDataText="ไม่พบข้อมูลร้านค้า"
                    Style="text-align: center" CssClass="grid">
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                       <asp:BoundField DataField="SECTOR_NAME" HeaderText="ภาค" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center">
                            <ItemStyle Width="10%"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="PROVINCE_NAME" HeaderText="จังหวัด" ItemStyle-Width="15%" ItemStyle-HorizontalAlign="Center">
                            <ItemStyle Width="15%"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="STORE_CODE" HeaderText="รหัสร้านค้า" ItemStyle-Width="15%" ItemStyle-HorizontalAlign="Center">
                            <ItemStyle Width="15%"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="STORE_NAME" HeaderText="ชื่อร้านค้า" ItemStyle-Width="20%" ItemStyle-HorizontalAlign="Center">
                            <ItemStyle Width="20%"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="WEIGHT" HeaderText="น้ำหนัก" ItemStyle-Width="15%" ItemStyle-HorizontalAlign="Center">
                            <ItemStyle Width="15%"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="TOTAL" HeaderText="ราคา" ItemStyle-Width="20%" ItemStyle-HorizontalAlign="Center">
                            <ItemStyle Width="20%"></ItemStyle>
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
                <div class="row" style="margin-top: 20px;">
                <div class="col-lg-6">
                    <div role="form">
                        <div class="form-group">
                            <table style="width: 893px">
                                <tr>
                                    <td class="auto-style21" style="text-align: right">
                                        <asp:Label ID="lb_TotalWeight" runat="server" Text="น้ำหนักรวม 0 กก." Font-Bold="True"  ></asp:Label>                         
                                        <b>&nbsp;</b></td>
                                </tr>
                                <tr>
                                    <td class="auto-style21" style="text-align: right">
                                        <asp:Label ID="lb_TotalAmount" runat="server" Font-Bold="True" Text="ราคารวม 0 บาท"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </div>
            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
            </div>
            <div style="margin-top: 10px;">
                 <table style="float:left;">
                    <tr>
                        <td>
                            <asp:Button ID="Button1" class="btn btn-primary"  runat="server" Text="ย้อนกลับ" Height="30px" Width="120px" PostBackUrl="~/Page/SendOrderStore2.aspx"/>
                        </td>
                    </tr>
                </table>
                <table style="float:right;">
                    <tr>
                        <td>
                            <asp:Button ID="btnNext" class="btn btn-primary"  runat="server" Text="ต่อไป" Height="30px" Width="120px" OnClick="btnNext_Click" />
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
</asp:Content>
