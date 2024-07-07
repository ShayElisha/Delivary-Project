<%@ Page Title="" Language="C#" MasterPageFile="~/AdminManager/BackAdmin.Master" AutoEventWireup="true" CodeBehind="ManagerList.aspx.cs" Inherits="Delivery_Project.AdminManager.ManagerList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainCnt" runat="server">
    <div class="row">
        <div class="col-lg-12">
            <h1 class="page-header">ניהול משתמשים</h1>
        </div>
        <!-- /.col-lg-12 -->
    </div>
    <!-- /.row -->
    <div class="row">
        <div class="col-lg-12">
            <div class="panel panel-default">
                <div class="panel-heading">
                    מאגר משתמשים במערכת
                </div>
                <!-- /.panel-heading -->
                <div class="panel-body">
                    <div class="table-responsive">
                        <table class="table table-striped table-bordered table-hover" id="mainTbl">
                            <thead>
                                <tr>
                                    <th> מזהה מנהל</th>
                                    <th>שם מלא</th>
                                    <th>אימייל</th>
                                    <th>סיסמה</th>
                                    <th>טלפון</th>
                                    <th>כתובת</th>
                                    <th>מזהה עיר</th>
                                    <th>סטטוס</th>
                                    <th>תאריך הוספה</th>
                                    <th>ניהול</th>
                                </tr>
                            </thead>
                            <tbody>
                                <asp:Repeater ID="RptProd" runat="server" OnItemCommand="RptAddresses_ItemCommand">
                                    <ItemTemplate>
                                        <tr class="odd gradeX">
                                            <td><%#Eval("ManagerID") %></td>
                                            <td><%#Eval("FullName") %></td>
                                            <td><%#Eval("Email") %></td>
                                            <td><%#Eval("Password") %></td>
                                            <td><%#Eval("Phone") %></td>
                                            <td><%#Eval("Address") %></td>
                                            <td><%#Eval("CityId") %></td>
                                            <td><%#Eval("status") %></td>
                                            <td><%#Eval("AddDate") %></td>
                                            <td class="center"><a href="ManagerAddEdit.aspx?ManagerId=<%#Eval("ManagerID") %>">ערוך <span class="fa fa-pencil"></span></a>                                      
                                            <asp:LinkButton ID="lnkDelete" runat="server" CommandName="Delete" CommandArgument='<%# Eval("ManagerID") %>' Text="מחק" OnClientClick="return confirm('האם אתה בטוח שברצונך למחוק כתובת זו?');"></asp:LinkButton>                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater> 
                            </tbody>
                        </table>
                    </div>
                    <!-- /.table-responsive -->
                    <div class="well">
                        <h4>DataTables Usage Information</h4>
                        <p>DataTables is a very flexible, advanced tables plugin for jQuery. In SB Admin, we are using a specialized version of DataTables built for Bootstrap 3. We have also customized the table headings to use Font Awesome icons
                            in place of images. For complete documentation on DataTables, visit their website at <a target="_blank" href="https://datatables.net/">https://datatables.net/</a>.</p>
                        <a class="btn btn-default btn-lg btn-block" target="_blank" href="https://datatables.net/">View DataTables Documentation</a>
                    </div>
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
