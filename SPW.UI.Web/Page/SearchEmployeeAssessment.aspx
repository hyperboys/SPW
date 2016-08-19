<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPageMainAdmin.Master" AutoEventWireup="true"
    CodeBehind="SearchEmployeeAssessment.aspx.cs" Inherits="SPW.UI.Web.Page.SearchEmployeeAssessment" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1 class="page-header">พนักงาน</h1>
    <div class="alert alert-warning" id="warning" runat="server" visible="false">
        <strong>
            <asp:Label ID="lblWarning" runat="server" Text=""></asp:Label></strong>
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
    </style>
    <div class="panel panel-primary">
        <div class="panel-heading">
            ข้อมูลพนักงาน             
        </div>
        <div class="panel-body">
            <div class="row">
                <div class="col-lg-12">
                    <div class="form">
                        <div class="form-group">
                            <%--first row--%>
                            <div class="row">
                                <div class="col-md-2">รหัสผนักงาน</div>
                                <div class="col-md-3">
                                    <asp:TextBox ID="txtEmployeeCode" class="form-control" runat="server" Height="35px" Width="200px" placeholder="รหัสผนักงาน"></asp:TextBox>
                                </div>
                                <div class="col-md-2">
                                    ชื่อพนักงาน
                                </div>
                                <div class="col-md-3">
                                    <asp:TextBox ID="txtName" class="form-control" runat="server" Height="35px" Width="200px" placeholder="ชื่อพนักงาน"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-2">&nbsp;</div>
                                <div class="col-md-3">&nbsp;</div>
                                <div class="col-md-2">&nbsp;</div>
                                <div class="col-md-3">&nbsp;</div>
                            </div>
                            <div class="row">
                                <div class="col-md-2">
                                    แผนก
                                </div>
                                <div class="col-md-3">
                                    <asp:DropDownList ID="ddlDepart" class="form-control" runat="server" Height="35px" Width="200px" SelectedValue='<%# Eval("Key") %>'>
                                        <asp:ListItem Text="กรุณาเลือก" Selected="True" Value="0"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-2">
                                </div>
                                <div class="col-md-3">
                                    <asp:Button ID="btnSearch" class="btn btn-primary" runat="server" Text="ค้นหา" OnClick="btnSearch_Click" Height="30px" Width="70px" />
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-12">
                        <asp:GridView ID="gridEmployee" runat="server" ForeColor="#507CD1" AutoGenerateColumns="False"
                            DataKeyNames="EMPLOYEE_ID" PageSize="20" Width="100%" EmptyDataText="ไม่พบข้อมูลพนักงาน"
                            OnPageIndexChanging="gridEmployee_PageIndexChanging"
                            Style="text-align: center" CssClass="grid" OnRowDataBound="gridEmployee_RowDataBound">
                            <AlternatingRowStyle BackColor="White" />
                            <Columns>
                                <asp:BoundField DataField="EMPLOYEE_CODE" HeaderText="รหัสพนักงาน" ItemStyle-Width="20%">
                                    <ItemStyle Width="20%"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="EMPLOYEE_NAME" HeaderText="ชื่อ" ItemStyle-Width="30%">
                                    <ItemStyle Width="30%"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="EMPLOYEE_SURNAME" HeaderText="นามสกุล" ItemStyle-Width="30%">
                                    <ItemStyle Width="30%"></ItemStyle>
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="แก้ไข" ItemStyle-Width="10%">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lbtnEdit" runat="server" CommandName="Edit">
                                            <div class='glyphicon glyphicon-edit'></div>
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="ประเมิน" ItemStyle-Width="10%">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lbtnSelect" runat="server" Visible="false" CommandName="Select">
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
                </div>
            </div>

        </div>
    </div>
</asp:Content>
