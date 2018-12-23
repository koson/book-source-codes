// In the if block change the code to the following 
// (to capture exceptions like the primary key already 
// exists, which will be the case if you run this 
// sample more than once). 

SqlCommand cmdTitles = new SqlCommand(sInsertCmd, cnPubs);

try
{
   cmdTitles.Connection.Open();
   cmdTitles.ExecuteNonQuery();
}
catch (Exception e)
{
   Console.WriteLine(e.Message);
}
finally
{
   cmdTitles.Connection.Close();
}
