<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterPageMainAdmin.Master" AutoEventWireup="true"
    CodeBehind="SearchEmpHdTemplate.aspx.cs" Inherits="SPW.UI.Web.Page.SearchEmpHdTemplate" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1 class="page-header">เกณฑ์การประเมินทักษะในแผนก</h1>
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
                    ข้อมูลเกณฑ์การประเมินทักษะในแผนก       
                </div>
                <div class="panel-body">
                    <div class="row">
                        <div class="col-lg-12">
                            <div class="form">
                                <div class="form-group">
                                    <%--first row--%>
                                    <div class="row">
                                        <div class="col-md-2">ชื่อแผนก</div>
                                        <div class="col-md-3">
                                            <asp:TextBox ID="txtDepartment" class="form-control" runat="server" Height="35px" placeholder="ชื่อแผนก" data-provide="typeahead" data-items="5" autocomplete="off"></asp:TextBox>
                                        </div>
                                        <div class="col-md-2">
                                            <asp:Button ID="btnSearch" class="btn btn-primary" runat="server" Text="ค้นหา" OnClick="btnSearch_Click" Height="30px" Width="70px" />
                                        </div>
                                        <div class="col-md-3">
                                        </div>
                                        <div class="col-md-2">
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-2">ชื่อประเภททักษะ</div>
                                        <div class="col-md-3">
                                            <asp:TextBox ID="txtSkill" class="form-control" runat="server" Height="35px" placeholder="ชื่อประเภททักษะ" data-provide="typeahead" data-items="5" autocomplete="off"></asp:TextBox>
                                        </div>
                                        <div class="col-md-2">
                                            <asp:Button ID="btnAdd" class="btn btn-primary" runat="server" Text="เพิ่ม" Height="30px" Width="70px" PostBackUrl="~/Page/ManageHDTemplate.aspx" />
                                        </div>
                                        <div class="col-md-3">
                                        </div>
                                        <div class="col-md-2">
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <!-- /.col-lg-6 (nested) -->
                    </div>
                    <!-- /.row (nested) -->
                    <div class="panel panel-primary">
                        <asp:GridView ID="grdEmpPos" runat="server" ForeColor="#507CD1" AutoGenerateColumns="False" OnRowDataBound="grdEmpPos_RowDataBound"
                            DataKeyNames="TEMPLATE_ID,EMP_SKILL_TYPE_ID" PageSize="20" Width="100%" EmptyDataText="ไม่พบข้อมูลเกณฑ์การประเมินทักษะในแผนก"
                            Style="text-align: center" CssClass="grid">
                            <AlternatingRowStyle BackColor="White" />
                            <Columns>
                                <asp:TemplateField HeaderText="ลำดับ" ItemStyle-Width="5%">
                                    <ItemTemplate>
                                        <%# Container.DataItemIndex + 1 %>
                                    </ItemTemplate>
                                    <ItemStyle BorderStyle="Solid" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="TEMPLATE_ID" HeaderText="รหัส Template" ItemStyle-Width="25%">
                                    <ItemStyle Width="25%"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="DEPARTMENT.DEPARTMENT_NAME" HeaderText="ชื่อแผนก" ItemStyle-Width="25%">
                                    <ItemStyle Width="25%"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="EMP_SKILL_TYPE.EMP_SKILL_TYPE_NA" HeaderText="ชื่อประเภททักษะ" ItemStyle-Width="35%">
                                    <ItemStyle Width="35%"></ItemStyle>
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="รายละเอียด" ItemStyle-Width="10%">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lbtnDetail" runat="server">
                                    <div class='glyphicon glyphicon-list'></div>
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
