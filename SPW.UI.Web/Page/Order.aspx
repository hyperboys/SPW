<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterPageMainAdmin.Master" AutoEventWireup="true"
    CodeBehind="Order.aspx.cs" Inherits="SPW.UI.Web.Page.Order" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1 class="page-header">สั่งสินค้า</h1>
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
    </style>

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>

    <%--<script type="text/javascript">
        $(function () {
            $('[id*=txtStoreCode]').typeahead({
                hint: true,
                highlight: true,
                minLength: 1
                , source: function (request, response) {
                    $.ajax({
                        url: '<%=ResolveUrl("Order.aspx/SearchTxtStoreCode") %>',
                        data: "{ 'STORE_CODE': '" + request + "'}",
                        dataType: "json",
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        success: function (data) {
                            items = [];
                            map = {};
                            $.each(data.d, function (i, item) {
                                var id = item;
                                var name = item;
                                map[name] = { id: id, name: name };
                                items.push(name);
                            });
                            response(items);
                            $(".dropdown-menu").css("height", "auto");
                        },
                        error: function (response) {
                            alert(response.responseText);
                        },
                        failure: function (response) {
                            alert(response.responseText);
                        }
                    });
                },
                updater: function (item) {
                    $('[id*=hfStoreCode').val(map[item].id);
                    return item;
                }
            });
        });
    </script>--%>

    <%--<script type="text/javascript">
        $(function () {
            $('[id*=txtStoreName]').typeahead({
                hint: true,
                highlight: true,
                minLength: 1
                , source: function (request, response) {
                    $.ajax({
                        url: '<%=ResolveUrl("Order.aspx/SearchTxtStoreName") %>',
                        data: "{ 'STORE_NAME': '" + request + "'}",
                        dataType: "json",
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        success: function (data) {
                            items = [];
                            map = {};
                            $.each(data.d, function (i, item) {
                                var id = item;
                                var name = item;
                                map[name] = { id: id, name: name };
                                items.push(name);
                            });
                            response(items);
                            $(".dropdown-menu").css("height", "auto");
                        },
                        error: function (response) {
                            alert(response.responseText);
                        },
                        failure: function (response) {
                            alert(response.responseText);
                        }
                    });
                },
                updater: function (item) {
                    $('[id*=hfStoreName]').val(map[item].id);
                    return item;
                }
            });
        });
    </script>--%>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="panel panel-primary">
                <div class="panel-heading">
                    สั่งสินค้า        
                </div>
                <div class="panel-body">
                    <div class="row">
                        <div class="col-lg-12">
                            <div class="form">
                                <div class="form-group">
                                    <%--first row--%>
                                    <div class="row">
                                        <div class="col-md-2">รหัสร้านค้า</div>
                                        <div class="col-md-3">
                                            <asp:TextBox ID="txtStoreCode" runat="server" class="form-control" data-provide="typeahead" data-items="5" autocomplete="off" Width="200px" placeholder="รหัสร้านค้า"></asp:TextBox>
                                            <%--<asp:TextBox ID="txtStoreCode" class="form-control" runat="server" Width="200px" placeholder="รหัสร้านค้า"></asp:TextBox>
                                            <asp:HiddenField ID="hfStoreCode" runat="server" />--%>
                                        </div>
                                        <div class="col-md-2">ชื่อร้านค้า</div>
                                        <div class="col-md-3">
                                            <asp:TextBox ID="txtStoreName" class="form-control" runat="server" data-provide="typeahead" data-items="5" autocomplete="off" Width="200px" placeholder="ชื่อร้านค้า"></asp:TextBox>
                                             <asp:HiddenField ID="hfStoreName" runat="server" />
                                        </div>
                                        <div class="col-md-2"></div>
                                    </div>
                                    <%--second row--%>
                                    <div class="row">
                                        <div class="col-md-2">ภาค</div>
                                        <div class="col-md-3">
                                            <asp:DropDownList ID="ddlSector" class="form-control" runat="server" Height="35px" Width="200px" OnSelectedIndexChanged="ddlSector_SelectedIndexChanged1" AutoPostBack="True" CssClass="form-control" SelectedValue='<%# Eval("Key") %>'>
                                                <asp:ListItem Value="0">กรุณาเลือก</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-md-2">จังหวัด</div>
                                        <div class="col-md-3">
                                            <asp:DropDownList ID="ddlProvince" class="form-control" runat="server" Height="35px" Width="200px" OnSelectedIndexChanged="ddlProvince_SelectedIndexChanged" AutoPostBack="True" CssClass="form-control" SelectedValue='<%# Eval("Key") %>'>
                                                <asp:ListItem Value="0">กรุณาเลือก</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-md-2"></div>
                                    </div>
                                    <%--third row--%>
                                    <div class="row">
                                        <div class="col-md-2">ถนน</div>
                                        <div class="col-md-3">
                                            <asp:DropDownList ID="ddlRoad" class="form-control" runat="server" Height="35px" Width="200px" CssClass="form-control" Enabled="False" SelectedValue='<%# Eval("Key") %>'>
                                                <asp:ListItem Value="0">กรุณาเลือก</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-md-2"></div>
                                        <div class="col-md-3"></div>
                                        <div class="col-md-2">
                                            <asp:Button ID="btnSearch" class="btn btn-primary" runat="server" Text="ค้นหา" Height="30px" Width="70px" OnClick="btnSearch_Click" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <asp:GridView ID="gdvStore" runat="server" ForeColor="#507CD1" AutoGenerateColumns="False"
                        DataKeyNames="STORE_ID" PageSize="20" Width="100%" EmptyDataText="ไม่พบข้อมูลร้านค้า" OnRowDataBound="gdvStore_RowDataBound" Visible="False">
                        <AlternatingRowStyle BackColor="White" />
                        <Columns>
                            <asp:TemplateField HeaderText="No" ItemStyle-Width="50px">
                                <ItemTemplate>
                                    <%# Container.DataItemIndex + 1 %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="ร้านค้า" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle" HeaderStyle-Height="35px">
                                <ItemTemplate>
                                    <asp:Table ID="Table1" runat="server" ItemStyle-HorizontalAlign="Left" HorizontalAlign="Left">
                                        <asp:TableRow Width="640px">
                                            <asp:TableCell Width="640px">
                                                <b>
                                                    <asp:HyperLink ID="NAME" runat="server">HyperLink</asp:HyperLink></b>
                                            </asp:TableCell>
                                        </asp:TableRow>
                                        <asp:TableRow Width="640px">
                                            <asp:TableCell Width="640px">
                                                <asp:Label ID="ADDRESS" runat="server" Text="Label"></asp:Label>
                                            </asp:TableCell>
                                        </asp:TableRow>
                                        <asp:TableRow Width="640px">
                                            <asp:TableCell Width="640px">
                                                <asp:Label ID="TEL" runat="server" Text="Label"></asp:Label>
                                            </asp:TableCell>
                                        </asp:TableRow>
                                    </asp:Table>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="รายละเอียด">
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
                    <div style="margin-top: 10px;">
                        <asp:PlaceHolder ID="PlaceHolder1" runat="server" Visible="false"></asp:PlaceHolder>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
