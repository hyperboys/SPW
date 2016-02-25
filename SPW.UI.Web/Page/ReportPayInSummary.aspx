<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterPageMainAdmin.Master" AutoEventWireup="true"
    CodeBehind="ReportPayInSummary.aspx.cs" Inherits="SPW.UI.Web.Page.ReportPayInSummary" %>


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
                    format: 'dd/mm/yyyy'
                });
            }
        });
        $(function () {
            $('.datetimepicker').datepicker({
                format: 'dd/mm/yyyy'
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1 class="page-header">รายงานสรุปรายการนำเข้าเช็ค</h1>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <style type="text/css">
        .right {
            text-align: right;
        }

        .row {
            margin-top: 5px;
        }
    </style>

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="panel panel-primary">
                <div class="panel-heading">
                    ข้อมูลการค้นหา        
                </div>
                <div class="panel-body">
                    <div class="row">
                        <div class="col-lg-12">
                            <div class="form">
                                <div class="form-group">
                                    <table>
                                        <tr>
                                            <td class="col-md-2">วันที่เริ่มต้น</td>
                                            <td class="col-md-3">
                                                <div class='input-group date' id='datetimepicker1'>
                                                    <asp:TextBox ID="txtStartDate" class="form-control datetimepicker" runat="server" Height="35px" placeholder="วันที่เริ่มต้น"></asp:TextBox>
                                                    <span class="input-group-addon">
                                                        <span class="glyphicon glyphicon-calendar"></span>
                                                    </span>
                                                </div>
                                            </td>
                                            <td class="col-md-2">วันที่สิ้นสุด</td>
                                            <td class="col-md-3">
                                                <div class='input-group date' id='Div1'>
                                                    <asp:TextBox ID="txtEndDate" class="form-control datetimepicker" runat="server" Height="35px" placeholder="วันที่สิ้นสุด"></asp:TextBox>
                                                    <span class="input-group-addon">
                                                        <span class="glyphicon glyphicon-calendar"></span>
                                                    </span>
                                                </div>
                                            </td>
                                            <td></td>
                                            <td class="col-md-3">
                                                <asp:Button ID="btnSearch" class="btn btn-primary" runat="server" Text="ค้นหา" Height="30px" Width="70px" OnClick="btnSearch_Click" />
                                            </td>
                                            <td style="width: 5px"></td>
                                            <td class="col-md-3">
                                                <asp:Button ID="btnPrint" class="btn btn-primary" runat="server" Text="พิมพ์" Height="30px" Width="70px" OnClick="btnPrint_Click" />

                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                        </div>
                    </div>
                    <asp:GridView ID="gridProduct" runat="server" ForeColor="#507CD1" AutoGenerateColumns="False"
                        DataKeyNames="" PageSize="20" Width="100%" EmptyDataText="ไม่พบการนำเข้าเช็ค" PageIndex="10"
                        Style="text-align: center" CssClass="grid">
                        <AlternatingRowStyle BackColor="White" />
                        <Columns>
                            <asp:BoundField DataField="PAYIN_DATE" HeaderText="วันที่" ItemStyle-Width="20%" ItemStyle-HorizontalAlign="Center">
                                <ItemStyle Width="20%"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="CHQ_NO" HeaderText="เลขที่เช็ค" ItemStyle-Width="20%" ItemStyle-HorizontalAlign="Center">
                                <ItemStyle Width="20%"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="ACCOUNT_ID" HeaderText="เลขที่บัญชีที่นำเข้า" ItemStyle-Width="20%" ItemStyle-HorizontalAlign="Center">
                                <ItemStyle Width="20%"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="BANK_NAME" HeaderText="ธนาคาร" ItemStyle-Width="20%" ItemStyle-HorizontalAlign="Center">
                                <ItemStyle Width="20%"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="CHQ_AMOUNT" HeaderText="จำนวนเงิน" ItemStyle-Width="20%" ItemStyle-HorizontalAlign="Center">
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
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
