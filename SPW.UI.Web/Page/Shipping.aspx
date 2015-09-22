<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPageMainAdmin.Master" AutoEventWireup="true"
    CodeBehind="Shipping.aspx.cs" Inherits="SPW.UI.Web.Page.Shipping" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1 class="page-header">จัดของ</h1>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <style type="text/css">
        .right {
            text-align: right;
        }

        .auto-style6 {
            width: 10px;
        }
        .auto-style7 {
            width: 110px;
        }
    </style>
    <div class="panel panel-primary">
        <div class="panel-heading">
            ข้อมูลใบขึ้นของ       
        </div>
        <div class="panel-body">
            <div class="row">
                <div class="col-lg-6">
                    <div class="form">
                        <div class="form-group">
                            <table style="width: 630px">
                                <tr>
                                    <td>รหัสใบขึ้นของ</td>
                                    <td class="auto-style6" style="text-align: center">:</td>
                                    <td>
                                        <asp:TextBox ID="txtDeliveryCode" class="form-control" runat="server" Width="115px"></asp:TextBox>
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
                                    <td>&nbsp;</td>
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
                    DataKeyNames="ORDER_ID" PageSize="20" Width="100%" EmptyDataText="ไม่พบข้อมูลใบขึ้นของ"
                    OnRowEditing="gridProduct_EditCommand" OnPageIndexChanging="gridProduct_PageIndexChanging"
                    Style="text-align: center" CssClass="grid">
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:CommandField ButtonType="Image" EditImageUrl="~/Image/Icon/find.png" HeaderText="ดูข้อมล"
                            ShowCancelButton="False" ShowEditButton="True" ItemStyle-Width="10%" HeaderStyle-HorizontalAlign="Center">
                            <ItemStyle Width="10%"></ItemStyle>
                        </asp:CommandField>
                        <asp:BoundField DataField="ORDER_CODE" HeaderText="รหัสใบสั่งซื้อ" ItemStyle-Width="30%" ItemStyle-HorizontalAlign="Center">
                            <ItemStyle Width="30%"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="STORE.STORE_CODE" HeaderText="รหัสร้านค้า" ItemStyle-Width="30%" ItemStyle-HorizontalAlign="Center">
                            <ItemStyle Width="30%"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="STORE.STORE_NAME" HeaderText="ชื่อร้านค้า" ItemStyle-Width="30%" ItemStyle-HorizontalAlign="Center">
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
                ข้อมูลใบขึ้นของ           
            </div>
            <div class="panel-body">
                <table style="width: 683px; height: 70px;">
                    <tr>
                        <td class="auto-style7">เลทะเบียนรถ</td>
                        <td style="text-align: center" class="auto-style1">:</td>
                        <td>
                               <asp:DropDownList ID="ddlVehicle" class="form-control" runat="server" Height="35px" Width="200px">
                                <asp:ListItem Value="0">กรุณาเลือก</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                </table>
                <asp:GridView ID="gridProductDetail" runat="server" ForeColor="#507CD1" AutoGenerateColumns="False"
                    DataKeyNames="ORDER_DETAIL_ID" PageSize="20" Width="683px" EmptyDataText="ไม่พบข้อมูลสินค้า"
                    Style="text-align: center;" CssClass="grid">
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:BoundField DataField="PRODUCT.PRODUCT_CODE" HeaderText="รหัสสินค้า" ItemStyle-Width="20%" ItemStyle-HorizontalAlign="Center">
                            <ItemStyle Width="20%"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="PRODUCT.PRODUCT_NAME" HeaderText="ชื่อสินค้า" ItemStyle-Width="20%" ItemStyle-HorizontalAlign="Center">
                            <ItemStyle Width="20%"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="PRODUCT_QTY" HeaderText="จำนวนทั้งหมด" ItemStyle-Width="20%" ItemStyle-HorizontalAlign="Center">
                            <ItemStyle Width="20%"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="PRODUCT_SEND_QTY" HeaderText="จำนวนที่ส่งแล้ว" ItemStyle-Width="20%" ItemStyle-HorizontalAlign="Center">
                            <ItemStyle Width="20%"></ItemStyle>
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="จำนวนจัดส่ง" ItemStyle-Width="15%" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:TextBox ID="txtQty" runat="server" Style="text-align: center" class="form-control" Text="0"></asp:TextBox>
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
                <table style="width: 204px; margin-top: 27px; height: 36px; margin-left: 431px;">
                    <tr>
                        <td></td>
                        <td>
                            <asp:Button ID="btnSave" runat="server" Text="บันทึกและพิมพ์" class="btn btn-primary" OnClick="btnSave_Click" />
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
    </asp:Panel>
    <asp:LinkButton ID="lnkFake" runat="server"></asp:LinkButton>
    <ajax:ModalPopupExtender ID="popup" runat="server" BackgroundCssClass="modalBackground" DropShadow="false" PopupControlID="Panel1" TargetControlID="lnkFake" Enabled="True">
    </ajax:ModalPopupExtender>
</asp:Content>
