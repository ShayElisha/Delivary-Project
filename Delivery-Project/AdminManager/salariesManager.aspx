<%@ Page Title="" Language="C#" MasterPageFile="~/AdminManager/BackAdmin.Master" AutoEventWireup="true" CodeBehind="salariesManager.aspx.cs" Inherits="Delivery_Project.AdminManager.salariesManager" EnableEventValidation="false" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
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
                                        <th>קוד משכורת</th>
                                        <th>קוד נהג</th>
                                        <th>כמות משלוחים</th>
                                        <th>כמות משלוחים <br>מינימלית</th>
                                        <th> בונוס</th>
                                        <th> כמות תקלות</th>
                                        <th> קנס</th>
                                        <th> משכורת</th>
                                        <th>תאריך הוספה</th>
                                        <th>ניהול</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <asp:Repeater ID="RptProd" runat="server">
                                        <ItemTemplate>
                                            <tr class="odd gradeX">
                                                <td><%#Eval("SalaryID") %></td>
                                                <td><%#Eval("DriverID") %></td>
                                                <td><%#Eval("DeliaryAmount") %></td>
                                                <td>
                                                   <asp:TextBox ID="MINIQU" Text=<%#Eval("MinimumQuantity") %> runat="server" />
                                               </td>
                                                 <td>
                                                        <asp:TextBox ID="TxtBonuse" Text=<%#Eval("Bonuse") %> runat="server" />
                                                    </td>
                                                    <td><%#Eval("faults") %></td>
                                                    <td>
                                                        <asp:TextBox ID="txtReport" Text=<%#Eval("Report") %> runat="server"/>
                                                    </td>
                                                <td><%#Eval("salary") %></td>
                                                <td><%#Eval("AddDate") %></td>
                                                <td><asp:Button ID="btnSave" CssClass="btn btn-primary" runat="server" Text="שמירה" OnClick="btnSave_Click" CommandArgument='<%# Eval("SalaryID") %>'   /></td>
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
