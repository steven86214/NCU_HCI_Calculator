using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MySql.Data.MySqlClient;

namespace NCU_HCI_Calculator
{
    /// <summary>
    /// MainWindow.xaml 的互動邏輯
    /// </summary>
    public partial class MainWindow : Window
    {
        //using MySql.Data.MySqlClient;
        
        public MainWindow()
        {
            InitializeComponent();
        }
        private void LV_1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
        private void Btn_1_Click(object sender, RoutedEventArgs e)
        {
            TBK_display.Text += "1";
        }
        private void Btn_2_Click(object sender, RoutedEventArgs e)
        {
            TBK_display.Text += "2";
        }
        private void Btn_3_Click(object sender, RoutedEventArgs e)
        {
            TBK_display.Text += "3";
        }
        private void Btn_4_Click(object sender, RoutedEventArgs e)
        {
            TBK_display.Text += "4";
        }
        private void Btn_5_Click(object sender, RoutedEventArgs e)
        {
            TBK_display.Text += "5";
        }
        private void Btn_6_Click(object sender, RoutedEventArgs e)
        {
            TBK_display.Text += "6";
        }
        private void Btn_7_Click(object sender, RoutedEventArgs e)
        {
            TBK_display.Text += "7";
        }
        private void Btn_8_Click(object sender, RoutedEventArgs e)
        {
            TBK_display.Text += "8";
        }
        private void Btn_9_Click(object sender, RoutedEventArgs e)
        {
            TBK_display.Text += "9";
        }
        private void Btn_0_Click(object sender, RoutedEventArgs e)
        {
            TBK_display.Text += "0";
        }
        private void Btn_Add_Click(object sender, RoutedEventArgs e)
        {
            if (TBK_display.Text.Length == 0) { }
            else
            {
                if (TBK_display.Text[TBK_display.Text.Length - 1] == '+' || TBK_display.Text[TBK_display.Text.Length - 1] == '-' || TBK_display.Text[TBK_display.Text.Length - 1] == '*' || TBK_display.Text[TBK_display.Text.Length - 1] == '/')
                {

                }
                else
                {
                    TBK_display.Text += " + ";
                }
            }
        }
        private void Btn_Minus_Click(object sender, RoutedEventArgs e)
        {
            if (TBK_display.Text.Length == 0) { }
            else
            {
                if (TBK_display.Text[TBK_display.Text.Length - 1] == '+' || TBK_display.Text[TBK_display.Text.Length - 1] == '-' || TBK_display.Text[TBK_display.Text.Length - 1] == '*' || TBK_display.Text[TBK_display.Text.Length - 1] == '/')
                {

                }
                else
                {
                    TBK_display.Text += " - ";
                }
            }
        }
        private void Btn_Multi_Click(object sender, RoutedEventArgs e)
        {
            if (TBK_display.Text.Length == 0) { }
            else
            {
                if (TBK_display.Text[TBK_display.Text.Length - 1] == '+' || TBK_display.Text[TBK_display.Text.Length - 1] == '-' || TBK_display.Text[TBK_display.Text.Length - 1] == '*' || TBK_display.Text[TBK_display.Text.Length - 1] == '/')
                {

                }
                else
                {
                    TBK_display.Text += " * ";
                }
            }
        }
        private void Btn_Div_Click(object sender, RoutedEventArgs e)
        {
            if (TBK_display.Text.Length == 0) { }
            else
            {
                if (TBK_display.Text[TBK_display.Text.Length - 1] == '+' || TBK_display.Text[TBK_display.Text.Length - 1] == '-' || TBK_display.Text[TBK_display.Text.Length - 1] == '*' || TBK_display.Text[TBK_display.Text.Length - 1] == '/')
                {

                }
                else
                {
                    TBK_display.Text += " / ";
                }
            }
        }
        private void Btn_BackSpace_Click(object sender, RoutedEventArgs e)
        {

            if (TBK_display.Text.Length > 0)
            {
                TBK_display.Text = TBK_display.Text.Substring(0, TBK_display.Text.Length - 1);
            }
        }
        private void Btn_Clean_Click(object sender, RoutedEventArgs e)
        {
            TBK_display.Text = "";
        }
        private void Btn_Enter_Click(object sender, RoutedEventArgs e)
        {
            String[] infix;
            infix = TBK_display.Text.Split(' ');
            TBK_Postorder.Text = String.Join("", InfixToPostfix(infix));
            TBK_Preorder.Text = String.Join("", InfixToPrefix(infix));

            //rev_postfix = InfixToPostfix(infix);
            //Array.Reverse(rev_postfix);
            System.Data.DataTable dt = new System.Data.DataTable();
            var v = dt.Compute(TBK_display.Text," ");
            
            TBK_Sol_Dec.Text = v.ToString();
            TBK_Sol_Bin.Text = Convert.ToString(Convert.ToInt32(v), 2);
            //TBK_Preorder.Text = String.Join("", InfixToPrefix(infix));
            //TBK_Preorder.Text = String.Join("", TBK_display.Text.Split(' '));


        }
        int[] operand = new int[30];
        
        public static int RegnePrioritet(String op)
        {
            switch (op)
            {
                case "*": case "/": return 2;
                case "+": case "-": return 1;
                default: return 0;
            }
        }

        public static string[] InfixToPostfix(string[] infixArray)
        {
            var stack = new Stack<string>();
            var postfix = new Stack<string>();

            string st;
            for (int i = 0; i < infixArray.Length; i++)
            {
                if (!("()*/+-".Contains(infixArray[i])))
                {
                    postfix.Push(infixArray[i]);
                }
                else
                {
                    if (infixArray[i].Equals("("))
                    {
                        stack.Push("(");
                    }
                    else if (infixArray[i].Equals(")"))
                    {
                        st = stack.Pop();
                        while (!(st.Equals("(")))
                        {
                            postfix.Push(st);
                            st = stack.Pop();
                        }
                    }
                    else
                    {
                        while (stack.Count > 0)
                        {
                            st = stack.Pop();
                            if (RegnePrioritet(st) >= RegnePrioritet(infixArray[i]))
                            {
                                postfix.Push(st);
                            }
                            else
                            {
                                stack.Push(st);
                                break;
                            }
                        }
                        stack.Push(infixArray[i]);
                    }
                }
            }
            while (stack.Count > 0)
            {
                postfix.Push(stack.Pop());
            }

            return postfix.Reverse().ToArray();
        }

        public static string[] InfixToPrefix(string[] infixArray)
        {
            
            Array.Reverse(infixArray);
            var stack = new Stack<string>();
            var t = new Stack<string>();

            string st;
            for (int i = 0; i < infixArray.Length; i++)
            {
                if (!("()*/+-".Contains(infixArray[i])))
                {
                    t.Push(infixArray[i]);
                }
                else
                {
                    if (infixArray[i].Equals("("))
                    {
                        stack.Push("(");
                    }
                    else if (infixArray[i].Equals(")"))
                    {
                        st = stack.Pop();
                        while (!(st.Equals("(")))
                        {
                            t.Push(st);
                            st = stack.Pop();
                        }
                    }
                    else
                    {
                        while (stack.Count > 0)
                        {
                            st = stack.Pop();
                            if (RegnePrioritet(st) > RegnePrioritet(infixArray[i]))
                            {
                                t.Push(st);
                            }
                            {
                                stack.Push(st);
                                break;
                            }
                        }
                        stack.Push(infixArray[i]);
                    }
                }
            }
            while (stack.Count > 0)
            {
                t.Push(stack.Pop());
            }

            return t.ToArray();
        }
        
        static bool isOperator(char c)
        {
            return (!(c >= 'a' && c <= 'z') &&
                    !(c >= '0' && c <= '9') &&
                    !(c >= 'A' && c <= 'Z'));
        }

        // Function to find priority  
        // of given operator.  
        static int getPriority(char C)
        {
            if (C == '-' || C == '+')
                return 1;
            else if (C == '*' || C == '/')
                return 2;
            else if (C == '^')
                return 3;
            return 0;
        }

        private void DBInsert(String expression, String prefix, String postfix, String dec, String bin)
        {
            string con;
            string query;

            con = @"datasource=127.0.0.1;port=3306;database=cs_practice;" + "User id=root;password=";
            MySqlConnection connection = new MySqlConnection(con);

            query = "insert into db_calculator values('" + expression + "','" +prefix+ "','" + postfix + "','" +dec + "','" + bin + "')";
            
            MySqlCommand command_insert = new MySqlCommand(query, connection);
            MySqlDataReader reader;
            try
            {
                connection.Open();
                
                if (IsInDB(expression))
                {
                    System.Windows.MessageBox.Show("expression is exist");
                }
                else
                {
                    reader = command_insert.ExecuteReader();
                }
                connection.Close();

            }
            catch(Exception ex)
            {

                System.Windows.MessageBox.Show(ex.Message);
            }
            
        }

        private bool IsInDB(string expression)
        {
            String con, query_search;
            con = @"datasource=127.0.0.1;port=3306;database=cs_practice;" + "User id=root;password=";
            MySqlConnection connection = new MySqlConnection(con);
            //            SqlConnection connection = new SqlConnection(con);
            query_search = "select * from db_calculator where expression = " + expression;
            MySqlCommand command_search = new MySqlCommand(query_search, connection);
            MySqlDataReader reader;
            try
            {
                connection.Open();
                
               
                reader = command_search.ExecuteReader();
                if (reader.HasRows)
                {
                    return true;
                }
                else
                {
                    return false;
                }
                connection.Close();

            }
            catch (Exception ex)
            {
                return true;
                System.Windows.MessageBox.Show(ex.Message);
            }
        }
        private void Btn_Insert_Click(object sender, RoutedEventArgs e)
        {
            DBInsert(TBK_display.Text,TBK_Preorder.Text,TBK_Postorder.Text,TBK_Sol_Dec.Text,TBK_Sol_Bin.Text);
        }

        private void Btn_Query_Click(object sender, RoutedEventArgs e)
        {

            Window1 win1 = new Window1();
            win1.Show();
            

        }
    }
}
