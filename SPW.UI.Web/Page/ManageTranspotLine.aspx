<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPageMainAdmin.Master" AutoEventWireup="true"
    CodeBehind="ManageTranspotLine.aspx.cs" Inherits="SPW.UI.Web.Page.ManageTranspotLine" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1 class="page-header">
        <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Page/SearchTranspotLine.aspx">สายจัดรถ</asp:HyperLink>
        -
        <asp:Label ID="lblName" runat="server" Text="จัดการสายจัดรถ"></asp:Label></h1>
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
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="panel panel-primary">
                <div class="panel-heading">
                    จัดการใบสั่งซื้อ        
                </div>
                <div class="panel-body">
                    <div class="row">
                        <div class="col-lg-12">
                            <div class="form">
                                <div class="form-group">
                                    <div class="row">
                                        <div class="col-md-2">สายจัดรถ</div>
                                        <div class="col-md-3">
                                            <asp:TextBox ID="txtTrans" class="form-control" runat="server" Height="35px" Width="228px" Enabled="false" BackColor="White"></asp:TextBox>
                                        </div>
                                        <div class="col-md-2"></div>
                                        <div class="col-md-3">
                                        </div>
                                        <div class="col-md-2">
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-2">รหัสร้าน</div>
                                        <div class="col-md-3">
                                            <asp:TextBox ID="txtStoreCode" class="form-control" runat="server" Height="35px" placeholder="รหัสร้าน" data-provide="typeahead" data-items="5" autocomplete="off" OnTextChanged="txtStoreCode_TextChanged"></asp:TextBox>
                                        </div>
                                        <div class="col-md-2">ชื่อร้าน</div>
                                        <div class="col-md-3">
                                            <asp:TextBox ID="txtStoreName" class="form-control" runat="server" Height="35px" placeholder="ชื่อร้าน" data-provide="typeahead" data-items="5" autocomplete="off"></asp:TextBox>
                                        </div>
                                        <div class="col-md-2">
                                            <asp:Button ID="btnAdd" class="btn btn-primary" runat="server" Text="เพิ่ม" Height="30px" Width="70px" OnClick="btnAdd_Click" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <!-- /.col-lg-6 (nested) -->
                    </div>
                    <!-- /.row (nested) -->
                    <div class="panel panel-primary">
                        <asp:GridView ID="grdTrans" runat="server" ForeColor="#507CD1" AutoGenerateColumns="False"
                            DataKeyNames="TRANS_LINE_ID,STORE_ID" PageSize="20" Width="100%" EmptyDataText="ไม่พบข้อมูลร้านในสายจัดรถ"
                            Style="text-align: center" CssClass="grid" OnRowDeleting="grdTrans_RowDeleting">
                            <AlternatingRowStyle BackColor="White" />
                            <Columns>
                                <asp:TemplateField HeaderText="ลำดับ" ItemStyle-Width="10%">
                                    <ItemTemplate>
                                        <%# Container.DataItemIndex + 1 %>
                                    </ItemTemplate>
                                    <ItemStyle BorderStyle="Solid" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="STORE.STORE_CODE" HeaderText="รหัสร้าน" ItemStyle-Width="30%">
                                    <ItemStyle Width="30%"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="STORE.STORE_NAME" HeaderText="ชื่อร้าน" ItemStyle-Width="50%">
                                    <ItemStyle Width="50%"></ItemStyle>
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="ลบ" ItemStyle-Width="10%">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lbtnDelete" runat="server" CommandName="Delete">
                                            <div class='glyphicon glyphicon-remove'></div>
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
                </div>
                <!-- /.panel-body -->
            </div>
            <!-- /.panel -->
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
