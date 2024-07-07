<%@ Page Title="" Language="C#" MasterPageFile="~/AdminManager/BackAdmin.Master" AutoEventWireup="true" CodeBehind="CitiesAddEdit.aspx.cs" Inherits="Delivery_Project.AdminManager.CitiesAddEdit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainCnt" runat="server">
                <div class="row">
    <div class="col-lg-12">
        <h1 class="page-header">ניהול ערים</h1>
    </div>
    <!-- /.col-lg-12 -->
        </div>
<!-- /.row -->
        <div class="row">
            <div class="col-lg-12">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        הוספה / עריכת ערים
                    </div>
                    <div class="panel-body">
                        <div class="row">
                            <div class="col-lg-6">
                                <asp:HiddenField ID="hidCid" runat="server" Value="-1"/>
                                    <div class="form-group">
                                        <label>שם עיר</label>
                                        <asp:TextBox ID="TxtCname" CssClass="form-control" runat="server" placeholder="נא הזן שם קטגוריה"/>
                                         <label>סטטוס</label>
                                        <asp:TextBox ID="TxtStatus" CssClass="form-control" runat="server" placeholder="נא הזן סטטוס"/>
                                    </div>   
                                    <asp:button ID="btnSave" type="submit" class="btn btn-primary" runat="server" onclick="btnSave_Click" Text="שמירה" />
                            </div>
                        </div>
                        <!-- /.row (nested) -->
                    </div>
                    <!-- /.panel-body -->
                </div>
                <!-- /.panel -->
            </div>
            <!-- /.col-lg-12 -->
        </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="FooterCnt" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="UnderFooterCnt" runat="server">
</asp:Content>
