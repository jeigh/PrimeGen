using PrimeGen.DataAccess;
using PrimeGen.Implementation;
using System.Diagnostics;
//using Microsoft.Extensions.Hosting;
//using Microsoft.Extensions.DependencyInjection;



//var services = new ServiceCollection();

////services.AddTransient<ISiteInterface, SiteRepo>();
//services.AddTransient<SiteService>();
//services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer("blah-blah"));








//ServiceProvider ServiceProvider = services.BuildServiceProvider();
//_siteService = serviceProvider.GetService<SiteService>();
//_appDbContext = serviceProvider.GetService<ApplicationDbContext>();


//services.AddDbContext<PrimeGenContext>(options =>
//    options.UseSqlServer(builder.Configuration.GetConnectionString("PrimeGenContext") ?? throw new InvalidOperationException("Connection string 'PrimeGenContext' not found.")));











SqlClientDataAccess _dataAccess = new PrimeGen.DataAccess.SqlClientDataAccess();
//todo: make this configurable...
_dataAccess.ConnectionString = "Server=(localdb)\\Mssqllocaldb;Database=PrimeGen;Trusted_Connection=True;MultipleActiveResultSets=true";

PrimeLogic _logic = new PrimeLogic(_dataAccess);

Stopwatch sw = new Stopwatch();
long previousSecondsElapsed = 0;
sw.Start();

_logic.GeneratePrimes(sw, previousSecondsElapsed);

sw.Stop();
