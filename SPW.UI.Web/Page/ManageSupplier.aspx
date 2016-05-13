<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage/MasterPageMainAdmin.Master" CodeBehind="ManageSupplier.aspx.cs" Inherits="SPW.UI.Web.Page.ManageSupplier" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1 class="page-header">
        <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Page/SearchSupplier.aspx">ซัพพลายเออร์</asp:HyperLink>
        /
        <asp:Label ID="lblName" runat="server" Text="เพิ่มซัพพลายเออร์ใหม่"></asp:Label></h1>
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
        .row {
            margin-top: 5px;
        }
    </style>

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>    
            <div class="panel panel-primary">
                <div class="panel-heading">
                    ข้อมูลซัพพลายเออร์         
                </div>
                <div class="panel-body">
                    <div class="row">
                        <div class="col-lg-12">
                            <div class="form">
                                <div class="form-group">
                                    <%--first row--%>
                                    <div class="row">
                                        <div class="col-md-2">รหัสซัพพลายเออร์</div>
                                        <div class="col-md-3">
                                            <asp:TextBox ID="txtSupplierCode" class="form-control" runat="server" Height="35px"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="กรุณากรอกรหัสซัพพลายเออร์" ValidationGroup="group" ControlToValidate="txtSupplierCode" Style="color: #FF0000; font-size: large;">*</asp:RequiredFieldValidator>
                                        </div>
                                        <div class="col-md-2">ชื่อซัพพลายเออร์</div>
                                        <div class="col-md-3">
                                            <asp:TextBox ID="txtSupplierName" class="form-control" runat="server" Height="35px"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="กรุณากรอกชื่อซัพพลายเออร์"
                                            ValidationGroup="group" ControlToValidate="txtSupplierName" Style="color: #FF0000; font-size: large;">*</asp:RequiredFieldValidator>
                                        </div>
                                        <div class="col-md-2"></div>
                                    </div>
                                    <%--second row--%>
                                    <div class="row">
                                        <div class="col-md-2">จังหวัด</div>
                                        <div class="col-md-3">                                            
                                            <asp:DropDownList ID="ddlProvince" class="form-control" runat="server" Height="35px" Width="200px">
                                                <asp:ListItem>กรุณาเลือก</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-md-2"></div>
                                        <div class="col-md-3"></div>
                                        <div class="col-md-2"></div>
                                    </div>
                                    <%--third row--%>
                                    <div class="row">
                                        <div class="col-md-2">อำเภอ</div>
                                        <div class="col-md-3">
                                            <asp:TextBox ID="txtAmpur" class="form-control" runat="server" Height="30px" Width="200px"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="กรุณากรอกอำเภอ"
                                            ValidationGroup="group" ControlToValidate="txtAmpur" Style="color: #FF0000; font-size: large;">*</asp:RequiredFieldValidator>
                                        </div>
                                        <div class="col-md-2">ตำบล</div>
                                        <div class="col-md-3">
                                            <asp:TextBox ID="txtTumbon" class="form-control" runat="server" Height="30px" Width="200px"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="กรุณากรอกตำบล"
                                            ValidationGroup="group" ControlToValidate="txtTumbon" Style="color: #FF0000; font-size: large;">*</asp:RequiredFieldValidator>
                                        </div>
                                        <div class="col-md-2">
                                        </div>
                                    </div>
                                    <%--forth row--%>
                                    <div class="row">
                                        <div class="col-md-2">ถนน</div>
                                        <div class="col-md-3">
                                            <asp:TextBox ID="txtRoad" class="form-control" runat="server" Height="30px" Width="200px" Visible="true"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="กรุณากรอกถนน"
                                            ValidationGroup="group" ControlToValidate="txtRoad" Style="color: #FF0000; font-size: large;">*</asp:RequiredFieldValidator>
                                        </div>
                                        <div class="col-md-2">รหัสไปรษณีย์</div>
                                        <div class="col-md-3">
                                            <asp:TextBox ID="txtPostCode" class="form-control" runat="server" Height="30px" Width="200px"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="กรุณากรอกรหัสไปรษณีย์"
                                            ValidationGroup="group" ControlToValidate="txtPostCode" Style="color: #FF0000; font-size: large;">*</asp:RequiredFieldValidator>
                                        </div>
                                        <div class="col-md-2"></div>
                                    </div>
                                    <%--fifth row--%>
                                    <div class="row">
                                        <div class="col-md-2">ที่อยู่</div>
                                        <div class="col-md-3">
                                            <asp:TextBox ID="txtAddress" Style="resize: none;" class="form-control" runat="server" Height="102px" TextMode="MultiLine" Width="646px"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ErrorMessage="กรุณากรอกที่อยู่"
                                            ValidationGroup="group" ControlToValidate="txtAddress" Style="color: #FF0000; font-size: large;">*</asp:RequiredFieldValidator>
                                        </div>
                                        <div class="col-md-2"></div>
                                        <div class="col-md-3"></div>
                                        <div class="col-md-2"></div>
                                    </div>
                                    <%--sixth row--%>
                                    <div class="row">
                                        <div class="col-md-2">เบอร์โทรศัพท์ 1</div>
                                        <div class="col-md-3">
                                            <asp:TextBox ID="txtTel1" class="form-control" runat="server" Height="30px" Width="200px"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ErrorMessage="กรุณากรอกเบอร์โทรศัพท์"
                                            ValidationGroup="group" ControlToValidate="txtTel1" Style="color: #FF0000; font-size: large;">*</asp:RequiredFieldValidator>
                                        </div>
                                        <div class="col-md-2">เบอร์โทรศัพท์ 2</div>
                                        <div class="col-md-3">
                                            <asp:TextBox ID="txtTel2" class="form-control" runat="server" Height="30px" Width="200px"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ErrorMessage="กรุณากรอกเบอร์โทรศัพท์"
                                            ValidationGroup="group" ControlToValidate="txtTel2" Style="color: #FF0000; font-size: large;">*</asp:RequiredFieldValidator>
                                        </div>
                                        <div class="col-md-2"></div>
                                    </div>
                                    <%--seventh row--%>
                                    <div class="row">
                                        <div class="col-md-2">เบอร์มือถือ</div>
                                        <div class="col-md-3">
                                            <asp:TextBox ID="txtMobli" class="form-control" runat="server" Height="30px" Width="200px"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ErrorMessage="กรุณากรอกเบอร์มือถือ"
                                            ValidationGroup="group" ControlToValidate="txtMobli" Style="color: #FF0000; font-size: large;">*</asp:RequiredFieldValidator>
                                        </div>
                                        <div class="col-md-2">แฟกต์</div>
                                        <div class="col-md-3">
                                            <asp:TextBox ID="txtFax" class="form-control" runat="server" Height="30px" Width="200px"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ErrorMessage="กรุณากรอกแฟกต์"
                                            ValidationGroup="group" ControlToValidate="txtFax" Style="color: #FF0000; font-size: large;">*</asp:RequiredFieldValidator>
                                        </div>
                                        <div class="col-md-2"></div>
                                    </div>
                                    <%--eight row--%>
                                    <div class="row">
                                        <div class="col-md-2">E-Mail</div>
                                        <div class="col-md-3">
                                            <asp:TextBox ID="txtEmail" class="form-control" runat="server" Height="30px" Width="200px"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ErrorMessage="กรุณากรอกอีเมล์"
                                            ValidationGroup="group" ControlToValidate="txtEmail" Style="color: #FF0000; font-size: large;">*</asp:RequiredFieldValidator>
                                        </div>
                                        <div class="col-md-2">ผู้ติดต่อ</div>
                                        <div class="col-md-3">
                                            <asp:TextBox ID="txtContact" class="form-control" runat="server" Height="30px" Width="200px"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ErrorMessage="กรุณากรอกผู้ติดต่อ"
                                            ValidationGroup="group" ControlToValidate="txtContact" Style="color: #FF0000; font-size: large;">*</asp:RequiredFieldValidator>
                                        </div>
                                        <div class="col-md-2"></div>
                                    </div>
                                    <%--ninth row--%>
                                    <div class="row">
                                        <div class="col-md-2">ช่วงของเครดิต</div>
                                        <div class="col-md-3">                                            
                                            <asp:DropDownList ID="ddlCreditIn" class="form-control" runat="server" Height="35px" Width="200px">
                                                <asp:ListItem Value="NO">ไม่ให้ Credit</asp:ListItem>
                                                <asp:ListItem Value="DD">นับเป็นวัน</asp:ListItem>
                                                <asp:ListItem Value="MM">นับเป็นเดือน</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-md-2">จำนวนเครดิต</div>
                                        <div class="col-md-3">
                                            <asp:TextBox ID="txtCreditValue" class="form-control" runat="server" Height="30px" Width="200px"></asp:TextBox>
                                        </div>
                                        <div class="col-md-2"></div>
                                    </div>
                                    <%--ten row--%>
                                    <div class="row">
                                        <div class="col-md-2">Vat Type</div>
                                        <div class="col-md-3">
                                            <asp:DropDownList ID="ddlVatType" class="form-control" runat="server" Height="35px" Width="200px">
                                                <asp:ListItem>กรุณาเลือก</asp:ListItem>
                                                <asp:ListItem Value="IN">Include Vat</asp:ListItem>
                                                <asp:ListItem Value="EX">Exclude Vat</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-md-2">อัตรา(%)</div>
                                        <div class="col-md-3">
                                            <asp:TextBox ID="txtVatRate" class="form-control" runat="server" Height="30px" Width="200px"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ErrorMessage="กรุณากรอกจำนวนอัตรา"
                                            ValidationGroup="group" ControlToValidate="txtVatRate" Style="color: #FF0000; font-size: large;">*</asp:RequiredFieldValidator>
                                        </div>
                                        <div class="col-md-2">
                                            <asp:Label ID="flag" runat="server" Text="Add" Visible="false"></asp:Label>
                                        </div>
                                    </div>
                                    <%--eleven row--%>
                                    <div class="row">
                                        <div class="col-md-2">
                                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" Style="color: #FF0000; font-size: large;"
                                                ValidationGroup="group" ShowMessageBox="True" ShowSummary="False" />
                                        </div>
                                        <div class="col-md-3"></div>
                                        <div class="col-md-2"></div>
                                        <div class="col-md-2"></div>
                                        <div class="col-md-3">
                                            <asp:Button ID="btnSave" ValidationGroup="group" runat="server" Text="บึนทึก" class="btn btn-primary" OnClick="btnSave_Click" />
                                            <asp:Button ID="btnCancel" class="btn btn-primary" Text="ยกเลิก" runat="server" OnClick="btnCancel_Click" />
                                            <asp:Label id="lblError" runat="server" ForeColor="Red" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <!-- /.col-lg-6 (nested) -->
                    </div>
                </div>
                <!-- /.panel-body -->
            </div>
            <!-- /.panel -->
    <ajax:ConfirmButtonExtender ID="btnSave_ConfirmButtonExtender" runat="server"
        ConfirmText="ต้องการจะบันทึกหรือไม่" Enabled="True" TargetControlID="btnSave">
    </ajax:ConfirmButtonExtender>
</asp:Content>