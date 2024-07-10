<%@ Page Title="" Language="C#" MasterPageFile="~/CustomerSide/CustomerSide.Master" AutoEventWireup="true" CodeBehind="HistoryOrder.aspx.cs" Inherits="Delivery_Project.CustomerSide.HistoryOrder" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .order-history-table {
            width: 100%;
            border-collapse: collapse;
            margin-bottom:90px;
        }

        .order-history-table th, .order-history-table td {
            border: 1px solid #ddd;
            padding: 8px;
        }

        .order-history-table th {
            background-color: #f2f2f2;
            text-align: left;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2>היסטוריית הזמנות</h2>
    <div class="table-responsive">
        <table class="order-history-table">
            <thead>
                <tr>
                    <th>מזהה הזמנה</th>
                    <th>שם מלא</th>
                    <th>אימייל</th>
                    <th>טלפון</th>
                    <th>כתובת</th>
                    <th>עיר</th>
                    <th>כמות</th>
                    <th>הערות</th>
                    <th>זמן משלוח</th>
                </tr>
            </thead>
            <tbody>
                <asp:Repeater ID="RptProd" runat="server">
                    <ItemTemplate>
                        <tr>
                            <td><%# Eval("OrderID") %></td>
                            <td><%# Eval("FullName") %></td>
                            <td><%# Eval("Email") %></td>
                            <td><%# Eval("Phone") %></td>
                            <td><%# Eval("Address") %></td>
                            <td><%# Eval("CityId") %></td>
                            <td><%# Eval("Quantity") %></td>
                            <td><%# Eval("Notes") %></td>
                            <td><%# Eval("Datedelivery") %></td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </tbody>
        </table>
    </div>
    <asp:Panel ID="NoOrdersPanel" runat="server" CssClass="no-orders" Visible="False">
        אין הזמנות ברשימה.
    </asp:Panel>
</asp:Content>
