<%@ Page language="c#" Codebehind="ReadingAndChangingProperties.aspx.cs" AutoEventWireup="false" Inherits="VisualCSharpBlueprint.photos" %>

<HTML>
	<HEAD>
		<meta name=vs_targetSchema content="Internet Explorer 5.0">
		<meta name="GENERATOR" Content="Microsoft Visual Studio 7.0">
		<meta name="CODE_LANGUAGE" Content="C#">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form method="post" runat="server">
			<asp:DropDownList id=cboPhotoList runat="server">
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
			<asp:Button id=cmdChangePhoto runat="server" Text="Change Photo">
			</asp:Button>
			<hr>
			<asp:Image ID="imgPhoto" Runat="server" ImageUrl="images/lr_day1_cinderella_castle_3.jpg">
			</asp:Image>
		</form>
	</body>
</HTML>
