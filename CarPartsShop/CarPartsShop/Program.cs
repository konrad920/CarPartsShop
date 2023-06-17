

using CarPartsShop;
using CarPartsShop.Components.CsvReader;
using CarPartsShop.Components.FileUser;
using CarPartsShop.Components.UserCommunication;
using CarPartsShop.Data;
using CarPartsShop.Data.Entities;
using CarPartsShop.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection();
services.AddSingleton<IApp, App>();
services.AddSingleton<IRepository<Employee>, ListRepository<Employee>>();
services.AddSingleton<IRepository<CarParts>, MSQLRepository<CarParts>>();
services.AddSingleton<IUserCommunication, UserCommunication>();
services.AddSingleton<ICsvReader, CsvReader>();
services.AddSingleton<IFileUser,  FileUser>();
services.AddDbContext<CarPartsDBContext>(options => options.UseSqlServer("Data Source=LAPTOP-QIGQKKJP\\SQLEXPRESS01;Initial Catalog=CarPartsStorage;Integrated Security=True;TrustServerCertificate=true"));

var serviceProvider = services.BuildServiceProvider();
var app = serviceProvider.GetService<IApp>()!;
app.Run();
