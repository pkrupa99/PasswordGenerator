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
//using System.Windows.Shapes;
using System.Collections.ObjectModel;

using System.IO;

namespace WpfApp21
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            buttonSaveItem.IsEnabled = false;
            
            
        }
        public int passwordAssessment;
        private void buttonGenerate_Click(object sender, RoutedEventArgs e)
        {
            buttonSaveItem.IsEnabled = true;
            //*Declaration of variables 
            bool lowerCase = true;
            bool upperCase = true;
            bool specialSign = true;
            bool numbers = true;
            bool underCore = true;
                
            //*if checkbox in MainWindow is not checked the variable will be set false;
            if (cb_LowC.IsChecked == false)
            {
                lowerCase = false;
            }
            if (cb_UpperC.IsChecked == false)
            {
                upperCase = false;
            }
            if (cb_SpecialS.IsChecked == false)
            {
                specialSign = false;
            }
            if (cb_Num.IsChecked == false)
            {
                numbers = false;
            }
            if (cb_UnderC.IsChecked == false)
            {
                underCore = false;
            }
             

            //Create new object of  PasswordGenerator class;
            PasswordGenerator p1 = new PasswordGenerator(lowerCase, upperCase, specialSign, numbers, underCore, Convert.ToByte(txtLenPassword.Text));
            //Run method to generate passwod;
            txtPassword.Text = p1.Generate().ToString();

            //string ocenaS = p1.PasswordStrenght().ToString();

            //Assign the result of the PasswordStrength() method to the variable passwordAssessment.
            passwordAssessment = Convert.ToInt32(p1.PasswordStrenght().ToString());

            if (passwordAssessment < 5)
            {
                txtPasswordColor.Background = Brushes.Red;
                txtStrenght.Text = "Weak";
            }
            else if (passwordAssessment < 10)
            {
                txtPasswordColor.Background = Brushes.Yellow;
                txtStrenght.Text = "Average";
            }
            else
            {
                txtPasswordColor.Background = Brushes.Green;
                txtStrenght.Text = "Strong";
            }

        }



        int lenPassword;
       

        //* Two methods - click on btnLenGPassword button or btnLenLPassword button cause increases or decreases lenPassword;
        private void btnLenGPassword_Click_1(object sender, RoutedEventArgs e)
        {
            lenPassword = Convert.ToInt32(txtLenPassword.Text);
            if (lenPassword < 99)
            {
                lenPassword++;
                txtLenPassword.Text = Convert.ToString(lenPassword);
            }
        }
        private void btnLenLPassword_Click_2(object sender, RoutedEventArgs e)
        {
            lenPassword = Convert.ToInt32(txtLenPassword.Text);
            if (lenPassword > 5)
            {
                lenPassword--;
                txtLenPassword.Text = Convert.ToString(lenPassword);
            }
        }
        //* Method to protect textbox against entering values below 5; 
        private void txtLenPassword_LostFocus(object sender, RoutedEventArgs e)
        {
            if (Convert.ToInt32(txtLenPassword.Text) < 5)
            {
                txtLenPassword.Text = "5";
            }
        }


        //*Method to protext textbox against entering values other than digit type
        private void txtLenPassword_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!e.Text.Any(char.IsDigit))
            {
                e.Handled = true;
            }

        }

        //*Method to delete row from ListView
        private void buttonDeleteRow_Click(object sender, RoutedEventArgs e)
        {
            if (LViewPassword.SelectedItems.Count > 0)
            {
                LViewPassword.Items.Remove(LViewPassword.SelectedItem);
            }
        }
        //*Method to clear rows from ListView
        private void buttonClearElements_Click_4(object sender, RoutedEventArgs e)
        {
            LViewPassword.Items.Clear();
        }

        //*Method to save items into a ListView
        private void buttonSaveItem_Click_3(object sender, RoutedEventArgs e)
        {
            Tuple<string, string> data = new Tuple<string, string>(txtPassword.Text, passwordAssessment.ToString());
            LViewPassword.Items.Add(data);
        }


        // Method to save a text file with a generated password. The text file is saved in the user's Downloads folder. If the file 
        // already exists, the file name is changed before saving.
        private void buttonTxtGenerate_Click(object sender, RoutedEventArgs e)
        {
            string fullPath = @"C:\Users\USER_NAME\Downloads\passwordGenerator.txt".Replace("USER_NAME", Environment.UserName);
            string fileName = Path.GetFileNameWithoutExtension(fullPath);
           
            int i = 0;
            while (File.Exists(fullPath))
            {
                i++;
                fullPath = (@"C:\Users\USER_NAME\Downloads\passwordGenerator" + i.ToString() + ".txt").Replace("USER_NAME", Environment.UserName);

            }


            if (Convert.ToInt32(LViewPassword.Items.Count) > 0)
            {
                using (StreamWriter writer = new StreamWriter(fullPath))
                {
                    writer.Write("Password" + ";" + "Strenght");
                    writer.WriteLine();
                    foreach (var item in LViewPassword.Items)
                    {
                        var tuple = item as Tuple<string, string>;
                        writer.Write(tuple.Item1 + ";" + tuple.Item2);
                        writer.WriteLine();
                    }
                }
                MessageBox.Show("File saved in location" + fullPath);
            }
            else
            {
                MessageBox.Show("The password table is empty!");
            }
        }

   
    }
}
