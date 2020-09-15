using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Diagnostics;
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

namespace TaskManager_2._1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollection<Proc> proc = new ObservableCollection<Proc>();

        public MainWindow()
        {
            InitializeComponent();

            double memory, MbMemory;

            foreach (Process process in Process.GetProcesses())
            {               
                memory = process.WorkingSet64;
                MbMemory = Math.Round(memory / 1024 / 1024, 2);
                proc.Add(new Proc() { Name = process.ProcessName, Memory = $"{MbMemory} Мб" });
            }

            list.ItemsSource = proc;
        }

        private void Remove_Click(object sender, RoutedEventArgs e)
        {
            Proc name = (Proc)list.SelectedItems[0];

            foreach (Process process in Process.GetProcessesByName(name.Name))
            {
                process.Kill();
            }

            proc.RemoveAt(list.SelectedIndex);
        }
    }
}
