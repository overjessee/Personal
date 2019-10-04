using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Data.Sql;
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

namespace Personal
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        public DataTable sqlQuery(string selectSQL) // функция подключения к базе данных и обработка запросов
        {
            DataTable dtTable = new DataTable("DB");

            SqlConnection sqlConn = new SqlConnection("server=DESKTOP-L8BN1IS;Trusted_Connection=Yes;DataBase=Personal;");
            sqlConn.Open();
            SqlCommand sqlComm = sqlConn.CreateCommand();
            sqlComm.CommandText = selectSQL;
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlComm);
            sqlDataAdapter.Fill(dtTable);
            return dtTable;
        }

        private void SignUp_Click(object sender, RoutedEventArgs e)
        {
            winRegistration winReg = new winRegistration();
            winReg.Show();
            Hide();
        }

        private async void FrmAuthorization_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                SqlConnectionStringBuilder sqlConnToPersonal = new SqlConnectionStringBuilder();
                sqlConnToPersonal.ConnectionString = Properties.Settings.Default["ServerName"].ToString();
                //sqlConnToPersonal.DataSource = Properties.Settings.Default["ServerName"].ToString();

                AppConnection.sqlConn = new SqlConnection(sqlConnToPersonal.ToString());
                await AppConnection.sqlConn.OpenAsync();
                MessageBox.Show("Подключение установлено");
            }
            catch (SqlException ex)
            {
                MessageBox.Show(Definitions.strErrConnToDB + Environment.NewLine + Environment.NewLine + "Ошибка №" + ex.Number.ToString() + ": " + ex.Message.ToString(), "Ошибка при подключении к серверу" ) ;
            }
        }
    }


}
