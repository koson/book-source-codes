<HTML>
   <HEAD>
      <SCRIPT LANGUAGE="C#" RUNAT="Server">
         void cmdDescription_Click(object Source, EventArgs e) 
         {
            if (pnlDescription.Visible == true)
               {
               pnlDescription.Visible = false;
               cmdDescription.Text = "Show Photo Description";
               }
            else
            {
               pnlDescription.Visible = true;
               cmdDescription.Text = "Hide Photo Description";
            }
         }
      </SCRIPT>
   </HEAD>
   <BODY>
      <FONT FACE="Verdana">
         <H3>
            Welcome to www.MySharedPhotoAlbum.com
         </H3>
         <FORM RUNAT="Server" ID="Form1">
            <P>
             <ASP:PANEL ID="pnlImage" HEIGHT="100px" WIDTH="300px" BACKCOLOR="Silver" RUNAT="SERVER">
            Your image goes here in an image tag. 
               <P>
                  <ASP:BUTTON id="cmdDescription" onclick="cmdDescription_Click" RUNAT="Server" TEXT="Continue"></ASP:BUTTON>
               </P>
            </ASP:PANEL>
            <br/>
            <ASP:PANEL ID="pnlDescription" BACKCOLOR="SkyBlue" HEIGHT="50px" WIDTH="300px" RUNAT="SERVER" VISIBLE="False">
            Here is where the description would be displayed. 
            </ASP:PANEL>
            <P>
            </P>
         </FORM>
      </FONT>
      <P>
      </P>
   </BODY>
</HTML>
