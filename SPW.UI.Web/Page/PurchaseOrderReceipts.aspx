<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPageMainAdmin.Master" AutoEventWireup="true" CodeBehind="PurchaseOrderReceipts.aspx.cs" Inherits="SPW.UI.Web.Page.PurchaseOrderReceipts" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1 class="page-header">
        <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Page/SearchPurchaseOrderReceipts.aspx">Search Purchase Order Receipts</asp:HyperLink>
        /
        <asp:Label ID="lblName" runat="server" Text="Purchase Order Receipts"></asp:Label>
    </h1>
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
                Purchase Order Receipts
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
                                        <div class="col-md-5">
                                            <div class="btn-group" role="group" aria-label="First group">
                                                <asp:Button ID="btnActive" class="btn" Text="Active" runat="server" type="button"/>
                                                <asp:Button ID="btnApprove" class="btn" Text="Approve" runat="server" type="button" OnClick="btnApprove_Click"/>
                                                <asp:Button ID="btnFinish" class="btn" Text="Finish" runat="server" type="button" OnClick="btnFinish_Click"/>
                                                <asp:Button ID="btnCancel" class="btn" Text="Cancel" runat="server" type="button"  OnClick="btnCancel_Click"/>
                                            </div>
                                        </div>
                                        <div class="col-md-1"></div>
                                        <div class="col-md-2"></div>
                                        <div class="col-md-1">เลมที่</div>
                                        <div class="col-md-3"><asp:TextBox ID="txtBKNo" class="form-control" runat="server" Height="35px" placeholder="BKYYXXXX"></asp:TextBox></div>
                                    </div>
                                    <%--secound row--%>
                                    <div class="row">
                                        <div class="col-md-2"></div>
                                        <div class="col-md-3"></div>
                                        <div class="col-md-3"></div>
                                        <div class="col-md-1">เลขที่</div>
                                        <div class="col-md-3"><asp:TextBox ID="txtRNNo" class="form-control" runat="server" Height="35px" placeholder="XXXX"></asp:TextBox></div>
                                    </div>
                                    <br/>
                                    <%--third row--%>
                                    <div class="row">
                                        <div class="col-md-2">ชื่อผู้จำหน่าย</div>
                                        <div class="col-md-3">
                                            <asp:TextBox ID="txtVendorName" class="form-control" runat="server" Height="35px" placeholder="ชื่อผู้จำหน่าย" data-provide="typeahead" data-items="5"></asp:TextBox>
                                        </div>
                                        <div class="col-md-2">รหัสผู้จำหน่าย</div>
                                        <div class="col-md-3">
                                            <div class="form-group has-success has-feedback">
                                                <asp:TextBox ID="txtVendorCode" class="form-control" runat="server" Height="35px" BackColor="LightGray"></asp:TextBox>
                                                <span class="glyphicon glyphicon-ok form-control-feedback" id="spVendorCode" runat="server" visible="false"></span>
                                            </div>
                                            <asp:HiddenField ID="isFoundVendorCode" runat="server"/>
                                        </div>
                                        <div class="col-md-2"></div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <!-- /.col-lg-12 (nested) -->
                    </div>
                    <!-- /.row (nested) -->                    
                    <div class="panel panel-primary">
                        <asp:GridView ID="gdvREC" runat="server" ForeColor="#507CD1" AutoGenerateColumns="False" DataKeyNames="RAW_ID" PageSize="20" Width="100%" EmptyDataText="ไม่พบข้อมูล" 
                            Style="text-align: center" CssClass="grid" OnRowEditing="gdvREC_EditCommand">
                            <AlternatingRowStyle BackColor="White" />
                            <Columns>
                                <asp:TemplateField HeaderText="No" ItemStyle-Width="5%" ItemStyle-Height="30px" HeaderStyle-Height="30px">
                                    <ItemTemplate>
                                        <%# Container.DataItemIndex + 1 %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="รหัสสินค้า" ItemStyle-Width="15%">
                                    <ItemTemplate>
                                        <asp:Label ID="lblRawID" runat="server" Text='<%# ((DATAGRID) Container.DataItem).RAW_PRODUCT.RAW_ID %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="ชื่อสินค้า(ปกติ)" ItemStyle-Width="15%">
                                    <ItemTemplate>
                                        <%# ((DATAGRID) Container.DataItem).RAW_PRODUCT.RAW_NAME1 %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="ชื่อสินค้า(สำหรับโรงงาน)" ItemStyle-Width="15%">
                                    <ItemTemplate>
                                        <%# ((DATAGRID) Container.DataItem).RAW_PRODUCT.RAW_NAME2 %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="จำนวนออเดอร์" ItemStyle-Width="10%">
                                    <ItemTemplate>
                                        <%# ((DATAGRID) Container.DataItem).PO_QTY %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="รวมจำนวนที่รับ" ItemStyle-Width="10%">
                                    <ItemTemplate>
                                        <%# ((DATAGRID) Container.DataItem).RECEIVE_QTY %>              
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="จำนวน" ItemStyle-Width="10%">
                                    <ItemTemplate>
                                        <asp:TextBox runat="server" ID="txtQtyReceive" class="form-control" style="text-align:center" Text="0" />                     
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="หน่วย" ItemStyle-Width="10%">
                                    <ItemTemplate>
                                        <%# ((DATAGRID) Container.DataItem).RAW_PACK_SIZE %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="ตรวจสอบ" ItemStyle-Width="5%">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lbtnEdit" runat="server" CommandName="Edit">
                                    <div class='glyphicon glyphicon-search'></div>
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
                                <div class="col-md-10">
                                    <asp:Label ID="lblerror2" runat="server" forecolor="Red"/>
                                </div>
                                <div class="col-md-2">
                                    <asp:Button ID="btnSave" class="btn btn-primary" runat="server" Text="บันทึก" Height="30px" Width="70px" OnClick="btnSave_Click"/> 
                                    <asp:Label ID="flag" runat="server" Text="Add" Visible="false"></asp:Label>                                         
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
