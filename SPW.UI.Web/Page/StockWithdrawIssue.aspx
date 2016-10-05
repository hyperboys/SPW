<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterPageMainAdmin.Master" enableEventValidation="false" AutoEventWireup="true" CodeBehind="StockWithdrawIssue.aspx.cs" Inherits="SPW.UI.Web.Page.StockWithdrawIssue" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1 class="page-header">
        <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Page/SearchStockWithdrawExpose.aspx">ค้นหาใบเบิกสินค้า</asp:HyperLink>
        /
        <asp:Label ID="lblName" runat="server" Text="สร้างใบเบิกสินค้า"></asp:Label></h1>
    <div class="alert alert-info" id="Div1" runat="server" visible="false">
        <strong>สั่งซื้อสินค้าสำเร็จ Order Success</strong>
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
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <div class="panel panel-primary">
            <div class="panel-heading">
                สร้างใบเบิกสินค้า     
            </div>
            <div class="panel-body">
                    
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <div class="row">
                        <div class="col-lg-12">
                            <div class="form">
                                <div class="form-group">
                                    <%--first row--%>
                                    <div class="row">
                                        <div class="col-md-2"></div>
                                        <div class="col-md-3"></div>
                                        <div class="col-md-3"></div>
                                        <div class="col-md-1">เลมที่</div>
                                        <div class="col-md-3"><asp:TextBox ID="txtBKNo" class="form-control" runat="server" Height="35px" placeholder="Booking No"></asp:TextBox></div>
                                    </div>
                                    <%--secound row--%>
                                    <div class="row">
                                        <div class="col-md-2"></div>
                                        <div class="col-md-3"></div>
                                        <div class="col-md-3"></div>
                                        <div class="col-md-1">เลขที่</div>
                                        <div class="col-md-3"><asp:TextBox ID="txtRNNo" class="form-control" runat="server" Height="35px" placeholder="Running No"></asp:TextBox></div>
                                    </div>
                                    <br/>
                                    <%--third row--%>
                                    <div class="row">
                                        <div class="col-md-2">ชื่อสินค้า</div>
                                        <div class="col-md-3">
                                                <asp:TextBox ID="txtRawName" class="form-control" runat="server" Height="35px" placeholder="ชื่อสินค้า" data-provide="typeahead" data-items="5" 
                                                    AutoPostBack="true" OnTextChanged="txtRawName_TextChanged"></asp:TextBox>
                                        </div>
                                        <div class="col-md-2">รหัสสินค้า</div>
                                        <div class="col-md-3">
                                            <div class="form-group has-success has-feedback">
                                                <asp:TextBox ID="txtRawID" class="form-control" runat="server" Height="35px" BackColor="LightGray"></asp:TextBox>
                                                <span class="glyphicon glyphicon-ok form-control-feedback" id="spRawCode" runat="server" visible="false"></span>
                                            </div>
                                            <asp:HiddenField ID="isFoundRawCode" runat="server"/>
                                        </div>
                                        <div class="col-md-2"></div>
                                    </div>
                                    <%--forth row--%>
                                    <div class="row">
                                        <div class="col-md-2">จำนวนขอเบิก</div>
                                        <div class="col-md-3">
                                                <asp:TextBox ID="txtWrQty" class="form-control" runat="server" Height="35px"></asp:TextBox>
                                        </div>
                                        <div class="col-md-2">แพ็ค</div>
                                        <div class="col-md-3">                                            
                                            <asp:DropDownList ID="ddlPack" class="form-control" runat="server" Height="35px" Width="200px">
                                                <asp:ListItem Value="0">กรุณาเลือก</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-md-2"></div>
                                    </div>
                                    <%--forth row--%>
                                    <div class="row">
                                        <div class="col-md-2">ผู้ขอเบิก</div>
                                        <div class="col-md-3">                                            
                                            <asp:DropDownList ID="ddlUserRequest" class="form-control" runat="server" Height="35px" Width="200px">
                                                <asp:ListItem Value="0">กรุณาเลือก</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-md-2">ผู้อนุมัติ</div>
                                        <div class="col-md-3">                                            
                                            <asp:DropDownList ID="ddlUserApprove" class="form-control" runat="server" Height="35px" Width="200px">
                                                <asp:ListItem Value="0">กรุณาเลือก</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-md-2"></div>
                                    </div>
                                    <%--fifth row--%>
                                    <div class="row">
                                        <div class="col-md-2">จำนวนให้เบิก</div>
                                        <div class="col-md-3">
                                                <asp:TextBox ID="txtGiveWrQty" class="form-control" runat="server" Height="35px"></asp:TextBox>
                                        </div>
                                        <div class="col-md-2"></div>
                                        <div class="col-md-3"></div>
                                        <div class="col-md-2"></div>
                                    </div>
                                    <%--sixth row--%>
                                    <div class="row">
                                        <div class="col-md-2"></div>
                                        <div class="col-md-3"></div>
                                        <div class="col-md-2"></div>
                                        <div class="col-md-3"></div>
                                        <div class="col-md-2">
                                            <asp:Button ID="btnAdd" class="btn btn-primary" runat="server" Text="เพิ่ม" Height="30px" Width="70px" OnClick="btnAdd_Click" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <!-- /.col-lg-12 (nested) -->
                    </div>
                    <!-- /.row (nested) -->                    
                    <div class="panel panel-primary">
                        <asp:GridView ID="gdvWR" runat="server" ForeColor="#507CD1" AutoGenerateColumns="False"
                            DataKeyNames="RAW_ID" PageSize="20" Width="100%" EmptyDataText="ไม่พบข้อมูล" OnRowDeleting="gdvWR_RowDeleting" OnRowEditing="gdvWR_RowEditing"
                            Style="text-align: center" CssClass="grid">
                            <AlternatingRowStyle BackColor="White" />
                            <Columns>
                                <asp:TemplateField HeaderText="No" ItemStyle-Width="5%" ItemStyle-Height="30px" HeaderStyle-Height="30px">
                                    <ItemTemplate>
                                        <%# Container.DataItemIndex + 1 %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="รหัสสินค้า" ItemStyle-Width="15%">
                                    <ItemTemplate>
                                        <%# ((DATAGRIDWR) Container.DataItem).RAW_PRODUCT.RAW_ID %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="ชื่อสินค้า(ปกติ)" ItemStyle-Width="15%">
                                    <ItemTemplate>
                                        <%# ((DATAGRIDWR) Container.DataItem).RAW_PRODUCT.RAW_NAME1 %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="ชื่อสินค้า(สำหรับโรงงาน)" ItemStyle-Width="15%">
                                    <ItemTemplate>
                                        <%# ((DATAGRIDWR) Container.DataItem).RAW_PRODUCT.RAW_NAME2 %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="จำนวน" ItemStyle-Width="10%">
                                    <ItemTemplate>
                                        <%# ((DATAGRIDWR) Container.DataItem).WR_QTY %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="รหัสแพ็ค" ItemStyle-Width="10%">
                                    <ItemTemplate>
                                        <%# ((DATAGRIDWR) Container.DataItem).RAW_PACK_ID %>
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
                    </div>
                    <!-- /.row (nested) -->                    
                    <div class="panel panel-primary">
                        <asp:GridView ID="gdvWRLot" runat="server" ForeColor="#507CD1" AutoGenerateColumns="False"
                            DataKeyNames="RAW_ID" PageSize="20" Width="100%" EmptyDataText="ไม่พบข้อมูลในคลัง" OnRowDeleting="gdvWRLot_RowDeleting" OnRowEditing="gdvWRLot_RowEditing"
                            Style="text-align: center" CssClass="grid">
                            <AlternatingRowStyle BackColor="White" />
                            <Columns>
                                <asp:TemplateField HeaderText="No" ItemStyle-Width="5%" ItemStyle-Height="30px" HeaderStyle-Height="30px">
                                    <ItemTemplate>
                                        <%# Container.DataItemIndex + 1 %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="รหัสสินค้า" ItemStyle-Width="15%">
                                    <ItemTemplate>
                                        <asp:Label ID="lblRawID" runat="server" Text='<%# ((DATAGRIDWRLOT) Container.DataItem).RAW_PRODUCT.RAW_ID %>'></asp:Label>

                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="ชื่อสินค้า(ปกติ)" ItemStyle-Width="15%">
                                    <ItemTemplate>
                                        <%# ((DATAGRIDWRLOT) Container.DataItem).RAW_PRODUCT.RAW_NAME1 %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="ชื่อสินค้า(สำหรับโรงงาน)" ItemStyle-Width="15%">
                                    <ItemTemplate>
                                        <%# ((DATAGRIDWRLOT) Container.DataItem).RAW_PRODUCT.RAW_NAME2 %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="คงเหลือ" ItemStyle-Width="10%">
                                    <ItemTemplate>
                                        <asp:Label ID="lblRawRemain" runat="server" Text='<%# ((DATAGRIDWRLOT) Container.DataItem).RAW_REMAIN %>'></asp:Label>
                                        <%--<%# ((DATAGRID) Container.DataItem).RAW_REMAIN %>--%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="จำนวนขอเบิก" ItemStyle-Width="10%">
                                    <ItemTemplate>
                                        <%# ((DATAGRIDWRLOT) Container.DataItem).WR_QTY %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="จำนวนเบิก" ItemStyle-Width="10%">
                                    <ItemTemplate>
                                        <asp:TextBox runat="server" ID="txtWRQty" class="form-control" style="text-align:center" Text="0" />      
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="รหัสแพ็ค" ItemStyle-Width="10%">
                                    <ItemTemplate>
                                        <%# ((DATAGRIDWRLOT) Container.DataItem).RAW_PACK_ID %>
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
                    </div>
                    <asp:PlaceHolder ID="PlaceHolder1" runat="server"></asp:PlaceHolder>
                </ContentTemplate>
            </asp:UpdatePanel>
            <div class="row">
                <div class="col-lg-12">
                    <div class="form">
                        <div class="form-group">
                            <%--first row--%>
                            <div class="row">
                                <div class="col-md-4">
                                    <asp:Label ID="lblerror2" runat="server" forecolor="Red"/>
                                </div>
                                <div class="col-md-8">
                                    <asp:Button ID="btnSave" class="btn btn-primary" runat="server" Text="บันทึก" Height="30px" Width="200px" OnClick="btnSave_Click"/> 
                                    <asp:Button ID="btnSaveNext" class="btn btn-primary" runat="server" Text="บันทึกและสร้างใหม่" Height="30px" Width="200px" OnClick="btnSaveNext_Click"/> 
                                    <asp:Label ID="flag" runat="server" Text="Add" Visible="false"></asp:Label>            
                                    <asp:Label ID="lblError" runat="server"></asp:Label>                                         
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            </div>
                <!-- /.panel-body -->
        </div>
            <!-- /.panel -->
</asp:Content>
