<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPageMainAdmin.Master"
    AutoEventWireup="true" CodeBehind="KeyInEmpAssessment.aspx.cs" Inherits="SPW.UI.Web.Page.KeyInEmpAssessment" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1 class="page-header">
        <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Page/SearchEmployeeAssessment.aspx">การประเมินพนักงาน</asp:HyperLink>
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
            ประเมินพนักงาน
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
                                            <asp:TextBox ID="txtEmpCode" class="form-control" runat="server" Height="35px" placeholder="รหัสพนักงาน" Enabled="false"></asp:TextBox>
                                        </div>
                                        <div class="col-md-2"></div>
                                        <div class="col-md-3">
                                        </div>
                                        <div class="col-md-2"></div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-2">ชื่อพนักงาน</div>
                                        <div class="col-md-3">
                                            <asp:TextBox ID="txtName" class="form-control" runat="server" Height="35px" placeholder="ชื่อพนักงาน" Enabled="false"></asp:TextBox>
                                        </div>
                                        <div class="col-md-2"></div>
                                        <div class="col-md-3">
                                        </div>
                                        <div class="col-md-2"></div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-2">ตำแหน่ง</div>
                                        <div class="col-md-3">
                                            <asp:TextBox ID="txtPosition" class="form-control" runat="server" Height="35px" placeholder="รหัสพนักงาน" Enabled="false"></asp:TextBox>
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
                                        <div class="col-md-2">Core Competency</div>
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
                                <asp:GridView ID="grdCore" runat="server" ForeColor="#507CD1" AutoGenerateColumns="False"
                                    DataKeyNames="TEMPLATE_ID,EMP_SKILL_TYPE_ID,SEQ_NO" PageSize="20" Width="100%"
                                    Style="text-align: center" CssClass="grid" EmptyDataText="ไม่มีข้อมูล Template">
                                    <AlternatingRowStyle BackColor="White" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="ลำดับ" ItemStyle-Width="5%">
                                            <ItemTemplate>
                                                <%# Container.DataItemIndex + 1 %>
                                            </ItemTemplate>
                                            <ItemStyle BorderStyle="Solid" />
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="SKILL_NAME" HeaderText="ชื่อทักษะ" ItemStyle-Width="30%">
                                            <ItemStyle Width="30%"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="SKILL_TYPE_NAME" HeaderText="ประเภททักษะ" ItemStyle-Width="30%">
                                            <ItemStyle Width="30%"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="SKILL_TARGET_SCORE" HeaderText="คะแนน" ItemStyle-Width="20%">
                                            <ItemStyle Width="20%"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="คะแนนที่ได้" ItemStyle-Width="15%">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtPoint" class="form-control" runat="server" Height="30px" placeholder="คะแนนที่ได้"></asp:TextBox>
                                            </ItemTemplate>
                                            <ItemStyle Width="15%"></ItemStyle>
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
                            <div class="form">
                                <div class="form-group">
                                    <div class="row">
                                        <div class="col-md-2">&nbsp;</div>
                                        <div class="col-md-3">&nbsp;</div>
                                        <div class="col-md-2">&nbsp;</div>
                                        <div class="col-md-3">&nbsp;</div>
                                        <div class="col-md-2">&nbsp;</div>
                                    </div>
                                </div>
                            </div>
                            <div class="form">
                                <div class="form-group">
                                    <div class="row">
                                        <div class="col-md-2">JOB BASE</div>
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
                                <asp:GridView ID="grdJob" runat="server" ForeColor="#507CD1" AutoGenerateColumns="False"
                                    DataKeyNames="TEMPLATE_ID,EMP_SKILL_TYPE_ID,SEQ_NO" PageSize="20" Width="100%"
                                    Style="text-align: center" CssClass="grid" EmptyDataText="ไม่มีข้อมูล Template">
                                    <AlternatingRowStyle BackColor="White" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="ลำดับ" ItemStyle-Width="5%">
                                            <ItemTemplate>
                                                <%# Container.DataItemIndex + 1 %>
                                            </ItemTemplate>
                                            <ItemStyle BorderStyle="Solid" />
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="SKILL_NAME" HeaderText="ชื่อทักษะ" ItemStyle-Width="30%">
                                            <ItemStyle Width="30%"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="SKILL_TYPE_NAME" HeaderText="ประเภททักษะ" ItemStyle-Width="30%">
                                            <ItemStyle Width="30%"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="SKILL_TARGET_SCORE" HeaderText="คะแนน" ItemStyle-Width="20%">
                                            <ItemStyle Width="20%"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="คะแนนที่ได้" ItemStyle-Width="15%">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtPoint" class="form-control" runat="server" Height="30px" placeholder="คะแนนที่ได้"></asp:TextBox>
                                            </ItemTemplate>
                                            <ItemStyle Width="15%"></ItemStyle>
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
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <div style="float: right; margin-top: 5px;">
                <table>
                    <tr>
                        <td>
                            <asp:Button ID="btnSave" runat="server" class="btn btn-primary" Text="บันทึก" OnClick="btnSave_Click" ValidationGroup="group" />

                        </td>
                        <td>&nbsp;&nbsp;</td>
                        <td>
                            <asp:Button ID="btnCancel" runat="server" class="btn btn-primary" Text="ยกเลิก" PostBackUrl="~/Page/SearchEmployeeAssessment.aspx" />
                        </td>
                    </tr>
                </table>
            </div>
            <div>
                <asp:ValidationSummary ID="ValidationSummary1" runat="server" Style="color: #FF0000; font-size: large;"
                    ValidationGroup="group" ShowMessageBox="True" ShowSummary="False" />
            </div>
        </div>
    </div>
    <ajax:ConfirmButtonExtender ID="btnSave_ConfirmButtonExtender" runat="server"
        ConfirmText="ต้องการจะบันทึกหรือไม่" Enabled="True" TargetControlID="btnSave">
    </ajax:ConfirmButtonExtender>
</asp:Content>
