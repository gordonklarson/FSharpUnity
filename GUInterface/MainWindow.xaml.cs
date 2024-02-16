
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

        private void FsprojButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = ".fsproj|*.fsproj";
            if (fileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                FSprojLoc.Text = fileDialog.FileName;
            }
        }

        private void ProjectLocationButton_Click(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog fileDialog = new FolderBrowserDialog();
            if (fileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                ProjectLocation.Text = fileDialog.SelectedPath;
            }
        }

        private void UpdateProjectButton_Click(object sender, RoutedEventArgs e)
        {
            FSharpUnity.FSharpUnity.UpdateProject( FSprojLoc.Text, ProjectLocation.Text);
        }

    }
}
