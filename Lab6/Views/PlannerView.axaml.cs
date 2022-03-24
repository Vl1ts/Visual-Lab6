using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace Lab6.Views
{
    public partial class PlannerView : UserControl
    {
        public PlannerView()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
