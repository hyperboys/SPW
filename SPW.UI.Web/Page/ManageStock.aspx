<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage/MasterPageMainAdmin.Master" CodeBehind="ManageStock.aspx.cs" Inherits="SPW.UI.Web.Page.ManageStock" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1 class="page-header">เลือกประเภทคลัง </h1>
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
        .card {
    position: relative;
    display: block;
    margin-bottom: .75rem;
    background-color: #fff;
    border: 1px solid #e5e5e5;
    border-radius: .25rem;
}
        .card-block {
    padding: 1.25rem;
}
    </style>
    <div class="row">
      <div class="col-sm-6">
        <div class="card card-block">
          <h3 class="card-title">คลังสำเร็จรูป</h3>
          <p class="card-text">The goods or merchandise kept on the premises of a business.</p>
          <a href="StockIn.aspx" class="btn btn-primary">Go</a>
        </div>
      </div>
      <div class="col-sm-6">
        <div class="card card-block">
          <h3 class="card-title">คลังวัตถุดิบ <%--<span class="label label-pill label-danger">Incomplete</span>--%></h3>
          <p class="card-text">The matter from which a thing is or can be made..</p>
          <a href="StockRaw.aspx" class="btn btn-primary">Go</a>
        </div>
      </div>
    </div>
</asp:Content>
