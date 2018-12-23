<%@ Page language="c#" Codebehind="ShowPaths.aspx.cs" AutoEventWireup="false" Inherits="Example.ShowPaths" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
	<head>
		<title>ShowPaths</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
	</head>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
		Server.MapPath("/")=<%= Server.MapPath("/")%>
		<br>
		Server.MapPath("")=<%= Server.MapPath("")%>
		<br>
		Server.MapPath("Default.aspx")=<%= Server.MapPath("Default.aspx")%>
		<br>
		Request.Url.AbsoluteUri.Replace(Request.Url.Query,"")=<% =RequestUrl() %>
		<br>
		</form>
	</body>
</html>
