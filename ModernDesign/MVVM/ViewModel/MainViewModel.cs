using ModernDesign.Core;

namespace ModernDesign.MVVM.ViewModel
{
    internal class MainViewModel : ObservableObject
    {
        public RelayCommand HomeViewCommand { get; set; }
        public RelayCommand DiscoveryViewCommand { get; set; }
        public HomeViewModel HomeViewModel { get; set; }
        public DiscoverViewModel DiscoveryViewModel { get; set; }

        private object _currentView;

        public object CurrentView
        {
            get { return _currentView; }
            set
            {
                _currentView = value;
                OnPropertyChanged();
            }
        }

        public MainViewModel()
        {
            HomeViewModel = new HomeViewModel();
            DiscoveryViewModel = new DiscoverViewModel();

            _currentView = HomeViewModel;

            HomeViewCommand = new RelayCommand(o =>
            {
                CurrentView = HomeViewModel;
            });

            DiscoveryViewCommand = new RelayCommand(o =>
            {
                CurrentView = DiscoveryViewModel;
            });
        }
    }
}
