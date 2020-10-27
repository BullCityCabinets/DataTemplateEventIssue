using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LGRM.XamF.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListOfGreensView : ContentPage
    {
        public ListOfGreensView()
        {
            InitializeComponent();
            BindingContext = App.MyListOfGreensVM;
            picker.SelectedIndex = 0;

        }

    }
}