using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace Calculator_KPO
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {

            InitializeComponent();

            DisplayTextBox.Focus();

            string[] operationSymbols = new string[] { "+", "-", "/", "*" };

            // Создание кнопок с цифрами от 1 до 9
            for (int i = 1; i <= 9; i++)
            {
                CreateButton(i.ToString(), (i - 1) / 3, (i - 1) % 3,
                             (sender, e) => FunctionCalculate.DigitButton_Click(sender, e, DisplayTextBox));
            }

            // Создание кнопок математических операций
            for (int i = 0; i < operationSymbols.Length; i++)
            {
                CreateButton(operationSymbols[i], i, 4,
                             (sender, e) => FunctionCalculate.OperationButton_Click(sender, e, DisplayTextBox, LastOperationLabel));
            }

            // Создание кнопки "0"
            CreateButton("0", 3, 1, (sender, e) => FunctionCalculate.DigitButton_Click(sender, e, DisplayTextBox));

            // Создание кнопки "="
            CreateButton("=", 3, 2, (sender, e) => FunctionCalculate.EqualsButton_Click(sender, e, DisplayTextBox));

            // Создание кнопки "CE"
            CreateButton("CE", 3, 0, (sender, e) => FunctionCalculate.ClearAll_Click(sender, e, DisplayTextBox, LastOperationLabel));



        }


        private void DisplayTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            TextBox textBox = sender as TextBox;

            if (textBox != null)
            {
                // Проверяем, является ли введенный символ цифрой или запятой
                if (!char.IsDigit(e.Text, 0) && e.Text != "," && e.Text != "-")
                {
                    e.Handled = true; // Если символ не цифра и не запятая, игнорируем его
                    return;
                }

                // Если введенная запятая является первым символом
                if (e.Text == "," && textBox.Text.Length == 0)
                {
                    e.Handled = true; // Запрещаем ввод запятой как первого символа
                    return;
                }

                // Если в тексте уже есть запятая, то запрещаем ввод еще одной
                if (e.Text == "," && textBox.Text.Contains(","))
                {
                    e.Handled = true; // Запрещаем ввод второй запятой
                    return;
                }
            }
        }



        private void CreateButton(string content, int row, int column, RoutedEventHandler clickHandler)
        {
            Button btn = new Button();
            btn.Content = content;
            btn.Width = 65;
            btn.Height = 65;
            btn.Margin = new Thickness(1);
            btn.Click += clickHandler;

            // Добавляем кнопку в DigitsGrid
            DigitsGrid.Children.Add(btn);
            Grid.SetRow(btn, row);  // Устанавливаем строку кнопки в Grid
            Grid.SetColumn(btn, column); // Устанавливаем столбец кнопки в Grid
        }


    }
}