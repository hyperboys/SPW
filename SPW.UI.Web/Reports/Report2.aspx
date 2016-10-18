﻿<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage/MasterPageMainAdmin.Master" CodeBehind="Report2.aspx.cs" Inherits="SPW.UI.Web.Reports.Report2" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%--<script type="text/javascript" src="/crystalreportviewers13/js/crviewer/crv.js"></script>--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1 class="page-header">รายงานแจ้งซ่อมรถ</h1>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" ReportSourceID="CrystalReportSource1" EnableDatabaseLogonPrompt="False" ToolPanelView="None" EnableTheming="True" />
    <CR:CrystalReportSource ID="CrystalReportSource1" runat="server">
        <Report FileName="ReportInvoices.rpt">
        </Report>
    </CR:CrystalReportSource>
</asp:Content>