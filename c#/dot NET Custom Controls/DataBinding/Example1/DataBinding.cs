using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

namespace Example1
{
	public class DataBinding : System.Windows.Forms.Form
	{
		private System.Windows.Forms.DataGrid dataGrid1;
		private System.Data.SqlClient.SqlDataAdapter sqlDataAdapter1;
		private System.Data.SqlClient.SqlCommand sqlSelectCommand1;
		private System.Data.SqlClient.SqlConnection sqlConnection1;
		private Example1.dset MyDataSet;

		private System.ComponentModel.Container components = null;

		public DataBinding()
		{
			InitializeComponent();

			// The only line I coded
			sqlDataAdapter1.Fill(MyDataSet);
		}

		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.dataGrid1 = new System.Windows.Forms.DataGrid();
			this.MyDataSet = new Example1.dset();
			this.sqlDataAdapter1 = new System.Data.SqlClient.SqlDataAdapter();
			this.sqlSelectCommand1 = new System.Data.SqlClient.SqlCommand();
			this.sqlConnection1 = new System.Data.SqlClient.SqlConnection();
			((System.ComponentModel.ISupportInitialize)(this.dataGrid1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.MyDataSet)).BeginInit();
			this.SuspendLayout();
			// 
			// dataGrid1
			// 
			this.dataGrid1.AlternatingBackColor = System.Drawing.SystemColors.Control;
			this.dataGrid1.DataMember = "";
			this.dataGrid1.DataSource = this.MyDataSet.authors;
			this.dataGrid1.Dock = System.Windows.Forms.DockStyle.Top;
			this.dataGrid1.HeaderForeColor = System.Drawing.SystemColors.ControlText;
			this.dataGrid1.Name = "dataGrid1";
			this.dataGrid1.Size = new System.Drawing.Size(564, 273);
			this.dataGrid1.TabIndex = 0;
			// 
			// MyDataSet
			// 
			this.MyDataSet.DataSetName = "dset";
			this.MyDataSet.Locale = new System.Globalization.CultureInfo("de-CH");
			this.MyDataSet.Namespace = "http://www.tempuri.org/dset.xsd";
			// 
			// sqlDataAdapter1
			// 
			this.sqlDataAdapter1.SelectCommand = this.sqlSelectCommand1;
			this.sqlDataAdapter1.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
																									  new System.Data.Common.DataTableMapping("Table", "authors", new System.Data.Common.DataColumnMapping[] {
																																																				 new System.Data.Common.DataColumnMapping("au_id", "au_id"),
																																																				 new System.Data.Common.DataColumnMapping("au_lname", "au_lname"),
																																																				 new System.Data.Common.DataColumnMapping("au_fname", "au_fname"),
																																																				 new System.Data.Common.DataColumnMapping("phone", "phone"),
																																																				 new System.Data.Common.DataColumnMapping("address", "address"),
																																																				 new System.Data.Common.DataColumnMapping("city", "city"),
																																																				 new System.Data.Common.DataColumnMapping("state", "state"),
																																																				 new System.Data.Common.DataColumnMapping("zip", "zip"),
																																																				 new System.Data.Common.DataColumnMapping("contract", "contract")})});
			// 
			// sqlSelectCommand1
			// 
			this.sqlSelectCommand1.CommandText = "SELECT au_id, au_lname, au_fname, phone, address, city, state, zip, contract FROM" +
				" authors";
			this.sqlSelectCommand1.Connection = this.sqlConnection1;
			// 
			// sqlConnection1
			// 
			this.sqlConnection1.ConnectionString = "data source=XEON;initial catalog=pubs;persist security info=False;user id=sa;pass" +
				"word=manager;workstation id=ARKUM;packet size=4096";
			// 
			// DataBinding
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(564, 373);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.dataGrid1});
			this.Name = "DataBinding";
			this.Text = "Simple Data Binding: Example 1";
			((System.ComponentModel.ISupportInitialize)(this.dataGrid1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.MyDataSet)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion

		[STAThread]
		static void Main() 
		{
			Application.Run(new DataBinding());
		}
	}
}
