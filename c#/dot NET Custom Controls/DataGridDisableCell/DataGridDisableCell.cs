using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;

namespace DataGridDisableCell
{
	// This declaration defines a delegate named DataGridDisableCellHandler,
	// which will encapsulate any method that takes two parameters:
	// object: the source of the event; that is the publishing object
	// DataGridDisableCellEventArgs: an object derived from EventArgs.
	public delegate void DataGridDisableCellHandler(object sender, DataGridDisableCellEventArgs e);

	// ====================== Our Application ================================= 
	// Disable DataGrid Cells by deriving a custom column
	// style and overriding its virtual Edit member.
	public class DataGridDisableCell : System.Windows.Forms.Form
	{
		private System.Windows.Forms.DataGrid dataGrid1;
		private System.ComponentModel.Container components = null;

		public DataGridDisableCell()
		{
			InitializeComponent();
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
			((System.ComponentModel.ISupportInitialize)(this.dataGrid1)).BeginInit();
			this.SuspendLayout();
			// 
			// dataGrid1
			// 
			this.dataGrid1.CaptionText = "Output from Table Customers";
			this.dataGrid1.DataMember = "";
			this.dataGrid1.HeaderForeColor = System.Drawing.SystemColors.ControlText;
			this.dataGrid1.Location = new System.Drawing.Point(8, 8);
			this.dataGrid1.Name = "dataGrid1";
			this.dataGrid1.Size = new System.Drawing.Size(456, 248);
			this.dataGrid1.TabIndex = 0;
			// 
			// DataGridDisableCell
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(472, 261);
			this.Controls.Add(this.dataGrid1);
			this.Name = "DataGridDisableCell";
			this.Text = "Show how to enable / disable a Datagrid Cell (red = disabled)";
			this.Load += new System.EventHandler(this.DataGridDisableCell_Load);
			((System.ComponentModel.ISupportInitialize)(this.dataGrid1)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion

		[STAThread]
		public static void Main() 
		{
			Application.Run(new DataGridDisableCell());
		}

		// Create a table style that will hold our own 
		// customized column style
		private void DataGridDisableCell_Load(object sender, System.EventArgs e)
		{
			// Fetch Table "customers" into the DataSet
			string connString = "server=xeon;" +
				"uid=sa; pwd=manager; database=northwind";
			string sqlString = "SELECT * FROM customers";
			SqlDataAdapter dataAdapter = null;
			DataSet _dataSet = null;
			SqlConnection connection = new SqlConnection(connString);
			dataAdapter = new SqlDataAdapter(sqlString, connection);
			_dataSet = new DataSet();
			dataAdapter.Fill(_dataSet, "customers");

			// Create a table style that will hold the new column style 
			// that we set and also tie it to our customer's table from our DB
			DataGridTableStyle tableStyle = new DataGridTableStyle();
			tableStyle.MappingName = "customers";

			// The dataset has things like field name and number of columns,
			// we will use those to create our own columnstyles for the columns
			// in the DataGrid
			int numCols = _dataSet.Tables["customers"].Columns.Count;
			DataGridTextBox dgtb;   // This is our own DataGrid Text Box

			// Loop through all DataGrid Cells and set the HeaderText, MappingName
			// and enable / disable the appropriate Cells by deriving a custom column
			// style and overriding its virtual Edit member
			for (int i=0; i<numCols; ++i)
			{
				dgtb = new DataGridTextBox(i);
				dgtb.HeaderText = _dataSet.Tables["customers"].Columns[i].ColumnName;
				dgtb.MappingName = _dataSet.Tables["customers"].Columns[i].ColumnName;

				// ====================== Our Subscriber =================================
				// Subscribe to our own EnableCell event handler
				dgtb.DataGridDisableCell += new DataGridDisableCellHandler(SetEnableValues);

				// Adds our column style to the collection
				tableStyle.GridColumnStyles.Add(dgtb);
			}
			
			// Now, make the dataGrid use our own tablestyle and bind it to our table
			dataGrid1.TableStyles.Clear();
			dataGrid1.TableStyles.Add(tableStyle);
			dataGrid1.DataSource = _dataSet.Tables["customers"];
		}

		// This is our own DataGridDisableCell Handler. Here we can do whatever
		// we want. Our Handler must conform to the paramters defined in the delegate.
		public void SetEnableValues(object sender, DataGridDisableCellEventArgs e)
		{
			// OK, our sample uses modulo 5 to disable the Cells. Note, that a Cell
			// has NO disable / enable Flag, this is done by the DataGridTextBoxColumn
			// Edit Function as follows:
			// Enable Cell: Call DataGridTextBoxColumn.Edit
			// Disable Cell: Do not call DataGridTextBoxColumn.Edit 
			if ((e.Column + e.Row) % 2 == 0)
			{
				e.EnableValue = false; // Do not call DataGridTextBoxColumn.Edit 
			}
			else 
			{
				e.EnableValue = true; // Do not call DataGridTextBoxColumn.Edit 
			}
			
		}
	} // End of our Application: DataGridDisableCell

	// ====================== Our Publisher ================================= 
	// This Class is our second Paramter for our Event, it must derived
	// from EventArgs. We publish the Properties: Column, Row and EnableValue
	// to our Subscribers.
	public class DataGridDisableCellEventArgs : EventArgs
	{
		private int _column;
		private int _row;
		private bool _enablevalue = true;

		// Constructor, set private values row, col.
		public DataGridDisableCellEventArgs(int row, int col)
		{
			_row = row;
			_column = col;
		}

		// Published Property Column
		public int Column
		{
			get {return _column;}
			set {_column = value;}
		}

		// Published Property Row
		public int Row
		{
			get {return _row;}
			set {_row = value;}
		}

		// Published Property EnableValue
		public bool EnableValue
		{
			get {return _enablevalue;}
			set {_enablevalue = value;}
		}
	}

	// This is our own, customized DataGrid TextBox Column. Here
	// we draw the Cell Background and disable / enable the Cell
	// according to the EnableValue in the defined Event.
	public class DataGridTextBox : DataGridTextBoxColumn
	{
		// Declare the Event for the defined Delegate.
		public event DataGridDisableCellHandler DataGridDisableCell;
		
		// Save the column number
		private int _col;

		// Our own Constructor, which must NOT conform the Constructor
		// in the Base Class (Constructors are not derived)
		public DataGridTextBox(int column)
		{
			_col = column;
		}

		// Here is the trick for the Background / Foreground Color
		// of the Cell - override the Paint method, with our
		// own functionality.
		protected override void Paint(
			System.Drawing.Graphics g,
			System.Drawing.Rectangle bounds,
			System.Windows.Forms.CurrencyManager source,
			int rowNum,  // Here is the Row Number
			System.Drawing.Brush backBrush,
			System.Drawing.Brush foreBrush,
			bool alignToRight)
		{
			// Do we have Subscribers - notify them if we have
			if (DataGridDisableCell != null)
			{
				// Initialize our Event with the current Row and Column Number
				DataGridDisableCellEventArgs e = new DataGridDisableCellEventArgs(rowNum, _col);
								
				// Notify Subscribers to call their EventHandlers - where they
				// can do whatever they want. After this we check the EnableValue
				// Flag, which may be set / unset by a Subscriber.
				DataGridDisableCell(this, e);

				// Set the Foreground / Back Color according to our Subscribers
				if (!e.EnableValue) 
				{
					backBrush = Brushes.Red;
					foreBrush = Brushes.White;
				}
			}
			
			// In any case (enabled or disabled) draw the Column using the Base Method
			base.Paint(g, bounds, source, rowNum, backBrush, foreBrush, alignToRight);
		}

		// Here is the trick to emable / disable the TextBox
		// of the Cell - override the Edit method, with our
		// own functionality.
		protected override void Edit(
			System.Windows.Forms.CurrencyManager source,
			int rowNum,
			System.Drawing.Rectangle bounds,
			bool readOnly,
			string instantText,
			bool cellIsVisible)
		{		
			DataGridDisableCellEventArgs e = null;

			// Do we have Subscribers - notify them if we have
			if (DataGridDisableCell != null)
			{
				// Initialize our Event with the current Row and Column Number
				e = new DataGridDisableCellEventArgs(rowNum, _col);

				// Notify Subscribers to call their EventHandlers - where they
				// can do whatever they want. After this we check the EnableValue
				// Flag, which may be set / unset by a Subscriber.			
				DataGridDisableCell(this, e);
			}

			// Only call the Edit Method (which enables the TextBox in the DataGrid)
			// when the Enable Flag has been set by the Subscriber
			if (e.EnableValue) 
			{
				base.Edit(source, rowNum, bounds, readOnly, instantText, cellIsVisible);
			}
		}
	}
}
