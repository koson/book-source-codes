<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .auto-style1
        {
            width: 951px;
        }
        .auto-style2
        {
            width: 951px;
            height: 55px;
            position: absolute;
            left: 10px;
            top: 15px;
        }
        .auto-style3
        {
            height: 55px;
        }
        .auto-style4
        {
            position: absolute;
        }
        .auto-style5
        {
            left: 10px;
        }
        .auto-style6
        {
            top: 15px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server" style="z-index: 1">
    <div class="auto-style2">
        <h1>Mortgage Calculator</h1>
        <asp:Label ID="Label1" runat="server" Font-Size="Medium" 
                style="z-index: 1; left: 18px; top: 78px; position: absolute; width: 136px; right: 797px; text-align: right;" 
                Text="Amount of loan:"></asp:Label>
         <asp:TextBox ID="txtAmount" runat="server" 
                style="z-index: 1; left: 165px; top: 77px; position: absolute">164000</asp:TextBox>

       <asp:Label ID="Label2" runat="server" Font-Size="Medium" 
            style="z-index: 1; left: 4px; top: 101px; position: absolute; width: 148px; right: 799px; text-align: right;" 
            Text="Interest rate (6% = 6): "></asp:Label>
        <asp:TextBox ID="txtInterestRate" runat="server" 
            style="z-index: 1; left: 165px; top: 99px; position: absolute" 
            TabIndex="1">5.875</asp:TextBox>

        <asp:TextBox ID="txtMonths" runat="server" 
            style="z-index: 1; left: 165px; top: 121px; position: absolute" 
            TabIndex="2">360</asp:TextBox>

        <asp:Button ID="btnSubmit" runat="server" 
            style="z-index: 1; left: 16px; top: 172px; position: absolute" 
            Text="Submit" TabIndex="3" OnClick="btnSubmit_Click" />    </div>

    <p>

        <asp:Label ID="lblPayment" runat="server" Font-Size="Medium"            
            style="z-index: 1; left: 27px; top: 232px; position: absolute; width: 136px; right: 811px; height: 18px; text-align: right;" 
            Text="Monthly payment:" Visible="False"></asp:Label>
        </p>

        <asp:Label ID="Label3" runat="server" Font-Size="Medium"            
            style="z-index: 1; left: 27px; top: 136px; position: absolute; width: 136px; right: 808px; height: 18px; text-align: right;" 
            Text="Months:"></asp:Label>
        <asp:TextBox ID="txtPayment" runat="server" 
        style="z-index: 1; left: 165px; top: 227px; position: absolute" 
        Visible="False"></asp:TextBox>

    </form>
</body>
</html>
