using KnifeStoreWpf.Data;
using KnifeStoreWpf.VM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace KnifeStoreWpf.UI
{
    /// <summary>
    /// Interaction logic for KnifeEditorWindow.xaml
    /// </summary>
    public partial class KnifeEditorWindow : Window
    {
        private EditorViewModel vM;

        public KnifeEditorWindow()
        {
            InitializeComponent();
            this.vM = this.FindResource("VM") as EditorViewModel;
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="KnifeEditorWindow"/> class.
        /// Constructor needed to edit elements.
        /// </summary>
        /// <param name="oldKnife">The item which is edited.</param>
        public KnifeEditorWindow(Knife oldKnife)
            : this()
        {
            this.vM.Knife= oldKnife;
        }

        /// <summary>
        /// Gets KnifeStore.
        /// </summary>
        public Knife Knife{ get => this.vM.Knife; }

        private void OK_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
