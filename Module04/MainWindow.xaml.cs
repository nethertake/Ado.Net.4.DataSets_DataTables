using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data;
using System.Data.SqlClient;

namespace Module04
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string _connectionString = "";
        public MainWindow()
        {
            InitializeComponent();
            _connectionString = "Data Source = 192.168.111.154; Initial Catalog = hMalServer; User ID = sa; Password = Mc123456;";
        }

        private void ConnectBtn_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = _connectionString;

            SqlDataAdapter da = new SqlDataAdapter("SELECT * from AT_Users; select * from AT_Roles", con);
            System.Data.DataSet ds = new System.Data.DataSet();
            da.Fill(ds);
            var relations = ds.Relations;
            var tt = "";
            foreach (DataTable table in ds.Tables)
            {
                DtColumns.ItemsSource = table.Columns;
            }

            foreach (DataTable table in ds.Tables)
            {
                DtColumns.ItemsSource = table.Columns;
                foreach (DataRow row in table.Rows) {

                    var rows = row.ItemArray;
                    foreach (object cell in rows)
                    {
                        tt = cell.ToString();
                    }

                }
            }


            con.Dispose();
        }

        private void CreateTableBtn_Click(object sender, RoutedEventArgs e)
        {
            DataSet ds = new DataSet();

            //1var
            DataTable tbl = new DataTable("AT_Users_New");
            ds.Tables.Add(tbl);

            //2var
            //ds.Tables.Add("AT_Users_New");

            DataColumn col = tbl.Columns.Add("UserID", typeof(int));
            col.AllowDBNull = false;
            //col.MaxLength = 5;
            col.Unique = true;

            tbl.PrimaryKey = new DataColumn[]
            {
                tbl.Columns["UserID"]
            };

            col.AutoIncrement = true;
            col.AutoIncrementSeed = -1;
            col.AutoIncrementStep = -1;
            col.ReadOnly = true;

            //добавление новой строки
            DataRow dr = ds.Tables["AT_USers"].NewRow();
            dr["UserLogin"] = "Test";
            dr["UserPassword"] = "123";
            dr["UserAge"] = DBNull.Value;

            //удаление строки (!)
            DataRow ddr = ds.Tables["AT_Users"].Rows.Find("2");
            ds.Tables["AT_Users"].Rows.Remove(ddr);

            //2способ удаления

            ds.Tables["AT_Users"].Rows.RemoveAt(2);


            foreach(DataTable table in ds.Tables)
            {
               
               foreach(DataRow row in table.Rows)
                {
                   
                    var st = row.RowState;
                    //var rows = row.ItemArray;
                }


            }

          
            SqlConnection con = new SqlConnection(_connectionString);
            con.Open();
            SqlDataAdapter da = new SqlDataAdapter("SELECT * from AT_Users_New", con);
         
            da.Update(ds);
            con.Close();

        }
    }
}
