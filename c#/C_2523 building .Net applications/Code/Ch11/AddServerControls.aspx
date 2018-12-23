<%@ Page language="c#" Codebehind="AddServerControls.aspx.cs" AutoEventWireup="false" Inherits="VisualCSharpBlueprint.AddServerControls" %>

<HTML>
	<HEAD>
		<meta name=vs_targetSchema content="Internet Explorer 5.0">
		<meta name="GENERATOR" Content="Microsoft Visual Studio 7.0">
		<meta name="CODE_LANGUAGE" Content="C#">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form method="post" runat="server">
			<asp:DropDownList id=DropDownList1 style="Z-INDEX: 101; LEFT: 21px; POSITION: absolute; TOP: 52px" runat="server">
				<asp:ListItem Value="lr_day1_cinderella_castle_3.jpg">
					Cinderalla's Castle
				</asp:ListItem>
				<asp:ListItem Value="lr_day1_electrical_parade_2.jpg">
					Mainstreet's Electrical Parade
				</asp:ListItem>
				<asp:ListItem Value="mickey_end_1.gif">
					Mickey Mouse
				</asp:ListItem>
			</asp:DropDownList>
			<asp:Label id=Label1 style="Z-INDEX: 103; LEFT: 18px; POSITION: absolute; TOP: 23px" runat="server" Width="166px" Height="16px">
				Choose Photo to View
			</asp:Label>
			<asp:Button id=Button1 style="Z-INDEX: 102; LEFT: 248px; POSITION: absolute; TOP: 50px" runat="server" Text="View Photo">
			</asp:Button>
		</form>
	</body>
</HTML>
