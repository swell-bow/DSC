using DSC.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DSC.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainMasterDetailPageDetail : ContentPage
    {
        public MainMasterDetailPageDetail()
        {
            InitializeComponent();

            BindingContext = new MainMasterDetailPageDetailViewModel();
        }

        private void ContentPage_Appearing(object sender, EventArgs e)
        {
            ((MainMasterDetailPageDetailViewModel)BindingContext).AppearingCommand.Execute(this);
        }
    }
}