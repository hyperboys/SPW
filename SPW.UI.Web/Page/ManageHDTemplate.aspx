<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPageMainAdmin.Master"
    AutoEventWireup="true" CodeBehind="ManageHDTemplate.aspx.cs" Inherits="SPW.UI.Web.Page.ManageHDTemplate" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1 class="page-header">
        <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Page/SearchEmpHdTemplate.aspx">ประเภททักษะพนักงาน</asp:HyperLink>
        -
        <asp:Label ID="lblName" runat="server" Text="เพิ่มประเภททักษะพนักงานใหม่"></asp:Label></h1>
    <div class="alert alert-info" id="alert" runat="server" visible="false">
        <strong>บันทึกข้อมูลสำเร็จ Save Success</strong>
    </div>
    <div class="alert alert-danger" id="danger" runat="server" visible="false">
        <strong>
            <asp:Label ID="lblError" runat="server" Text="Label"></asp:Label>
        </strong>
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
            ข้อมูลประเภททักษะพนักงาน
        </div>
        <div class="panel-body">
            <div class="row">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div class="col-lg-12">
                            <div class="form">
                                <div class="form-group">
                                    <div class="row">
                                        <div class="col-md-2">ชื่อแผนก</div>
                                        <div class="col-md-3">
                                            <asp:DropDownList ID="ddlDepartment" class="form-control" runat="server" Height="35px" Width="200px" AutoPostBack="True" SelectedValue='<%# Eval("Key") %>'>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-md-2">ชื่อประเภททักษะ</div>
                                        <div class="col-md-3">
                                            <asp:DropDownList ID="ddlSkillType" class="form-control" runat="server" Height="35px" Width="200px" SelectedValue='<%# Eval("Key") %>'>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-md-2"></div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-2">&nbsp;</div>
                                        <div class="col-md-3">&nbsp;</div>
                                        <div class="col-md-2">&nbsp;</div>
                                        <div class="col-md-3">&nbsp;</div>
                                        <div class="col-md-2">&nbsp;</div>
                                    </div>
                                     <div class="row" runat="server">
                                        <div class="col-md-2">เปอร์เซ็น</div>
                                        <div class="col-md-3">
                                            <asp:TextBox ID="txtPercen" class="form-control" runat="server" Height="35px" placeholder="เปอร์เซ็น"></asp:TextBox>
                                        </div>
                                        <div class="col-md-2"></div>
                                        <div class="col-md-3"></div>
                                        <div class="col-md-2">
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-2">&nbsp;</div>
                                        <div class="col-md-3">&nbsp;</div>
                                        <div class="col-md-2">&nbsp;</div>
                                        <div class="col-md-3">&nbsp;</div>
                                        <div class="col-md-2">&nbsp;</div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-2">
                                            เพิ่มข้อมูลรายละเอียด
                                        </div>
                                        <div class="col-md-3">
                                            <asp:Button ID="btnAddDetail" runat="server" Text="+" class="btn btn-primary" OnClick="btnAddDetail_Click" />
                                        </div>
                                        <div class="col-md-2">&nbsp;</div>
                                        <div class="col-md-3">&nbsp;</div>
                                        <div class="col-md-2">&nbsp;</div>
                                    </div>
                                    <div class="row" id="rowAdd4" visible="false" runat="server">
                                        <div class="col-md-2">&nbsp;</div>
                                        <div class="col-md-3">&nbsp;</div>
                                        <div class="col-md-2">&nbsp;</div>
                                        <div class="col-md-3">&nbsp;</div>
                                        <div class="col-md-2">&nbsp;</div>
                                    </div>
                                    <div class="row" id="rowAdd1" visible="false" runat="server">
                                        <div class="col-md-2">ลำดับ</div>
                                        <div class="col-md-3">
                                            <asp:TextBox ID="txtSeq" class="form-control" runat="server" Height="35px" Width="200px" placeholder="ลำดับ" Enabled="false"></asp:TextBox>
                                        </div>
                                        <div class="col-md-2">ทักษะ</div>
                                        <div class="col-md-3">
                                            <asp:DropDownList ID="ddlSkill" class="form-control" runat="server" Height="35px" Width="200px" SelectedValue='<%# Eval("Key") %>'>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-md-2"></div>
                                    </div>
                                    <div class="row" id="rowAdd3" visible="false" runat="server">
                                        <div class="col-md-2">&nbsp;</div>
                                        <div class="col-md-3">&nbsp;</div>
                                        <div class="col-md-2">&nbsp;</div>
                                        <div class="col-md-3">&nbsp;</div>
                                        <div class="col-md-2">&nbsp;</div>
                                    </div>
                                    <div class="row" id="rowAdd2" visible="false" runat="server">
                                        <div class="col-md-2">เปอร์เซ็น</div>
                                        <div class="col-md-3">
                                            <asp:TextBox ID="txtAddPercen" class="form-control" runat="server" Height="35px" placeholder="เปอร์เซ็น"></asp:TextBox>
                                        </div>
                                        <div class="col-md-2"></div>
                                        <div class="col-md-3">
                                            <asp:Button ID="btnAdd" runat="server" class="btn btn-primary" Text="บันทึกข้อมูลทักษะ" OnClick="btnAdd_Click" />
                                        </div>
                                        <div class="col-md-2">
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-12">
                            <asp:GridView ID="grdEmpPos" runat="server" ForeColor="#507CD1" AutoGenerateColumns="False"
                                DataKeyNames="TEMPLATE_ID,EMP_SKILL_TYPE_ID,SEQ_NO" PageSize="20" Width="100%"
                                Style="text-align: center" CssClass="grid" OnRowCommand="grdEmpPos_RowCommand">
                                <AlternatingRowStyle BackColor="White" />
                                <Columns>
                                    <asp:TemplateField HeaderText="ลำดับ" ItemStyle-Width="5%">
                                        <ItemTemplate>
                                            <%# Container.DataItemIndex + 1 %>
                                        </ItemTemplate>
                                        <ItemStyle BorderStyle="Solid" />
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="SKILL_TYPE_NAME" HeaderText="ประเภททักษะ" ItemStyle-Width="30%">
                                        <ItemStyle Width="30%"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="SKILL_NAME" HeaderText="ชื่อทักษะ" ItemStyle-Width="40%">
                                        <ItemStyle Width="40%"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="SKILL_TARGET_SCORE" HeaderText="คะแนน" ItemStyle-Width="20%">
                                        <ItemStyle Width="20%"></ItemStyle>
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
                    </ContentTemplate>
                </asp:UpdatePanel>

                <!-- /.col-lg-6 (nested) -->
            </div>
            <div style="float: right; margin-top: 5px;">
                <table>
                    <tr>
                        <td>
                            <asp:Button ID="btnSave" runat="server" class="btn btn-primary" Text="บันทึก" OnClick="btnSave_Click" ValidationGroup="group" />

                        </td>
                        <td>&nbsp;&nbsp;</td>
                        <td>
                            <asp:Button ID="btnCancel" runat="server" class="btn btn-primary" Text="ยกเลิก" PostBackUrl="~/Page/SearchEmpHdTemplate.aspx" />
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
