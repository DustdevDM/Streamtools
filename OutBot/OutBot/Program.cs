using Ninject;
using OutBot;
using System.Reflection;

StandardKernel kernel = new();
kernel.Load(Assembly.GetExecutingAssembly());

Bot bot = kernel.Get<Bot>();
await bot.runBot();