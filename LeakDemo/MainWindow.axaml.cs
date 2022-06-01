using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Threading;
using JetBrains.Annotations;

namespace LeakDemo
{
    public class MyViewModel : INotifyPropertyChanged
    {
        public string RamUsage { get; private set; }

        public event PropertyChangedEventHandler? PropertyChanged;

        public MyViewModel()
        {
            DispatcherTimer.Run(UpdateRamUsage, TimeSpan.FromMilliseconds(10));
        }

        private bool UpdateRamUsage()
        {
            RamUsage = (GC.GetTotalMemory(true) / 1024) + " KB";
            OnPropertyChanged(nameof(RamUsage));
            return true;
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
    
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            DataContext = new MyViewModel();
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        private void Add_OnClick(object? sender, RoutedEventArgs e)
        {
            Panel panel = this.FindControl<Panel>("Panel");
            panel.Children.Add(new MassiveUserControl()
            {
                DataContext = new BigDataContext()
            });
        }

        private void Remove_OnClick(object? sender, RoutedEventArgs e)
        {
            Panel panel = this.FindControl<Panel>("Panel");
            panel.Children.Clear();
        }

        private void ForceGC_OnClick(object? sender, RoutedEventArgs e)
        {
            for (int i = 0; i < 10; ++i)
                GC.Collect();
        }
    }
}