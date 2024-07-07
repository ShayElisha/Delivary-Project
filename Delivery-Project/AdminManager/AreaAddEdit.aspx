<%@ Page Title="" Language="C#" MasterPageFile="~/AdminManager/BackAdmin.Master" AutoEventWireup="true" CodeBehind="AreaAddEdit.aspx.cs" Inherits="Delivery_Project.AdminManager.AreaAddEdit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <!-- Select2 CSS -->
    <link href="https://cdnjs.cloudflare.com/ajax/libs/select2/4.0.13/css/select2.min.css" rel="stylesheet" />
    <!-- jQuery (נדרש עבור Select2) -->
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jquery/3.6.0/jquery.min.js"></script>
    <!-- Select2 JavaScript -->
    <script src="https://cdnjs.cloudflare.com/ajax/libs/select2/4.0.13/js/select2.min.js"></script>
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
                                <label>שם איזור</label>
                                <asp:TextBox ID="TxtCname" CssClass="form-control" runat="server" placeholder="נא הזן שם איזור"/>
                                <label>סטטוס</label>
                                <asp:ListBox ID="TxtCities" CssClass="form-control" runat="server" SelectionMode="Multiple"/>
                            </div>   
                            <asp:Button ID="btnSave" type="submit" CssClass="btn btn-primary" runat="server" OnClick="btnSave_Click" Text="שמירה" />
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

    <!-- הוספת סקריפט להפעלת Select2 על ה-ListBox -->
    <script type="text/javascript">
        $(document).ready(function() {
            $('#<%= TxtCities.ClientID %>').select2({
                placeholder: 'נא הזן שם עיר',
                allowClear: true,
                width: '100%' // מתאים את רוחב הרכיב לרוחב השדה
            });
        });
    </script>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="FooterCnt" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="UnderFooterCnt" runat="server">
</asp:Content>
