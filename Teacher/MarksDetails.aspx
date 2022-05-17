<%@ Page Title="" Language="C#" MasterPageFile="~/Teacher/TeacherMst.Master" AutoEventWireup="true" CodeBehind="MarksDetails.aspx.cs" Inherits="ZealEducationManager.Teacher.MarksDetails" %>

<%@ Register Src="~/MarksDetailUserControl.ascx" TagPrefix="uc" TagName="MarksDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc:MarksDetail runat="server" ID="MarksDetail"/>
</asp:Content>
