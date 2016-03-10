<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterPageMainAdmin.Master" AutoEventWireup="true" CodeBehind="ManageOrderHQEdit.aspx.cs" Inherits="SPW.UI.Web.Page.ManageOrderHQEdit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
    <script type="text/javascript">
        function check_change_qty(e) {
            var multi = parseFloat($("#" + e.id).closest('tr').find("[id*='lblPRODUCT_PRICE']").text()).toFixed(1);
            var old = parseInt($("#" + e.id).closest('tr').find("[id*='hfRemainQty']").val(), 10);
            var add = parseInt($("#" + e.id).val(), 10);
            var lblSumQty = parseInt($('#<%= lblSumQty.ClientID %>').text(), 10);
            var lblTotalPrice = parseFloat($('#<%= lblTotalPrice.ClientID %>').text());
            if (add >= 0) {
                $("#" + e.id).closest('tr').find("[id*='lblPRODUCT_PRICE_TOTAL']").text(multi * add);
                $('#<%= lblSumQty.ClientID %>').text(lblSumQty + (add - old)); 
                $('#<%= lblTotalPrice.ClientID %>').text(lblTotalPrice + ((add - old) * multi));
                $("#" + e.id).closest('tr').find("[id*='hfRemainQty']").val(add);
            }
        }
        function check_change_flee(e) {
            var old = parseInt($("#" + e.id).closest('tr').find("[id*='hfRemainFlee']").val(), 10);
            var add = parseInt($("#" + e.id).val(), 10);
            var lblFlee = parseInt($('#<%= lblFlee.ClientID %>').text(), 10);
            if (add >= 0) {
                $('#<%= lblFlee.ClientID %>').text(lblFlee + (add - old));
                $("#" + e.id).closest('tr').find("[id*='hfRemainFlee']").val(add);
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1 class="page-header">
        <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Page/ManageOrderHQ.aspx">จัดการใบสั่งซื้อ</asp:HyperLink>
        /
        <asp:Label ID="lblName" runat="server" Text="แก้ไขใบสั่งซื้อ"></asp:Label>
    </h1>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <style type="text/css">
        .right {
            text-align: right;
        }

        .grid td, .grid th {
            text-align: center;
        }

        .auto-style11 {
            width: 5px;
        }

        .auto-style12 {
            width: 81px;
        }

        .auto-style13 {
            width: 429px;
        }

        .auto-style17 {
            width: 81px;
            height: 48px;
        }

        .auto-style18 {
            width: 5px;
            height: 48px;
        }

        .auto-style19 {
            width: 429px;
            height: 48px;
        }

        .auto-style20 {
            height: 40px;
        }

        .auto-style21 {
            height: 48px;
        }
    </style>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="panel panel-primary">
                <div class="panel-heading">
                    ข้อมูลสั่งสินค้า          
                </div>
                <div class="panel-body">
                    <div class="row">
                        <div class="col-lg-6">
                            <div role="form">
                                <div class="form-group">
                                    <table style="width: 893px">
                                        <tr>
                                            <td class="auto-style17">เลขที่ใบสั่งซื้อ</td>
                                            <td style="text-align: center" class="auto-style18"></td>
                                            <td class="auto-style19">
                                                <asp:TextBox ID="txtOrderId" class="form-control" runat="server" Height="30px" Width="125px" Enabled="false"></asp:TextBox>
                                            </td>
                                            <td class="auto-style21"></td>
                                            <td class="auto-style21"></td>
                                            <td class="auto-style21"></td>
                                        </tr>
                                        <tr>
                                            <td class="auto-style17">รหัสร้าน</td>
                                            <td style="text-align: center" class="auto-style18"></td>
                                            <td class="auto-style19">
                                                <asp:TextBox ID="txtStoreCode" class="form-control" runat="server" Height="30px" Width="125px" Enabled="false"></asp:TextBox>
                                            </td>
                                            <td class="auto-style21"></td>
                                            <td class="auto-style21"></td>
                                            <td class="auto-style21"></td>
                                        </tr>
                                        <tr>
                                            <td class="auto-style12">ชื่อร้านค้า</td>
                                            <td style="text-align: center" class="auto-style11"></td>
                                            <td class="auto-style13">
                                                <asp:TextBox ID="txtStoreName" class="form-control" runat="server" Height="30px" Width="373px" Enabled="false"></asp:TextBox>
                                            </td>
                                            <td></td>
                                            <td></td>
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
                        <asp:GridView ID="gdvManageOrderHQDetail" runat="server" ForeColor="#507CD1" AutoGenerateColumns="False" OnRowDeleting="gdvManageOrderHQDetail_RowDeleting"
                            DataKeyNames="ORDER_DETAIL_ID" PageSize="20" Width="100%" EmptyDataText="ไม่พบข้อมูลหมวดหมู่สินค้า" OnRowDataBound="gdvManageOrderHQDetail_RowDataBound"
                            Style="text-align: center" CssClass="grid">
                            <AlternatingRowStyle BackColor="White" />
                            <Columns>
                                <asp:TemplateField HeaderText="No" ItemStyle-Width="5%">
                                    <ItemTemplate>
                                        <%# Container.DataItemIndex + 1 %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="PRODUCT_NAME" HeaderText="ชื่อสินค้า" ItemStyle-Width="15%" ItemStyle-HorizontalAlign="Center">
                                    <ItemStyle Width="15%"></ItemStyle>
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="ลวดลาย" ItemStyle-Width="13%" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddlCOLOR_TYPE_NAME" runat="server" class="form-control"></asp:DropDownList>
                                        <asp:HiddenField ID="hfCOLOR_TYPE_NAME" runat="server" Value='<%# Bind("COLOR_TYPE_NAME") %>' />
                                        <asp:HiddenField ID="hfPRODUCT_SEQ" runat="server" Value='<%# Bind("PRODUCT_SEQ") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="สี" ItemStyle-Width="12%" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddlCOLOR_NAME" runat="server" class="form-control"></asp:DropDownList>
                                        <asp:HiddenField ID="hfCOLOR_NAME" runat="server" Value='<%# Bind("COLOR_NAME") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="PRODUCT_WEIGHT" HeaderText="น้ำหนัก" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center">
                                    <ItemStyle Width="10%"></ItemStyle>
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="ราคาต่อหน่วย" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPRODUCT_PRICE" runat="server" Style="height: 35px;" Text='<%# Eval("PRODUCT_PRICE") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="จำนวน" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtQty" runat="server" class="form-control" Style="height: 35px;" Text='<%# Eval("PRODUCT_SEND_REMAIN_QTY") %>' OnChange="check_change_qty(this)"></asp:TextBox>
                                        <asp:HiddenField ID="hfOldRemainQty" runat="server" Value='<%# Bind("PRODUCT_SEND_REMAIN_QTY") %>' />
                                        <asp:HiddenField ID="hfOldQty" runat="server" Value='<%# Bind("PRODUCT_QTY") %>' />
                                        <asp:HiddenField ID="hfRemainQty" runat="server" Value='<%# Bind("PRODUCT_SEND_REMAIN_QTY") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="ราคารวม" ItemStyle-Width="15%" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPRODUCT_PRICE_TOTAL" runat="server" Style="height: 35px;" Text='<%# Eval("PRODUCT_PRICE_TOTAL") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="ฟรี" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtFleeQty" runat="server" class="form-control" Style="height: 35px;" Text='<%# Eval("PRODUCT_SEND_REMAIN_FLEE") %>' OnChange="check_change_flee(this)"></asp:TextBox>
                                        <asp:HiddenField ID="hfOldRemainFlee" runat="server" Value='<%# Bind("PRODUCT_SEND_REMAIN_FLEE") %>' />
                                        <asp:HiddenField ID="hfOldFlee" runat="server" Value='<%# Bind("FLEE") %>' />
                                        <asp:HiddenField ID="hfRemainFlee" runat="server" Value='<%# Bind("PRODUCT_SEND_REMAIN_FLEE") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="ยกเลิก" ItemStyle-Width="5%">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lbtnDelete" runat="server" CommandName="Delete">
                                    <div class='glyphicon glyphicon-remove'></div>
                                        </asp:LinkButton>
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
                    <div class="row">
                        <table style="margin-right: 15px;" align="right">
                            <tr>
                                <td class="auto-style21" style="text-align: right">
                                    <h4>จำนวนชิ้นทั้งหมด :
                                        <asp:Label ID="lblSumQty" runat="server"></asp:Label>
                                        ชิ้น </h4>
                                </td>
                            </tr>
                            <tr>
                                <td class="auto-style21" style="text-align: right">
                                    <h4>ของแถมทั้งหมด :
                                        <asp:Label ID="lblFlee" runat="server"></asp:Label>
                                        ชิ้น </h4>
                                </td>
                            </tr>
                            <tr>
                                <td class="auto-style21" style="text-align: right">
                                    <h4>ราคารวมทั้งหมด :
                                        <asp:Label ID="lblTotalPrice" runat="server"></asp:Label>
                                        บาท</h4>
                                </td>
                            </tr>
                        </table>
                        <asp:HiddenField ID="hfSumQty" runat="server" />
                        <asp:HiddenField ID="hfFlee" runat="server" />
                        <asp:HiddenField ID="hfTotalPrice" runat="server" />
                    </div>
                    <div style="margin-top: 10px;">
                        <table style="float: left">
                            <tr>
                                <td>
                                    <%--<asp:LinkButton ID="btnNew" Text="เพิ่มการสั่งซื้อสินค้า" class="btn btn-primary" Height="30px" Width="125px" runat="server" PostBackUrl="~/Page/OrderProduct.aspx" OnClick="btnNew_Click"></asp:LinkButton>--%>
                                </td>
                                <td>&nbsp;&nbsp;</td>
                                <td></td>
                            </tr>
                        </table>
                        <table style="float: right">
                            <tr>
                                <td>&nbsp;</td>
                                <td>&nbsp;&nbsp;</td>
                                <td>
                                    <asp:Button ID="btnCancel" runat="server" Text="ยกเลิกการสั่งซื้อ" class="btn btn-primary" Height="30px" Width="125px" OnClick="btnCancel_Click" OnClientClick="return confirm('ยกเลิกการสั่งซื้อ?')" />
                                    <asp:Button ID="btnSave" runat="server" Text="ยืนยันการสั่งซื้อ" class="btn btn-primary" Height="30px" Width="125px" OnClick="btnSave_Click" OnClientClick="return confirm('ยืนยันการสั่งซื้อ?')" />

                                </td>
                            </tr>
                            <tr>
                                <td>&nbsp;</td>
                                <td>&nbsp;&nbsp;</td>
                                <td>
                                    <asp:Label ID="lblError" runat="server" ForeColor="Red"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </div>
            <ajax:ConfirmButtonExtender ID="ConfirmButtonExtender1" runat="server"
                ConfirmText="ต้องการยกเลิกการสั่งซื้อสินค้าหรือไม่" Enabled="True" TargetControlID="btnCancel">
            </ajax:ConfirmButtonExtender>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

