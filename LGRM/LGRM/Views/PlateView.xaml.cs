using System;
using System.Diagnostics;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LGRM.XamF.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PlateView : ContentPage
    {        
        public ICommand TestCommand { get; set; }

        public PlateView()
        {
            InitializeComponent();
            BindingContext = App.MyPlateVM;

            TestCommand = new Command(OnTestCommand);
        }

        public void OnTestCommand()
        {
            Debug.WriteLine("~~ TestCommand Called form PlateView.xaml.cs !!!");
            Debug.WriteLine("~~ TestCommand Called form PlateView.xaml.cs !!!");
            Debug.WriteLine("~~ TestCommand Called form PlateView.xaml.cs !!!");
        }



        async void GoToGreensButton_Clicked(object sender, System.EventArgs e)
        {
            indicatorG.IsRunning = true;
            await Navigation.PushAsync(App.MyListOfGreensView);
            indicatorG.IsRunning = false;
        }

    }
}
