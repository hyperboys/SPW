<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage/MasterPageMainAdmin.Master" CodeBehind="SearchInvoice.aspx.cs" Inherits="SPW.UI.Web.Page.SearchInvoice" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
    <script type="text/javascript" src="../JQuery/bootstrap-datepicker.js"></script>
    <link href="../CSS/datepicker.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        //แก้ไขตอนที่ใส่ update panel แล้ว datetimepicker ไม่ทำงาน
        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);

            function EndRequestHandler(sender, args) {
                $('.datetimepicker').datepicker({
                    format: 'dd/mm/yyyy',
                    showOn: "button"
                });
            }
        });
        $(function () {
            $('.datetimepicker').datepicker({
                format: 'dd/mm/yyyy',
                showOn: "button"
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1 class="page-header">ค้นหารายการส่งซ่อม</h1>
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
            ค้นหารายการส่งซ่อม        
        </div>
        <div class="panel-body">
            <div class="row">
                <div class="col-lg-6">
                    <div class="form">
                        <div class="form-group">
                            <table style="width: 630px">
                                <tr>
                                    <td>ทรัพย์สิน</td>
                                    <td class="auto-style6" style="text-align: center"></td>
                                    <td>
                                        <div class='input-group date' id='datetimepicker1'>
                                            <asp:DropDownList ID="ddlAssetType" class="form-control" runat="server" Height="35px" Width="200px" AutoPostBack="True">
                                                <asp:ListItem Value="0">กรุณาเลือก</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </td>
                                    <td style="text-align: center">&nbsp;</td>
                                    <td>ทะเบียน</td>
                                    <td class="auto-style6" style="text-align: center"></td>
                                    <td>
                                        <div class='input-group date' id='Div1'>
                                            <asp:DropDownList ID="ddlVehicle" class="form-control" runat="server" Height="35px" Width="200px" AutoPostBack="True">
                                                <asp:ListItem Value="0">กรุณาเลือก</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td></td>
                                    <td class="auto-style6" style="text-align: center"></td>
                                    <td>
                                    </td>
                                    <td style="text-align: center">&nbsp;</td>
                                    <td></td>
                                    <td class="auto-style6" style="text-align: center"></td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td>เริ่มต้น</td>
                                    <td class="auto-style6" style="text-align: center"></td>
                                    <td>
                                        <div class='input-group date' id='Div2'>
                                            <asp:TextBox ID="txtStartDate" class="form-control datetimepicker" runat="server" Height="35px" placeholder="วันที่เริ่มต้น"></asp:TextBox>
                                            <span class="input-group-addon">
                                                <span class="glyphicon glyphicon-calendar"></span>
                                            </span>
                                        </div>
                                    </td>
                                    <td style="text-align: center">&nbsp;</td>
                                    <td>สิ้นสุด</td>
                                    <td class="auto-style6" style="text-align: center"></td>
                                    <td>
                                        <div class='input-group date' id='Div3'>
                                            <asp:TextBox ID="txtEndDate" class="form-control datetimepicker" runat="server" Height="35px" placeholder="วันที่สิ้นสุด"></asp:TextBox>
                                            <span class="input-group-addon">
                                                <span class="glyphicon glyphicon-calendar"></span>
                                            </span>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td></td>
                                    <td class="auto-style6" style="text-align: center"></td>
                                    <td></td>
                                    <td style="text-align: center">&nbsp;</td>
                                    <td></td>
                                    <td class="auto-style6" style="text-align: center"></td>
                                    <td></td>
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
            </div>
            <div class="panel panel-primary">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                <asp:GridView ID="gdvInv" runat="server" ForeColor="#507CD1" AutoGenerateColumns="False"
                    DataKeyNames="AP_VEHICLE_TRANS_ID" PageSize="20" Width="100%" EmptyDataText="ไม่พบข้อมูล"
                    OnRowEditing="gdvInv_EditCommand" OnPageIndexChanging="gdvInv_PageIndexChanging"
                    Style="text-align: center" CssClass="grid">
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:CommandField ButtonType="Image" EditImageUrl="~/Image/Icon/find.png" HeaderText="ดูข้อมล"
                            ShowCancelButton="False" ShowEditButton="True" ItemStyle-Width="10%" HeaderStyle-HorizontalAlign="Center">
                            <ItemStyle Width="10%"></ItemStyle>
                        </asp:CommandField>
                        <asp:BoundField DataField="AP_VEHICLE_TRANS_ID" HeaderText="เลขที่ส่งซ่อม" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center">
                            <ItemStyle Width="10%"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="AP_VEHICLE_TRANS_DATE" HeaderText="วันที่ส่งซ่อม" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:dd/M/yyyy}">
                            <ItemStyle Width="10%"></ItemStyle>
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="ประเภททรัพย์สิน" ItemStyle-Width="10%">
                            <ItemTemplate>
                                <%# ((SPW.Model.AP_VEHICLE_TRANS) Container.DataItem).ASSET_TYPE.ASSET_TYPE_NAME %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="ทะเบียนรถ" ItemStyle-Width="10%">
                            <ItemTemplate>
                                <%# ((SPW.Model.AP_VEHICLE_TRANS) Container.DataItem).VEHICLE.VEHICLE_REGNO %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="VENDOR_CODE" HeaderText="รหัสซัพพลายเออร์" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center">
                            <ItemStyle Width="10%"></ItemStyle>
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="ชื่อซัพพลายเออร์" ItemStyle-Width="10%">
                            <ItemTemplate>
                                <%# ((SPW.Model.AP_VEHICLE_TRANS) Container.DataItem).VENDOR.VENDOR_NAME %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="MA_AMOUNT" HeaderText="ราคา" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:#,0}">
                            <ItemStyle Width="10%"></ItemStyle>
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
                    <asp:PlaceHolder ID="PlaceHolder1" runat="server"></asp:PlaceHolder>
                        </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
</asp:Content>

