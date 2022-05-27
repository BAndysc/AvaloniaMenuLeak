using System;
using System.Windows.Input;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace LeakDemo
{
    public class MassiveUserControl : UserControl, ICommand
    {
        public ICommand SomeCommand => this;
        public byte[] massiveArray;
        
        public MassiveUserControl()
        {
            DataContext = this;
            InitializeComponent();
            massiveArray = new byte[1024 * 1024 * 1024];
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            Console.WriteLine("Hello!");
        }

        public event EventHandler? CanExecuteChanged;
    }
}