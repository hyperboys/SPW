<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPageMainAdmin.Master" AutoEventWireup="true" CodeBehind="StockRaw.aspx.cs" Inherits="SPW.UI.Web.Page.StockRaw" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
        <link rel="stylesheet" href="../CSS/bootstrap-3.3.2.min.css" type="text/css" />
        <link rel="stylesheet" href="../CSS/bootstrap-multiselect.css" type="text/css" />
        <script type="text/javascript" src="../JQuery/bootstrap-multiselect.js"></script>
        <script type="text/javascript" src="../JQuery/bootstrap-multiselect-collapsible-groups.js"></script>
    <script type="text/javascript">
        function check_change(e) {
            var temp = parseInt($("#" + e.id).closest('tr').find("[id*='hfSTOCK_REMAIN']").val(), 10);
            var add = parseInt($("#" + e.id).val(), 10);
            $("#" + e.id).closest('tr').find("[id*='txtSTOCK_REMAIN']").val(temp + add);
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1 class="page-header">ปรับยอดสินค้าคงเหลือในคลังวัตถุดิบ</h1>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <style type="text/css">
        .right
        {
            text-align: right;
        }

        .auto-style5
        {
            width: 13px;
        }

        .auto-style10
        {
            width: 67px;
        }
        .row
        {
            margin-top:5px;
        }
    </style>
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    
    <asp:UpdateProgress ID="updProgress" AssociatedUpdatePanelID="UpdatePanel1" runat="server">
        <ProgressTemplate>
            Loading...
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
    <div class="panel panel-primary">
        <div class="panel-heading">
            ปรับยอดสินค้าคงเหลือในคลังวัตถุดิบ
        </div>
        <div class="panel-body">
            <div class="row">
                <div class="col-lg-12">
                    <div class="form">
                        <div class="form-group">
                            <div class="row">
                              <div class="col-md-2">รายการ</div>
                              <div class="col-md-3">
                                        <asp:DropDownList ID="ddlCategory" class="form-control" runat="server" Height="35px" Width="200px">
                                            <asp:ListItem Value="0">กรุณาเลือก</asp:ListItem>
                                        </asp:DropDownList>
                              </div>
                              <div class="col-md-2"></div>
                              <div class="col-md-3">
                              </div>
                              <div class="col-md-2">ผู้ทำรายการ : <asp:Label ID="lblUser" runat="server"></asp:Label></div>
                            </div>
                            <div class="row">
                              <div class="col-md-2">รหัสสินค้า</div>
                              <div class="col-md-3">
                                    <asp:TextBox ID="txtRawID" class="form-control" runat="server" Height="35px" placeholder="รหัสสินค้า"></asp:TextBox>
                              </div>
                              <div class="col-md-2">ชื่อสินค้า</div>
                              <div class="col-md-3">
                                    <asp:TextBox ID="txtRawName" class="form-control" runat="server" Height="35px" placeholder="ชื่อสินค้า"></asp:TextBox>
                              </div>
                              <div class="col-md-2">
                                        <asp:Button ID="btnSearch" class="btn btn-primary" runat="server" Text="ค้นหา" OnClick="btnSearch_Click" Height="30px" Width="70px" />
                                        <%--<asp:Button ID="btnSave" class="btn btn-primary" runat="server" Height="30px" Width="70px" Text="บันทึก" OnClick="btnSave_Click" />--%>
                              </div>
                            </div>
                        </div>
                    </div>
                </div>
                <!-- /.col-lg-6 (nested) -->
            </div>
            <!-- /.row (nested) -->
            <div class="panel panel-primary">
                <asp:Panel ID="PanelSet" runat="server">
                    <asp:GridView ID="gridStockRawSet" runat="server" ForeColor="#507CD1" AutoGenerateColumns="False" DataKeyNames="RAW_ID" PageSize="20" Width="100%" 
                        EmptyDataText="ไม่พบข้อมูลสินค้า" OnRowEditing="gridStockRawSet_EditCommand" Style="text-align: center" CssClass="grid">
                        <AlternatingRowStyle BackColor="White" />
                        <Columns>
                            <asp:TemplateField HeaderText="รหัสสินค้า" ItemStyle-Width="20%">
                                <ItemTemplate>
                                    <asp:Label ID="lblRAW_ID" runat="server" Text='<%# Bind("RAW_ID") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Width="20%"></ItemStyle>
                            </asp:TemplateField>
                            <asp:BoundField DataField="RAW_NAME1" HeaderText="ชื่อสินค้า1" ItemStyle-Width="20%">
                                <ItemStyle Width="20%"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="RAW_NAME2" HeaderText="ชื่อสินค้า2" ItemStyle-Width="20%">
                                <ItemStyle Width="20%"></ItemStyle>
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="ยอดคงเหลือ" ItemStyle-Width="25%">
                                <ItemTemplate>
                                    <asp:Label ID="lblRAW_REMAIN" runat="server" Text='<%# Bind("RAW_REMAIN") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Width="10%"></ItemStyle>
                            </asp:TemplateField>
                            <%--<asp:TemplateField HeaderText="ยอดคงเหลือ" ItemStyle-Width="25%" Visible="false">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtRAW_REMAIN" class="form-control" runat="server" Text='<%# Bind("RAW_REMAIN") %>' Visible="false"></asp:TextBox>
                                    <asp:HiddenField ID="hfRAW_REMAIN" runat="server" Value='<%# Bind("RAW_REMAIN") %>' />
                                </ItemTemplate>
                                <ItemStyle Width="25%"></ItemStyle>
                            </asp:TemplateField>--%>
                            <asp:TemplateField HeaderText="Safety Stock" ItemStyle-Width="10%">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtRAW_MINIMUM" class="form-control" runat="server" Text='<%# Bind("RAW_MINIMUM") %>'></asp:TextBox>
                                    <asp:HiddenField ID="hfRAW_MINIMUM" runat="server" Value='<%# Bind("RAW_MINIMUM") %>' />
                                </ItemTemplate>
                                <ItemStyle Width="10%"></ItemStyle>
                            </asp:TemplateField>
                                <asp:TemplateField HeaderText="ตรวจสอบ" ItemStyle-Width="5%">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lbtnEdit" runat="server" CommandName="Edit">
                                    <div class='glyphicon glyphicon glyphicon glyphicon-list'></div>
                                        </asp:LinkButton>
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
                </asp:Panel>
                <asp:Panel ID="PanelAdd" runat="server">
                    <asp:GridView ID="gridStockRawAdd" runat="server" ForeColor="#507CD1" AutoGenerateColumns="False"
                        DataKeyNames="RAW_ID" PageSize="20" Width="100%" EmptyDataText="ไม่พบข้อมูลสินค้า"
                        Style="text-align: center" CssClass="grid">
                        <AlternatingRowStyle BackColor="White" />
                        <Columns>
                            <asp:TemplateField HeaderText="รหัสสินค้า" ItemStyle-Width="20%">
                                <ItemTemplate>
                                    <asp:Label ID="lblRAW_ID" runat="server" Text='<%# Bind("RAW_ID") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Width="20%"></ItemStyle>
                            </asp:TemplateField>
                            <asp:BoundField DataField="RAW_NAME1" HeaderText="ชื่อสินค้า1" ItemStyle-Width="20%">
                                <ItemStyle Width="20%"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="RAW_NAME2" HeaderText="ชื่อสินค้า2" ItemStyle-Width="20%">
                                <ItemStyle Width="20%"></ItemStyle>
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="ยอดสุทธิ(เดิม)" ItemStyle-Width="15%">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtOLD_STOCK_REMAIN" class="form-control" runat="server" Text='<%# Bind("RAW_REMAIN") %>' disabled="disabled"></asp:TextBox>
                                </ItemTemplate>
                                <ItemStyle Width="15%"></ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="เพิ่ม/ลด" ItemStyle-Width="15%">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtAddSTOCK_REMAIN" class="form-control" runat="server" Text="0" OnChange="check_change(this)"></asp:TextBox>
                                </ItemTemplate>
                                <ItemStyle Width="15%"></ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="ยอดสุทธิ(ใหม่)" ItemStyle-Width="15%">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtSTOCK_REMAIN" class="form-control" runat="server" Text='<%# Bind("RAW_REMAIN") %>' disabled="disabled"></asp:TextBox>
                                    <asp:HiddenField ID="hfSTOCK_REMAIN" runat="server" Value='<%# Bind("RAW_REMAIN") %>' />
                                </ItemTemplate>
                                <ItemStyle Width="15%"></ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Safety Stock" ItemStyle-Width="10%" Visible="false">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtSTOCK_MINIMUM" class="form-control" runat="server" Text='<%# Bind("RAW_MINIMUM") %>'></asp:TextBox>
                                    <asp:HiddenField ID="hfSTOCK_MINIMUM" runat="server" Value='<%# Bind("RAW_MINIMUM") %>' />
                                </ItemTemplate>
                                <ItemStyle Width="10%"></ItemStyle>
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

                </asp:Panel>
            </div>
        </div>
        <!-- /.panel-body -->
    </div>
    <!-- /.panel -->
            </ContentTemplate>
     </asp:UpdatePanel>
</asp:Content>