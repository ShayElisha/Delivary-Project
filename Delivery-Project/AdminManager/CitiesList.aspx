<%@ Page Title="" Language="C#" MasterPageFile="~/AdminManager/BackAdmin.Master" AutoEventWireup="true" CodeBehind="CitiesList.aspx.cs" Inherits="Delivery_Project.AdminManager.CitiesList" %>
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
    background-color: var(--background-color);
    color: black;
}

.panel-default {
    border-color: var(--primary-color);
}

.panel-heading {
    background-color: var(--secondary-color);
    color: var(--text-color);
    border-color: var(--primary-color);
}

.table thead th {
    background-color: var(--secondary-color);
    color: var(--text-color);
}

.table tbody tr:nth-child(odd) {
    background-color: var(--glow-color);
}

.table tbody tr:hover {
    background-color: var(--primary-color);
    color: var(--text-color);
}

.btn-primary {
    background-color: var(--primary-color);
    border-color: var(--primary-color);
}

.btn-primary:hover {
    background-color: var(--accent-color);
    border-color: var(--accent-color);
}

.form-control {
    background-color: var(--secondary-color);
    color: var(--text-color);
    border-color: var(--primary-color);
}

.form-control::placeholder {
    color: var(--accent-color);
}

label {
    color: var(--primary-color);
}
.panel-body{padding:0}

    </style>
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
                            מאגר ערים במערכת
                        </div>
                        <!-- /.panel-heading -->
                        <div class="panel-body">
                            <div class="table-responsive">
                                <table class="table table-striped table-bordered table-hover" id="mainTbl">
                                    <thead>
                                        <tr>
                                            <th>קוד עיר</th>
                                            <th>שם עיר</th>
                                            <th>סטטוס</th>
                                            <th>תאריך הוספה</th>
                                            <th> פעולות</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <asp:Repeater ID="RptProd" runat="server">
                                            <ItemTemplate>
                                                <tr class="odd gradeX">
                                                    <td><%#Eval("CityId") %></td>
                                                    <td><%#Eval("CityName") %></td>
                                                    <td><%#Eval("status") %></td>
                                                    <td><%#Eval("AddDate") %></td>
                                                    <td class="center"><a href="citiesAddEdit.aspx?CityId=<%#Eval("CityId") %>">ערוך <spann class="fa fa-pencil"></spann></a></td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater> 
                                    </tbody>
                                </table>
                            </div>
                            <!-- /.table-responsive -->
                         
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

