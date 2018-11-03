using SongBeamerEdit.ViewModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace SongBeamerEdit
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
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
            }
        }

        private void DateiText_TextChanged(object sender, TextChangedEventArgs e)
        {
            var _text = sender as TextBox;
            SongViewModel.SVM.Erkennen(_text.Text);
            SongViewModel.SVM.IsChanged = true;
    }
}
}
