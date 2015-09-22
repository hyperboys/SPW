<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPageMainAdmin.Master"
     AutoEventWireup="true" CodeBehind="SearchVehicle.aspx.cs" Inherits="SPW.UI.Web.Page.SearchVehicle" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1 class="page-header">รถบรรทุกสินค้า</h1>
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

        .grid td, .grid th
        {
            text-align: center;
        }

        .auto-style4
        {
            width: 108px;
        }

        .auto-style5
        {
            width: 287px;
        }

        .auto-style6
        {
            width: 84px;
        }

        .auto-style7
        {
            width: 80px;
        }

        .auto-style8
        {
            width: 83px;
        }

        .auto-style9
        {
            width: 215px;
        }

        .auto-style10
        {
            width: 184px;
        }

        .auto-style11
        {
            width: 183px;
        }

        .auto-style12
        {
            width: 229px;
        }

        .auto-style13
        {
            width: 63px;
        }
    </style>
    <div class="panel panel-primary">
        <div class="panel-heading">
            ข้อมูลรถ
        </div>
        <div class="panel-body">
            <div class="row">
                <div class="col-lg-6">
                    <div class="form">
                        <div class="form-group">
                            <table style="width: 840px">
                                <tr>
                                    <td class="auto-style9">รหัสรถ</td>
                                    <td class="auto-style1" style="text-align: center">:</td>
                                    <td>
                                        <asp:TextBox ID="txtColorTypeSubName" class="form-control" runat="server" Height="30px" Width="125px"></asp:TextBox>
                                    </td>
                                    <td class="auto-style1" style="text-align: center">&nbsp;</td>
                                    <td class="auto-style10">เลขทะเบียนรถ</td>
                                    <td class="auto-style1" style="text-align: center">:</td>
                                    <td>
                                        <asp:TextBox ID="txtColorTypeName" class="form-control" runat="server" Height="30px" Width="125px"></asp:TextBox>
                                    </td>
                                    <td class="auto-style1" style="text-align: center">&nbsp;</td>
                                    <td class="auto-style7">
                                        <asp:Button ID="btnSearch" class="btn btn-primary" runat="server" Text="ค้นหา" OnClick="btnSearch_Click" Height="30px" Width="70px" />
                                    </td>
                                    <td class="auto-style1" style="text-align: center">&nbsp;</td>
                                    <td class="auto-style8">
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
                <asp:GridView ID="gridVehicle" runat="server" ForeColor="#507CD1" AutoGenerateColumns="False"
                    DataKeyNames="VEHICLE_ID" PageSize="20" Width="100%" EmptyDataText="ไม่พบข้อมูลหมวดหมู่สินค้า"
                    OnRowEditing="gridVehicle_EditCommand" OnPageIndexChanging="gridVehicle_PageIndexChanging"
                    Style="text-align: center" CssClass="grid">
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:CommandField ButtonType="Image" EditImageUrl="~/Image/Icon/find.png" HeaderText="ดูข้อมล"
                            ShowCancelButton="False" ShowEditButton="True" ItemStyle-Width="10%" HeaderStyle-HorizontalAlign="Center">
                            <ItemStyle Width="10%"></ItemStyle>
                        </asp:CommandField>
                        <asp:BoundField DataField="VEHICLE_CODE" HeaderText="รหัสรถ" ItemStyle-Width="45%" ItemStyle-HorizontalAlign="Center">
                            <ItemStyle Width="45%"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="VEHICLE_REGNO" HeaderText="เลขทะเบียนรถ" ItemStyle-Width="45%" ItemStyle-HorizontalAlign="Center">
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
                ข้อมูลรถ
            </div>
            <div class="panel-body">
                <div class="row">
                    <div class="col-lg-6">
                        <div class="form">
                            <div class="form-group">
                                <table style="width: 835px; height: 80px;">
                                    <tr>
                                        <td class="auto-style5">รหัสรถ</td>
                                        <td class="auto-style1" style="text-align: center">:</td>
                                        <td class="auto-style2">
                                            <asp:TextBox ID="popTxtCode" class="form-control" runat="server" Height="30px" Width="145px"></asp:TextBox>
                                        </td>
                                        <td class="auto-style12">เลขทะเบียนรถ</td>
                                        <td class="auto-style1" style="text-align: center">:</td>
                                        <td class="auto-style2">
                                            <asp:TextBox ID="popTxtReg" class="form-control" runat="server" Height="30px" Width="145px"></asp:TextBox>
                                        </td>
                                        <td class="auto-style4"></td>
                                        <td>
                                            <asp:Button ID="btnSave" runat="server" Text="บันทึก" class="btn btn-primary" OnClick="btnSave_Click" />
                                        </td>
                                        <td class="auto-style13"></td>
                                        <td>
                                            <asp:Button ID="btnCancel" class="btn btn-primary" Text="ยกเลิก" runat="server" OnClick="btnCancel_Click" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="auto-style12">ชนิดรถ</td>
                                        <td class="auto-style1" style="text-align: center">:</td>
                                        <td class="auto-style2">
                                            <asp:TextBox ID="popTxtTypNO" class="form-control" runat="server" Height="30px" Width="145px" MaxLength="2"></asp:TextBox>
                                        </td>
                                        <td class="auto-style12"></td>
                                        <td class="auto-style1" style="text-align: center"></td>
                                        <td class="auto-style2"></td>
                                        <td class="auto-style4"></td>
                                        <td></td>
                                        <td class="auto-style13"></td>
                                        <td></td>
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
