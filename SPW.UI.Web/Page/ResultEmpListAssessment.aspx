<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPageMainAdmin.Master" AutoEventWireup="true"
    CodeBehind="ResultEmpListAssessment.aspx.cs" Inherits="SPW.UI.Web.Page.ResultEmpListAssessment" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1 class="page-header">ผลการประเมินแบบกลุ่ม</h1>
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
            ข้อมูลผลการประเมิน             
        </div>
        <div class="panel-body">
            <div class="row">
                <div class="col-lg-12">
                    <div class="form">
                        <div class="form-group">
                            <div class="row">
                                <div class="col-md-2">
                                    ปี
                                </div>
                                <div class="col-md-3">
                                    <asp:TextBox ID="txtYear" class="form-control" runat="server" Height="35px" Width="200px" placeholder="ปี"></asp:TextBox>
                                </div>
                                <div class="col-md-2">
                                    ช่วง
                                </div>
                                <div class="col-md-3">
                                    <asp:DropDownList ID="ddlSeq" class="form-control" runat="server" Height="35px" Width="200px">
                                        <asp:ListItem Text="ต้นปี" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="ปลายปี" Value="2"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-2">
                                    แผนก
                                </div>
                                <div class="col-md-3">
                                    <asp:DropDownList ID="ddlDepart" class="form-control" runat="server" Height="35px" Width="200px" SelectedValue='<%# Eval("Key") %>'>
                                        <asp:ListItem Text="ทั้งหมด" Selected="True" Value="0"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-2">
                                </div>
                                <div class="col-md-3">
                                    <asp:Button ID="btnSearch" class="btn btn-primary" runat="server" Text="พิมพ์" OnClick="btnSearch_Click" Height="30px" Width="70px" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

        </div>
    </div>
</asp:Content>
