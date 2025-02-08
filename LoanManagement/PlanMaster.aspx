<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PlanMaster.aspx.cs" Inherits="LoanManagement.PlanMaster" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <title>Create Scheme</title>
    <link href="https://maxcdn.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css" rel="stylesheet" />
    <style>
        .container {
            margin-top: 50px;
            max-width: 400px;
            background-color: #f8f9fa;
            padding: 20px;
            border-radius: 8px;
            box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
        }
        .form-group label {
            font-weight: bold;
        }
        .btn-success {
            width: 100%;
        }
    </style>
</head>
<body>
 <form id="form1" runat="server">
        <div class="container">
            <h3>Create Scheme</h3>
            <div class="form-group">
                <label for="txtPlanName">Plan Name</label>
                <asp:TextBox ID="txtPlanName" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="form-group">
                <label for="txtTenure">Tenure (Months)</label>
                <asp:TextBox ID="txtTenure" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="form-group">
                <label for="txtROI">ROI (%)</label>
                <asp:TextBox ID="txtROI" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
            <asp:Button ID="btnSave" runat="server" CssClass="btn btn-success" Text="Save" OnClick="btnSave_Click" />
        </div>
    </form>
</body>
</html>


