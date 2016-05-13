<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage/MasterPageMainAdmin.Master" CodeBehind="ManageRawProduct.aspx.cs" Inherits="SPW.UI.Web.Page.ManageRawProduct" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1 class="page-header">
        <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Page/SearchRawProduct.aspx">วัตถุดิบ</asp:HyperLink>
        /
        <asp:Label ID="lblName" runat="server" Text="เพิ่มวัตถุดิบ"></asp:Label>
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

        .auto-style12 {
            width: 128px;
        }

        .auto-style13 {
            width: 652px;
        }

        .auto-style15 {
            width: 208px;
        }

        .auto-style16 {
            width: 73px;
        }

        .auto-style18 {
            width: 15px;
        }

        .auto-style19 {
            width: 136px;
        }

        .auto-style20 {
            width: 131px;
        }

        .auto-style22 {
            width: 126px;
        }
        .auto-style23 {
            width: 9px;
        }
        .row {
            margin-top: 5px;
        }
    </style>

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>

    
            <div class="panel panel-primary">
                <div class="panel-heading">
                    วัตถุดิบ        
                </div>
                <div class="panel-body">
                    <div class="row">
                        <div class="col-lg-12">
                            <div class="form">
                                <div class="form-group">
                                    <%--first row--%>
                                    <div class="row">
                                        <div class="col-md-2">ประเภทวัตถุดิบ</div>
                                        <div class="col-md-3">
                                            <asp:DropDownList ID="ddlRawProductType" class="form-control" runat="server" Height="35px" Width="200px" OnSelectedIndexChanged="ddlRawProductType_SelectedIndexChanged" AutoPostBack="True">
                                                <asp:ListItem Value="0">กรุณาเลือก</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-md-2"></div>
                                        <div class="col-md-3"></div>
                                        <div class="col-md-2"></div>
                                    </div>
                                    <div id="rawOptionDiv" runat="server">
                                    <%--second row--%>
                                    <div class="row">
                                        <div class="col-md-2">ชื่อสินค้า(ปกติ)</div>
                                        <div class="col-md-3">
                                            <asp:TextBox ID="txtRawName1" class="form-control" runat="server" Height="35px" placeholder="ชื่อสินค้าปกติ"></asp:TextBox>
                                        </div>
                                        <div class="col-md-2">ชื่อสินค้า(สำหรับโรงงาน)</div>
                                        <div class="col-md-3">
                                            <asp:TextBox ID="txtRawName2" class="form-control" runat="server" Height="35px" placeholder="ชื่อสินค้าสำหรับโรงงาน"></asp:TextBox>
                                        </div>
                                        <div class="col-md-2"></div>
                                    </div>
                                    </div>
                                    <div id="otherOptionDiv" runat="server">
                                    <%--third row--%>
                                    <div class="row">
                                        <div class="col-md-2">ความกว้าง</div>
                                        <div class="col-md-3">
                                            <asp:TextBox ID="txtRawWD" class="form-control" runat="server" Height="35px" placeholder="ความกว้าง"></asp:TextBox>
                                        </div>
                                        <div class="col-md-2">หน่วย</div>
                                        <div class="col-md-3">
                                            <asp:DropDownList ID="ddlWDUnit" class="form-control" runat="server" Height="35px" Width="200px" SelectedValue='<%# Eval("Key") %>'>
                                                <asp:ListItem Value="0">กรุณาเลือก</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-md-2"></div>
                                    </div>
                                    <%--forth row--%>
                                    <div class="row">
                                        <div class="col-md-2">ความสูง</div>
                                        <div class="col-md-3">
                                            <asp:TextBox ID="txtRawHG" class="form-control" runat="server" Height="35px" placeholder="ความหนา"></asp:TextBox>
                                        </div>
                                        <div class="col-md-2">หน่วย</div>
                                        <div class="col-md-3">
                                            <asp:DropDownList ID="ddlHGUnit" class="form-control" runat="server" Height="35px" Width="200px" SelectedValue='<%# Eval("Key") %>'>
                                                <asp:ListItem Value="0">กรุณาเลือก</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-md-2">
                                        </div>
                                    </div>
                                    <%--fifth row--%>
                                    <div class="row">
                                        <div class="col-md-2">ความหนา</div>
                                        <div class="col-md-3">
                                            <asp:TextBox ID="txtRawBD" class="form-control" runat="server" Height="35px" placeholder="ความหนา"></asp:TextBox>
                                        </div>
                                        <div class="col-md-2">หน่วย</div>
                                        <div class="col-md-3">
                                            <asp:DropDownList ID="ddlBDUnit" class="form-control" runat="server" Height="35px" Width="200px" SelectedValue='<%# Eval("Key") %>'>
                                                <asp:ListItem Value="0">กรุณาเลือก</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-md-2">
                                        </div>
                                    </div>
                                    </div>
                                    <%--sixth row--%>
                                    <div class="row">
                                        <div class="col-md-2"></div>
                                        <div class="col-md-3">
                                        </div>
                                        <div class="col-md-2"></div>
                                        <div class="col-md-3">
                                        </div>
                                        <div class="col-md-2">
                                            <asp:Button ID="btnSave" class="btn btn-primary" runat="server" Text="บันทึก" Height="30px" Width="70px" OnClick="btnSave_Click" />
                                            <asp:Label ID="lblError" runat="server" Text="" forecolor="Red"></asp:Label>
                                            <asp:Label ID="flag" runat="server" Text="Add" Visible="false"></asp:Label>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <!-- /.col-lg-6 (nested) -->
                    </div>
                </div>
                <!-- /.panel-body -->
            </div>
            <!-- /.panel -->
</asp:Content>


