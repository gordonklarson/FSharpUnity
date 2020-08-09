
using System.Windows;
using System.Windows.Forms;
using FSharpUnity;

namespace GUInterface
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void DLLButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = ".dll|*.dll";
            if(fileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                CSharpDLL.Text = fileDialog.FileName;
            }
        }

        private void CSharpProjButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = ".csproj|*.csproj";
            if (fileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                CSharpProj.Text = fileDialog.FileName;
            }
        }

        private void FsprojButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = ".fsproj|*.fsproj";
            if (fileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                FSprojLoc.Text = fileDialog.FileName;
            }
        }

        private void BuildDestinationButton_Click(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog fileDialog = new FolderBrowserDialog();
            if (fileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                BuildDestination.Text = fileDialog.SelectedPath;
            }
        }

        private void UpdateProjectButton_Click(object sender, RoutedEventArgs e)
        {
            FSharpUnity.FSharpUnity.UpdateProject(CSharpProj.Text, FSprojLoc.Text, BuildDestination.Text, CSharpDLL.Text);
        }

    }
}
