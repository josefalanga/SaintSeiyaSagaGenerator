using System;
using System.Net.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace SeiyaSagaGen
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");

            builder.Services.AddTransient(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

            await builder.Build().RunAsync();
        }
    }

    public static class Extension
    {
        static Random random = new Random();
        public static String GetRandomElement(this List<String> list)
        {
            return list[random.Next(0, list.Count - 1)];
        }
        public static List<String> GetRandomElements(this List<String> list,int amount)
        {
            var subset = new List<String>();
            while(subset.Count != amount)
            {
                var randomItem = list[random.Next(0, list.Count - 1)];
                if (subset.Contains(randomItem))
                {
                    subset.Add(randomItem);
                }
            }
            return subset;
        }

        public static String RemoveRandomElement(this List<String> list)
        {
            var item = list.GetRandomElement();
            list.Remove(item);
            return item;
        }

        public static List<String> RemoveRandomElements(this List<String> list, int amount)
        {
            var subset = list.GetRandomElements(amount);
            foreach (var item in subset)
            {
                list.Remove(item);
            }
            return subset;
        }

        public static void RemoveIfExists(this List<String> list, String element)
        {
            if (list.Contains(element)) list.Remove(element);
        }
    }
}
