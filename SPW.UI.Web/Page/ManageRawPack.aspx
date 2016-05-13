﻿<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage/MasterPageMainAdmin.Master" CodeBehind="ManageRawPack.aspx.cs" Inherits="SPW.UI.Web.Page.ManageRawPack" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1 class="page-header">
        <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Page/SearchRawPack.aspx">ขนาดบรรจุภัณฑ์</asp:HyperLink>
        /
        <asp:Label ID="lblName" runat="server" Text="เพิ่มขนาดบรรจุภัณฑ์"></asp:Label>
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
            ขนาดบรรจุภัณฑ์        
        </div>
        <div class="panel-body">
            <div class="row">
                <div class="col-lg-12">
                    <div class="form">
                        <div class="form-group">
                            <%--first row--%>
                            <div class="row">
                                <div class="col-md-2">แพ็คไซส์</div>
                                <div class="col-md-3">
                                    <asp:TextBox ID="txtPackSize" class="form-control" runat="server" Height="35px" placeholder="แพคไซส์"></asp:TextBox>
                                </div>
                                <div class="col-md-2"></div>
                                <div class="col-md-3"></div>
                                <div class="col-md-2"></div>
                            </div>
                            <%--second row--%>
                            <div class="row">
                                <div class="col-md-2">จำนวนหน่วย</div>
                                <div class="col-md-3">
                                    <asp:TextBox ID="txtUnitQty" class="form-control" runat="server" Height="35px" placeholder="1/12/24/48..."></asp:TextBox>
                                </div>
                                <div class="col-md-2">ชื่อหน่วย</div>
                                <div class="col-md-3">
                                    <asp:TextBox ID="txtUnitDesc" class="form-control" runat="server" Height="35px" placeholder="ชิ้น/อัน/ด้าม..."></asp:TextBox>
                                </div>
                                <div class="col-md-2"></div>
                            </div>
                            <%--third row--%>
                            <div class="row">
                                <div class="col-md-2">จำนวนแพ็ค</div>
                                <div class="col-md-3">
                                    <asp:TextBox ID="txtPackQty" class="form-control" runat="server" Height="35px" placeholder="1/12/24/48..."></asp:TextBox>
                                </div>
                                <div class="col-md-2">ชื่อแพ็ค</div>
                                <div class="col-md-3">
                                    <asp:TextBox ID="txtPackDesc" class="form-control" runat="server" Height="35px" placeholder="มัด/โหล/กระสอบ..."></asp:TextBox>
                                </div>
                                <div class="col-md-2">
                                </div>
                            </div>
                            <%--forth row--%>
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


