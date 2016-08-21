<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage/MasterPageMainAdmin.Master" 
    CodeBehind="SelectResultEmpAssessment.aspx.cs" Inherits="SPW.UI.Web.Page.SelectResultEmpAssessment" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1 class="page-header">เลือกประเภทผลการประเมิน</h1>
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
          <h3 class="card-title">ผลการประเมินแบบกลุ่ม</h3><a href="ResultEmpListAssessment.aspx" class="btn btn-primary">Go</a>
        </div>
      </div>
      <div class="col-sm-6">
        <div class="card card-block">
          <h3 class="card-title">ผลการประเมินรายบุคคล </h3>  <a href="SearchEmpResultAssessment.aspx" class="btn btn-primary">Go</a>
        </div>
      </div>
    </div>
</asp:Content>
