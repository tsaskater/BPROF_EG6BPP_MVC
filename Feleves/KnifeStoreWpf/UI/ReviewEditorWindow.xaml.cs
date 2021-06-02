using KnifeStoreWpf.Data;
using KnifeStoreWpf.VM;
using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace KnifeStoreWpf.UI
{
    /// <summary>
    /// Interaction logic for ReviewEditorWindow.xaml
    /// </summary>
    public partial class ReviewEditorWindow : Window
    {
        private EditorViewModel vM;

        public ReviewEditorWindow()
        {
            InitializeComponent();
            this.vM = this.FindResource("VM") as EditorViewModel;
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="ReviewEditorWindow"/> class.
        /// Constructor needed to edit elements.
        /// </summary>
        /// <param name="oldReview">The item which is edited.</param>
        public ReviewEditorWindow(Review oldReview)
            : this()
        {
            this.vM.Review = oldReview;
        }

        /// <summary>
        /// Gets KnifeStore.
        /// </summary>
        public Review Review { get => this.vM.Review; }

        private void OK_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}
