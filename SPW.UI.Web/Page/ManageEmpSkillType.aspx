<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPageMainAdmin.Master"
    AutoEventWireup="true" CodeBehind="ManageEmpSkillType.aspx.cs" Inherits="SPW.UI.Web.Page.ManageEmpSkillType" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1 class="page-header">
        <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Page/SearchEmpSkillType.aspx">ประเภททักษะพนักงาน</asp:HyperLink>
        -
        <asp:Label ID="lblName" runat="server" Text="เพิ่มประเภททักษะพนักงานใหม่"></asp:Label></h1>
    <div class="alert alert-info" id="alert" runat="server" visible="false">
        <strong>บันทึกข้อมูลสำเร็จ Save Success</strong>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
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

        .auto-style4 {
            width: 85px;
        }

        .auto-style5 {
            width: 287px;
        }

        .auto-style7 {
            width: 80px;
        }

        .auto-style8 {
            width: 83px;
        }

        .auto-style9 {
            width: 215px;
        }

        .auto-style10 {
            width: 184px;
        }

        .auto-style12 {
            width: 229px;
        }

        .auto-style13 {
            width: 63px;
        }

        .auto-style14 {
            width: 29px;
        }

        .auto-style15 {
            width: 76px;
        }
    </style>
    <div class="panel panel-primary">
        <div class="panel-heading">
            ข้อมูลประเภททักษะพนักงาน
        </div>
        <div class="panel-body">
            <div class="row">
                <div class="col-lg-12">
                    <div class="form">
                        <div class="form-group">
                            <%--first row--%>
                            <div class="row">
                                <div class="col-md-2">ชื่อประเภททักษะ</div>
                                <div class="col-md-3">
                                    <asp:TextBox ID="txtPosition" class="form-control" runat="server" Height="35px" placeholder="ชื่อประเภททักษะ"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="กรุณากรอกชื่อประเภททักษะ"
                                        ControlToValidate="txtPosition" ValidationGroup="group" Style="color: #FF0000; font-size: large;">*</asp:RequiredFieldValidator>
                                </div>
                                <div class="col-md-2">
                                    เปอร์เซ็น
                                </div>
                                <div class="col-md-3">
                                    <asp:TextBox ID="txtPercen" ondrop="return true;" onkeypress="return IsNumeric(event);" onpaste="return true;" class="form-control" runat="server" Height="35px" placeholder="เปอร์เซ็น"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="กรุณากรอกเปอร์เซ็น"
                                        ControlToValidate="txtPosition" ValidationGroup="group" Style="color: #FF0000; font-size: large;">*</asp:RequiredFieldValidator>
                                    <span id="error" style="color: Red; display: none">*กรุณกรอกเป็นตัวเลข</span>
                                    <script type="text/javascript">
                                        var specialKeys = new Array();
                                        specialKeys.push(8); //Backspace
                                        function IsNumeric(e) {
                                            var keyCode = e.which ? e.which : e.keyCode
                                            var ret = (keyCode != 13 && keyCode != 46 && (keyCode < 48 || keyCode > 57) || specialKeys.indexOf(keyCode) != -1);
                                            //var ret = ((keyCode >= 48 && keyCode <= 57) || specialKeys.indexOf(keyCode) != -1);
                                            document.getElementById("error").style.display = ret ? "inline" : "none";
                                            return !ret;
                                        }
                                    </script>
                                </div>
                                <div class="col-md-2">
                                    <asp:Button ID="btnSave" runat="server" class="btn btn-primary" Text="บันทึก" OnClick="btnSave_Click" ValidationGroup="group" />
                                    <asp:Button ID="btnCancel" runat="server" class="btn btn-primary" Text="ยกเลิก" PostBackUrl="~/Page/SearchEmpSkillType.aspx" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <!-- /.col-lg-6 (nested) -->
            </div>
            <div>
                <asp:ValidationSummary ID="ValidationSummary1" runat="server" Style="color: #FF0000; font-size: large;"
                    ValidationGroup="group" ShowMessageBox="True" ShowSummary="False" />
            </div>
        </div>
    </div>
    <ajax:ConfirmButtonExtender ID="btnSave_ConfirmButtonExtender" runat="server"
        ConfirmText="ต้องการจะบันทึกหรือไม่" Enabled="True" TargetControlID="btnSave">
    </ajax:ConfirmButtonExtender>
</asp:Content>
