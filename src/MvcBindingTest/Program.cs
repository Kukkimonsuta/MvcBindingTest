using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc.ModelBinding;
using System.Globalization;
using System.Net.Http;
using System.Diagnostics;

namespace MvcBindingTest
{
	public class Program
	{
		public static void Main(string[] args)
		{
			WarmUp().GetAwaiter().GetResult();

			Run(10).GetAwaiter().GetResult();
			Run(100).GetAwaiter().GetResult();
			Run(1000).GetAwaiter().GetResult();
			Run(2000).GetAwaiter().GetResult();
			Run(3000).GetAwaiter().GetResult();
			Run(4000).GetAwaiter().GetResult();
			Run(5000).GetAwaiter().GetResult();
			Run(6000).GetAwaiter().GetResult();
			Run(7000).GetAwaiter().GetResult();
			Run(8000).GetAwaiter().GetResult();
			Run(9000).GetAwaiter().GetResult();

			Console.ReadKey();
		}

		private static async Task WarmUp()
		{
			using (var client = new HttpClient())
			{
				using (var response = await client.PostAsync("http://localhost:7500/", new FormUrlEncodedContent(new Dictionary<string, string>())))
				{
				}
			}
		}

		private static async Task Run(int count)
		{
			var form = PrepareData(count);

			await Run($"FORM_{count}", form);
		}

		private static async Task Run(string name, Dictionary<string, string> form)
		{
			using (var client = new HttpClient())
			{
				Console.WriteLine($"{name}: starting..");
				var content = new FormUrlEncodedContent(form);

				var sw = Stopwatch.StartNew();
				using (var response = await client.PostAsync("http://localhost:7500/", content))
				{
					sw.Stop();
				}
				Console.WriteLine($"{name}: finished after {sw.Elapsed}");
			}

			Console.WriteLine();
		}

		private static Dictionary<string, string> PrepareData(int count)
		{
			var random = new Random();
			var dict = new Dictionary<string, string>();

			// root model
			dict.Add("Foo", "Bar");

			// child models
			for (var i = 0; i < count; i++)
			{
				dict.Add($"ChildModel[{i}].Created", DateTime.UtcNow.ToString("o"));
				dict.Add($"ChildModel[{i}].Num1", random.Next(100000, 999999).ToString());
				dict.Add($"ChildModel[{i}].Num2", random.Next(100000, 999999).ToString());
				dict.Add($"ChildModel[{i}].Num3", random.Next(100000, 999999).ToString());
				dict.Add($"ChildModel[{i}].Bar", "foo");
			}

			return dict;
		}
	}
}
