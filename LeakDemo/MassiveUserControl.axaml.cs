using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using JetBrains.Annotations;

namespace LeakDemo
{
    public class MassiveUserControl : UserControl
    {
        public MassiveUserControl()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
    
    public class BigDataContext : INotifyPropertyChanged, ICommand
    {
        public byte[] massiveArray;
        private bool isBool;

        public ICommand SomeCommand => this;

        public BigDataContext()
        {
            massiveArray = new byte[1024 * 1024 * 1024];
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
        public event PropertyChangedEventHandler? PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}