using Xamarin.Forms;

namespace LGRM.XamF.Views
{
    public class MyDataTemplateSelector : DataTemplateSelector
    {
        public DataTemplate MyDataTemplate1 { get; set; }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            return MyDataTemplate1;
            
        }
    }
}