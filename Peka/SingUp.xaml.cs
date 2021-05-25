﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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

namespace Peka
{
    /// <summary>
    /// Логика взаимодействия для SingUp.xaml
    /// </summary>
    public partial class SingUp : Window
    {
        VHODEntities db = new VHODEntities();
        public SingUp()
        {
            InitializeComponent();
            
        }

        private void BReg_Click(object sender, RoutedEventArgs e)
        {
            Func f = new Func();
            VHOD emp = new VHOD
            {
                Login = Email.Text,
                Password = f.GetHashPassword(PS1.Password),
            }; 
            if (String.IsNullOrEmpty(Email.Text) || String.IsNullOrEmpty(PS1.Password)) //Проверка,если ничего не ввели
            {
                MessageBox.Show("Заполните все поля", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);

            }
            else
            {
                if (PS1.Password.Length < 3) //Проверка,пароля на колл символов
                {
                    MessageBox.Show("Пароль должен быть больше 3 символов!","Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {                
                    try //Проверка на повторяющейся логин
                    {
                    db.VHOD.Add(emp);
                    db.SaveChanges();

                      }
                    catch (System.Data.Entity.Infrastructure.DbUpdateException)
                    {
                        db.VHOD.Remove(emp);
                        db.SaveChanges();
                        Email.Clear();
                        PS1.Clear();
                        MessageBox.Show("Логин существует");
                        return;
                    }
                 MessageBoxResult res = MessageBox.Show("Человек зарегестрирован! Повторить?", "Добавление", MessageBoxButton.YesNo, MessageBoxImage.Question); //Проверка на повторную регистрацию
                    if (res == MessageBoxResult.No)
                    {       
                        MainWindow c = new MainWindow();
                        
                        c.Show();
                        this.Close();
                    }
                     else
                     {
                     Email.Clear();
                     PS1.Clear();
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