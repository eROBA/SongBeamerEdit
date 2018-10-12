using SongBeamerEdit.ViewModel;
using System.ComponentModel;
using System.Windows;

namespace SongBeamerEdit
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MainViewModel vm;

        public MainWindow()
        {
            InitializeComponent();
            vm = (MainViewModel)this.TryFindResource("vm");
            if (vm != null)
            {
                this.CommandBindings.Add(vm.NewCommandBinding);
                this.CommandBindings.Add(vm.DeleteCommandBinding);
                this.CommandBindings.Add(vm.SaveCommandBinding);
                this.CommandBindings.Add(vm.UndoCommandBinding);
            }
            this.Closing += MainWindow_Closing;
            vm.ConfirmDeleting += Vm_ConfirmDeleting;
        }

        private void Vm_ConfirmDeleting(object sender, CancelEventArgs e)
        {
            string message = "Wollen Sie wirklich löschen?";
            string caption = "MVVM_Sample";
            if (MessageBoxResult.Yes == MessageBox.Show(message, caption, MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No))
            {
                e.Cancel = false;
                return;
            }
            e.Cancel = true;
        }

        void MainWindow_Closing(object sender, CancelEventArgs e)
        {
            vm.CancelViewClosing();
        }
    }
}
