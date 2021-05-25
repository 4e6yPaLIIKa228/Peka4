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

namespace Peka
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class N2 : Window
    {
        VHODEntities db = new VHODEntities();
        public N2()
        {
            InitializeComponent();

        }

        private void button_Copy_Click(object sender, RoutedEventArgs e)
        {
            SingUp SingUp = new SingUp();
            SingUp.ShowDialog();
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(Login.Text) || String.IsNullOrEmpty(Password.Password))
            {
                MessageBox.Show("Заполните все поля", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);

            }
            else
            {
                if (Password.Password.Length < 3) //Проверка,пароля на колл символов
                {
                    MessageBox.Show("Пароль должен быть больше 3 символов!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {


                    VHOD emp = db.VHOD.SingleOrDefault(c => c.Login == Login.Text);
                    if (emp == null)
                    {
                        MessageBox.Show("Такого пользователя не существует");
                        return;
                    }
                    Func f = new Func();
                    if (f.CheckPassword(emp.Password, f.GetHashPassword(Password.Password)))
                    {
                        Saver.login = emp.Login;
                        MessageBox.Show($"Здравствуйте, {emp.Login}");
                        N4 N4 = new N4();
                        
                        this.Close();
                        N4.Show();
                    }
                    else
                    {
                        MessageBox.Show("Пароль неверный!");
                    }
                }
            }
           
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {    
            MainWindow c = new MainWindow();
            c.Show();
            this.Close();
        }
    }
}