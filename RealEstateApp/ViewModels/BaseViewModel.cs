using CommunityToolkit.Mvvm.ComponentModel;

namespace RealEstateApp.ViewModels
{
    public abstract partial class BaseViewModel : ObservableObject
    {
        [ObservableProperty]
        private string _title = string.Empty;

        [ObservableProperty]
        [NotifyPropertyChangedFor("IsNotBusy")]
        private bool _isBusy;

        public bool IsNotBusy => !IsBusy;
    }
}
