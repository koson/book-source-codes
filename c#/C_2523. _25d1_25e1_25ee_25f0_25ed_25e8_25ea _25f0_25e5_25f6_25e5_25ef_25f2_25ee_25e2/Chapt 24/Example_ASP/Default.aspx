<%@ Page language="c#" Codebehind="Default.aspx.cs" AutoEventWireup="false" Inherits="Example.DefaultPage" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>WebForm1</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<script>
			function wnd(url) { window.open(url); return false; }		
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<a href="ShowPaths.aspx">��������� ���������� �����</a>
			<br>
			<asp:LinkButton id="btnServerTransfer" runat="server">������� �� Server.Transfer (URL �� ���������)</asp:LinkButton>
			<br>
			<a href="SetFocusScript.aspx">��������� ������ �����</a>
			<br>
			<a href="javascript:'<XMP>' + window.document.body.outerHTML + '</XMP>'">�������� ��� ���� ��������</a>
			<br>
			<asp:LinkButton id="OpenInNewWindow" runat="server">������� � ����� ����</asp:LinkButton>
			<br>
			<asp:LinkButton id="btnException" runat="server">��������� ���������� (��������)</asp:LinkButton>
			<br>
			<asp:LinkButton id="btnGException" runat="server">��������� ���������� (���������)</asp:LinkButton>
			<br>
			<a href="PrintTest.aspx">������</a>
			<br>
			<a href="Cookies.aspx">������ � cookie</a>
			<br>
			<a href="DownloadTxt.aspx">������� ����</a>
			
		</form>
	</body>
</HTML>
