using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace LGRM.XamF.Views
{
    public class CustomEntry : Entry
    {
        public static readonly BindableProperty TextChangedCommandProperty = BindableProperty.Create(nameof(TextChangedCommand), typeof(ICommand), typeof(CustomEntry));
        public ICommand TextChangedCommand
        {
            get => (ICommand)GetValue(TextChangedCommandProperty);
            set => SetValue(TextChangedCommandProperty, value);
        }

        public CustomEntry() : base()
        {
            TextChanged += (sender, args) =>
            {
                TextChangedCommand.Execute(args.NewTextValue);
            };
        }
    }
}
