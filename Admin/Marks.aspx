<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMst.Master" AutoEventWireup="true" CodeBehind="Marks.aspx.cs" Inherits="ZealEducationManager.Admin.Marks" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="admin-home">
        <div class="container p-md-4 p-sm-4">
            <div>
                <asp:Label ID="lblMsg" runat="server"></asp:Label>
            </div>

            <h2 class="text-center">Add marks</h2>

            <div class="row mb-3 mr-lg-5 ml-lg-5 mt-md-5">
                <div class="col-md-6">
                    <label for="ddlClass" class="label-font-size" >Class</label>

                    <asp:DropDownList ID="ddlClass" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlClass_SelectedIndexChanged"></asp:DropDownList>

                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Class is required" ControlToValidate="ddlClass" Display="Dynamic" ForeColor="Red" InitialValue="Select class" 
                        SetFocusOnError="True"></asp:RequiredFieldValidator>
                </div>

                <div class="col-md-6">
                    <label for="ddlSubject" class="label-font-size" >Subject</label>

                    <asp:DropDownList ID="ddlSubject" runat="server" CssClass="form-control"></asp:DropDownList>

                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Subject is required" ControlToValidate="ddlSubject" Display="Dynamic" ForeColor="Red" InitialValue="Select subject" 
                        SetFocusOnError="True"></asp:RequiredFieldValidator>
                </div>

                <div class="col-md-12 mt-2">
                    <label for="txtRoll" class="label-font-size" >Student roll number</label>
                    
                    <asp:TextBox ID="txtRoll" runat="server" CssClass="form-control" placeholder="Enter student roll number" required MaxLength="50"></asp:TextBox>
                </div>

                <div class="col-md-6 mt-2">
                    <label for="txtStudentMarks" class="label-font-size" >Total mark (Student marks)</label>
                    
                    <asp:TextBox ID="txtStudentMarks" runat="server" CssClass="form-control" placeholder="Enter total mark (Student marks)" TextMode="Number" required></asp:TextBox>
                </div>

                <div class="col-md-6 mt-2">
                    <label for="txtOutOfMarks" class="label-font-size" >Out of marks</label>
                    
                    <asp:TextBox ID="txtOutOfMarks" runat="server" CssClass="form-control" placeholder="Enter out of marks" TextMode="Number" required></asp:TextBox>
                </div>
            </div>

            <div class="row mb-3 mr-lg-5 ml-lg-5">
                <div class="col-md-12 col-md-offset-2 mb-3">
                    <asp:Button ID="btnAdd" runat="server" CssClass="col-md-12 col-sm-12 btn btn-primary btn-block btn-bg-gradiant" Text="Add marks" OnClick="btnAdd_Click"/>
                </div>                
            </div>

            <div class="row mb-3 mr-lg-5 ml-lg-5">
                <div class="col-md-12">
                    <asp:GridView ID="GridView1" runat="server" CssClass="table-font-size-17-bold table table-hover table-bordered" EmptyDataText="No record to display" AutoGenerateColumns="False" AllowPaging="True" PageSize="5"
                        OnPageIndexChanging="GridView1_PageIndexChanging" DataKeyNames="ExamId" OnRowCancelingEdit="GridView1_RowCancelingEdit" OnRowEditing="GridView1_RowEditing" 
                        OnRowUpdating="GridView1_RowUpdating" OnRowDeleting="GridView1_RowDeleting" OnRowDataBound="GridView1_RowDataBound">
                        <Columns>
                            <asp:BoundField DataField="Sr.No" HeaderText="Sr.No" ReadOnly="True">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>

                            <asp:TemplateField HeaderText="Class">
                                <EditItemTemplate>
                                    <asp:DropDownList ID="ddlClassGv" runat="server" DataSourceID="SqlDataSource1" DataTextField="ClassName" DataValueField="ClassId" SelectedValue='<%# Eval("ClassId") %>' CssClass="form-control"
                                        AutoPostBack="true" OnSelectedIndexChanged="ddlClassGv_SelectedIndexChanged">
                                        <asp:ListItem>Select class</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ZealEducationManager %>" SelectCommand="SELECT * FROM [Class]"></asp:SqlDataSource>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label2" runat="server" Text='<%# Eval("ClassName") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Subject">
                                <EditItemTemplate>
                                    <asp:DropDownList ID="ddlSubjectGv" runat="server" CssClass="form-control"></asp:DropDownList>
                                </EditItemTemplate>

                                <ItemTemplate>
                                    <asp:Label ID="Label1" runat="server" Text='<%# Eval("SubjectName") %>'></asp:Label>
                                </ItemTemplate>

                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Roll number">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtRollNoGv" runat="server" CssClass="form-control" Text='<%# Eval("RollNo") %>' MaxLength="50"></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label3" runat="server" Text='<%# Eval("RollNo") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Total marks">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtStudMarksGv" runat="server" CssClass="form-control" Text='<%# Eval("TotalMarks") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label3" runat="server" Text='<%# Eval("TotalMarks") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Out of marks">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtOutOfMarksGv" runat="server" CssClass="form-control" Text='<%# Eval("OutOfMarks") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label3" runat="server" Text='<%# Eval("OutOfMarks") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:TemplateField>

                            <asp:CommandField CausesValidation="false" HeaderText="Operation" ShowEditButton="True" ShowDeleteButton="True">
                                <ItemStyle HorizontalAlign="Center" CssClass="Operation"/>
                            </asp:CommandField>
                        </Columns>

                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" BackColor="#ac32e4" ForeColor="White"/>

                        <PagerStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass = "GridPager" />
                    </asp:GridView>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
