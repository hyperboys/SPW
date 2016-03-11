<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPageMainAdmin.Master" AutoEventWireup="true"
    CodeBehind="SearchTranspotLine.aspx.cs" Inherits="SPW.UI.Web.Page.SearchTranspotLine" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1 class="page-header">สายจัดรถ</h1>
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
                    ข้อมูลสายจัดรถ        
                </div>
                <div class="panel-body">
                    <div class="row">
                        <div class="col-lg-12">
                            <div class="form">
                                <div class="form-group">
                                    <div class="row">
                                        <div class="col-md-2">รหัสร้าน</div>
                                        <div class="col-md-3">
                                            <asp:TextBox ID="txtTrans" class="form-control" runat="server" Height="35px" placeholder="รหัสร้าน"></asp:TextBox>
                                        </div>
                                        <div class="col-md-2">สถานะ</div>
                                        <div class="col-md-3">
                                            <asp:DropDownList ID="ddlStatus" class="form-control" runat="server" Height="35px" Width="200px" SelectedValue='<%# Eval("Key") %>'>
                                                <asp:ListItem Value="0" Selected="True">ใช้งาน</asp:ListItem>
                                                <asp:ListItem Value="1">ไม่ใช้งาน</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-md-2">
                                            <asp:Button ID="btnSearch" class="btn btn-primary" runat="server" Text="ค้นหา" OnClick="btnSearch_Click" Height="30px" Width="70px" />
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
                            DataKeyNames="TRANS_LINE_ID" PageSize="20" Width="100%" EmptyDataText="ไม่พบข้อมูลสายจัดรถ"
                            Style="text-align: center" CssClass="grid" OnRowDataBound="grdTrans_RowDataBound">
                            <AlternatingRowStyle BackColor="White" />
                            <Columns>
                                 <asp:TemplateField HeaderText="แก้ไข" ItemStyle-Width="10%">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lbtnEdit" runat="server" PostBackUrl="~/Page/ManageOrderHQDetail.aspx">
                                            <div class='glyphicon glyphicon-edit'></div>
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="TRANS_LINE_ID" HeaderText="ลำดับ" ItemStyle-Width="10%" DataFormatString="{0:dd/MM/yyyy}" ItemStyle-Height="30px">
                                    <ItemStyle Width="10%"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="TRANS_LINE_NAME" HeaderText="สายจัดรถ" ItemStyle-Width="90%">
                                    <ItemStyle Width="80%"></ItemStyle>
                                </asp:BoundField>
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
