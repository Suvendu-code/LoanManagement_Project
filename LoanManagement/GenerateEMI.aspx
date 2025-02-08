<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GenerateEMI.aspx.cs" Inherits="LoanManagement.GenerateEMI" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>EMI Schedule</title>
    <link href="https://maxcdn.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css" rel="stylesheet" />
    <style>
        .container {
            margin-top: 20px;
        }

        .form-group label {
            font-weight: bold;
        }

        .table-container {
            background-color: #f8f9fa;
            padding: 20px;
            border-radius: 10px;
        }

        .btn-primary {
            margin-top: 10px;
        }
    </style>
</head>
<body>
    <form id="form2" runat="server">
        <div class="container">
            <div class="row">
                <!-- Left Section -->
                <div class="col-md-6">
                    <h3>EMI Schedule</h3>
                    <div class="form-group">
                        <label for="ddlPlanName">Select Plan Name</label>
                        <asp:DropDownList ID="ddlPlanName" runat="server" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddlPlanName_SelectedIndexChanged">
                            <asp:ListItem Text="--Select Scheme--" Value="" />
                        </asp:DropDownList>

                    </div>
                    <div class="form-group">
                        <label for="txtTenure">Tenure</label>
                        <asp:TextBox ID="txtTenure" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <label for="txtROI">ROI</label>
                        <asp:TextBox ID="txtROI" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <label for="txtLoanAmount">Enter Loan Amount</label>
                        <asp:TextBox ID="TextBox1" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <label for="txtLoanDate">Loan Date</label>
                        <asp:TextBox ID="TextBox2" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <asp:Button ID="btnCalculateEMI" runat="server" Text="Calculate EMI" CssClass="btn btn-primary" OnClick="btnCalculateEMI_Click" />
                    </div>
                    <div class="form-group">
                        <label for="txtEMIAmount">EMI Amount</label>
                        <asp:TextBox ID="txtEMIAmount" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <asp:Button ID="btnGenerateSchedule" runat="server" Text="Generate Schedule" CssClass="btn btn-success" OnClick="btnGenerateSchedule_Click" />
                    </div>
                </div>


                <div class="col-md-6 table-container">
                    <asp:GridView ID="gvEMISchedule" runat="server" CssClass="table table-bordered" AutoGenerateColumns="false">
                        <Columns>
                            <asp:BoundField DataField="EMINo" HeaderText="EMI No" />
                            <asp:BoundField DataField="DueDate" HeaderText="Due Date" />
                            <asp:BoundField DataField="EMIAmount" HeaderText="EMI Amount" />
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
