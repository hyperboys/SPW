<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPageMainAdmin.Master" AutoEventWireup="true"
     CodeBehind="SearchStore.aspx.cs" Inherits="SPW.UI.Web.Page.SearchStore" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1 class="page-header">ร้านค้า</h1>
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

        .auto-style6
        {
            width: 10px;
        }

        .auto-style9
        {
            width: 97px;
        }
        .auto-style10
        {
            width: 92px;
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
                            <table style="width: 630px">
                                <tr>
                                    <td>รหัสร้านค้า</td>
                                    <td class="auto-style6" style="text-align: center">:</td>
                                    <td>
                                        <asp:TextBox ID="txtStoreCode" class="form-control" runat="server" Width="115px"></asp:TextBox>
                                    </td>
                                    <td style="text-align: center">&nbsp;</td>
                                    <td>ชื่อร้านค้า</td>
                                    <td class="auto-style6" style="text-align: center">:</td>
                                    <td>
                                        <asp:TextBox ID="txtStoreName" class="form-control" runat="server" Width="115px"></asp:TextBox>
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
                <!-- /.col-lg-6 (nested) -->
            </div>
            <!-- /.row (nested) -->
            <div class="panel panel-primary">
                <asp:GridView ID="gridStore" runat="server" ForeColor="#507CD1" AutoGenerateColumns="False"
                    DataKeyNames="STORE_ID" PageSize="20" Width="100%" EmptyDataText="ไม่พบข้อมูลร้านค้า"
                    OnRowEditing="gridProduct_EditCommand" OnPageIndexChanging="gridProduct_PageIndexChanging"
                    Style="text-align: center" CssClass="grid">
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
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>

    <asp:Panel ID="Panel1" runat="server" Visible="True">
        <div class="panel panel-primary">
            <div class="panel-heading">
                ข้อมูลร้านค้า            
            </div>
            <div class="panel-body">
                <div class="row">
                    <div class="col-lg-6">
                        <div class="form">
                            <div class="form-group">
                                <table style="width: 651px; height: 171px;">
                                    <tr>
                                        <td class="auto-style10">รหัสร้านค้า</td>
                                        <td style="text-align: center" class="auto-style5">:</td>
                                        <td>
                                            <asp:TextBox ID="popTxtStoreCode" class="form-control" runat="server" Height="30px" Width="200px"></asp:TextBox>
                                        </td>
                                        <td>ชื่อร้านค้า</td>
                                        <td style="text-align: center" class="auto-style5">:</td>
                                        <td>
                                            <asp:TextBox ID="poptxtStoreName" class="form-control" runat="server" Height="30px" Width="200px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="auto-style10">ภาค</td>
                                        <td style="text-align: center" class="auto-style5">:</td>
                                        <td>
                                            <asp:DropDownList ID="ddlSector" class="form-control" runat="server" Height="35px" Width="200px" AutoPostBack="True" OnSelectedIndexChanged="ddlSector_SelectedIndexChanged">
                                                <asp:ListItem Value="0">กรุณาเลือก</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td>จังหวัด</td>
                                        <td style="text-align: center" class="auto-style5">:</td>
                                        <td>
                                            <asp:DropDownList ID="ddlProvince" class="form-control" runat="server" Height="35px" Width="200px" Enabled="False" AutoPostBack="True" OnSelectedIndexChanged="ddlProvince_SelectedIndexChanged">
                                                <asp:ListItem>กรุณาเลือก</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="auto-style10">อำเภอ</td>
                                        <td style="text-align: center" class="auto-style5">:</td>
                                        <td>
                                            <asp:TextBox ID="txtAmpur" class="form-control" runat="server" Height="30px" Width="200px"></asp:TextBox>
                                        </td>
                                        <td>ตำบล</td>
                                        <td style="text-align: center" class="auto-style5">:</td>
                                        <td>
                                            <asp:TextBox ID="txtTumbon" class="form-control" runat="server" Height="30px" Width="200px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="auto-style10">ถนน</td>
                                        <td style="text-align: center" class="auto-style5">:</td>
                                        <td>
                                            <asp:TextBox ID="txtRoad" class="form-control" runat="server" Height="30px" Width="200px" Visible="true"></asp:TextBox>
                                            <asp:DropDownList ID="ddlRoad" class="form-control" runat="server" Height="35px" Width="200px" Visible="false">
                                                <asp:ListItem Value="0">กรุณาเลือก</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td>รหัสไปรษณีย์</td>
                                        <td style="text-align: center" class="auto-style5">:</td>
                                        <td>
                                            <asp:TextBox ID="txtPostCode" class="form-control" runat="server" Height="30px" Width="200px"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                                <table style="width: 653px">
                                    <tr>
                                        <td class="auto-style9">ที่อยู่
                                        </td>
                                        <td style="text-align: center" class="auto-style5">:
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtAddress" class="form-control" runat="server" Height="102px" TextMode="MultiLine" Width="527px"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                                <table style="width: 651px; height: 136px; margin-top: 5px;">
                                    <tr>
                                        <td>เบอร์โทรศัพท์ 1</td>
                                        <td style="text-align: center" class="auto-style5">:</td>
                                        <td>
                                            <asp:TextBox ID="txtTel1" class="form-control" runat="server" Height="30px" Width="200px"></asp:TextBox>
                                        </td>
                                        <td>เบอร์โทรศัพท์ 2</td>
                                        <td style="text-align: center" class="auto-style5">:</td>
                                        <td>
                                            <asp:TextBox ID="txtTel2" class="form-control" runat="server" Height="30px" Width="200px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>เบอร์มือถือ</td>
                                        <td style="text-align: center" class="auto-style5">:</td>
                                        <td>
                                            <asp:TextBox ID="txtMobli" class="form-control" runat="server" Height="30px" Width="200px"></asp:TextBox>
                                        </td>
                                        <td>แฟกต์</td>
                                        <td style="text-align: center" class="auto-style5">:</td>
                                        <td>
                                            <asp:TextBox ID="txtFax" class="form-control" runat="server" Height="30px" Width="200px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>ราคาขายสินค้า</td>
                                        <td style="text-align: center" class="auto-style5">:</td>
                                        <td>
                                           <asp:DropDownList ID="ddlZone" class="form-control" runat="server" Height="35px" Width="200px" OnSelectedIndexChanged="ddlSector_SelectedIndexChanged">
                                                <asp:ListItem Value="0">กรุณาเลือก</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td></td>
                                        <td style="text-align: center" class="auto-style5"></td>
                                        <td></td>
                                    </tr>
                                </table>
                                <table style="width: 204px; margin-top: 27px; height: 36px; margin-left: 456px;">
                                    <tr>
                                        <td></td>
                                        <td>
                                            <asp:Button ID="btnSave" runat="server" Text="บึนทึก" class="btn btn-primary" OnClick="btnSave_Click" />
                                        </td>
                                        <td>
                                            <asp:Button ID="btnCancel" class="btn btn-primary" Text="ยกเลิก" runat="server" OnClick="btnCancel_Click" />
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
