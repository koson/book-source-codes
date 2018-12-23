<html>
<script language="C#" runat="server">
   void Submit_Click(object sender, EventArgs e)
   {
      if (txtName.Value == "RobertPhillips" & 
         txtPwd.Value == "pharmacist")
         spnMessage.InnerHtml = "You are authenticated!";
      else
         spnMessage.InnerHtml = "Login Failed!";
   }  
</script>
   <body>
      <form method=post runat=server>
         <h3>Enter Name: 
         <input id="txtName" type=text size=40 runat=server>
         <h3>Enter Password: 
         <input id="txtPwd" type=password size=40 runat=server>
         <input type=submit value="Enter" 
            OnServerClick="Submit_Click" runat=server>
         <h1><span id="spnMessage" runat=server> </span></h1>
      </form>
   </body>
</html>
