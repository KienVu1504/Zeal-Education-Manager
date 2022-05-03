<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMst.Master" AutoEventWireup="true" CodeBehind="AddClass.aspx.cs" Inherits="ZealEducationManager.Admin.AddClass" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="admin-home">
        <div class="container p-md-4 p-sm-4">
            <div>
                <asp:Label ID="lblMsg" runat="server"></asp:Label>
            </div>

            <h2 class="text-center">New Class</h2>

            <div class="row mb-3 mr-lg-5 ml-lg-5 mt-md-5">
                <div class="col-md-12">
                    <label class="label-font-size" for="txtClass">Class Name</label>

                    <asp:TextBox ID="txtClass" runat="server" CssClass="form-control" placeholder="Enter Class Name" required></asp:TextBox>
                </div>
            </div>

            <div class="row mb-3 mr-lg-5 ml-lg-5">
                <div class="col-md-12 col-md-offset-2 mb-3">
                    <asp:Button ID="btnAdd" runat="server" CssClass="col-md-12 col-sm-12 btn btn-primary btn-block btn-bg-gradiant" Text="Add Class" OnClick="btnAdd_Click" />
                </div>                
            </div>

            <div class="row mb-3 mr-lg-5 ml-lg-5">
                <div class="col-md-12">
                    <asp:GridView ID="GridView1" runat="server" CssClass="table-font-size-17-bold table table-hover table-bordered" DataKeyNames="ClassID" AutoGenerateColumns="False" EmptyDataText="No Record to display" 
                        OnPageIndexChanging="GridView1_PageIndexChanging" OnRowCancelingEdit="GridView1_RowCancelingEdit" OnRowEditing="GridView1_RowEditing" OnRowUpdating="GridView1_RowUpdating" AllowPaging="True" PageSize="5" 
                        OnRowDeleting="GridView1_RowDeleting">
                        <Columns>
                            <asp:BoundField DataField="Sr.No" HeaderText="Sr.No" ReadOnly="True">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>

                            <asp:TemplateField HeaderText="Class">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtClassEdit" runat="server" Text='<%# Eval("ClassName") %>' CssClass="form-control"></asp:TextBox>
                                </EditItemTemplate>

                                <ItemTemplate>
                                    <asp:Label ID="lblClassName" runat="server" Text='<%# Eval("ClassName") %>'></asp:Label>
                                </ItemTemplate>

                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>

                            <asp:CommandField CausesValidation="False" HeaderText="Operation" ShowEditButton="True" ShowDeleteButton="True">
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="Operation"/>
                            </asp:CommandField>
                        </Columns>

                        <HeaderStyle HorizontalAlign = "Center" BackColor="#ac32e4" ForeColor="White"/>

                        <PagerStyle HorizontalAlign = "Center" CssClass = "GridPager" />
                    </asp:GridView>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
