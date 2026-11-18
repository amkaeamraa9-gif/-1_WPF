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

namespace Упражнения1_WPF
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
        private void Check_Click(object sender, RoutedEventArgs e)
        {
            
            FrameworkElement checkBox = e.Source as FrameworkElement;
            if (checkBox == null) return;

            switch (checkBox.Name)
            {
                case "checkIgnoreSymbol":
                    if (checkIgnoreSymbol.IsChecked == true)
                        checkIgnorePreviewTextBox.IsChecked = false;
                    break;

                case "checkIgnorePreviewTextBox":
                    if (checkIgnorePreviewTextBox.IsChecked == true)
                        checkIgnoreSymbol.IsChecked = false;
                    break;
            }

            textBox.Focus();
        }


        private void textBox_KeyEvent(object sender, KeyEventArgs e)
        {
 
            if (checkIgnoreRepeat.IsChecked == true && e.IsRepeat)
                return;

            if (checkIgnoreOther.IsChecked == true)
            {
                if (e.Key == Key.Enter || e.Key == Key.Tab ||
                    e.Key == Key.Space || e.Key == Key.Escape)
                {
                    e.Handled = true;
                    return;
                }
            }

            string message = e.RoutedEvent.ToString();
            message = message.Substring(message.IndexOf('.') + 1);
            message = String.Format("Event: {0,-25}", message) + "\t Key: " + e.Key;

            listBox.Items.Add(message);
        }

        private void textBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {

            if (checkIgnorePreviewTextBox.IsChecked == true)
                return;
            if (checkIgnoreSymbol.IsChecked == true)
            {
                short val;
                bool success = Int16.TryParse(e.Text, out val);
                if (!success)
                {
                    e.Handled = true;  
                    return;
                }
            }
            string displayText = e.Text;
            if (checkConvertNumber.IsChecked == true)
            {
                short val;
                if (Int16.TryParse(e.Text, out val))
                {
                    displayText = "[" + val + "]";  
                }
            }
            string message = e.RoutedEvent.ToString();
            message = message.Substring(message.IndexOf('.') + 1);
            message = String.Format("Event: {0,-25}", message) + "\t Text: " + displayText;

            listBox.Items.Add(message);
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            listBox.Items.Clear();
            textBox.Focus();
        }
    }
}
    

