<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MarksDetailUserControl.ascx.cs" Inherits="ZealEducationManager.MarksDetailUserControl" %>

<div class="admin-home">
    <div class="container p-md-4 p-sm-4">
        <div>
            <asp:Label ID="lblMsg" runat="server"></asp:Label>
        </div>
             
        <h2 class="text-center">Marks detail</h2>

        <div class="row mb-3 mr-lg-5 ml-lg-5 mt-md-5">
            <div class="col-md-6">
                <label for="ddlClass" class="label-font-size" >Class</label>

                <asp:DropDownList ID="ddlClass" runat="server" CssClass="form-control"></asp:DropDownList>

                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Class is required" ControlToValidate="ddlClass" Display="Dynamic" ForeColor="Red" InitialValue="Select class" 
                    SetFocusOnError="True"></asp:RequiredFieldValidator>
            </div>

            <div class="col-md-6">
                <label for="txtRoll" class="label-font-size" >Student roll number</label>

                <asp:TextBox ID="txtRoll" runat="server" CssClass="form-control" placeholder="Enter student roll number" required></asp:TextBox>
            </div>
        </div>

        <div class="row mb-3 mr-lg-5 ml-lg-5">
            <div class="col-md-12 col-md-offset-2 mb-3">
                <asp:Button ID="btnAdd" runat="server" CssClass="col-md-12 col-sm-12 btn btn-primary btn-block btn-bg-gradiant" Text="Get marks" OnClick="btnAdd_Click"/>
            </div>                
        </div>

        <div class="row mb-3 mr-lg-5 ml-lg-5">
            <div class="col-md-12" style="overflow:auto">
                <asp:GridView ID="GridView1" runat="server" CssClass="table-font-size-17-bold table table-hover table-bordered" EmptyDataText="No record to display" AutoGenerateColumns="False" AllowPaging="true" PageSize="5"
                    OnPageIndexChanging="GridView1_PageIndexChanging">
                    <Columns>
                        <asp:BoundField DataField="Sr.No" HeaderText="Sr.No">
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"/>
                        </asp:BoundField>

                        <asp:BoundField DataField="ExamId" HeaderText="ExamId">
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:BoundField>

                        <asp:BoundField DataField="ClassName" HeaderText="Class" >
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:BoundField>

                        <asp:BoundField DataField="SubjectName" HeaderText="Subject" >
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:BoundField>

                        <asp:BoundField DataField="RollNo" HeaderText="Roll number" >
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:BoundField>

                        <asp:BoundField DataField="TotalMarks" HeaderText="Total marks" >
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:BoundField>

                        <asp:BoundField DataField="OutOfMarks" HeaderText="Out of marks" >
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