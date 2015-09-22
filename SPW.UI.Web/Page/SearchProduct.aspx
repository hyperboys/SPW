<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPageMainAdmin.Master" AutoEventWireup="true" CodeBehind="SearchProduct.aspx.cs" Inherits="SPW.UI.Web.Page.SearchProduct" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1 class="page-header">สินค้า</h1>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <style type="text/css">
        .auto-style1 {
            width: 18px;
        }

        .right {
            text-align: right;
        }

        .auto-style4 {
            width: 79px;
        }

        .auto-style7 {
            width: 42px;
        }

        .auto-style8 {
            width: 162px;
        }
        .auto-style9 {
            width: 17px;
        }
        .auto-style10 {
            width: 43px;
        }
    </style>
    <div class="panel panel-primary">
        <div class="panel-heading">
            ข้อมูลสินค้า         
        </div>
        <div class="panel-body">
            <div class="row">
                <div class="col-lg-6">
                    <div class="form">
                        <div class="form-group">
                            <table style="width: 610px">
                                <tr>
                                    <td>รหัสสินค้า</td>
                                    <td class="auto-style1" style="text-align: center">:</td>
                                    <td>
                                        <asp:TextBox ID="txtProductCode" class="form-control" runat="server"></asp:TextBox>
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
                    </div>
                </div>
                <!-- /.col-lg-6 (nested) -->
            </div>
            <!-- /.row (nested) -->
            <div class="panel panel-primary">
                <asp:GridView ID="gridProduct" runat="server" ForeColor="#507CD1" AutoGenerateColumns="False"
                    DataKeyNames="PRODUCT_ID" PageSize="20" Width="100%" EmptyDataText="ไม่พบข้อมูลสินค้า" PageIndex="10"
                    OnRowEditing="gridProduct_EditCommand" OnPageIndexChanging="gridProduct_PageIndexChanging"
                    Style="text-align: center" CssClass="grid">
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:CommandField ButtonType="Image" EditImageUrl="~/Image/Icon/find.png" HeaderText="ดูข้อมล"
                            ShowCancelButton="False" ShowEditButton="True" ItemStyle-Width="10%" HeaderStyle-HorizontalAlign="Center">
                            <ItemStyle Width="10%"></ItemStyle>
                        </asp:CommandField>
                        <asp:BoundField DataField="PRODUCT_CODE" HeaderText="รหัสสินค้า" ItemStyle-Width="45%" ItemStyle-HorizontalAlign="Center">
                            <ItemStyle Width="45%"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="PRODUCT_NAME" HeaderText="ชื่อสินค้า" ItemStyle-Width="45%" ItemStyle-HorizontalAlign="Center">
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
                ข้อมูลสินค้า           
            </div>
            <div class="panel-body">
                <div class="row">
                    <div class="col-lg-6">
                        <div class="form">
                            <div class="form-group">
                                <table style="width: 683px; height: 150px;">
                                    <tr>
                                        <td>รหัสสินค้า</td>
                                        <td style="text-align: center" class="auto-style1">:</td>
                                        <td>
                                            <asp:TextBox ID="popTxtProductCode" class="form-control" runat="server" Height="30px" Width="200px"></asp:TextBox>
                                        </td>
                                        <td>ชื่อสินค้า</td>
                                        <td style="text-align: center" class="auto-style1">:</td>
                                        <td>
                                            <asp:TextBox ID="poptxtProductName" class="form-control" runat="server" Height="30px" Width="224px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>ประเภทสินค้า</td>
                                        <td style="text-align: center" class="auto-style1">:</td>
                                        <td>
                                            <asp:DropDownList ID="ddlCategory" class="form-control" runat="server" Height="35px" Width="200px">
                                                <asp:ListItem Value="0">กรุณาเลือก</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td>ชนิด</td>
                                        <td style="text-align: center" class="auto-style1">:</td>
                                        <td>
                                            <asp:DropDownList ID="ddlkind" class="form-control" runat="server" Height="35px" Width="200px">
                                                <asp:ListItem Value="0" Selected="True">กรุณาเลือก</asp:ListItem>
                                                <asp:ListItem Value="1">สำเร็จรูป</asp:ListItem>
                                                <asp:ListItem Value="2">วันถุดิบ</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>จำนวน</td>
                                        <td style="text-align: center" class="auto-style1">:</td>
                                        <td>
                                            <asp:TextBox ID="txtPacking" class="form-control" runat="server" Height="30px" Width="200px"></asp:TextBox>
                                        </td>
                                        <td></td>
                                        <td style="text-align: center" class="auto-style1"></td>
                                        <td>
                                            <table>
                                                <tr>
                                                    <td>
                                                        <asp:DropDownList ID="ddlPakUDesc" class="form-control" runat="server" Height="35px" Width="112px">
                                                            <asp:ListItem Value="0">กรุณาเลือก</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td>/</td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlPakPDesc" class="form-control" runat="server" Height="35px" Width="112px">
                                                            <asp:ListItem Value="0">กรุณาเลือก</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>น้ำหนัก</td>
                                        <td style="text-align: center" class="auto-style1">:</td>
                                        <td>
                                            <asp:TextBox ID="txtWeight" class="form-control" runat="server" Height="30px" Width="200px"></asp:TextBox>
                                        </td>
                                        <td>หน่วย</td>
                                        <td style="text-align: center" class="auto-style1">:</td>
                                        <td>
                                            <asp:TextBox ID="txtUnit" class="form-control" runat="server" Height="30px" Width="224px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="auto-style4">รูปภาพ</td>
                                        <td style="text-align: center" class="auto-style1">:</td>
                                        <td>
                                            <asp:FileUpload ID="FileUpload1" runat="server" Height="30px" Width="222px" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td></td>
                                        <td></td>
                                        <td></td>
                                    </tr>
                                </table>
                            </div>
                        </div>

                    </div>
                </div>
                <table style="margin-top: 10px; margin-bottom: 10px;">
                    <tr>
                        <td></td>
                    </tr>
                </table>
                <asp:GridView ID="gridProductDetail" runat="server" ForeColor="#507CD1" AutoGenerateColumns="False"
                    DataKeyNames="ZONE_ID" PageSize="20" Width="640px" EmptyDataText="ไม่พบข้อมูลราคาสินค้า"
                    Style="text-align: center;" CssClass="grid" OnRowDataBound="gridProductDetail_RowDataBound">
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:BoundField DataField="ZONE.ZONE_CODE" HeaderText="รหัสราคาขาย" ItemStyle-Width="30%" ItemStyle-HorizontalAlign="Center">
                            <ItemStyle Width="30%"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="ZONE.ZONE_NAME" HeaderText="ชื่อราคาขาย" ItemStyle-Width="30%" ItemStyle-HorizontalAlign="Center">
                            <ItemStyle Width="30%"></ItemStyle>
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="ราคา" ItemStyle-Width="40%" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:TextBox ID="txtPrice" runat="server" class="form-control"></asp:TextBox>
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
                <table style="width: 204px; margin-top: 10px; margin-bottom: 10px; height: 36px;">
                    <tr>
                        <td></td>
                        <td>
                            <asp:Button ID="AddPromotion" runat="server" Text="Promotion" class="btn btn-primary" OnClick="AddPromotion_Click" />
                        </td>
                    </tr>
                </table>

                <asp:GridView ID="gridPromotion" runat="server" ForeColor="#507CD1" AutoGenerateColumns="False"
                    DataKeyNames="PROMOTION_ID" PageSize="20" Width="640px" EmptyDataText="ไม่พบข้อมูลโปรโมชัน" PageIndex="10"
                    OnRowEditing="gridPromotion_EditCommand"
                    Style="text-align: center" CssClass="grid">
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:CommandField ButtonType="Image" EditImageUrl="~/Image/Icon/find.png" HeaderText="ดูข้อมล"
                            ShowCancelButton="False" ShowEditButton="True" ItemStyle-Width="10%" HeaderStyle-HorizontalAlign="Center">
                            <ItemStyle Width="10%"></ItemStyle>
                        </asp:CommandField>
                        <asp:BoundField DataField="ZONE.ZONE_NAME" HeaderText="ราคาขาย" ItemStyle-Width="30%" ItemStyle-HorizontalAlign="Center">
                            <ItemStyle Width="30%"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="PRODUCT_CONDITION_QTY" HeaderText="ซื้อ" ItemStyle-Width="30%" ItemStyle-HorizontalAlign="Center">
                            <ItemStyle Width="30%"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="PRODUCT_FREE_QTY" HeaderText="ฟรี" ItemStyle-Width="30%" ItemStyle-HorizontalAlign="Center">
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

                <table style="width: 204px; margin-top: 27px; height: 36px; margin-left: 434px;">
                    <tr>
                        <td></td>
                        <td>
                            <asp:Button ID="btnSave" runat="server" Text="บันทึก" class="btn btn-primary" OnClick="btnSave_Click" />
                        </td>
                        <td>
                            <asp:Button ID="btnCancel" class="btn btn-primary" Text="ยกเลิก" runat="server" OnClick="btnCancel_Click" />
                        </td>
                    </tr>
                </table>
                <asp:Label ID="flag" runat="server" Text="Add" Visible="false"></asp:Label>
            </div>
        </div>
    </asp:Panel>

    <asp:Panel ID="Panel2" runat="server" Visible="True" Height="272px">
        <div class="panel panel-primary">
            <div class="panel-heading">
                ข้อมูลโปรโมชัน       
            </div>
            <div class="panel-body">
                <div class="row">
                    <div class="col-lg-6">
                        <div class="form">
                            <div class="form-group">
                                <table style="width: 686px; height: 111px;">
                                    <tr>
                                        <td class="auto-style8">ซื้อ</td>
                                        <td style="text-align: center" class="auto-style1">:</td>
                                        <td>
                                            <asp:TextBox ID="txtQty" class="form-control" runat="server" Height="30px" Width="200px"></asp:TextBox>
                                        </td>
                                        <td class="auto-style10"></td>
                                        <td class="auto-style7">ฟรี</td>
                                        <td style="text-align: center" class="auto-style1">:</td>
                                        <td>
                                            <asp:TextBox ID="txtFreeQty" class="form-control" runat="server" Height="30px" Width="224px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="auto-style8">ประเภทราคาขาย</td>
                                        <td style="text-align: center" class="auto-style1">:</td>
                                        <td>
                                            <asp:DropDownList ID="ddlZonePromotion" class="form-control" runat="server" Height="35px" Width="200px">
                                                <asp:ListItem Value="0">กรุณาเลือก</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td class="auto-style10"></td>
                                        <td class="auto-style7"></td>
                                        <td style="text-align: center" class="auto-style1"></td>
                                        <td></td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                    </div>
                </div>

                <table style="width: 189px; margin-top: 20px; height: 36px; margin-left: 450px;">
                    <tr>
                        <td></td>
                        <td>
                            <asp:Button ID="btnAddPromotion" runat="server" Text="บันทึก" class="btn btn-primary" OnClick="btnAddPromotion_Click" />
                        </td>
                        <td>
                            <asp:Button ID="btnCancelPromotion" class="btn btn-primary" Text="ยกเลิก" runat="server" OnClick="btnCancelPromotion_Click" />
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
