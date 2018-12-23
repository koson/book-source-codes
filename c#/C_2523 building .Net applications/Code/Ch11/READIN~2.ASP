<%@ Page smartNavigation="True" %>
<HTML>
   <HEAD>
      <SCRIPT LANGUAGE="C#" RUNAT="Server">
      void SubmitBtn_Click(Object Sender, EventArgs e) 
      {
         if (Day.Checked) {calPhoto.SelectionMode = CalendarSelectionMode.Day;}
         else if (DayWeek.Checked) {calPhoto.SelectionMode = CalendarSelectionMode.DayWeek; }
         else if (DayWeekMonth.Checked) {calPhoto.SelectionMode = CalendarSelectionMode.DayWeekMonth; }
         else if (None.Checked) {calPhoto.SelectionMode = CalendarSelectionMode.None; }
      }
      </SCRIPT>
   </HEAD>
   <BODY>
      <FONT FACE="Verdana">
         <H3>
            Welcome to www.MySharedPhotoAlbum.com
         </H3>
         Select the date you wish to view photos for.
         <FORM RUNAT="Server" ID="Form1">
            <ASP:CALENDAR ID="calPhoto" RUNAT="Server" DAYNAMEFORMAT="FirstLetter" SELECTIONMODE="DayWeekMonth" 
FONT-NAME="Verdana" FONT-SIZE="10pt" HEIGHT="220px" WIDTH="400px" TODAYDAYSTYLE-FONT-BOLD="True" DAYHEADERSTYLE-FONT-BOLD="True" OTHERMONTHDAYSTYLE-FORECOLOR="Gray" TITLESTYLE-BACKCOLOR="Navy" TITLESTYLE-FORECOLOR="White" TITLESTYLE-FONT-BOLD="True" SELECTEDDAYSTYLE-BACKCOLOR="LightBlue" SELECTEDDAYSTYLE-FONT-BOLD="True" NEXTPREVFORMAT="FullMonth" NEXTPREVSTYLE-FORECOLOR="White" NEXTPREVSTYLE-FONT-SIZE="10px" SELECTORSTYLE-BACKCOLOR="Khaki" SELECTORSTYLE-FORECOLOR="Navy" SELECTORSTYLE-FONT-SIZE="9px" SELECTWEEKTEXT="W" SELECTMONTHTEXT="Select Month" BackColor="White" ForeColor="Black" Font-Names="Times New Roman" BorderColor="Black">
               <TodayDayStyle BackColor="#CCCC99"></TodayDayStyle>
<SelectorStyle Font-Size="8pt" Font-Names="Verdana" Font-Bold="True" ForeColor="#333333" Width="1%" BackColor="#CCCCCC"></SelectorStyle>
               <DayStyle Width="14%"></DayStyle>
               <NextPrevStyle Font-Size="8pt" ForeColor="White"></NextPrevStyle>
<DayHeaderStyle Font-Size="7pt" Font-Names="Verdana" Font-Bold="True" Height="10px" ForeColor="#333333" BackColor="#CCCCCC"></DayHeaderStyle>
               <SelectedDayStyle ForeColor="White" BackColor="#CC3333"></SelectedDayStyle>
               <TitleStyle Font-Size="13pt" Font-Bold="True" Height="14pt" ForeColor="White" BackColor="Black"></TitleStyle>
               <OtherMonthDayStyle ForeColor="#999999" BackColor="#E0E0E0"></OtherMonthDayStyle>
            </ASP:CALENDAR>
            <P><asp:RadioButton id="Day" Text="Day" Checked="True" GroupName="RadioGroup1" runat="server" />
            <br>
            <asp:RadioButton id="DayWeek" Text="DayWeek" GroupName="RadioGroup1" runat="server" />
            <br>
            <asp:RadioButton id="DayWeekMonth" Text="DayWeekMonth" GroupName="RadioGroup1" runat="server" />
            <br>
            <asp:RadioButton id="None" Text="None" GroupName="RadioGroup1" runat="server" />
            <br>
            <asp:Button id="Button1" Text="Change Selection Type" OnClick="SubmitBtn_Click" runat="server" />
         </FORM>
      </FONT></P>
   </BODY>
</HTML>
