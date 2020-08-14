using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Shapes;

namespace NCU_HCI_Calculator
{
    /// <summary>
    /// Window1.xaml 的互動邏輯
    /// </summary>
    public partial class Window1 : Window
    {
        public List<Class1> datas { get; set; }
        public Window1()
        {
            InitializeComponent();
            string con;
            string query;
            datas = new List<Class1>();
            
            con = @"datasource=127.0.0.1;port=3306;database=cs_practice;" + "User id=root;password=";
            MySqlConnection connection = new MySqlConnection(con);

            query = "select * from db_calculator";

            MySqlCommand command_insert = new MySqlCommand(query, connection);
            MySqlDataReader reader;
            try
            {
                connection.Open();
                reader = command_insert.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Class1 row = new Class1();
                        row.expression = reader.GetString(0);
                        row.prefix = reader.GetString(1);
                        row.postfix = reader.GetString(2);
                        row.Decimal = reader.GetString(3);
                        row.Binary = reader.GetString(4);
                        datas.Add(row);
                        
                        
                    }
                    Console.WriteLine(datas.Count);
                    DataContext = this;
                }
                connection.Close();

            }
            catch (Exception ex)
            {

                System.Windows.MessageBox.Show(ex.Message);
            }

        }

        private void btn_close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void btn_delete_Click(object sender, RoutedEventArgs e)
        {
            
            string con;
            string query;
            con = @"datasource=127.0.0.1;port=3306;database=cs_practice;" + "User id=root;password=";
            MySqlConnection connection = new MySqlConnection(con);

            query = "delete from db_calculator where expression = '"+ datas[list1.SelectedIndex].expression+"'";

            MySqlCommand command_insert = new MySqlCommand(query, connection);
            MySqlDataReader reader;
            try
            {
                connection.Open();
                reader = command_insert.ExecuteReader();
                connection.Close();

            }
            catch (Exception ex)
            {

                System.Windows.MessageBox.Show(ex.Message);
            }

            datas.RemoveAt(list1.SelectedIndex);
            //InitializeComponent();

            list1.Items.Refresh();
        }
    }
}
