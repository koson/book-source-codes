
// This line of code...
cmdByRoyalty.Parameters.Add("@percentage",SqlDbType.Int, 15).Value=50;

// ...can replace the follow lines that are in the task...
SqlParameter prmPercentage = new SqlParameter();

prmPercentage.ParameterName = "@percentage";
prmPercentage.SqlDbType= SqlDbType.Int32;
prmPercentage.Direction= ParameterDirection.Input;
prmPercentage.Value=50;

cmdByRoyalty.Parameters.Add(prmPercentage);