﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPageMainAdmin.Master" AutoEventWireup="true" CodeBehind="StockRawLot.aspx.cs" Inherits="SPW.UI.Web.Page.StockRawLot" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1 class="page-header">
        <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Page/StockRaw.aspx">ปรับยอดสินค้าคงเหลือในคลังวัตถุดิบ</asp:HyperLink>
        /
        <asp:Label ID="lblName" runat="server" Text="รายละเอียด"></asp:Label>
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
                รายละเอียด
            </div>
            <div class="panel-body">
                    
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                <ContentTemplate>                    
                    <div class="panel panel-primary">
                        <asp:GridView ID="gdvRawSetting" runat="server" ForeColor="#507CD1" AutoGenerateColumns="False" DataKeyNames="RAW_ID" PageSize="20" Width="100%" EmptyDataText="ไม่พบข้อมูล" 
                            Style="text-align: center" CssClass="grid">
                            <AlternatingRowStyle BackColor="White" />
                            <Columns>
                                <asp:TemplateField HeaderText="No" ItemStyle-Width="5%" ItemStyle-Height="30px" HeaderStyle-Height="30px">
                                    <ItemTemplate>
                                        <%# Container.DataItemIndex + 1 %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="รหัสสินค้า" ItemStyle-Width="5%">
                                    <ItemTemplate>
                                        <asp:Label ID="lblRawID" runat="server" Text='<%# ((DATAGRID) Container.DataItem).RAW_PRODUCT.RAW_ID %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="ชื่อสินค้า" ItemStyle-Width="20%">
                                    <ItemTemplate>
                                        <%# ((DATAGRID) Container.DataItem).RAW_PRODUCT.RAW_NAME1 %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="รหัสเวนเดอร์" ItemStyle-Width="5%">
                                    <ItemTemplate>
                                        <asp:Label ID="lblVendorID" runat="server" Text='<%# ((DATAGRID) Container.DataItem).VENDOR_ID %>'></asp:Label>                                        
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="ชื่อเวนเดอร์" ItemStyle-Width="20%">
                                    <ItemTemplate>
                                        <%# ((DATAGRID) Container.DataItem).VENDOR_NAME %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="เลขล็อท" ItemStyle-Width="10%">
                                    <ItemTemplate>
                                        <asp:Label ID="lblLotNo" runat="server" Text='<%# ((DATAGRID) Container.DataItem).LOT_NO %>'></asp:Label>                                        
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="จำนวนคงเหลือ" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtRawRemain" runat="server" class="form-control" Style="height: 35px;" Text='<%# ((DATAGRID) Container.DataItem).RAW_REMAIN %>'></asp:TextBox>
                                        <asp:HiddenField ID="hfOldRawRemain" runat="server" Value='<%# ((DATAGRID) Container.DataItem).RAW_REMAIN %>' />
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
                    <div class="row">
                        <div class="col-lg-12">
                            <div class="form">
                                <div class="form-group">
                                    <%--first row--%>
                                    <div class="row">
                                        <div class="col-md-2">ชื่อผู้จำหน่าย</div>
                                        <div class="col-md-3">
                                            <asp:TextBox ID="txtVendorName" class="form-control" runat="server" Height="35px" placeholder="ชื่อผู้จำหน่าย" data-provide="typeahead" data-items="5"
                                                AutoPostBack="true" OnTextChanged="txtVendorName_TextChanged"></asp:TextBox>
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
                                    <%--secound row--%>
                                    <div class="row">
                                        <div class="col-md-2">จำนวน</div>
                                        <div class="col-md-3">
                                            <asp:TextBox ID="txtStockQty" class="form-control" runat="server" Height="35px" placeholder="จำนวน" data-provide="typeahead" data-items="5"></asp:TextBox>
                                        </div>
                                        <div class="col-md-2"></div>
                                        <div class="col-md-3"></div>
                                        <div class="col-md-2"></div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <!-- /.col-lg-12 (nested) -->
                    </div>
                    <!-- /.row (nested) -->
                </ContentTemplate>
            </asp:UpdatePanel>
            <div class="row">
                <div class="col-lg-12">
                    <div class="form">
                        <div class="form-group">
                            <%--first row--%>
                            <div class="row">
                                <div class="col-md-8">
                                    <asp:Label ID="lblerror2" runat="server" forecolor="Red"/>
                                </div>
                                <div class="col-md-4">
                                    <asp:Button ID="btnSave" class="btn btn-primary" runat="server" Text="บันทึก" Height="30px" Width="70px" OnClick="btnSave_Click"/> 
                                    <asp:Button ID="btnUpdate" class="btn btn-primary" runat="server" Text="อัพเดท" Height="30px" Width="70px" OnClick="btnUpdate_Click"/> 
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

