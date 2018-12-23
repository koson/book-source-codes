<%@ Page language="c#" Codebehind="PrintTest.aspx.cs" AutoEventWireup="false" Inherits="Example.PrintTest" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" > 

<html>
  <head>
    <title>PrintTest</title>
    <meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" Content="C#">
    <meta name=vs_defaultClientScript content="JavaScript">
    <meta name=vs_targetSchema content="http://schemas.microsoft.com/intellisense/ie5">
   
    <object ID="wb" WIDTH=300 HEIGHT=151
        CLASSID="CLSID:8856F961-340A-11D0-A96B-00C04FD705A2" VIEWASTEXT>
         <param name="ExtentX" value="7938">
         <param name="ExtentY" value="3986">
         <param name="ViewMode" value="0">
         <param name="Offline" value="0">
         <param name="Silent" value="0">
         <param name="RegisterAsBrowser" value="0">
         <param name="RegisterAsDropTarget" value="1">
         <param name="AutoArrange" value="0">
         <param name="NoClientEdge" value="0">
         <param name="AlignLeft" value="0">
         <param name="NoWebView" value="0">
         <param name="HideFilenames" value="0">
         <param name="SingleClick" value="0">
         <param name="SingleSelection" value="0">
         <param name="NoFolders" value="0">
         <param name="Transparent" value="0">
         <param name="ViewID" value="{0057D0E0-3573-11CF-AE69-08002B2E1262}">
      </object>

      <script language="JavaScript">
        function PrintPage()
        {
          wb.ExecWB(6, 2, 2, 2);
        }
  
        function SetupPage()
        {
          wb.ExecWB(8, 0, 0, 0);
        }
     </script>
    
  </head>
  <body>
    <form id="Form1" method="post" runat="server">
	  <input type="button" value="Print Page" onclick="PrintPage()">
	  <input type="button" value="Print Setup" onclick="SetupPage()">
     </form>
	
  </body>
</html>
