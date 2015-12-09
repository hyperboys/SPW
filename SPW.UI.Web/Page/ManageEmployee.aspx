<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPageMainAdmin.Master"
    AutoEventWireup="true" CodeBehind="ManageEmployee.aspx.cs" Inherits="SPW.UI.Web.Page.ManageEmployee" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1 class="page-header">
        <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Page/SearchEmployee.aspx">พนักงาน</asp:HyperLink>
        -
        <asp:Label ID="lblName" runat="server" Text="เพิ่มพนักงานใหม่"></asp:Label></h1>
    <div class="alert alert-info" id="alert" runat="server" visible="false">
        <strong>บันทึกข้อมูลสำเร็จ Save Success</strong>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <style type="text/css">
        .right {
            text-align: right;
        }
    </style>
    <div class="panel panel-primary">
        <div class="panel-heading">
            ข้อมูลพนักงาน              
        </div>
        <div class="panel-body">
            <div class="row">
                <div class="col-lg-6">
                    <div class="form">
                        <div class="form-group">
                            <table style="width: 803px; height: 80px;">
                                <tr>
                                    <td>รหัสพนักงาน</td>
                                    <td style="text-align: center">:</td>
                                    <td>
                                        <table>
                                            <tr>
                                                <td>
                                                    <asp:TextBox ID="popTxtEmployeeCode" class="form-control" runat="server" Height="35px" Width="200px"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="กรุณากรอกรหัสพนักงาน"
                                                        ValidationGroup="group" ControlToValidate="popTxtEmployeeCode" Style="color: #FF0000; font-size: large;">*</asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td></td>
                                    <td>แผนก</td>
                                    <td style="text-align: center">:</td>
                                    <td>
                                        <asp:DropDownList ID="ddlDepartment" class="form-control" runat="server" Height="35px" Width="200px" SelectedValue='<%# Eval("Key") %>'>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>ชื่อ</td>
                                    <td style="text-align: center">:</td>
                                    <td>
                                        <table>
                                            <tr>
                                                <td>
                                                    <asp:TextBox ID="txtName" class="form-control" runat="server" Height="35px" Width="200px"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="กรุณากรอกชื่อ"
                                                        ValidationGroup="group" ControlToValidate="txtName" Style="color: #FF0000; font-size: large;">*</asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td></td>
                                    <td>นามสกุล</td>
                                    <td style="text-align: center">:</td>
                                    <td>
                                        <table>
                                            <tr>
                                                <td>
                                                    <asp:TextBox ID="txtLastName" class="form-control" runat="server" Height="35px" Width="200px"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                            <asp:Label ID="flag" runat="server" Text="Add" Visible="false"></asp:Label>

                        </div>

                    </div>
                </div>
            </div>
           <%-- <table style="margin-top: 10px; margin-bottom: 10px; height: 36px;">
                <tr>
                    <td>
                        <asp:Button ID="AddZone" runat="server" Text="เพิ่มการคุมโซนราคา" class="btn btn-primary" Enabled="False" />
                    </td>
                </tr>
            </table>--%>
            <%--<asp:GridView ID="gridZone" runat="server" ForeColor="#507CD1" AutoGenerateColumns="False"
                DataKeyNames="ZONE_DETAIL_ID" PageSize="20" Width="100%" EmptyDataText="ไม่พบข้อมูลราคาขาย"
                Style="text-align: center;" CssClass="grid" OnRowDeleting="gridZone_RowDeleting" OnRowDataBound="gridZone_RowDataBound" Visible="False">
                <AlternatingRowStyle BackColor="White" />
                <Columns>
                    <asp:TemplateField HeaderText="ลบ" ShowHeader="False">
                        <ItemTemplate>
                            <asp:ImageButton ID="ImageButton1" runat="server" CausesValidation="False" CommandName="Delete"
                                ImageUrl="~/Image/Icon/close.png" Text="ลบ" />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" Width="10%" />
                    </asp:TemplateField>
                    <asp:BoundField DataField="ZONE.ZONE_CODE" HeaderText="รหัสราคาขายสินค้า" ItemStyle-Width="45%" ItemStyle-HorizontalAlign="Center">
                        <ItemStyle Width="45%"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="ZONE.ZONE_NAME" HeaderText="ชื่อราคาขายสินค้า" ItemStyle-Width="45%" ItemStyle-HorizontalAlign="Center">
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
            </asp:GridView>--%>
            <div></div>
            <div style="float: right; margin-top: 20px;">
                <table>
                    <tr>
                        <td>
                            <asp:Button ID="btnSave" runat="server" Text="บันทึก" class="btn btn-primary" ValidationGroup="group" OnClick="btnSave_Click" />
                        </td>
                        <td>&nbsp;&nbsp;</td>
                        <td>
                            <asp:Button ID="btnCancel" class="btn btn-primary" Text="ยกเลิก" runat="server" OnClick="btnCancel_Click" />
                        </td>
                    </tr>
                </table>
            </div>
            <div>
                <asp:ValidationSummary ID="ValidationSummary1" runat="server" Style="color: #FF0000; font-size: large;"
                    ValidationGroup="group" ShowMessageBox="True" ShowSummary="False" />
            </div>
        </div>
    </div>
    <%--<asp:Panel ID="Panel2" runat="server" Visible="True" Height="272px">
        <div class="panel panel-primary">
            <div class="panel-heading">
                ข้อมูลราคาขายสินค้า      
            </div>
            <div class="panel-body">
                <asp:GridView ID="gridSelectZone" runat="server" ForeColor="#507CD1" AutoGenerateColumns="False"
                    DataKeyNames="ZONE_ID" PageSize="20" Width="100%" EmptyDataText="ไม่พบข้อมูลฟังก์ชัน"
                    OnPageIndexChanging="gridSelectZone_PageIndexChanging"
                    Style="text-align: center" CssClass="grid">
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:TemplateField HeaderText="เลือก" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:CheckBox ID="check" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="ZONE_CODE" HeaderText="รหัสราคาขายสินค้า" ItemStyle-Width="45%" ItemStyle-HorizontalAlign="Center">
                            <ItemStyle Width="45%"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="ZONE_NAME" HeaderText="ชื่อราคาขายสินค้า" ItemStyle-Width="45%" ItemStyle-HorizontalAlign="Center">
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
                <div style="float: right; margin-top: 20px;">
                    <table>
                        <tr>
                            <td>
                                <asp:Button ID="btnAddZone" runat="server" Text="บันทึก" class="btn btn-primary" OnClick="btnAddZone_Click" />
                            </td>
                            <td></td>
                            <td>
                                <asp:Button ID="btnCancelZone" class="btn btn-primary" Text="ยกเลิก" runat="server" />
                            </td>
                        </tr>
                    </table>
                </div>
                <asp:Label ID="flag2" runat="server" Text="Add" Visible="false"></asp:Label>
            </div>
        </div>
    </asp:Panel>--%>
    <%--<ajax:ModalPopupExtender ID="popup2" runat="server" BackgroundCssClass="modalBackground" DropShadow="false" PopupControlID="Panel2" TargetControlID="AddZone" Enabled="True">
    </ajax:ModalPopupExtender>--%>
    <ajax:ConfirmButtonExtender ID="btnSave_ConfirmButtonExtender" runat="server"
        ConfirmText="ต้องการจะบันทึกหรือไม่" Enabled="True" TargetControlID="btnSave">
    </ajax:ConfirmButtonExtender>

</asp:Content>
