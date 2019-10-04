using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Data.Sql;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Personal
{
    /// <summary>
    /// Логика взаимодействия для winRegistration.xaml
    /// </summary>
    /// 

    
    public partial class winRegistration : Window
    {
        public const string strWindowTitle = "Форма регистрации";

        public winRegistration()
        {
            InitializeComponent();
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
             try
             {
                string strName = txbName.Text.Replace(" ", string.Empty);
                string strSurname = txbSurname.Text.Replace(" ", string.Empty);
                string strPatronymic = txbPatronymic.Text.Replace(" ", string.Empty);

                if (string.IsNullOrEmpty(txbName.Text) || string.IsNullOrEmpty(txbPatronymic.Text) || string.IsNullOrEmpty(txbSurname.Text))
                {
                    MessageBox.Show(Definitions.strMsgRequiredFields, strWindowTitle);
                    return;
                }

                string strInfo = txbDescription.Text;
                
                    strInfo = strInfo.TrimStart();
                    strInfo = strInfo.TrimEnd();

                string strPassword = strName.Substring(0, 2) + strSurname.Substring(0, 2) + strPatronymic.Substring(0, 2);

                SqlCommand sqlMaxIDC = new SqlCommand(Definitions.strQNewIDC, AppConnection.sqlConn);
                DataTable dtRCmaxIDC = new DataTable();
                        
                using (SqlDataReader sqlDR = sqlMaxIDC.ExecuteReader())
                {
                    dtRCmaxIDC.Load(sqlDR);
                }

                int intIDC = (int)dtRCmaxIDC.Rows[0][0];

                string strIDCtext = Definitions.strPrefix.ToString() + strSurname.ToString() + strName.Substring(0, 1).ToString() + strPatronymic.Substring(0, 1);
                SqlCommand QExecute = new SqlCommand($"EXEC sp_Add_counterparty {intIDC.ToString()}, '{strIDCtext.ToString()}'", AppConnection.sqlConn);
                QExecute.ExecuteNonQuery();
                QExecute = new SqlCommand($"EXEC sp_Add_property {intIDC.ToString()}, '{strIDCtext.ToString()}'", AppConnection.sqlConn);
                QExecute.ExecuteNonQuery();
                // MessageBox.Show("Успешно " + intRowsAffected.ToString());
                MainWindow winAuthorization = new MainWindow();
                winAuthorization.Show();
                this.Close();
            }

            catch (Exception ErrEx)
             {
                MessageBox.Show("Ошибка при регистрации контрагента " + Environment.NewLine + Environment.NewLine + ErrEx.Message.ToString(), strWindowTitle);
             }

        }
    }
}
