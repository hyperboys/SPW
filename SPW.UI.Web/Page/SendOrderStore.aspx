<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPageMainAdmin.Master" AutoEventWireup="true"
    CodeBehind="SendOrderStore.aspx.cs" Inherits="SPW.UI.Web.Page.SendOrderStore" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1 class="page-header">เลือกร้านค้า</h1>
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
            width: 216px;
        }

        .auto-style4 {
            width: 310px;
        }

        .auto-style5 {
            width: 16px;
        }
        .auto-style6 {
            width: 58px;
        }
        .auto-style7 {
            width: 58px;
            height: 14px;
        }
        .auto-style8 {
            height: 14px;
        }
    </style>
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="panel panel-primary">
                <div class="panel-heading">
                    ข้อมูลร้านค้า        
                </div>
                <div class="panel-body">
                    <div class="row">
                        <div class="col-lg-6">
                            <div class="form">
                                <div class="form-group">
                                    <table style="width: 748px; height: 36px;">
                                        <tr>
                                            <td>จังหวัด</td>
                                            <td class="auto-style1" style="text-align: center">:</td>
                                            <td>
                                                <asp:DropDownList ID="ddlProvince" class="form-control" runat="server" Height="35px" Width="200px" AutoPostBack="True" OnSelectedIndexChanged="ddlProvince_SelectedIndexChanged">
                                                    <asp:ListItem Value="0">เลือกทั้งหมด</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td class="auto-style1" style="text-align: center">&nbsp;</td>
                                            <td>สายจัดรถ</td>
                                            <td class="auto-style1" style="text-align: center">:</td>
                                            <td>
                                                <asp:DropDownList ID="ddlTranspot" class="form-control" runat="server" Height="35px" Width="200px" SelectedValue='<%# Eval("Key") %>'>
                                                    <asp:ListItem Value="0">กรุณาเลือก</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td class="auto-style1" style="text-align: center">&nbsp;</td>
                                            <td>
                                                <asp:Button ID="btnSearch" class="btn btn-primary" runat="server" Text="ค้นหา" Height="30px" Width="70px" OnClick="btnSearch_Click" />
                                            </td>
                                            <td class="auto-style1" style="text-align: center">&nbsp;</td>
                                            <td></td>
                                        </tr>
                                    </table>
                                    <table style="width: 748px; margin-top: 11px;">
                                        <tr>
                                            <td class="auto-style2">
                                                <asp:GridView ID="grdProvince" runat="server" ForeColor="#507CD1" AutoGenerateColumns="False"
                                                    DataKeyNames="PROVINCE_ID" PageSize="20" Width="100%" EmptyDataText="ไม่พบข้อมูลร้านค้า"
                                                    Style="text-align: center" CssClass="grid" OnRowDeleting="grdProvince_RowDeleting">
                                                    <AlternatingRowStyle BackColor="White" />
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="No" ItemStyle-Width="10%">
                                                            <ItemTemplate>
                                                                <%# Container.DataItemIndex + 1 %>
                                                            </ItemTemplate>
                                                            <ItemStyle Width="10" />
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="PROVINCE_NAME" HeaderText="จังหวัด" ItemStyle-Width="75%" ItemStyle-HorizontalAlign="Center">
                                                            <ItemStyle Width="75%"></ItemStyle>
                                                        </asp:BoundField>
                                                        <asp:TemplateField HeaderText="ยกเลิก" ItemStyle-Width="15%">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lbtnDelete" runat="server" CommandName="Delete" CausesValidation="False">
                                                                    <div class='glyphicon glyphicon-remove'></div>
                                                                </asp:LinkButton>
                                                            </ItemTemplate>
                                                            <ItemStyle Width="15%" />
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
                                            </td>
                                            <td class="auto-style5"></td>
                                            <td class="auto-style4">
                                                <table>
                                                    <tr>
                                                        <td class="auto-style6">รหัสร้าน</td>
                                                        <td class="auto-style1" style="text-align: center">:</td>
                                                        <td>
                                                            <asp:TextBox ID="txtStoreCode" class="form-control" runat="server" Height="30px" Width="200px" placeholder="รหัสร้านค้า" data-provide="typeahead" data-items="5" autocomplete="off" />
                                                        </td>
                                                        <td></td>
                                                    </tr>
                                                    <tr>
                                                        <td class="auto-style7"></td>
                                                        <td class="auto-style8"></td>
                                                        <td class="auto-style8"></td>
                                                        <td class="auto-style8"></td>
                                                    </tr>
                                                    <tr>
                                                        <td class="auto-style6">ชื่อร้าน</td>
                                                        <td class="auto-style1" style="text-align: center">:</td>
                                                        <td>
                                                            <asp:TextBox ID="txtStoreName" class="form-control" runat="server" Height="30px" Width="200px" placeholder="ชื่อร้าน" data-provide="typeahead" data-items="5" autocomplete="off" />
                                                        </td>
                                                        <td></td>
                                                    </tr>
                                                    <tr>
                                                        <td class="auto-style6">&nbsp;</td>
                                                        <td>&nbsp;</td>
                                                        <td>&nbsp;</td>
                                                        <td></td>
                                                    </tr>
                                                    <tr>
                                                        <td class="auto-style6">&nbsp;</td>
                                                        <td class="auto-style1" style="text-align: center">&nbsp;</td>
                                                        <td>&nbsp;</td>
                                                        <td></td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="panel panel-primary">

                        <asp:GridView ID="grideInOrder" runat="server" ForeColor="#507CD1" AutoGenerateColumns="False"
                            DataKeyNames="STORE_ID" PageSize="20" Width="100%" EmptyDataText="ไม่พบข้อมูลร้านค้า"
                            Style="text-align: center" CssClass="grid">
                            <AlternatingRowStyle BackColor="White" />
                            <Columns>
                                <asp:BoundField DataField="STORE_ID" HeaderText="MasterKey" ItemStyle-Width="0%" ItemStyle-HorizontalAlign="Center" Visible="false">
                                    <ItemStyle Width="0%"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="SECTOR_NAME" HeaderText="ภาค" ItemStyle-Width="15%" ItemStyle-HorizontalAlign="Center">
                                    <ItemStyle Width="15%"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="PROVINCE_NAME" HeaderText="จังหวัด" ItemStyle-Width="15%" ItemStyle-HorizontalAlign="Center">
                                    <ItemStyle Width="15%"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="STORE_CODE" HeaderText="รหัสร้านค้า" ItemStyle-Width="20%" ItemStyle-HorizontalAlign="Center">
                                    <ItemStyle Width="20%"></ItemStyle>
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
                                <asp:TemplateField HeaderText="เลือก" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="check" runat="server" />
                                    </ItemTemplate>
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


                    </div>
                    <div style="margin-top: 10px;">
                        <table style="float: left;">
                            <tr>
                                <td></td>
                            </tr>
                        </table>
                        <table style="float: right;">
                            <tr>
                                <td>
                                    <asp:Button ID="btnNextStep" class="btn btn-primary" runat="server" Text="ต่อไป" Height="30px" Width="120px" OnClick="btnNextStep_Click" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnSearch" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="ddlProvince" EventName="SelectedIndexChanged" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
