using HMI.Maui.ViewModels;

namespace HMI.Maui
{
    public partial class MainPageCopy : ContentPage
    {
        private readonly EventsViewModel _viewModel;
        int count = 0;

        public MainPageCopy(EventsViewModel viewModel)
        {
            _viewModel = viewModel;
            InitializeComponent();
            BindingContext = _viewModel;
        }

        private void OnCounterClicked(object sender, EventArgs e)
        {
            count++;

            if (count == 1)
                CounterBtn.Text = $"Clicked {count} time";
            else
                CounterBtn.Text = $"Clicked {count} times";

            SemanticScreenReader.Announce(CounterBtn.Text);
        }
    }

}
