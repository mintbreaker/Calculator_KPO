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


            string[] operationSymbols = new string[] { "+", "-", "/", "*" };


            // Добавляем кнопки с цифрами от 1 до 9 в новый Grid (DigitsGrid)
            for (int i = 1; i <= 9; i++)
            {
                Button btn = new Button();
                btn.Content = i.ToString();
                btn.Width = 65;
                btn.Height = 65;
                btn.Margin = new Thickness(1);
                btn.Click += (sender, e) => FunctionCalculate.DigitButton_Click(sender, e, DisplayTextBox);

                // Вычисляем позицию в Grid (DigitsGrid)
                int row = (i - 1) / 3; // Рассчитываем строку (0, 1 или 2)
                int col = (i - 1) % 3; // Рассчитываем столбец (0, 1 или 2)

                // Добавляем кнопку в DigitsGrid
                DigitsGrid.Children.Add(btn);
                Grid.SetRow(btn, row);  // Устанавливаем строку кнопки в Grid
                Grid.SetColumn(btn, col); // Устанавливаем столбец кнопки в Grid
            }


            for (int i = 0; i < operationSymbols.Length; i++)
            {
                Button operationButton = new Button();
                operationButton.Content = operationSymbols[i];
                operationButton.Width = 65;
                operationButton.Height = 65;
                operationButton.Margin = new Thickness(1);
                operationButton.Click += (sender, e) => FunctionCalculate.OperationButton_Click(sender, e, DisplayTextBox, LastOperationLabel);


                DigitsGrid.Children.Add(operationButton);
                Grid.SetRow(operationButton, i);  // Устанавливаем строку кнопки в Grid (первая строка)
                Grid.SetColumn(operationButton, 4); // Устанавливаем столбец кнопки в Grid (пятый столбец)
            }


            Button btnNull = new Button();
            btnNull.Content = "0";
            btnNull.Width = 65;
            btnNull.Height = 65;
            btnNull.Margin = new Thickness(1);
            btnNull.Click += (sender, e) => FunctionCalculate.DigitButton_Click(sender, e, DisplayTextBox);

            // Добавляем кнопку в DigitsGrid
            DigitsGrid.Children.Add(btnNull);
            Grid.SetRow(btnNull, 3);  // Устанавливаем строку кнопки в Grid
            Grid.SetColumn(btnNull, 1); // Устанавливаем столбец кнопки в Grid


            Button Equals = new Button();
            Equals.Content = "=";
            Equals.Width = 65;
            Equals.Height = 65;
            Equals.Margin = new Thickness(1);
            Equals.Click += (sender, e) => FunctionCalculate.EqualsButton_Click(sender, e, DisplayTextBox);

            // Добавляем кнопку в DigitsGrid
            DigitsGrid.Children.Add(Equals);
            Grid.SetRow(Equals, 3);  // Устанавливаем строку кнопки в Grid
            Grid.SetColumn(Equals, 2); // Устанавливаем столбец кнопки в Grid


            Button ClearAll = new Button();
            ClearAll.Content = "CE";
            ClearAll.Width = 65;
            ClearAll.Height = 65;
            ClearAll.Margin = new Thickness(1);
            ClearAll.Click += (sender, e) => FunctionCalculate.ClearAll_Click(sender, e, DisplayTextBox, LastOperationLabel);


            // Добавляем кнопку в DigitsGrid
            DigitsGrid.Children.Add(ClearAll);
            Grid.SetRow(ClearAll, 3);  // Устанавливаем строку кнопки в Grid
            Grid.SetColumn(ClearAll, 0); // Устанавливаем столбец кнопки в Grid
        

        }


        private void DisplayTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            // Проверяем, является ли введенный символ цифрой или запятой
            if (!char.IsDigit(e.Text, 0) && e.Text != ",")
            {
                e.Handled = true; // Если символ не цифра и не запятая, игнорируем его
            }
        }


    }
}