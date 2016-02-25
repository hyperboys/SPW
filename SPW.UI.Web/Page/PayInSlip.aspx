﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPageMainAdmin.Master" AutoEventWireup="true"
    CodeBehind="PayInSlip.aspx.cs" Inherits="SPW.UI.Web.Page.PayInSlip" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1 class="page-header">
        <asp:Label ID="lblName" runat="server" Text="PayIn Slip"></asp:Label></h1>
    <div class="alert alert-info" id="alert" runat="server" visible="false">
        <strong>บันทึกข้อมูลสำเร็จ Save Success</strong>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <style type="text/css">
        .auto-style1 {
            width: 18px;
        }

        .right {
            text-align: right;
        }

        .auto-style2 {
            width: 160px;
        }

        .grid td, .grid th {
            text-align: center;
        }

        .auto-style5 {
            width: 147px;
        }

        .auto-style12 {
            width: 195px;
        }

        .auto-style13 {
            width: 60px;
        }

        .auto-style14 {
            width: 43px;
            font-weight: 700;
        }

        .auto-style16 {
            width: 510px;
        }

        .auto-style17 {
            width: 95px;
        }
    </style>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="panel panel-primary">
                <div class="panel-heading">
                    จัดการใบ Payin
                </div>
                <div class="panel-body">
                    <div class="row">
                        <div class="col-lg-6">
                            <div class="form">
                                <div class="form-group">
                                    <table style="width: 800px; height: 222px;">
                                        <tr>
                                            <td class="auto-style5"></td>
                                            <td class="auto-style1" style="text-align: center"></td>
                                            <td class="auto-style2">
                                                <asp:RadioButton ID="rbBankThai" runat="server" Text="ธ.ทหารไทย" TextAlign="Right" GroupName="bankGroup" Width="170px" AutoPostBack="True" OnCheckedChanged="rbBankThai_CheckedChanged" Checked="True" />
                                            </td>
                                            <td class="auto-style13"></td>
                                            <td class="auto-style12">
                                                <asp:RadioButton ID="rbBankKrungThai" runat="server" Text="ธ.กรุงศรีอยุธยา" TextAlign="Right" GroupName="bankGroup" Width="150px" AutoPostBack="True" OnCheckedChanged="rbBankKrungThai_CheckedChanged" />
                                            </td>
                                            <td class="auto-style1" style="text-align: center">&nbsp;</td>
                                            <td class="auto-style2">
                                                <asp:Label ID="lblDateTime" runat="server" Text="วันที่ " Style="font-weight: 700"></asp:Label>
                                            </td>
                                            <td></td>
                                        </tr>
                                        <tr>
                                            <td class="auto-style5">เลือกบัญชี
                                            </td>
                                            <td class="auto-style1" style="text-align: center">:</td>
                                            <td class="auto-style2">
                                                <asp:DropDownList ID="ddlAccountMast" class="form-control" runat="server" Height="35px" Width="200px" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddlAccountMast_SelectedIndexChanged">
                                                    <asp:ListItem Value="0">กรุณาเลือก</asp:ListItem>
                                                </asp:DropDownList></td>
                                            <td class="auto-style13"></td>
                                            <td class="auto-style12">ชื่อบัญชี</td>
                                            <td class="auto-style1" style="text-align: center">:</td>
                                            <td class="auto-style2">
                                                <asp:TextBox ID="txtAccountName" class="form-control" runat="server" Height="35px" Width="200px" Enabled="false" BackColor="White"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtAccountName"
                                                    ErrorMessage="กรุณาเลือกบัญชี" Style="color: #FF0000; font-size: large;" ValidationGroup="group">*</asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="auto-style5">เลขที่เช็ค
                                            </td>
                                            <td class="auto-style1" style="text-align: center">:</td>
                                            <td class="auto-style2">
                                                <asp:TextBox ID="txtCheck" class="form-control" runat="server" Height="35px" Width="200px"></asp:TextBox>
                                            </td>
                                            <td class="auto-style13">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtCheck"
                                                    ErrorMessage="กรุณากรอกเลขที่เช็ค" Style="color: #FF0000; font-size: large;" ValidationGroup="group">*</asp:RequiredFieldValidator>
                                            </td>
                                            <td class="auto-style12"></td>
                                            <td class="auto-style1" style="text-align: center"></td>
                                            <td class="auto-style2"></td>
                                            <td></td>
                                        </tr>
                                        <tr>
                                            <td class="auto-style5">ธนาคาร(เช็ค)
                                            </td>
                                            <td class="auto-style1" style="text-align: center">:</td>
                                            <td class="auto-style2">
                                                <asp:TextBox ID="txtBankCheck" class="form-control" runat="server" Height="35px" Width="200px"></asp:TextBox>
                                            </td>
                                            <td class="auto-style13">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtBankCheck"
                                                    ErrorMessage="กรุณากรอกธนาคาาร(เช็ค)" Style="color: #FF0000; font-size: large;" ValidationGroup="group">*</asp:RequiredFieldValidator>
                                            </td>
                                            <td class="auto-style12">สาขา(เช็ค)</td>
                                            <td class="auto-style1" style="text-align: center">:</td>
                                            <td class="auto-style2">
                                                <asp:TextBox ID="txtBranceCheck" class="form-control" runat="server" Height="35px" Width="200px"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtBranceCheck"
                                                    ErrorMessage="กรุณากรอกสาขา(เช็ค)" Style="color: #FF0000; font-size: large;" ValidationGroup="group">*</asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="auto-style5">จำนวนเงิน
                                            </td>
                                            <td class="auto-style1" style="text-align: center">:</td>
                                            <td class="auto-style2">
                                                <asp:TextBox ID="txtAmount" class="form-control" ondrop="return false;" onkeypress="return IsNumeric(event);" onpaste="return false;" runat="server" Height="35px" Width="200px" MaxLength="8"></asp:TextBox>
                                                <span id="error" style="color: Red; display: none">* กรุณากรอก (0 - 9)</span>
                                                <script type="text/javascript">
                                                    var specialKeys = new Array();
                                                    specialKeys.push(8); //Backspace
                                                    function IsNumeric(e) {
                                                        var keyCode = e.which ? e.which : e.keyCode
                                                        var ret = ((keyCode >= 48 && keyCode <= 57) || specialKeys.indexOf(keyCode) != -1);
                                                        document.getElementById("error").style.display = ret ? "none" : "inline";
                                                        return ret;
                                                    }
                                                </script>
                                            </td>
                                            <td class="auto-style13">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtAmount"
                                                    ErrorMessage="กรุณากรอกจำนวนเงิน" Style="color: #FF0000; font-size: large;" ValidationGroup="group">*</asp:RequiredFieldValidator>
                                            </td>
                                            <td class="auto-style12">
                                                <asp:Button ID="btnAdd" class="btn btn-primary" runat="server" ValidationGroup="group" Text="ตกลง" Height="30px" Width="100px" OnClick="btnAdd_Click" /></td>
                                            <td class="auto-style1" style="text-align: center"></td>
                                            <td class="auto-style2">
                                                <asp:Button ID="btnSave" class="btn btn-primary" runat="server" Text="บันทึก&พิมพ์" Height="30px" Width="100px" OnClick="btnSave_Click" Visible="False" />
                                            </td>
                                            <td></td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div style="margin-top: 20px;">
                        <asp:GridView ID="grdBank" runat="server" ForeColor="#507CD1" AutoGenerateColumns="False"
                            DataKeyNames="" PageSize="20" Width="800px" EmptyDataText="ยังไม่ได้เพิ่มธนาคาร" PageIndex="10"
                            Style="text-align: center" CssClass="grid">
                            <AlternatingRowStyle BackColor="White" />
                            <Columns>
                                <asp:BoundField DataField="CHQ_SEQ_NO" HeaderText="ลำดับ" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Center">
                                    <ItemStyle Width="5%"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="CHQ_NO" HeaderText="เลขที่เช็ค" ItemStyle-Width="25%" ItemStyle-HorizontalAlign="Center">
                                    <ItemStyle Width="25%"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="CHQ_BANK" HeaderText="ธนาคาร" ItemStyle-Width="45%" ItemStyle-HorizontalAlign="Center">
                                    <ItemStyle Width="45%"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="CHQ_AMOUNT" HeaderText="จำนวนเงิน" DataFormatString="{0:N2}" ItemStyle-Width="25%" ItemStyle-HorizontalAlign="Center">
                                    <ItemStyle Width="25%"></ItemStyle>
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
                    <div style="margin-top: 10px;">
                        <table style="width: 800px; height: 30px;">
                            <tr>
                                <td class="auto-style16">
                                    <asp:Label ID="lblAmount" runat="server" Text="ศูนย์บาท" Style="font-weight: 700"></asp:Label>
                                </td>
                                <td class="auto-style17"></td>
                                <td class="auto-style14">รวม</td>
                                <td>
                                    <asp:Label ID="lblNumAmount" runat="server" Text="0 บาท" Style="font-weight: 700"></asp:Label>
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
            <ajax:ConfirmButtonExtender ID="btnSave_ConfirmButtonExtender" runat="server"
                ConfirmText="ต้องการจะพิมพ์หรือไม่" Enabled="True" TargetControlID="btnSave">
            </ajax:ConfirmButtonExtender>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>