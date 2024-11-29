using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Interactivity;
using RPM5_YieldReturn.Models;

namespace RPM5_YieldReturn.Views;

public partial class MainWindow : Window
{
    public ObservableCollection<int> Numbers { get; set; } = new ObservableCollection<int>();

    public MainWindow()
    {
        InitializeComponent();

        NumberListBox.ItemsSource = Numbers;
    }

    private async void OnGenerateNumbersClicked(object? sender, RoutedEventArgs e)
    {
        GeneratedButton.IsEnabled = false;
        if (int.TryParse(StartValueInput.Text, out int startValue) && 
            int.TryParse(EndValueInput.Text, out int endValue) && 
            startValue <= endValue)
        {
            Numbers.Clear();
            ProgressBar.Value = 0;
            CompletionMessage.Text = string.Empty;

            var generator = new NumberGenerator();
            var numbers = generator.GenerateNumbers(startValue, endValue).ToList();
            int totalNumbers = numbers.Count;
                
            for (int i = 0; i < totalNumbers; i++)
            {
                Numbers.Add(numbers[i]);
                ProgressBar.Value = ((i + 1) / (double)totalNumbers) * 100;
                
                await Task.Delay(500);
            }

            GeneratedButton.IsEnabled = true;
            CompletionMessage.Text = "Генерация чисел завершена!";
        }
        else
        {
            GeneratedButton.IsEnabled = true;
            CompletionMessage.Text = "Пожалуйста, введите корректные значения для диапазона.";
        }
    }
}