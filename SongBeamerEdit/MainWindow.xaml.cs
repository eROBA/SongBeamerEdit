using SongBeamerEdit.ViewModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace SongBeamerEdit
{
    public partial class MainWindow : Window
    {
        MainViewModel mvm;
        public MainWindow()
        {
            InitializeComponent();
            mvm = (MainViewModel)this.TryFindResource("mvm");
            if (mvm != null)
            {
                this.CommandBindings.Add(mvm.SaveCommandBinding);
                this.CommandBindings.Add(mvm.SaveAsCommandBinding);
                this.CommandBindings.Add(mvm.OpenCommandBinding);
                this.CommandBindings.Add(mvm.PrintCommandBinding);
                this.Closing += MainWindow_Closing;
            }
        }
        void MainWindow_Closing(object sender, CancelEventArgs e)
        {
            mvm.CancelViewClosing();
        }

    }
}
