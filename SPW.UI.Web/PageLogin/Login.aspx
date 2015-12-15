<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPageLogin.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="SPW.UI.Web.PageLogin.Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <div class="row">
            <div class="col-md-4 col-md-offset-4">
                <div class="login-panel panel panel-default">
                    <div class="panel-heading">
                        <h3 class="panel-title">Please Sign In</h3>
                    </div>
                    <div class="panel-body">
                        <div class="form">
                            <div>
                                <div class="form-group">
                                    <asp:TextBox ID="txtUsername" class="form-control" placeholder="Username" name="Username" value="" autofocus="True" runat="server"></asp:TextBox>
                                </div>
                                <div class="form-group">
                                    <asp:TextBox ID="txtPassword" class="form-control" placeholder="Password" name="password" type="password" value="" runat="server"></asp:TextBox>
                                </div>
                                <div class="alert alert-danger" id="alert" runat="server" visible="false">
                                    <strong>username หรือ password ผิด!</strong> กรุณา Sign In ใหม่อีกครั้ง
                                </div>
                                <!-- Change this to a button or input when using this as a form -->
                                <%--<asp:Button ID="btnLogin" class="btn btn-lg btn-success btn-block" runat="server" Text="Login" />--%> 
                                <asp:Button ID="btnSingon" class="btn btn-primary btn-lg btn-block" runat="server" Text="Login" OnClick="btnSingon_Click" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
