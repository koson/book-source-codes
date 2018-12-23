<%@ Page language="c#" Codebehind="default.aspx.cs" AutoEventWireup="false" Inherits="NoAJAX.WebForm1" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>WebForm1</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<script language="JavaScript">
			function callServer()
			{
				serverData.innerHTML = '<IFRAME src="Data.aspx?ID='+
					document.Form1.DropDown.value+
					'" width="200" height="50" frameborder="0" scrolling="no"></IFRAME>';
			}
		</script>
		
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
			<table height="100" border="0">
				<tr>
					<td valign="middle">
						<select name="DropDown" onchange="callServer()">
							<option value="0" selected>Выбор...</option>
							<option value="1">Value1</option>
							<option value="2">Value2</option>
							<option value="3">Value3</option>
						</select>
					</td>
					<td valign="middle">
						<div id="serverData"></div>
					</td>
				</tr>
			</table>
		
		</form>
	</body>
</HTML>
