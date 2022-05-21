<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMst.Master" AutoEventWireup="true" CodeBehind="AdminHome.aspx.cs" Inherits="ZealEducationManager.Admin.AdminHome" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link href="//maxcdn.bootstrapcdn.com/bootstrap/4.1.1/css/bootstrap.min.css" rel="stylesheet" id="bootstrap-css">
    <script src="//maxcdn.bootstrapcdn.com/bootstrap/4.1.1/js/bootstrap.min.js"></script>
    <script src="//cdnjs.cloudflare.com/ajax/libs/jquery/3.2.1/jquery.min.js"></script>
        
    <div class="admin-home">
        <div class="container p-md-4 p-sm-4">
            <div>
                <asp:Label ID="lblMsg" runat="server"></asp:Label>
            </div>

            <h2 class="text-center">Admin Home Page</h2>
        </div>

        <div class="container">
            <div class="row pt-5">
                <div class="col-md-3">
                    <div class="card-counter primary">
                        <i class="fas fa-user-graduate"></i>
                        <span class="count-numbers"><%Response.Write(Session["student"]); %></span>
                        <span class="count-name">Total Students</span>
                    </div>
                </div>

                <div class="col-md-3">
                    <div class="card-counter danger">
                        <i class="fas fa-chalkboard-teacher"></i>
                        <span class="count-numbers"><%Response.Write(Session["teacher"]); %></span>
                        <span class="count-name">Total Teachers</span>
                    </div>
                </div>

                <div class="col-md-3">
                    <div class="card-counter success">
                        <i class="fas fa-building"></i>
                        <span class="count-numbers"><%Response.Write(Session["class"]); %></span>
                        <span class="count-name">Total Classes</span>
                    </div>
                </div>

                <div class="col-md-3">
                    <div class="card-counter info">
                        <i class="fas fa-book"></i>
                        <span class="count-numbers"><%Response.Write(Session["subject"]); %></span>
                        <span class="count-name">Total Subjects</span>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
