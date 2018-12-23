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
			<a href="ShowPaths.aspx">Получение абсолютных путей</a>
			<br>
			<asp:LinkButton id="btnServerTransfer" runat="server">Переход по Server.Transfer (URL не изменится)</asp:LinkButton>
			<br>
			<a href="SetFocusScript.aspx">Установка фокуса ввода</a>
			<br>
			<a href="javascript:'<XMP>' + window.document.body.outerHTML + '</XMP>'">Исходный код этой страницы</a>
			<br>
			<asp:LinkButton id="OpenInNewWindow" runat="server">Открыть в новом окне</asp:LinkButton>
			<br>
			<asp:LinkButton id="btnException" runat="server">Обработка исключения (локально)</asp:LinkButton>
			<br>
			<asp:LinkButton id="btnGException" runat="server">Обработка исключения (глобально)</asp:LinkButton>
			<br>
			<a href="PrintTest.aspx">Печать</a>
			<br>
			<a href="Cookies.aspx">Работа с cookie</a>
			<br>
			<a href="DownloadTxt.aspx">Скачать файл</a>
			
		</form>
	</body>
</HTML>
