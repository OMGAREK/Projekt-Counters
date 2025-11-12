using System.Diagnostics.Metrics;

namespace CounterAW4E
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage()
        {
            InitializeComponent();
        }

        private void OnCounterPlusClicked(object sender, EventArgs e)
        {
            count++;

            if (count == 1)
                Counter.Text = $"Clicked {count} time";
            else
                Counter.Text = $"Clicked {count} times";

            SemanticScreenReader.Announce(Counter.Text);
        }
        private void OnCounterMinusClicked(object sender, EventArgs e)
        {
            count--;

            if (count == 1)
                Counter.Text = $"Clicked {count} time";
            else
                Counter.Text = $"Clicked {count} times";

            SemanticScreenReader.Announce(Counter.Text);
        }
    }

}
