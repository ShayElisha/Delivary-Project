<%@ Page Title="" Language="C#" MasterPageFile="~/AdminManager/BackAdmin.Master" AutoEventWireup="true" CodeBehind="OrderList.aspx.cs" Inherits="Delivery_Project.AdminManager.OrderList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .disabled-link {
            color: grey;
            pointer-events: none;
            text-decoration: none;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainCnt" runat="server">
    <div class="row">
        <div class="col-lg-12">
            <h1 class="page-header">רשימת משלוחים</h1>
        </div>
        <!-- /.col-lg-12 -->
    </div>
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <input type="date" id="dateToWork" name="dateToWork" />
            <asp:Button ID="btnWork" runat="server" OnClick="btnWork_Click" Text="הקצאת סידור עבודה" CssClass="btn btn-primary" />
            <!-- /.row -->
            <div class="row">
                <div class="col-lg-12">
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            מאגר משלוחים במערכת
                        </div>
                        <!-- /.panel-heading -->
                        <div class="panel-body">
                            <div class="table-responsive">
                                <table class="table table-striped table-bordered table-hover" id="mainTbl">
                                    <thead>
                                        <tr>
                                            <th>מזהה הזמנה</th>
                                            <th>מזהה לקוח</th>
                                            <th>שם מלא</th>
                                            <th>אימייל</th>
                                            <th>טלפון</th>
                                            <th>כתובת</th>
                                            <th>עיר</th>
                                            <th>כמות</th>
                                            <th>הערות</th>
                                            <th>זמן משלוח צפוי</th>
                                            <th>תאריך העמסה</th>
                                            <th>מועד מסירה</th>
                                            <th>תאריך הזמנה</th>
                                            <th>סטטוס</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <asp:Repeater ID="RptProd" runat="server" OnItemDataBound="RptProd_ItemDataBound">
                                            <ItemTemplate>
                                                <tr class="odd gradeX">
                                                    <td><%# Eval("OrderID") %></td>
                                                    <td><%# Eval("CustomerID") %></td>
                                                    <td><%# Eval("FullName") %></td>
                                                    <td><%# Eval("Email") %></td>
                                                    <td><%# Eval("Phone") %></td>
                                                    <td><%# Eval("Address") %></td>
                                                    <td><%# Eval("CityId") %></td>
                                                    <td><%# Eval("Quantity") %></td>
                                                    <td><%# Eval("Notes") %></td>
                                                    <td><%# Eval("ChooseDeliveryTime") %></td><!-- תאריך מועד משלוח -->
                                                    <td><%# Eval("DeliveryTime") %></td><!-- תאריך העמסה -->
                                                    <td><%# Eval("Datedelivery") %></td><!-- תאריך מסירה-->
                                                    <td><%# Eval("orderDate") %></td><!-- תאריך ההזמנה-->
                                                    <td><%# Eval("Status") %></td>
                                                    <td class="center">
                                                        <asp:HyperLink ID="ExportButton" runat="server" NavigateUrl='<%# "ShipmentAddEdit.aspx?OrderID=" + Eval("OrderID") %>'>
                                                            ייצא לנהג <span class="fa fa-pencil"></span>
                                                        </asp:HyperLink>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </tbody>
                                </table>
                            </div>
                            <!-- /.table-responsive -->
                            <asp:Panel ID="NoShipmentsPanel" runat="server" CssClass="no-shipments" Visible="False">
                                אין משלוחים ברשימה.
                            </asp:Panel>
                        </div>
                        <!-- /.panel-body -->
                    </div>
                    <!-- /.panel -->
                </div>
                <!-- /.col-lg-12 -->
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnWork" EventName="Click" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="FooterCnt" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="UnderFooterCnt" runat="server">
</asp:Content>
