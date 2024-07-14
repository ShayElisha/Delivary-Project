<%@ Page Title="" Language="C#" MasterPageFile="~/AdminManager/BackAdmin.Master" AutoEventWireup="true" CodeBehind="mainPage.aspx.cs" Inherits="Delivery_Project.AdminManager.mainPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
       :root {
    --primary-color: #c0b283;
    --secondary-color: #373737;
    --background-color: #1c1c1c;
    --text-color: #f4f4f4;
    --accent-color: #dcd0c0;
    --glow-color: rgba(192, 178, 131, 0.3);
}

body { 
    font-family: 'Montserrat', sans-serif;
    background-color: var(--background-color); 
    color: var(--text-color);
    overflow-x: hidden;
}

.admin-container { 
    max-width: 100%; 
    margin: 0 auto; 
    padding: 20px;
    background:var(--background-color);
    position: relative;
}

.admin-container::before {
    content: '';
    position: absolute;
    top: 0;
    left: 0;
    right: 0;
    bottom: 0;
    background: linear-gradient(45deg, var(--primary-color), var(--secondary-color));
    filter: blur(20px);
    opacity: 0.1;
    z-index: -1;
}

.admin-header { 
    text-align: center; 
    margin-bottom: 30px; 
    color: var(--primary-color);
    text-shadow: 0 0 10px var(--glow-color);
    animation: subtle-pulse 4s infinite;
}

@keyframes subtle-pulse {
    0% { opacity: 0.9; }
    50% { opacity: 1; }
    100% { opacity: 0.9; }
}

.admin-summary { 
    display: flex; 
    justify-content: space-around; 
    margin-bottom: 30px;
}

.summary-item { 
    text-align: center; 
    background-color: rgba(255, 255, 255, 0.05);
    padding: 15px; 
    border-radius: 8px; 
    box-shadow: 0 0 15px rgba(0, 0, 0, 0.2);
    transition: all 0.3s ease;
}

.summary-item:hover {
    transform: translateY(-5px);
    box-shadow: 0 5px 15px rgba(0, 0, 0, 0.3);
}

.summary-item h3 { 
    color: var(--primary-color);
}

.admin-table { 
    width: 100%; 
    border-collapse: separate; 
    border-spacing: 0; 
    margin-bottom: 20px;
}

.admin-table th, .admin-table td { 
    border: 1px solid var(--accent-color);
    padding: 12px; 
    text-align: right;
}

.admin-table th { 
    background-color: var(--secondary-color); 
    color: var(--primary-color);
}

.admin-table tr:hover {
    background-color: rgba(255, 255, 255, 0.05);
}

.filter-section { 
    margin-bottom: 20px; 
    background-color: rgba(255, 255, 255, 0.05);
    padding: 15px; 
    border-radius: 8px; 
    box-shadow: 0 0 15px rgba(0, 0, 0, 0.2);
}

.filter-section input[type="date"], 
.filter-section input[type="submit"] { 
    padding: 8px; 
    margin-right: 10px;
    background-color: var(--secondary-color);
    color: var(--text-color);
    border: 1px solid var(--accent-color);
    border-radius: 4px;
}

.filter-section input[type="submit"] { 
    background-color: var(--primary-color);
    color: var(--background-color);
    border: none; 
    cursor: pointer;
    transition: all 0.3s ease;
}

.filter-section input[type="submit"]:hover {
    background-color: var(--accent-color);
    box-shadow: 0 0 10px var(--glow-color);
}

.table-container { 
    max-height: 400px; 
    overflow-y: auto; 
    margin-bottom: 30px;
    scrollbar-width: thin;
    scrollbar-color: var(--primary-color) var(--background-color);
}

.table-container::-webkit-scrollbar {
    width: 8px;
}

.table-container::-webkit-scrollbar-track {
    background: var(--background-color);
}

.table-container::-webkit-scrollbar-thumb {
    background-color: var(--primary-color);
    border-radius: 20px;
    border: 2px solid var(--background-color);
}

.section-title { 
    color: var(--primary-color);
    border-bottom: 2px solid var(--primary-color);
    padding-bottom: 10px; 
    margin-top: 30px;
    text-shadow: 0 0 5px var(--glow-color);
}
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainCnt" runat="server">
    <div class="admin-container">
        <div class="admin-header">
            <h1>לוח בקרה למנהל</h1>
        </div>

        <div class="admin-summary">
            <div class="summary-item">
                <h3>סה"כ הזמנות</h3>
                <asp:Label ID="lblTotalOrders" runat="server"></asp:Label>
            </div>
            <div class="summary-item">
                <h3>כמות כוללת של<br> חבילות</h3>
                <asp:Label ID="lblTotalRevenue" runat="server"></asp:Label>
            </div>
            <div class="summary-item">
                <h3>כמות כוללת של<br> משלוחים</h3>
                <asp:Label ID="lblTotalOrder" runat="server"></asp:Label>
            </div>
            <div class="summary-item">
                <h3>לקוחות פעילים</h3>
                <asp:Label ID="lblActiveCustomers" runat="server"></asp:Label>
            </div>
            <div class="summary-item">
                <h3>הזמנות היום</h3>
                <asp:Label ID="lblTodayOrders" runat="server"></asp:Label>
            </div>
        </div>

        <div class="filter-section">
            <h3>סינון הזמנות</h3>
            <asp:TextBox ID="txtStartDate" runat="server" TextMode="Date"></asp:TextBox>
            <asp:TextBox ID="txtEndDate" runat="server" TextMode="Date"></asp:TextBox>
            <asp:Button ID="btnFilter" runat="server" Text="סנן"  />
        </div>

        <h2 class="section-title">הזמנות אחרונות</h2>
        <div class="table-container">
            <asp:Repeater ID="rptRecentOrders" runat="server">
                <HeaderTemplate>
                    <table class="admin-table">
                        <thead>
                            <tr>
                                <th>מס' הזמנה</th>
                                <th>שם לקוח</th>
                                <th>תאריך</th>
                                <th>כמות</th>
                                <th>סטטוס</th>
                            </tr>
                        </thead>
                        <tbody>
                </HeaderTemplate>
                <ItemTemplate>
                            <tr>
                                <td><%# Eval("OrderID") %></td>
                                <td><%# Eval("FullName") %></td>
                                <td><%# Eval("OrderDate", "{0:dd/MM/yyyy}") %></td>
                                <td><%# Eval("Quantity" ) %></td>
                                <td><%# Eval("status" ) %></td>

                            </tr>
                </ItemTemplate>
                <FooterTemplate>
                        </tbody>
                        <tfoot>
                            <tr style="background-color: #3498db; color: White; font-weight: bold;">
                                <td colspan="5"></td>
                            </tr>
                        </tfoot>
                    </table>
                </FooterTemplate>
            </asp:Repeater>
        </div>

        <h2 class="section-title">נהגים פעילים</h2>
        <div class="table-container">
            <asp:Repeater ID="rptDrivers" runat="server">
                <HeaderTemplate>
                    <table class="admin-table">
                        <thead>
                            <tr>
                                <th>מס' נהג</th>
                                <th>שם הנהג</th>
                                <th>הזמנות פעילות</th>
                                <th>סה"כ משלוחים</th>
                                <th>דירוג</th>
                            </tr>
                        </thead>
                        <tbody>
                </HeaderTemplate>
                <ItemTemplate>
                            <tr>
                                <td><%# Eval("Id") %></td>
                                <td><%# Eval("FullName") %></td>
                                <td><%# Eval("TotalDeliveries") %></td>
                                <td><%# Eval("TotalDeliver") %></td>
                                <td><%# Eval("RatingDriver") %>&#9734;</td>

                            </tr>
                </ItemTemplate>
                <FooterTemplate>
                        </tbody>
                        <tfoot>
                            <tr style="background-color: #3498db; color: White; font-weight: bold;">
                                <td colspan="5"></td>
                            </tr>
                        </tfoot>
                    </table>
                </FooterTemplate>
            </asp:Repeater>
        </div>

        <h2 class="section-title">מנהלים</h2>
        <div class="table-container">
            <asp:Repeater ID="rptManagers" runat="server">
                <HeaderTemplate>
                    <table class="admin-table">
                        <thead>
                            <tr>
                                <th>מס' מנהל</th>
                                <th>שם המנהל</th>
                                <th>מחלקה</th>
                            </tr>
                        </thead>
                        <tbody>
                </HeaderTemplate>
                <ItemTemplate>
                            <tr>
                                <td><%# Eval("ManagerID") %></td>
                                <td><%# Eval("FullName") %></td>
                            </tr>
                </ItemTemplate>
                <FooterTemplate>
                        </tbody>
                        <tfoot>
                            <tr style="background-color: #3498db; color: White; font-weight: bold;">
                                <td colspan="5"></td>
                            </tr>
                        </tfoot>
                    </table>
                </FooterTemplate>
            </asp:Repeater>
        </div>
    </div>
</asp:Content>
