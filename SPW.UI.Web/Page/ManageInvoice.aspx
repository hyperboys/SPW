<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage/MasterPageMainAdmin.Master" CodeBehind="ManageInvoice.aspx.cs" Inherits="SPW.UI.Web.Page.ManageInvoice" %>

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
    <script type="text/javascript">
        function checkRadioBtn(id) {
            var gv = document.getElementById('<%=gridSupplier.ClientID %>');

        for (var i = 1; i < gv.rows.length; i++) {
            var radioBtn = gv.rows[i].cells[0].getElementsByTagName("input");

            // Check if the id not same
            if (radioBtn[0].id != id.id) {
                radioBtn[0].checked = false;
            }
        }
    }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1 class="page-header">ทำรายการส่งซ่อม</h1>
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
    <style type="text/css">
    .modalBackground
    {
        background-color: Black;
        filter: alpha(opacity=90);
        opacity: 0.8;
    }
    .modalPopup
    {
        background-color: #FFFFFF;
        border-color: black;
        padding:5px;
        width:750px;
    }
    </style>
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional" >
        <ContentTemplate>
            <asp:Panel ID="pnl" runat="server">
                <div class="alert alert-info" id="alert" runat="server">
                    <strong>บันทึกข้อมูลสำเร็จ Save Success</strong>
                </div>
            </asp:Panel>
            <div class="panel panel-primary">
                <div class="panel-heading">
                    รายการส่งซ่อม        
                </div>
                <div class="panel-body">
                    <div class="row">
                        <div class="col-lg-12">
                            <div class="form">
                                <div class="form-group">
                                    <%--first row--%>
                                    <div class="row">
                                        <div class="col-md-2">ประเภททรัพย์สิน</div>
                                        <div class="col-md-3">
                                            <div class='input-group date' id='datetimepicker1'>
                                                <asp:DropDownList ID="ddlAssetType" class="form-control" runat="server" Height="35px" Width="200px" AutoPostBack="True">
                                                    <asp:ListItem Value="0">กรุณาเลือก</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-md-2">ทะเบียนรถ</div>
                                        <div class="col-md-3">
                                            <div class='input-group date' id='Div1'>
                                                <asp:DropDownList ID="ddlVehicle" class="form-control" runat="server" Height="35px" Width="200px" AutoPostBack="True">
                                                    <asp:ListItem Value="0">กรุณาเลือก</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-md-2"></div>
                                    </div>
                                    <%--second row--%>
                                    <div class="row">
                                        <div class="col-md-2">ชื่อ Supplier</div>
                                        <div class="col-md-3">
                                            <asp:TextBox ID="txtVendorName" runat="server" Height="35px" placeholder="ชื่อ"></asp:TextBox>
                                        </div>
                                        <div class="col-md-2">รหัส Supplier</div>
                                        <div class="col-md-3">
                                            <asp:TextBox ID="txtVendorCode" runat="server" Height="35px" placeholder="รหัส"></asp:TextBox>
                                            <asp:HiddenField ID="hfVendorID" runat="server" />
                                            <asp:Button ID="btnShow" class="btn btn-primary" runat="server" Text="ค้นหา" OnClick="btnShow_Click" />
                                        </div>
                                        <div class="col-md-2" style="text-align:left">
                                        </div>
                                    </div>
                                    <%--third row--%>
                                    <div class="row">
                                        <div class="col-md-2">เลขไมล์</div>
                                        <div class="col-md-3">
                                            <asp:TextBox ID="txtMileNo" runat="server" Height="35px" placeholder="เลขไมล์"></asp:TextBox>
                                        </div>
                                        <div class="col-md-2"></div>
                                        <div class="col-md-3"></div>
                                        <div class="col-md-2"></div>
                                    </div>
                                    <%--forth row--%>
                                    <div class="row">
                                        <div class="col-md-2"><h5><u>ระยะเวลาที่เข้าซ่อม</u></h5></div>
                                        <div class="col-md-3"></div>
                                        <div class="col-md-2"></div>
                                        <div class="col-md-3"></div>
                                        <div class="col-md-2"></div>
                                    </div>
                                    <%--fifth row--%>
                                    <div class="row">
                                        <div class="col-md-2">เริ่มต้น</div>
                                        <div class="col-md-3">
                                            <div class='input-group date' id='Div2'>
                                                <asp:TextBox ID="txtStartDate" class="form-control datetimepicker" runat="server" Height="35px" placeholder="วันที่เริ่มต้น"></asp:TextBox>
                                                <span class="input-group-addon">
                                                    <span class="glyphicon glyphicon-calendar"></span>
                                                </span>
                                            </div>
                                        </div>
                                        <div class="col-md-2">สิ้นสุด</div>
                                        <div class="col-md-3">
                                            <div class='input-group date' id='Div3'>
                                                <asp:TextBox ID="txtEndDate" class="form-control datetimepicker" runat="server" Height="35px" placeholder="วันที่สิ้นสุด"></asp:TextBox>
                                                <span class="input-group-addon">
                                                    <span class="glyphicon glyphicon-calendar"></span>
                                                </span>
                                            </div>
                                        </div>
                                        <div class="col-md-2"></div>
                                    </div>
                                    <%--six row--%>
                                    <div class="row">
                                        <div class="col-md-2">เวลาเริ่มต้น</div>
                                        <div class="col-md-3">
                                            <div class='input-group date' id='Div4'>
                                                <asp:TextBox ID="TextBox4" runat="server" Height="35px" placeholder="วันที่เริ่มต้น"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-2">เวลาสิ้นสุด</div>
                                        <div class="col-md-3">
                                            <div class='input-group date' id='Div5'>
                                                <asp:TextBox ID="TextBox5" runat="server" Height="35px" placeholder="วันที่สิ้นสุด"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-2"></div>
                                    </div>
                                    <%--seven row--%>
                                    <div class="row">
                                        <div class="col-md-2">ค่าใช้จ่าย</div>
                                        <div class="col-md-3">
                                            <div class="input-group">
                                                <asp:TextBox ID="txtAmt" runat="server" Height="35px" placeholder="ค่าใช้จ่าย" class="form-control" ></asp:TextBox>
                                                <span class="input-group-addon">บาท</span>
                                            </div>
                                        </div>
                                        <div class="col-md-2"></div>
                                        <div class="col-md-3"></div>
                                        <div class="col-md-2"></div>
                                    </div>
                                    <%--eight row--%>
                                    <div class="row">
                                        <div class="col-md-2">ชื่อผู้บันทึก  <asp:Label ID="lblUser" runat="server" ForeColor="Blue"></asp:Label></div>
                                        <div class="col-md-3"></div>
                                        <div class="col-md-2"></div>
                                        <div class="col-md-5"><asp:Button ID="btnSave" class="btn btn-primary" runat="server" Text="บันทึก" OnClick="btnSave_Click" /></div>                                        
                                    </div>
                                    <asp:Label ID="lblError" runat="server" ForeColor="Red"></asp:Label>
                                </div>
                            </div>
                        </div>
                        <!-- /.col-lg-6 (nested) -->
                    </div>
                </div>
                <!-- /.panel-body -->
            </div>
            <!-- /.panel -->
 
<!-- ModalPopupExtender -->
    <ajax:ModalPopupExtender ID="modalSearch" runat="server" PopupControlID="Panel1" TargetControlID="btnShow" BackgroundCssClass="modalBackground" OkControlID="btnMFinish" CancelControlID="btnMClose"></ajax:ModalPopupExtender>
    <asp:Panel ID="Panel1" runat="server" CssClass="modalPopup" align="center" style="display:none">
        <asp:UpdatePanel ID="udpOutterUpdatePanel" runat="server"> 
             <ContentTemplate> 
                <div class="panel panel-primary">
                        <div class="panel-heading">
                            ค้นหา Suppiler        
                        </div>
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-lg-12">
                                    <div class="form">
                                        <div class="form-group">
                                            <%--first row--%>
                                            <div class="row">
                                                <div class="col-md-2">ชื่อ</div>
                                                <div class="col-md-3">
                                                    <asp:TextBox ID="txtModalName" runat="server" Height="35px" placeholder="ชื่อ"></asp:TextBox>
                                                </div>
                                                <div class="col-md-2">รหัส</div>
                                                <div class="col-md-3">
                                                    <asp:TextBox ID="txtModalCode" runat="server" Height="35px" placeholder="รหัส"></asp:TextBox>
                                                </div>
                                                <div class="col-md-2"><asp:Button ID="btnModalSearch" class="btn btn-primary" runat="server" Text="ค้นหา" OnClick="btnModalSearch_Click" /></div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <!-- /.col-lg-6 (nested) -->
                            </div>      
                        </div>
                        <asp:GridView ID="gridSupplier" runat="server" ForeColor="#507CD1" AutoGenerateColumns="False" DataKeyNames="VENDOR_ID" PageSize="20" EmptyDataText="ไม่พบข้อมูลซัพพลายเออร์"
                            Style="text-align: center" CssClass="grid">
                            <AlternatingRowStyle BackColor="White" />
                            <Columns>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:RadioButton ID="RowSelector" runat="server" GroupName="SelectGroup" onclick="checkRadioBtn(this);" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="รหัสซัพพลายเออร์" ItemStyle-Width="30%" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblVENDOR_CODE" runat="server" Text='<%# Bind("VENDOR_CODE") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="ชื่อซัพพลายเออร์" ItemStyle-Width="50%" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblVENDOR_NAME" runat="server" Text='<%# Bind("VENDOR_NAME") %>'></asp:Label>
                                    </ItemTemplate>
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
                        <div style="padding:10px;">
                            <asp:Button ID="btnModalClose" class="btn btn-primary" runat="server" Text="ยกเลิก" OnClick="btnModalClose_Click" />
                            <asp:Button ID="btnModalSave" class="btn btn-primary" runat="server" Text="ยืนยัน" OnClick="btnModalSave_Click" />
                        </div>
                        <!-- /.panel-body -->
                    </div>
             </ContentTemplate>      
            <Triggers> 
                <asp:AsyncPostBackTrigger ControlID="btnModalSearch" EventName="Click" /> 
            </Triggers> 
        </asp:UpdatePanel> 
        <asp:Button ID="btnMClose" runat="server" Style="visibility: hidden" />
        <asp:Button ID="btnMFinish" runat="server" Style="visibility: hidden" />
            <!-- /.panel -->
    </asp:Panel>
<!-- ModalPopupExtender -->
        </ContentTemplate>    
        <Triggers> 
            <asp:AsyncPostBackTrigger ControlID="btnSave" EventName="Click" /> 
            <asp:AsyncPostBackTrigger ControlID="btnModalSave" EventName="Click" /> 
        </Triggers> 
    </asp:UpdatePanel>
</asp:Content>
