<%@ Page Title="" Language="C#" MasterPageFile="~/AdminManager/BackAdmin.Master" AutoEventWireup="true" CodeBehind="ShipmentList.aspx.cs" Inherits="Delivery_Project.AdminManager.ShipmentList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainCnt" runat="server">
        <div class="row">
        <div class="col-lg-12">
            <h1 class="page-header">רשימת משלוחים</h1>
        </div>
        <!-- /.col-lg-12 -->
    </div>
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
                                    <th>מזהה משלוח</th>
                                    <th>מזהה הזמנה</th>
                                    <th>מזהה לקוח</th>
                                    <th>שם לקוח</th>
                                    <th>כתובת מקור</th>
                                    <th>עיר מקור</th>
                                    <th>כתובת יעד</th>
                                    <th>עיר יעד</th>
                                    <th>טלפון</th>
                                    <th>תאריך איסוף</th>
                                    <th>תאריך משלוח</th>
                                    <th>תאריך שחרור</th>
                                    <th>כמות</th>
                                    <th>מזהה נהג</th>
                                    <th>זטטוס</th>
                                    <th>תאריך הוספה</th>
                                    <th>פעולות</th>
                                </tr>
                            </thead>
                            <tbody>
                                <asp:Repeater ID="RptProd" runat="server" OnItemCommand="RptAddresses_ItemCommand">
                                    <ItemTemplate>
                                        <tr class="odd gradeX">
                                            <td><%#Eval("ShipId") %></td>
                                            <td><%#Eval("OrderID") %></td>
                                            <td><%#Eval("CustomerID") %></td>
                                            <td><%#Eval("CustomerName") %></td>
                                            <td><%#Eval("SourceAdd") %></td>
                                            <td><%#Eval("SourceCity") %></td>
                                            <td><%#Eval("DestinationAdd") %></td>
                                            <td><%#Eval("DestinationCity") %></td>
                                            <td><%#Eval("Phone") %></td>
                                            <td><%#Eval("CollectionDate") %></td>
                                            <td><%#Eval("DateDelivery") %></td>
                                            <td><%#Eval("ReleaseDate") %></td>
                                            <td><%#Eval("Quantity") %></td>
                                            <td><%#Eval("DriverId") %></td>
                                            <td><%#Eval("status") %></td>
                                            <td><%#Eval("AddDate") %></td>
                                            <td>
                                                <a href="ShipmentAddEdit.aspx?ShipId=<%#Eval("ShipId") %>">ערוך <span class="fa fa-pencil"></span></a>  
                                                <asp:LinkButton ID="lnkDelete" runat="server" CommandName="Delete" CommandArgument='<%# Eval("ShipId") %>' Text="מחק" OnClientClick="return confirm('האם אתה בטוח שברצונך למחוק כתובת זו?');"></asp:LinkButton>                                                               
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
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="FooterCnt" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="UnderFooterCnt" runat="server">
</asp:Content>
