<%@ Page language="c#" Codebehind="ConfigureDataGrid.aspx.cs" AutoEventWireup="false" Inherits="VisualCSharpBlueprint.ConfigureDataGrid" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<meta name="GENERATOR" Content="Microsoft Visual Studio 7.0">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript (ECMAScript)">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="DisplayDataGrid" method="post" runat="server">
			<asp:DataGrid id="dgdTitles" style="Z-INDEX: 101; LEFT: 8px; POSITION: absolute; TOP: 8px" runat="server" BorderStyle="None" BorderWidth="1px" BorderColor="#CCCCCC" BackColor="White" CellPadding="3" PageSize="5" AllowSorting="True" AutoGenerateColumns="False">
				<FooterStyle ForeColor="#000066" BackColor="White"></FooterStyle>
				<HeaderStyle Font-Bold="True" ForeColor="White" BackColor="#006699"></HeaderStyle>
				<PagerStyle HorizontalAlign="Left" ForeColor="#000066" BackColor="White" Mode="NumericPages"></PagerStyle>
				<SelectedItemStyle Font-Bold="True" ForeColor="White" BackColor="#669999"></SelectedItemStyle>
				<AlternatingItemStyle BackColor="Gray"></AlternatingItemStyle>
				<ItemStyle ForeColor="#000066"></ItemStyle>
				<Columns>
					<asp:BoundColumn DataField="title" SortExpression="title" HeaderText="Title"></asp:BoundColumn>
					<asp:BoundColumn DataField="notes" HeaderText="Notes"></asp:BoundColumn>
					<asp:BoundColumn DataField="price" SortExpression="price" HeaderText="Price"></asp:BoundColumn>
				</Columns>
			</asp:DataGrid>
		</form>
	</body>
</HTML>
