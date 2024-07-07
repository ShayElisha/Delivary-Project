<%@ Page Title="" Language="C#" MasterPageFile="~/AdminManager/BackAdmin.Master" AutoEventWireup="true" CodeBehind="ManagerAddEdit.aspx.cs" Inherits="Delivery_Project.AdminManager.ManagerAddEdit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/select2/4.0.13/js/select2.min.js"></script>
    <link href="https://cdnjs.cloudflare.com/ajax/libs/select2/4.0.13/css/select2.min.css" rel="stylesheet" />

    <script type="text/javascript">
        $(document).ready(function () {
            $('#<%= DDLcity.ClientID %>').select2({
                placeholder: 'בחר עיר',
                allowClear: true,
                width: '100%'
            });
        });
    </script>
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
                    הוספה / עריכת נהגים
                </div>
                <div class="panel-body">
                    <div class="row">
                        <div class="col-lg-6">
                            <asp:HiddenField ID="hidCid" runat="server" Value="-1" />
                            <div class="form-group">
                                <label>שם מלא</label>
                                <asp:TextBox ID="FullName" CssClass="form-control" runat="server" placeholder="נא הזן שם מלא" />
                                <label>אימייל</label>
                                <asp:TextBox ID="Email" CssClass="form-control" runat="server" placeholder="נא הזן אימייל" />
                            </div>
                            <div class="form-group">
                                <label>סיסמה</label>
                                <asp:TextBox ID="Password" CssClass="form-control" runat="server" placeholder="נא הזן סיסמה" />
                            </div>
                            <div class="form-group">
                                <label for="ddlCity">עיר:</label>
                                <asp:DropDownList ID="DDLcity" runat="server" CssClass="form-control"></asp:DropDownList>
                            </div>
                            <div class="form-group">
                                <label>כתובת</label>
                                <asp:TextBox ID="Address" CssClass="form-control" runat="server" placeholder="נא הזן כתובת" />
                                <label>טלפון</label>
                                <asp:TextBox ID="Phone" CssClass="form-control" runat="server" placeholder="נא הזן טלפון" />
                            </div>
                            <div class="form-group">
                                <asp:TextBox ID="status" CssClass="form-control" runat="server" placeholder="נא הזן סטטוס" />
                            </div>
                            <asp:Button ID="btnSave" runat="server" CssClass="btn btn-primary" OnClick="btnSave_Click" Text="שמירה" />
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
