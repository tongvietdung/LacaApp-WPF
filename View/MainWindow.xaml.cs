using LacaApp.ViewModel;
using System.Windows;
using System.Windows.Input;

namespace LacaApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        readonly MainViewModel vm;

        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainViewModel();
            vm = (MainViewModel)DataContext;
        }

        // Double click handler to edit recipe
        private void ListViewItem_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            vm.ExecuteOpenAddRecipeWindowCommand(RecipeList.SelectedIndex);
        }
    }
}
