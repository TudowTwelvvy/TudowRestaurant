using TudowRestaurant.Data;

namespace TudowRestaurant
{
    public partial class App : Application
    {
        private DatabaseService _databaseService;

        public App(DatabaseService databaseService)
        {
            InitializeComponent();

            MainPage = new AppShell();
            _databaseService = databaseService;

            Task.Run(async () => await databaseService.InitializeDatabaseAsync()).GetAwaiter().GetResult();
        }
        protected override async void OnStart()
        {
            base.OnStart();
            //Initialize and seed database(it's the perrfect place)
            await _databaseService.InitializeDatabaseAsync();
        }
    }
}
