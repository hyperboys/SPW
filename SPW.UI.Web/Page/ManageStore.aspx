<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPageMainAdmin.Master" AutoEventWireup="true"
    CodeBehind="ManageStore.aspx.cs" Inherits="SPW.UI.Web.Page.ManageStore" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1 class="page-header">
        <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Page/SearchStore.aspx">ร้านค้า</asp:HyperLink>
        -
        <asp:Label ID="lblName" runat="server" Text="เพิ่มร้านค้าใหม่"></asp:Label></h1>
    <div class="alert alert-info" id="alert" runat="server" visible="false">
        <strong>บันทึกข้อมูลสำเร็จ Save Success</strong>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <style type="text/css">
        .right {
            text-align: right;
        }

        .auto-style5 {
            width: 13px;
        }

        .auto-style12 {
            width: 128px;
        }

        .auto-style13 {
            width: 652px;
        }

        .auto-style15 {
            width: 208px;
        }

        .auto-style16 {
            width: 73px;
        }

        .auto-style18 {
            width: 15px;
        }

        .auto-style19 {
            width: 136px;
        }

        .auto-style20 {
            width: 131px;
        }

        .auto-style22 {
            width: 126px;
        }

        .auto-style23 {
            width: 9px;
        }
    </style>

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>


    <div class="panel panel-primary">
        <div class="panel-heading">
            ข้อมูลร้านค้า            
        </div>
        <div class="panel-body">
            <div class="row">
                <div class="col-lg-6">
                    <div class="form">
                        <div class="form-group">
                            <table style="width: 972px; height: 171px;">
                                <tr>
                                    <td class="auto-style12">รหัสร้านค้า</td>
                                    <td style="text-align: center" class="auto-style5">:</td>
                                    <td class="auto-style15">
                                        <asp:TextBox ID="popTxtStoreCode" class="form-control" runat="server" Height="30px" Width="200px"></asp:TextBox>
                                    </td>
                                    <td class="auto-style5">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="กรุณากรอกรหัสร้านค้า"
                                            ValidationGroup="group" ControlToValidate="popTxtStoreCode" Style="color: #FF0000; font-size: large;">*</asp:RequiredFieldValidator>
                                    </td>
                                    <td class="auto-style16"></td>
                                    <td class="auto-style20">ชื่อร้านค้า</td>
                                    <td style="text-align: center" class="auto-style5">:</td>
                                    <td class="auto-style15">
                                        <asp:TextBox ID="poptxtStoreName" class="form-control" runat="server" Height="30px" Width="200px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="กรุณากรอกชื่อร้านค้า"
                                            ValidationGroup="group" ControlToValidate="poptxtStoreName" Style="color: #FF0000; font-size: large;">*</asp:RequiredFieldValidator>
                                    </td>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td class="auto-style12">ภาค</td>
                                    <td style="text-align: center" class="auto-style5">:</td>
                                    <td class="auto-style15">
                                        <asp:DropDownList ID="ddlSector" class="form-control" runat="server" Height="35px" Width="200px" AutoPostBack="True" OnSelectedIndexChanged="ddlSector_SelectedIndexChanged" SelectedValue='<%# Eval("Key") %>'>
                                            <asp:ListItem Value="0">กรุณาเลือก</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td class="auto-style5"></td>
                                    <td class="auto-style16"></td>
                                    <td class="auto-style20">จังหวัด</td>
                                    <td style="text-align: center" class="auto-style5">:</td>
                                    <td class="auto-style15">
                                        <asp:TextBox ID="txtProvince" class="form-control" runat="server" Height="35px" placeholder="จังหวัด" data-provide="typeahead" data-items="5" autocomplete="off"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ErrorMessage="กรุณากรอกจังหวัด"
                                            ValidationGroup="group" ControlToValidate="txtProvince" Style="color: #FF0000; font-size: large;">*</asp:RequiredFieldValidator>
                                    </td>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td class="auto-style12">อำเภอ</td>
                                    <td style="text-align: center" class="auto-style5">:</td>
                                    <td class="auto-style15">
                                        <asp:TextBox ID="txtAmpur" class="form-control" runat="server" Height="30px" Width="200px"></asp:TextBox>
                                    </td>
                                    <td class="auto-style5">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="กรุณากรอกอำเภอ"
                                            ValidationGroup="group" ControlToValidate="txtAmpur" Style="color: #FF0000; font-size: large;">*</asp:RequiredFieldValidator>
                                    </td>
                                    <td class="auto-style16"></td>
                                    <td class="auto-style20">ตำบล</td>
                                    <td style="text-align: center" class="auto-style5">:</td>
                                    <td class="auto-style15">
                                        <asp:TextBox ID="txtTumbon" class="form-control" runat="server" Height="30px" Width="200px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="กรุณากรอกตำบล"
                                            ValidationGroup="group" ControlToValidate="txtTumbon" Style="color: #FF0000; font-size: large;">*</asp:RequiredFieldValidator>
                                    </td>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td class="auto-style12">ถนน</td>
                                    <td style="text-align: center" class="auto-style5">:</td>
                                    <td class="auto-style15">
                                        <asp:TextBox ID="txtRoad" class="form-control" runat="server" Height="30px" Width="200px" Visible="true"></asp:TextBox>
                                    </td>
                                    <td class="auto-style5">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="กรุณากรอกถนน"
                                            ValidationGroup="group" ControlToValidate="txtRoad" Style="color: #FF0000; font-size: large;">*</asp:RequiredFieldValidator>
                                    </td>
                                    <td class="auto-style16"></td>
                                    <td class="auto-style20">รหัสไปรษณีย์</td>
                                    <td style="text-align: center" class="auto-style5">:</td>
                                    <td class="auto-style15">
                                        <asp:TextBox ID="txtPostCode" class="form-control" runat="server" Height="30px" Width="200px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="กรุณากรอกรหัสไปรษณีย์"
                                            ValidationGroup="group" ControlToValidate="txtPostCode" Style="color: #FF0000; font-size: large;">*</asp:RequiredFieldValidator>
                                    </td>
                                    <td></td>
                                </tr>
                            </table>
                            <table style="width: 972px">
                                <tr>
                                    <td class="auto-style12">ที่อยู่
                                    </td>
                                    <td style="text-align: center" class="auto-style5">:
                                    </td>
                                    <td class="auto-style13">
                                        <asp:TextBox ID="txtAddress" Style="resize: none;" class="form-control" runat="server" Height="102px" TextMode="MultiLine" Width="646px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ErrorMessage="กรุณากรอกที่อยู่"
                                            ValidationGroup="group" ControlToValidate="txtAddress" Style="color: #FF0000; font-size: large;">*</asp:RequiredFieldValidator>
                                    </td>
                                    <td></td>
                                </tr>
                            </table>
                            <table style="width: 971px; height: 136px; margin-top: 5px;">
                                <tr>
                                    <td class="auto-style22">เบอร์โทรศัพท์ 1</td>
                                    <td style="text-align: center" class="auto-style5">:</td>
                                    <td class="auto-style15">
                                        <asp:TextBox ID="txtTel1" class="form-control" runat="server" Height="30px" Width="200px"></asp:TextBox>
                                    </td>
                                    <td class="auto-style18">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ErrorMessage="กรุณากรอกเบอร์โทรศัพท์ 1"
                                            ValidationGroup="group" ControlToValidate="txtTel1" Style="color: #FF0000; font-size: large;">*</asp:RequiredFieldValidator>
                                    </td>
                                    <td class="auto-style16"></td>
                                    <td class="auto-style19">เบอร์โทรศัพท์ 2</td>
                                    <td style="text-align: center" class="auto-style23">:</td>
                                    <td>
                                        <asp:TextBox ID="txtTel2" class="form-control" runat="server" Height="30px" Width="200px"></asp:TextBox>
                                    </td>
                                    <td></td>

                                </tr>
                                <tr>
                                    <td class="auto-style22">เบอร์มือถือ</td>
                                    <td style="text-align: center" class="auto-style5">:</td>
                                    <td class="auto-style15">
                                        <asp:TextBox ID="txtMobli" class="form-control" runat="server" Height="30px" Width="200px"></asp:TextBox>
                                    </td>
                                    <td class="auto-style18"></td>
                                    <td class="auto-style16"></td>
                                    <td class="auto-style19">แฟกต์</td>
                                    <td style="text-align: center" class="auto-style23">:</td>
                                    <td>
                                        <asp:TextBox ID="txtFax" class="form-control" runat="server" Height="30px" Width="200px"></asp:TextBox>
                                    </td>
                                    <td></td>

                                </tr>
                                <tr>
                                    <td class="auto-style22">ราคาขายสินค้า</td>
                                    <td style="text-align: center" class="auto-style5">:</td>
                                    <td class="auto-style15">
                                        <asp:DropDownList ID="ddlZone" class="form-control" runat="server" Height="35px" Width="200px" OnSelectedIndexChanged="ddlSector_SelectedIndexChanged" SelectedValue='<%# Eval("Key") %>'>
                                            <asp:ListItem Value="0">กรุณาเลือก</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td></td>
                                    <td></td>
                                    <td class="auto-style22">พนักงานขาย</td>
                                    <td style="text-align: center" class="auto-style23">:</td>
                                    <td class="auto-style19">
                                        <asp:DropDownList ID="ddlSell" class="form-control" runat="server" Height="35px" Width="200px" SelectedValue='<%# Eval("Key") %>'>
                                            <asp:ListItem Value="0">กรุณาเลือก</asp:ListItem>
                                        </asp:DropDownList></td>
                                    <td></td>

                                </tr>
                            </table>
                            <asp:Label ID="flag" runat="server" Text="Add" Visible="false"></asp:Label>
                        </div>
                    </div>
                </div>
                <div>
                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" Style="color: #FF0000; font-size: large;"
                        ValidationGroup="group" ShowMessageBox="True" ShowSummary="False" />
                </div>
            </div>
            <div style="margin-top: 10px; float: right;">
                <table>
                    <tr>
                        <td>
                            <asp:Button ID="btnSave" ValidationGroup="group" runat="server" Text="บันทึก" class="btn btn-primary" OnClick="btnSave_Click" />
                        </td>
                        <td>&nbsp;&nbsp;</td>
                        <td>
                            <asp:Button ID="btnCancel" class="btn btn-primary" Text="ยกเลิก" runat="server" OnClick="btnCancel_Click" />
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
    <ajax:ConfirmButtonExtender ID="btnSave_ConfirmButtonExtender" runat="server"
        ConfirmText="ต้องการจะบันทึกหรือไม่" Enabled="True" TargetControlID="btnSave">
    </ajax:ConfirmButtonExtender>
</asp:Content>


