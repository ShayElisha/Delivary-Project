<%@ Page Title="" Language="C#" MasterPageFile="~/DriverSide/DriverSide.Master" AutoEventWireup="true" CodeBehind="HistoryShipments.aspx.cs" Inherits="Delivery_Project.DriverSide.HistoryShipments" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server" dir="rtl">
    <style>
        body {
            font-family: Arial, sans-serif;
            background-color: #f4f4f4;
            margin: 0;
            padding: 0;
        }

        .container {
            width: 95%;
            max-width: 1200px;
            margin: 20px auto;
            padding: 20px;
            border-radius: 10px;
            background-color: #fff;
            box-shadow: 0 4px 8px rgba(0,0,0,0.1);
        }

        h2 {
            text-align: center;
            color: #333;
            margin-bottom: 30px;
        }

        .search-container {
            display: flex;
            justify-content: center;
            align-items: center;
            flex-wrap: wrap;
            gap: 10px;
            margin-bottom: 20px;
        }

        .form-control {
            width: 200px;
            padding: 10px;
            border: 1px solid #ddd;
            border-radius: 5px;
            font-size: 14px;
        }

        .btn {
            padding: 10px 20px;
            font-size: 14px;
            font-weight: bold;
            text-align: center;
            text-decoration: none;
            background-color: #007bff;
            color: #fff;
            border: none;
            border-radius: 5px;
            cursor: pointer;
            transition: background-color 0.3s, transform 0.3s;
            display: block;
            margin: 10px auto 0;
        }

        .btn:hover {
            background-color: #0056b3;
            transform: translateY(-2px);
        }

        table {
            width: 100%;
            border-collapse: collapse;
            margin-top: 20px;
        }

        th, td {
            padding: 12px;
            text-align: right;
            border-bottom: 1px solid #ddd;
        }

        th {
            background-color: #f8f9fa;
            font-weight: bold;
            color: #333;
        }

        tr:hover {
            background-color: #f5f5f5;
        }

        @media (max-width: 768px) {
            .form-control, .btn {
                width: 100%;
            }
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <h2>רשימת הזמנות</h2>
         <div class="row">
            <div class="col-md-6">
                <asp:DropDownList ID="ddlMonth" runat="server" CssClass="form-control"></asp:DropDownList>
            </div>
            <div class="col-md-6">
                <asp:DropDownList ID="ddlYear" runat="server" CssClass="form-control"></asp:DropDownList>
            </div>
            <div class="col-md-12 text-center">
                <asp:Button ID="btnSearch" runat="server" Text="חפש" CssClass="btn btn-primary" OnClick="btnSearch_Click" />
            </div>
        </div>
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
