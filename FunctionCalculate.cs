using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Media;

namespace Calculator_KPO
{
    internal static class FunctionCalculate
    {


        public static double currentNumber = 0; // Текущее число
        public static double previousNumber = 0; // Предыдущее число
        public static string operation = ""; // Операция (+, -, *, /)
        public static string currentEqauls = ""; // Промежуточный ответ в textbox

        public static void ClearAll_Click(object sender, RoutedEventArgs e, TextBox displayTextBox, Label lastOperationLabel)
        {
            displayTextBox.Text = "";
            lastOperationLabel.Content = "";
            currentNumber = 0;
            previousNumber = 0;
            operation = "";
            currentEqauls = "";
        }

        public static void Clear_Click(object sender, RoutedEventArgs e, TextBox displayTextBox)
        {
            if (displayTextBox.Text.Length > 0)
            {
                displayTextBox.Text = displayTextBox.Text.Substring(0, displayTextBox.Text.Length - 1);
            }
        }

        public static void DigitButton_Click(object sender, RoutedEventArgs e, TextBox displayTextBox)
        {
            Button clickedButton = sender as Button;
            if (clickedButton != null)
            {   
                if(displayTextBox.Text == "0")
                {
                    displayTextBox.Text = "";
                }
                string digit = clickedButton.Content.ToString();
                displayTextBox.Text += digit;
            }
        }


        public static void EqualsButton_Click(object sender, RoutedEventArgs e, TextBox displayTextBox)
        {
            if (/*previousNumber != 0 &&*/ !string.IsNullOrEmpty(operation) && displayTextBox.Text != "")
            {
                // Получаем текущее число из текстового поля
                currentNumber = double.Parse(displayTextBox.Text);

                // Выполняем операцию между previousNumber и currentNumber
                PerformOperation();

                // Проверяем, было ли выполнено деление на ноль
                if (operation == "/" && currentNumber == 0)
                {
                    MessageBox.Show("Деление на ноль невозможно!");
                    displayTextBox.Text = ""; // Очищаем TextBox
                    return;
                }

                //Очищаем в TextBox
                displayTextBox.Text = "";

                previousNumber = currentNumber;

                // Обновляем Label после вычисления
                UpdateLastOperationLabel($"{previousNumber}");
            }
        }



        public static void OperationButton_Click(object sender, RoutedEventArgs e, TextBox displayTextBox, Label lastOperationLabel)
        {
            string operationSymbol;
            Button clickedButton = sender as Button;

            if (clickedButton != null && displayTextBox.Text == "")
            {
                operation = clickedButton.Content.ToString();

                // Обновляем Label с последней операцией
                lastOperationLabel.Content = $"{previousNumber} {operation}";
                return;

            }

            else if (clickedButton != null && displayTextBox.Text != "")
            {
                operationSymbol = clickedButton.Content.ToString();

                // Преобразуем текущий текст в числовое значение и сохраняем в currentNumber
                currentNumber = double.Parse(displayTextBox.Text);

                // Если у нас уже есть предыдущее число и операция, выполняем операцию
                if (previousNumber != 0 && !string.IsNullOrEmpty(operation))
                {
                    PerformOperation();
                }

                previousNumber = currentNumber;

                // Обновляем Label с последней операцией
                lastOperationLabel.Content = $"{currentNumber} {operationSymbol}";

                // Сохраняем текущую операцию
                operation = operationSymbol;

                // Очищаем TextBox для ввода следующего числа
                displayTextBox.Text = "";
            }
        }



        private static void PerformOperation()
        {
            switch (operation)
            {
                case "+":
                    currentNumber = previousNumber + currentNumber;
                    break;
                case "-":
                    currentNumber = previousNumber - currentNumber;
                    break;
                case "*":
                    currentNumber = previousNumber * currentNumber;
                    break;
                case "/":
                    if (currentNumber != 0)
                    {
                        currentNumber = previousNumber / currentNumber;
                    }
                    else
                    {
                        //MessageBox.Show("Делене на ноль невозможно!");
                    }
                    break;
                default:
                    break;
            }
        }


        private static void UpdateLastOperationLabel(string text)
        {
            MainWindow mainWindow = Application.Current.MainWindow as MainWindow;
            if (mainWindow != null)
            {
                mainWindow.LastOperationLabel.Content = text;
            }
        }

    }
}
