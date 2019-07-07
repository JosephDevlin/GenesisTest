using Xamarin.Forms;

namespace GenesisTest.Forms.UI.Behaviours
{
    public class SearchBarCancelBehaviour : Behavior<SearchBar>
    {
        protected override void OnAttachedTo(SearchBar bindable)
        {
            base.OnAttachedTo(bindable);
            bindable.TextChanged += Bindable_TextChanged;
        }

        protected override void OnDetachingFrom(SearchBar bindable)
        {
            base.OnDetachingFrom(bindable);
            bindable.TextChanged -= Bindable_TextChanged;
        }

        private void Bindable_TextChanged(object sender, TextChangedEventArgs e)
        {
            // if the cancel button is pressed to remove the text then fire the command to refresh
            if (string.IsNullOrEmpty(e.NewTextValue))
            {
                ((SearchBar)sender).SearchCommand?.Execute(null);
            }
        }
    }
}
