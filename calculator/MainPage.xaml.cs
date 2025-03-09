using System;
using Microsoft.Maui.Controls;

namespace calculator
{
    public partial class MainPage : ContentPage
    {
        private double _result = 0;
        private string _operation = string.Empty;
        private bool _isOperationPerformed = false;
        private string _history = string.Empty;

        public MainPage()
        {
            InitializeComponent();
        }

        private void OnButtonClicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            if (button != null)
            {
                if (_isOperationPerformed)
                {
                    ResultLabel.Text = string.Empty;
                    _isOperationPerformed = false;
                }

                ResultLabel.Text += button.Text;
            }
        }

        private void OnClearClicked(object sender, EventArgs e)
        {
            ResultLabel.Text = string.Empty;
            _result = 0;
            _operation = string.Empty;
            _history = string.Empty;
            HistoryLabel.Text = string.Empty; // Очищаем историю
        }

        private void OnClearLastClicked(object sender, EventArgs e)
        {
            if (ResultLabel.Text.Length > 0)
            {
                ResultLabel.Text = ResultLabel.Text.Remove(ResultLabel.Text.Length - 1);
                
            }
        }

        private void OnEqualClicked(object sender, EventArgs e)
        {
            if (double.TryParse(ResultLabel.Text, out double secondOperand))
            {
                switch (_operation)
                {
                    case "+":
                        _result += secondOperand;
                        break;
                    case "-":
                        _result -= secondOperand;
                        break;
                    case "*":
                        _result *= secondOperand;
                        break;
                    case "/":
                        if (secondOperand != 0)
                        {
                            _result /= secondOperand;
                        }
                        else
                        {
                            ResultLabel.Text = "Error"; // не даёт нулю барагозить 
                        }
                        break;
                }

                // Сохраняем результат в истории США
                _history += $"{_result} {Environment.NewLine}";
                HistoryLabel.Text = _history;

                ResultLabel.Text = _result.ToString();
                _operation = string.Empty;
            }
        }

        private void OnOperationClicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            if (button != null)
            {
                if (_result != 0)
                {
                    OnEqualClicked(sender, e);
                }

                _operation = button.Text;
                _isOperationPerformed = true;

                // Сохраняем текущее значение в _result ради бога
                if (double.TryParse(ResultLabel.Text, out double currentValue))
                {
                    _result = currentValue;
                }
            }
        }

        private void OnSquareRootClicked(object sender, EventArgs e)
        {
            if (double.TryParse(ResultLabel.Text, out double value))
            {
                _result = Math.Sqrt(value);
                ResultLabel.Text = _result.ToString();
                // Добавляем в историю Китая
                _history += $"√{value} = {_result} {Environment.NewLine}";
                HistoryLabel.Text = _history;
            }
        }

        private void OnPercentageClicked(object sender, EventArgs e)
        {
            if (double.TryParse(ResultLabel.Text, out double value))
            {
                _result = value / 100;
                ResultLabel.Text = _result.ToString();
                // Добавляем в историю Индии
                _history += $"{value}% = {_result} {Environment.NewLine}";
                HistoryLabel.Text = _history;
            }
        }

        private void OnPowerClicked(object sender, EventArgs e)
        {
            if (double.TryParse(ResultLabel.Text, out double value))
            {
                _result = Math.Pow(value, 2); // Возведение в квадрат круга
                ResultLabel.Text = _result.ToString();
                // Добавляем в историю
                _history += $"{value}^2 = {_result} {Environment.NewLine}";
                HistoryLabel.Text = _history;
            }
        }

        private void OnToggleSignClicked(object sender, EventArgs e)
        {
            if (double.TryParse(ResultLabel.Text, out double value))
            {
                _result = -value;
                ResultLabel.Text = _result.ToString();
            }
        }
    }
}
