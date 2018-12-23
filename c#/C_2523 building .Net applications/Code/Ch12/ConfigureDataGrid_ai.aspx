<%@ Page language="c#" %>
<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="System.Data.SqlClient" %>
<SCRIPT language="C#" runat="server">
      string SortExpression = "";
      void Page_Load(object sender, System.EventArgs e)
      {
            
         if (!IsPostBack) 
         {
            BindData();
         }
      }
      void Grid_Change(Object sender, DataGridPageChangedEventArgs e) 
      {
    
         // Set CurrentPageIndex to the page the user clicked.
         dgdTitles.CurrentPageIndex = e.NewPageIndex;
         // Rebind the data. 
         BindData();
         
      }
      void Sort_Grid(Object sender, DataGridSortCommandEventArgs e) 
      {
         // Set CurrentPageIndex to the page the user clicked.
         SortExpression = e.SortExpression.ToString();
         //SortExpression = "title";
         // Rebind the data. 
         BindData();
         
      }
      void BindData() 
      {
      
         if (SortExpression == "")
            SortExpression = "title";
         SqlConnection cnPubs = new SqlConnection(
            "server=(local);uid=sa;pwd=;database=pubs");
         SqlDataAdapter daTitles = new SqlDataAdapter(
            "select title, notes, price, pubdate from titles order by " + SortExpression, cnPubs);
         DataSet dsTitles = new DataSet();
         daTitles.Fill(dsTitles, "titles");
         dgdTitles.DataSource=dsTitles.Tables["titles"].DefaultView;
         dgdTitles.DataBind();
      }
</SCRIPT>
<HTML>
   <body>
      <form id="PageGrid" method="post" runat="server">
         <asp:DataGrid id="dgdTitles" style="Z-INDEX: 101; LEFT: 8px; POSITION: absolute; TOP: 8px" runat="server" AllowPaging="True" OnPageIndexChanged="Grid_Change" AllowSorting="true" OnSortCommand="Sort_Grid" AutoGenerateColumns="true"></asp:DataGrid>
      </form>
   </body>
</HTML>
