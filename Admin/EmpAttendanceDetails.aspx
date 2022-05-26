<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMst.Master" AutoEventWireup="true" CodeBehind="EmpAttendanceDetails.aspx.cs" Inherits="ZealEducationManager.Admin.EmpAttendanceDetails" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="admin-home">
        <div class="container p-md-4 p-sm-4">
            <div>
                <asp:Label ID="lblMsg" runat="server"></asp:Label>
            </div>

            <h2 class="text-center">Teacher attendance details</h2>

            <div class="row mb-3 mr-lg-5 ml-lg-5 mt-md-5">
                <div class="col-md-6">
                    <label for="ddlClass" class="label-font-size" >Teacher</label>

                    <label for="ddlTeacher" class="label-font-size" >Teacher</label>

                    <asp:DropDownList ID="ddlTeacher" runat="server" CssClass="form-control"></asp:DropDownList>

                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Teacher is required" ControlToValidate="ddlTeacher" Display="Dynamic" ForeColor="Red" InitialValue="Select teacher" 
                        SetFocusOnError="True"></asp:RequiredFieldValidator>
                </div>

                <div class="col-md-6">
                    <label for="ddlSubject" class="label-font-size" >Month</label>

                    <asp:TextBox ID="txtMonth" CssClass="form-control" runat="server" TextMode="Month" required></asp:TextBox>
                </div>
            </div>

            <div class="row mb-3 mr-lg-5 ml-lg-5">
                <div class="col-md-12 col-md-offset-2 mb-3">
                    <asp:Button ID="btnCheckAttendance" runat="server" CssClass="col-md-12 col-sm-12 btn btn-primary btn-block btn-bg-gradiant" Text="Check attendance" OnClick="btnCheckAttendance_Click"/>
                </div>                
            </div>

            <div class="row mb-3 mr-lg-5 ml-lg-5">
                <div class="col-md-12">
                    <asp:GridView ID="GridView1" runat="server" CssClass="table-font-size-17-bold table table-hover table-bordered" EmptyDataText="No record to display" AutoGenerateColumns="False">
                        <Columns>
                            <asp:BoundField DataField="Sr.No" HeaderText="Sr.No">
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"/>
                            </asp:BoundField>

                            <asp:BoundField DataField="Name" HeaderText="Name">
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:BoundField>

                            <asp:TemplateField HeaderText="Status">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="label1" Text='<%# Boolean.Parse(Eval("Status").ToString()) ? "Present" : "Absent" %>'></asp:Label>
                                </ItemTemplate>

                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:TemplateField>

                            <asp:BoundField DataField="Date" HeaderText="Date" DataFormatString="{0:dd-MM-yyyy}">
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:BoundField>
                        </Columns>

                        <HeaderStyle HorizontalAlign = "Center" VerticalAlign="Middle" BackColor="#ac32e4" ForeColor="White"/>

                        <PagerStyle HorizontalAlign = "Center" VerticalAlign="Middle" CssClass = "GridPager" />
                    </asp:GridView>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
