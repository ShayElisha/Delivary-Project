<%@ Page Title="" Language="C#" MasterPageFile="~/DriverSide/DriverSide.Master" AutoEventWireup="true" CodeBehind="HistoryShipments.aspx.cs" Inherits="Delivery_Project.DriverSide.HistoryShipments" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        body {
            font-family: Arial, sans-serif;
            background-color: #f4f4f4;
            margin: 0;
            padding: 0;
        }

        .container {
            width: 100%;
            padding: 20px;
            border: 1px solid #ccc;
            border-radius: 10px;
            background-color: #fff;
            box-shadow: 0 4px 8px rgba(0,0,0,0.1);
            box-sizing: border-box;
        }

        h2 {
            text-align: center;
            color: #333;
            margin-bottom: 20px;
        }

        table {
            width: 100%;
            border-collapse: collapse;
            margin-bottom: 20px;
        }

        table, th, td {
            border: 1px solid #ddd;
        }

        th, td {
            padding: 12px;
            text-align: left;
        }

        th {
            background-color: #336699;
            color: white;
        }

        .odd {
            background-color: #f2f2f2;
        }

        .gradeX:hover {
            background-color: #e0e0e0;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <h2>רשימת הזמנות</h2>
        <div class="table-responsive">
            <table class="table table-striped table-bordered">
                <thead>
                    <tr>
                        <th>מזהה</th>
                        <th>מזהה משלוח</th>
                        <th>מזהה הזמנה</th>
                        <th>מזהה לקוח</th>
                        <th>כתובת מקור ועיר</th>
                        <th>כתובת יעד ועיר</th>
                        <th>טלפון</th>
                        <th>תאריך איסוף</th>
                        <th>תאריך הגעה מבוקש</th>
                        <th>תאריך מסירה</th>
                        <th>סטטוס</th>
                        <th>תאריך הוספה</th>
                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater ID="RptProd" runat="server">
                        <ItemTemplate>
                            <tr class="odd gradeX">
                                <td><%# Eval("Id") %></td>
                                <td><%# Eval("ShipId") %></td>
                                <td><%# Eval("OrderID") %></td>
                                <td><%# Eval("CustomerID") %></td>
                                <td><%# Eval("sourceAdd") + ", " + Eval("sourceCity") %></td>
                                <td><%# Eval("DestinationAdd") + ", " + Eval("DestinationCity") %></td>
                                <td><%# Eval("Phone") %></td>
                                <td><%# Eval("CollectionDate") %></td>
                                <td><%# Eval("DateDelivery") %></td>
                                <td><%# Eval("ReleaseDate") %></td>
                                <td><%# Eval("Status") %></td>
                                <td><%# Eval("addDate") %></td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
            </table>
        </div>
    </div>
</asp:Content>
