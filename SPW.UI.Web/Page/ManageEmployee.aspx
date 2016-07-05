<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterPageMainAdmin.Master" AutoEventWireup="true"
    CodeBehind="ManageEmployee.aspx.cs" Inherits="SPW.UI.Web.Page.ManageEmployee" %>

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
    <h1 class="page-header">
        <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Page/SearchEmployee.aspx">พนักงาน</asp:HyperLink>
        -
        <asp:Label ID="lblName" runat="server" Text="เพิ่มพนักงานใหม่"></asp:Label></h1>
    <div class="alert alert-info" id="alert" runat="server" visible="false">
        <strong>
            <asp:Label ID="lblAlert" runat="server" Text="บันทึกข้อมูลสำเร็จ Save Success"></asp:Label></strong>
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

        .auto-style10 {
            width: 67px;
        }

        .row {
            margin-top: 5px;
        }
    </style>
    <script type="text/javascript">
        var specialKeys = new Array();
        specialKeys.push(8); //Backspace
        function IsNumeric(e) {
            var keyCode = e.which ? e.which : e.keyCode
            var ret = (keyCode != 13 && keyCode != 46 && (keyCode < 48 || keyCode > 57) || specialKeys.indexOf(keyCode) != -1);
            //var ret = ((keyCode >= 48 && keyCode <= 57) || specialKeys.indexOf(keyCode) != -1);
            document.getElementById("error1").style.display = ret ? "inline" : "none";
            return !ret;
        }
    </script>
    <script type="text/javascript">
        var specialKeys = new Array();
        specialKeys.push(8); //Backspace
        function IsNumeric(e) {
            var keyCode = e.which ? e.which : e.keyCode
            var ret = (keyCode != 13 && keyCode != 46 && (keyCode < 48 || keyCode > 57) || specialKeys.indexOf(keyCode) != -1);
            //var ret = ((keyCode >= 48 && keyCode <= 57) || specialKeys.indexOf(keyCode) != -1);
            document.getElementById("error2").style.display = ret ? "inline" : "none";
            return !ret;
        }
    </script>
    <script type="text/javascript">
        var specialKeys = new Array();
        specialKeys.push(8); //Backspace
        function IsNumeric(e) {
            var keyCode = e.which ? e.which : e.keyCode
            var ret = (keyCode != 13 && keyCode != 46 && (keyCode < 48 || keyCode > 57) || specialKeys.indexOf(keyCode) != -1);
            //var ret = ((keyCode >= 48 && keyCode <= 57) || specialKeys.indexOf(keyCode) != -1);
            document.getElementById("error3").style.display = ret ? "inline" : "none";
            return !ret;
        }
    </script>
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

    <div class="panel panel-primary">
        <div class="panel-heading">
            ข้อมูลพนักงาน        
        </div>

        <div class="panel-body">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <div class="row">
                        <div class="col-lg-12">
                            <div class="form">
                                <div class="form-group">
                                    <div class="row">
                                        <div class="col-md-2">รหัสพนักงาน</div>
                                        <div class="col-md-3">
                                            <asp:TextBox ID="popTxtEmployeeCode" class="form-control" runat="server" Height="35px" Width="200px" placeholder="รหัสพนักงาน"></asp:TextBox>
                                        </div>
                                        <div class="col-md-2"></div>
                                        <div class="col-md-3">
                                        </div>
                                        <div class="col-md-2"></div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-2">ชื่อ</div>
                                        <div class="col-md-3">
                                            <asp:TextBox ID="txtName" class="form-control" runat="server" Height="35px" Width="200px" placeholder="ชื่อ"></asp:TextBox>
                                        </div>
                                        <div class="col-md-2">นามสกุล</div>
                                        <div class="col-md-3">
                                            <asp:TextBox ID="txtLastName" class="form-control" runat="server" Height="35px" Width="200px" placeholder="นามสกุล"></asp:TextBox>
                                        </div>
                                        <div class="col-md-2"></div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-2">เพศ</div>
                                        <div class="col-md-3">
                                            <asp:DropDownList ID="ddlSex" class="form-control" runat="server" Height="35px" Width="200px" AutoPostBack="True" SelectedValue='<%# Eval("Key") %>'>
                                                <asp:ListItem Value="1" Selected="True">ชาย</asp:ListItem>
                                                <asp:ListItem Value="2">หญิง</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-md-2">วันเกิด</div>
                                        <div class="col-md-3">
                                            <div class='input-group date' id='datetimepicker1'>
                                                <asp:TextBox ID="txtBDate" class="form-control datetimepicker" runat="server" Height="35px" placeholder="วันเกิด"></asp:TextBox>
                                                <span class="input-group-addon">
                                                    <span class="glyphicon glyphicon-calendar"></span>
                                                </span>
                                            </div>
                                        </div>
                                        <div class="col-md-2"></div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-2">สัญชาติ</div>
                                        <div class="col-md-3">
                                            <asp:TextBox ID="txtNationality" class="form-control" runat="server" Height="35px" Width="200px" placeholder="สัญชาติ"></asp:TextBox>

                                        </div>
                                        <div class="col-md-2">ศาสนา</div>
                                        <div class="col-md-3">
                                            <asp:TextBox ID="txtReligion" class="form-control" runat="server" Height="35px" Width="200px" placeholder="ศาสนา"></asp:TextBox>
                                        </div>
                                        <div class="col-md-2"></div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-2">ที่อยู่ที่ 1</div>
                                        <div class="col-md-3">
                                            <asp:TextBox ID="txtAddress1" class="form-control" runat="server" Height="100px" Width="620px" placeholder="ที่อยู่ที่ 1" TextMode="MultiLine"></asp:TextBox>

                                        </div>
                                        <div class="col-md-2"></div>
                                        <div class="col-md-3">
                                        </div>
                                        <div class="col-md-2"></div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-2">เบอร์โทรที่ 1</div>
                                        <div class="col-md-3">
                                            <asp:TextBox ID="txtPhone1" class="form-control" runat="server" Height="35px" Width="200px" placeholder="เบอร์โทรที่ 1"></asp:TextBox>
                                        </div>
                                        <div class="col-md-2">เบอร์โทรที่ 2</div>
                                        <div class="col-md-3">
                                            <asp:TextBox ID="txtPhone2" class="form-control" runat="server" Height="35px" Width="200px" placeholder="เบอร์โทรที่ 2"></asp:TextBox>

                                        </div>
                                        <div class="col-md-2"></div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-2">สถานภาพทหาร</div>
                                        <div class="col-md-3">
                                            <asp:TextBox ID="txtSoldier" class="form-control" runat="server" Height="35px" Width="200px" placeholder="สถานภาพทหาร"></asp:TextBox>
                                        </div>
                                        <div class="col-md-2">สถานภาพสมรส</div>
                                        <div class="col-md-3">
                                            <asp:TextBox ID="txtMarry" class="form-control" runat="server" Height="35px" Width="200px" placeholder="สถานภาพสมรส"></asp:TextBox>
                                        </div>
                                        <div class="col-md-2"></div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-2">ระดับการศึกษาสูงสุด</div>
                                        <div class="col-md-3">
                                            <asp:TextBox ID="txtGrade" class="form-control" runat="server" Height="35px" Width="200px" placeholder="ระดับการศึกษาสูงสุด"></asp:TextBox>
                                        </div>
                                        <div class="col-md-2">สถานศึกษาที่จบ</div>
                                        <div class="col-md-3">
                                            <asp:TextBox ID="txtUniversity" class="form-control" runat="server" Height="35px" Width="200px" placeholder="สถานศึกษาที่จบ"></asp:TextBox>
                                        </div>
                                        <div class="col-md-2"></div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-2">
                                            เพิ่มข้อมูลการทำงาน
                                        </div>
                                        <div class="col-md-3">
                                            <asp:Button ID="btnAddEmpPos" runat="server" Text="+" class="btn btn-primary" OnClick="btnAddEmpPos_Click" />
                                        </div>
                                        <div class="col-md-2"></div>
                                        <div class="col-md-3">
                                        </div>
                                        <div class="col-md-2"></div>
                                    </div>
                                    <div runat="server" id="addEmpPos1" visible="false" class="row">
                                        <div class="col-md-2">
                                            ลำดับที่
                                        </div>
                                        <div class="col-md-3">
                                            <asp:TextBox ID="txtEmpPosSeq" class="form-control" runat="server" Height="35px" Width="200px" Enabled="false"></asp:TextBox>
                                        </div>
                                        <div class="col-md-2">ตำแหน่ง</div>
                                        <div class="col-md-3">
                                            <asp:DropDownList ID="dllEmpPos" class="form-control" runat="server" Height="35px" Width="200px" SelectedValue='<%# Eval("Key") %>'>
                                                <asp:ListItem Text="กรุณาเลือก" Selected="True" Value="0"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-md-2"></div>
                                    </div>
                                    <div runat="server" id="addEmpPos2" visible="false" class="row">
                                        <div class="col-md-2">
                                            แผนก
                                        </div>
                                        <div class="col-md-3">
                                            <asp:DropDownList ID="ddlDepart" class="form-control" runat="server" Height="35px" Width="200px" SelectedValue='<%# Eval("Key") %>'>
                                                <asp:ListItem Text="กรุณาเลือก" Selected="True" Value="0"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-md-2">เงินเดือน</div>
                                        <div class="col-md-3">
                                            <asp:TextBox ID="txtEmpPosSalary" class="form-control" runat="server" Height="35px" Width="200px" placeholder="เงินเดือน" ondrop="return true;" onkeypress="return IsNumeric(event);" onpaste="return true;"></asp:TextBox>
                                            <span id="error1" style="color: Red; display: none">*กรุณกรอกเป็นจำนวนเงิน</span>
                                        </div>
                                        <div class="col-md-2"></div>
                                    </div>
                                    <div runat="server" id="addEmpPos3" visible="false" class="row">
                                        <div class="col-md-2">
                                            ค่าตำแหน่ง
                                        </div>
                                        <div class="col-md-3">
                                            <asp:TextBox ID="txtEmpPosSalaryPos" class="form-control" runat="server" Height="35px" Width="200px" placeholder="ค่าตำแหน่ง" ondrop="return true;" onkeypress="return IsNumeric(event);" onpaste="return true;"></asp:TextBox>
                                            <span id="error2" style="color: Red; display: none">*กรุณกรอกเป็นจำนวนเงิน</span>
                                        </div>
                                        <div class="col-md-2">ค่าจ้างตามความถนัด</div>
                                        <div class="col-md-3">
                                            <asp:TextBox ID="txtEmpPosSalarySkill" class="form-control" runat="server" Height="35px" Width="200px" placeholder="ค่าจ้างตามความถนัด" ondrop="return true;" onkeypress="return IsNumeric(event);" onpaste="return true;"></asp:TextBox>
                                            <span id="error3" style="color: Red; display: none">*กรุณกรอกเป็นจำนวนเงิน</span>
                                        </div>
                                        <div class="col-md-2"></div>
                                    </div>
                                    <div runat="server" id="addEmpPos4" visible="false" class="row">
                                        <div class="col-md-2">วันที่มีผล</div>
                                        <div class="col-md-3">
                                            <div class='input-group date' id='Div2'>
                                                <asp:TextBox ID="txtEmpPosEff" class="form-control datetimepicker" runat="server" Height="35px" placeholder="วันที่มีผล"></asp:TextBox>
                                                <span class="input-group-addon">
                                                    <span class="glyphicon glyphicon-calendar"></span>
                                                </span>
                                            </div>
                                        </div>
                                        <div class="col-md-2">วันที่สิ้นสุด</div>
                                        <div class="col-md-3">
                                            <div class='input-group date' id='Div1'>
                                                <asp:TextBox ID="txtEmpPosExp" class="form-control datetimepicker" runat="server" Height="35px" placeholder="วันที่สิ้นสุด"></asp:TextBox>
                                                <span class="input-group-addon">
                                                    <span class="glyphicon glyphicon-calendar"></span>
                                                </span>
                                            </div>
                                        </div>
                                        <div class="col-md-2"></div>
                                    </div>
                                    <div runat="server" id="addEmpPos5" visible="false" class="row">
                                        <div class="col-md-2"></div>
                                        <div class="col-md-3">
                                        </div>
                                        <div class="col-md-2"></div>
                                        <div class="col-md-3">
                                            <asp:Button ID="btnAdd" runat="server" Text="เพิ่ม" Width="75px" class="btn btn-primary" OnClick="btnAdd_Click" />
                                        </div>
                                        <div class="col-md-2"></div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <!-- /.col-lg-6 (nested) -->
                </ContentTemplate>
            </asp:UpdatePanel>
            <div class="panel panel-primary">
                <asp:GridView ID="grdEmpPos" runat="server" ForeColor="#507CD1" AutoGenerateColumns="False" 
                    DataKeyNames="POSITION_ID" PageSize="20" Width="100%" EmptyDataText="ไม่พบข้อมูลตำแหน่งพนักงาน"
                    Style="text-align: center" CssClass="grid" OnRowDataBound="grdEmpPos_RowDataBound" OnRowDeleting="grdEmpPos_RowDeleting">
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:TemplateField HeaderText="ลำดับ" ItemStyle-Width="5%">
                            <ItemTemplate>
                                <%# Container.DataItemIndex + 1 %>
                            </ItemTemplate>
                            <ItemStyle BorderStyle="Solid" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="POSITION_NAME" HeaderText="ชื่อตำแหน่งพนักงาน" ItemStyle-Width="75%">
                            <ItemStyle Width="85%"></ItemStyle>
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="รายละเอียด" ItemStyle-Width="10%">
                            <ItemTemplate>
                                <asp:LinkButton ID="lbtnDetail" runat="server">
                                    <div class='glyphicon glyphicon-list'></div>
                                </asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="ลบ" ShowHeader="False">
                            <ItemTemplate>
                                <asp:ImageButton ID="ImageButton1" runat="server" ItemStyle-Width="10%" CausesValidation="False" CommandName="Delete"
                                    ImageUrl="~/Image/Icon/close.png" Text="ลบ" />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
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
            </div>
        </div>
        <div style="float: right;">
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
    </div>
    <!-- /.panel -->
    <ajax:ConfirmButtonExtender ID="btnSave_ConfirmButtonExtender" runat="server"
        ConfirmText="ต้องการจะบันทึกหรือไม่" Enabled="True" TargetControlID="btnSave">
    </ajax:ConfirmButtonExtender>
</asp:Content>
