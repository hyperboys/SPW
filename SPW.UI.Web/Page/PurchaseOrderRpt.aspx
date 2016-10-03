<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage/MasterPageMainAdmin.Master" CodeBehind="PurchaseOrderRpt.aspx.cs" Inherits="SPW.UI.Web.Page.PurchaseOrderRpt" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1 class="page-header">พิมพ์ใบสั่งซื้อ</h1>
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
            พิมพ์ใบสั่งซื้อ        
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
                                        <asp:TextBox ID="txtBkNo" class="form-control" runat="server" Width="115px"></asp:TextBox>
                                    </td>
                                    <td style="text-align: center">&nbsp;</td>
                                    <td>ชื่อ</td>
                                    <td class="auto-style6" style="text-align: center"></td>
                                    <td>
                                        <asp:TextBox ID="txtRnNo" class="form-control" runat="server" Width="115px"></asp:TextBox>
                                    </td>
                                    <td style="text-align: center">&nbsp;</td>
                                    <td>
                                        <%--<asp:Button ID="btnPrint" class="btn btn-primary" runat="server" Text="พิมพ์" OnClick="btnPrint_Click"/>--%>
                                    </td>
                                    <td style="text-align: center">&nbsp;</td>
                                    <td>
                                    </td>
                                    <td style="text-align: center">&nbsp;</td>
                                    <td>
                                        <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="true" HasToggleGroupTreeButton="False" ToolPanelView="None"/>
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
                <asp:GridView ID="gridPO" runat="server" ForeColor="#507CD1" AutoGenerateColumns="False"
                    DataKeyNames="PO_BK_NO" PageSize="20" Width="100%" EmptyDataText="ไม่พบข้อมูลใบสั่งซือ"
                    OnRowEditing="gridPO_EditCommand" OnPageIndexChanging="gridPO_PageIndexChanging"
                    Style="text-align: center" CssClass="grid">
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:CommandField ButtonType="Image" EditImageUrl="~/Image/Icon/find.png" HeaderText="พิมพ์"
                            ShowCancelButton="False" ShowEditButton="True" ItemStyle-Width="10%" HeaderStyle-HorizontalAlign="Center">
                            <ItemStyle Width="10%"></ItemStyle>
                        </asp:CommandField>
                        <asp:BoundField DataField="PO_BK_NO" HeaderText="เล่มที่ PO" ItemStyle-Width="40%" ItemStyle-HorizontalAlign="Center">
                            <ItemStyle Width="20%"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="PO_RN_NO" HeaderText="เลขที่ PO" ItemStyle-Width="40%" ItemStyle-HorizontalAlign="Center">
                            <ItemStyle Width="20%"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="VENDOR_ID" HeaderText="รหัสซัพพลายเออร์" ItemStyle-Width="40%" ItemStyle-HorizontalAlign="Center">
                            <ItemStyle Width="20%"></ItemStyle>
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="สถานะ" ItemStyle-Width="10%">
                            <ItemTemplate>
                                <%# (Eval("PO_HD_STATUS").ToString() == "10") ? "Active" : ((Eval("PO_HD_STATUS").ToString() == "20") ? "Approved" : ((Eval("PO_HD_STATUS").ToString() == "30") ? "Finish" : "Cancel")) %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <%--<asp:TemplateField HeaderText="พิมพ์" ItemStyle-Width="5%">
                            <ItemTemplate>
                                <asp:LinkButton ID="lbtnEdit" runat="server" CommandName="Edit">
                                    <div class='glyphicon glyphicon glyphicon-print'></div>
                                </asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>--%>
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

