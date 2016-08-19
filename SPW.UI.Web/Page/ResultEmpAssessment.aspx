<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPageMainAdmin.Master"
    AutoEventWireup="true" CodeBehind="ResultEmpAssessment.aspx.cs" Inherits="SPW.UI.Web.Page.ResultEmpAssessment" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1 class="page-header">
        <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Page/SearchEmpResultAssessment.aspx">ผลการประเมิน</asp:HyperLink>
        -
        <asp:Label ID="lblName" runat="server" Text=""></asp:Label></h1>
    <div class="alert alert-info" id="alert" runat="server" visible="false">
        <strong>บันทึกข้อมูลสำเร็จ Save Success</strong>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <style type="text/css">
        .right {
            text-align: right;
        }

        .grid td, .grid th {
            text-align: center;
        }
    </style>
    <div class="panel panel-primary">
        <div class="panel-heading">
            ผลการประเมินพนักงาน
        </div>
        <div class="panel-body">
            <div class="row">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div class="col-lg-12">
                            <div class="form">
                                <div class="form-group">
                                    <div class="row">
                                        <div class="col-md-2">รหัสพนักงาน</div>
                                        <div class="col-md-3">
                                            <asp:TextBox ID="txtEmpCode" class="form-control" runat="server" Height="35px" Width="200px" placeholder="รหัสพนักงาน" Enabled="false"></asp:TextBox>
                                        </div>
                                        <div class="col-md-2"></div>
                                        <div class="col-md-3">
                                        </div>
                                        <div class="col-md-2"></div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-2">ชื่อพนักงาน</div>
                                        <div class="col-md-3">
                                            <asp:TextBox ID="txtName" class="form-control" runat="server" Height="35px"  Width="200px" placeholder="ชื่อพนักงาน" Enabled="false"></asp:TextBox>
                                        </div>
                                        <div class="col-md-2"></div>
                                        <div class="col-md-3">
                                        </div>
                                        <div class="col-md-2"></div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-2">ตำแหน่ง</div>
                                        <div class="col-md-3">
                                            <asp:TextBox ID="txtPosition" class="form-control" runat="server" Height="35px" Width="200px" placeholder="รหัสพนักงาน" Enabled="false"></asp:TextBox>
                                        </div>
                                        <div class="col-md-2"></div>
                                        <div class="col-md-3">
                                        </div>
                                        <div class="col-md-2"></div>
                                    </div>
                                </div>
                            </div>
                            <div class="form">
                                <div class="form-group">
                                    <div class="row">
                                        <div class="col-md-2">ผลการประเมิน</div>
                                        <div class="col-md-3">
                                        </div>
                                        <div class="col-md-2"></div>
                                        <div class="col-md-3">
                                        </div>
                                        <div class="col-md-2"></div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-lg-12">
                                <asp:GridView ID="grdResult" runat="server" ForeColor="#507CD1" AutoGenerateColumns="False"
                                    DataKeyNames="" PageSize="20" Width="100%" 
                                    Style="text-align: center" CssClass="grid" EmptyDataText="ไม่มีผลการประเมิน">
                                    <AlternatingRowStyle BackColor="White" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="ลำดับ" ItemStyle-Width="5%">
                                            <ItemTemplate>
                                                <%# Container.DataItemIndex + 1 %>
                                            </ItemTemplate>
                                            <ItemStyle BorderStyle="Solid" />
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="MEASURE_YY" HeaderText="ปี" ItemStyle-Width="25%">
                                            <ItemStyle Width="25%"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="YEAR_NAME" HeaderText="ช่วง" ItemStyle-Width="30%">
                                            <ItemStyle Width="30%"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="GRADE" HeaderText="เกรด" ItemStyle-Width="20%">
                                            <ItemStyle Width="20%"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="POINT" HeaderText="คะแนน" ItemStyle-Width="20%">
                                            <ItemStyle Width="20%"></ItemStyle>
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
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <div style="float: right; margin-top: 5px;">
                <table>
                    <tr>
                        <td>
                        </td>
                        <td>&nbsp;&nbsp;</td>
                        <td>
                            <asp:Button ID="btnCancel" runat="server" class="btn btn-primary" Text="กลับ" PostBackUrl="~/Page/SearchEmpResultAssessment.aspx" />
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
</asp:Content>
