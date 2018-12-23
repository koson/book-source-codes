<%@ Page CodeBehind="input.aspx.cs" Language="c#" AutoEventWireup="false" Inherits="ChatApp.WebForm1" %>
</script>
<html>
	<body>
		<form method="post" runat=server>
			<input name="msg" runat=server id="msg">
			<input type="submit" name="Post" value="Post" runat=server>
		</form>
	</body>
</html>
