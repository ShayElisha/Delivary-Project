<%@ Page Title="" Language="C#" MasterPageFile="~/CustomerSide/CustomerSide.Master" AutoEventWireup="true" CodeBehind="OrderPage.aspx.cs" Inherits="Delivery_Project.CustomerSide.OrderPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>דף הזמנה</title>
    <style>
        body {
            font-family: Arial, sans-serif;
            background-color: #f4f4f4;
            margin: 0;
            padding: 0;
        }

        .container {
            width: 50%;
            margin: 50px auto;
            padding: 20px;
            border: 1px solid #ccc;
            border-radius: 10px;
            background-color: #fff;
            box-shadow: 0 4px 8px rgba(0,0,0,0.1);
        }

        h2 {
            text-align: center;
            color: #333;
            margin-bottom: 20px;
        }

        .form-group {
            margin-bottom: 20px;
        }

        .form-group label {
            display: block;
            margin-bottom: 5px;
            color: #555;
        }

        .form-group input[type="text"],
        .form-group input[type="email"],
        .form-group input[type="number"],
        .form-group textarea {
            width: 100%;
            padding: 12px;
            border: 1px solid #ddd;
            border-radius: 5px;
            box-sizing: border-box;
            transition: all 0.3s ease;
        }

        .form-group input[type="text"]:focus,
        .form-group input[type="email"]:focus,
        .form-group input[type="number"]:focus,
        .form-group textarea:focus {
            border-color: #007bff;
            box-shadow: 0 0 5px rgba(0,123,255,0.2);
        }

        .form-group button {
            width: 100%;
            padding: 15px;
            border: none;
            border-radius: 5px;
            background-color: #007bff;
            color: white;
            font-size: 16px;
            cursor: pointer;
            transition: background-color 0.3s ease;
        }

        .form-group button:hover {
            background-color: #0056b3;
        }

        .form-group .error {
            color: #ff0000;
            font-size: 14px;
        }

        .form-group .success {
            color: #28a745;
            font-size: 14px;
        }

        .form-group .loader {
            display: none;
            margin: 10px auto;
            border: 4px solid #f3f3f3;
            border-radius: 50%;
            border-top: 4px solid #007bff;
            width: 30px;
            height: 30px;
            -webkit-animation: spin 1s linear infinite;
            animation: spin 1s linear infinite;
        }

        @-webkit-keyframes spin {
            0% { -webkit-transform: rotate(0deg); }
            100% { -webkit-transform: rotate(360deg); }
        }

        @keyframes spin {
            0% { transform: rotate(0deg); }
            100% { transform: rotate(360deg); }
        }
    </style>
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

            $('#ddlDeliveryTime').change(function () {
                updateExpectedDeliveryDate();
            });
        });

        function showLoader() {
            document.getElementById("loader").style.display = "block";
        }

        function updateExpectedDeliveryDate() {
            const ddlDeliveryTime = document.getElementById("<%= ddlDeliveryTime.ClientID %>");
            const selectedValue = parseInt(ddlDeliveryTime.value, 10);
            const expectedDate = new Date();
            expectedDate.setDate(expectedDate.getDate() + selectedValue);

            const options = { year: 'numeric', month: 'long', day: 'numeric' };
            document.getElementById("expectedDeliveryDate").innerText = expectedDate.toLocaleDateString('he-IL', options);
            document.getElementById("<%= hiddenExpectedDeliveryDate.ClientID %>").value = expectedDate.toISOString().split('T')[0];
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <asp:HiddenField ID="cusID" runat="server" />

        <h2>בצע הזמנה</h2>
        <asp:HiddenField ID="hidCid" runat="server" Value="-1"/>
        <div class="form-group">
            <label for="FullName">שם מלא:</label>
            <asp:TextBox ID="FullName" runat="server" CssClass="form-control" Placeholder="הזן את שמך המלא"></asp:TextBox>
        </div>
        <div class="form-group">
            <label for="txtEmail">אימייל:</label>
            <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" Placeholder="הזן את האימייל שלך"></asp:TextBox>
        </div>
        <div class="form-group">
            <label for="txtPhone">טלפון:</label>
            <asp:TextBox ID="txtPhone" runat="server" CssClass="form-control" Placeholder="הזן את מספר הטלפון שלך"></asp:TextBox>
        </div>
        <div class="form-group">
            <label for="txtAddress">כתובת:</label>
            <asp:TextBox ID="txtAddress" runat="server" CssClass="form-control" Placeholder="הזן את הכתובת למשלוח"></asp:TextBox>
        </div>
       <div class="form-group">
            <label for="ddlCity">עיר:</label>
            <asp:DropDownList ID="DDLcity" runat="server" CssClass="form-control"></asp:DropDownList>
        </div>
        <div class="form-group">
            <label for="txtQuantity">כמות:</label>
            <asp:TextBox ID="txtQuantity" runat="server" CssClass="form-control" Placeholder="הזן את הכמות" TextMode="Number"></asp:TextBox>
        </div>
        <div class="form-group">
            <label for="txtNotes">הערות:</label>
            <asp:TextBox ID="txtNotes" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="4" Placeholder="הזן הערות נוספות להזמנה"></asp:TextBox>
        </div>
        <div class="form-group">
            <label for="ddlDeliveryTime">זמן משלוח:</label>
            <asp:DropDownList ID="ddlDeliveryTime" runat="server" CssClass="form-control" OnChange="updateExpectedDeliveryDate()">
                <asp:ListItem Text="בחר זמן משלוח" Value="0" Selected="True"></asp:ListItem>
                <asp:ListItem Text="21 ימים" Value="21"></asp:ListItem>
                <asp:ListItem Text="7 ימים" Value="7"></asp:ListItem>
                <asp:ListItem Text="3 ימים" Value="3"></asp:ListItem>
            </asp:DropDownList>
            <p>תאריך צפוי: <span id="expectedDeliveryDate"></span></p>
            <asp:HiddenField ID="hiddenExpectedDeliveryDate" runat="server" />
        </div>
        <div class="form-group">
            <asp:Button ID="btnSubmit" runat="server" CssClass="btn btn-primary" OnClick="btnSubmit_Click" Text="שלח הזמנה" />
            <div id="loader" class="loader"></div>
        </div>
        <asp:Literal ID="LtlMsg" runat="server" EnableViewState="false"></asp:Literal>
    </div>
</asp:Content>
