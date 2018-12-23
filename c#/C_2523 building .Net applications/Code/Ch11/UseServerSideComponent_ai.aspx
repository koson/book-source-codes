<%@ Page language="c#"%>
<html>
   <head>
   </head>
   <body>
      <form method="post" runat="server">
         <%
         HttpRequest oRequest;
         oRequest = this.Request;
          
         foreach (string sRequest in oRequest.ServerVariables)
         {
            Response.Write(sRequest + " = " + 
               oRequest.ServerVariables[sRequest] + "<br>");
         }
         %>
      </form>
   </body>
</html>

