<%@ Page Title="" Language="C#" MasterPageFile="~/AdminManager/BackAdmin.Master" AutoEventWireup="true" CodeBehind="shipmentAddEdit.aspx.cs" Inherits="Delivery_Project.AdminManager.shipmentAddEdit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function validateForm() {
            var requiredFields = ["OrderID", "CustomerID", "SourceAdd", "SourceCity", "DestinationAdd", "DestinationCity", "customerName", "Phone", "ddlDriverId", "Quantity", "ddlStatus"];
            for (var i = 0; i < requiredFields.length; i++) {
                var field = document.getElementById(requiredFields[i]);
                if (field.value === "" || field.value === "0") {
                    alert("אנא מלא את כל השדות");
                    return false;
                }
            }
            return true;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainCnt" runat="server">
    <div class="row">
        <div class="col-lg-12">
            <h1 class="page-header">ניהול משלוחים</h1>
        </div>
        <!-- /.col-lg-12 -->
    </div>
    <!-- /.row -->
    <div class="row">
        <div class="col-lg-12">
            <div class="panel panel-default">
                <div class="panel-heading">
                    הוספה / עריכת משלוחים
                </div>
                <div class="panel-body">
                    <div class="row">
                        <div class="col-lg-6">
                            <asp:HiddenField ID="hidShipId" runat="server" Value="-1"/>
                            <asp:HiddenField ID="hidChooseDeliveryTime" runat="server" Value=""/>
                            <div class="form-group">
                                <label>קוד הזמנה</label>
                                <asp:TextBox ID="OrderID" CssClass="form-control" runat="server" placeholder="נא הזן כתובת מקור" ReadOnly="true"/>
                                <label>קוד לקוח</label>
                                 <asp:TextBox ID="CustomerID" CssClass="form-control" runat="server" placeholder="נא הזן כתובת מקור" ReadOnly="true"/>
                                <label>כתובת מקור</label>
                                <asp:TextBox ID="SourceAdd" CssClass="form-control" Text="אריה בן אליעזר 106" runat="server" placeholder="נא הזן כתובת מקור"/>
                                <label>עיר מקור</label>
                                <asp:TextBox ID="SourceCity" CssClass="form-control" Text="אשקלון" runat="server" placeholder="נא הזן עיר מקור"/>
                            </div>
                            <div class="form-group">
                                <label>כתובת יעד</label>
                                <asp:TextBox ID="DestinationAdd" CssClass="form-control" runat="server" placeholder="נא הזן כתובת יעד"/>
                                <label>עיר יעד</label>
                                <asp:TextBox ID="DestinationCity" CssClass="form-control" runat="server" placeholder="נא הזן עיר יעד"/>
                            </div>
                            <div class="form-group">
                                 <label>שם לקוח</label>
                                <asp:TextBox ID="customerName" CssClass="form-control" runat="server" placeholder="נא הזן טלפון"/>
                                <label>טלפון</label>
                                <asp:TextBox ID="Phone" CssClass="form-control" runat="server" placeholder="נא הזן טלפון"/>
                                <label>כמות</label>
                                <asp:TextBox ID="Quantity" CssClass="form-control" runat="server" placeholder="נא הזן כמות"/>
                            </div>
                           
                            <div class="form-group">
                                <label>מזהה נהג</label>
                               <asp:DropDownList ID="ddlDriverId" CssClass="form-control" runat="server">
                                    <asp:ListItem Text="בחר נהג" Value="0"></asp:ListItem>
                                </asp:DropDownList>
                            
                            <asp:Button ID="btnSave" CssClass="btn btn-primary" runat="server" Text="שמירה" OnClick="btnSave_Click"  />
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
