using System;
using System.Globalization;

namespace Fiziki_şəxsin_net_gəlir_hesablayıcısı
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.Clear();

			System.Threading.Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("az-AZ");

			Console.OutputEncoding = System.Text.Encoding.UTF8;

			Console.WriteLine("Fiziki şəxsin net gəlir hesablayıcısı");
			Console.WriteLine();

			decimal kocurmeMeblegi = PrintMsgAndGet<decimal>("Köçürülən məbləği daxil edin (₼): ");
			decimal bankFaizi = PrintMsgAndGet<decimal>("Bank faizini daxil edin (susmaya görə 0%): ", true, (decimal)0.00);
			decimal vergiFaizi = PrintMsgAndGet<decimal>("Nağdlaşdırma faizini daxil edin (susmaya görə 1%): ", true, 1);
			decimal sosial = PrintMsgAndGet<decimal>("Sosial müdafiə məbləğini daxil edin (susmaya görə 62,5₼): ", true, (decimal)62.5);
			decimal digerXercler = PrintMsgAndGet<decimal>("Digər xərclərin cəmini daxil edin: ");

			Console.WriteLine();
			decimal totalCosts;
			if (bankFaizi.CompareTo((decimal)0.00) > 0)
				totalCosts = ((kocurmeMeblegi * vergiFaizi / 100) * bankFaizi / 100) + sosial + digerXercler;
			else
				totalCosts = (kocurmeMeblegi * vergiFaizi / 100) + sosial + digerXercler;

			decimal gelirVergisi = (kocurmeMeblegi - totalCosts) * 5 / 100;
			decimal hamisi = gelirVergisi + totalCosts;

			Console.WriteLine($"Gəlir vergisi: {gelirVergisi}");
			Console.WriteLine($"Xərclərin cəmi: {hamisi}");
			Console.WriteLine($"Yekun net gəlir: {kocurmeMeblegi - hamisi}");

			Console.WriteLine();
			Console.Write("Yeni hesablama üçün Enter basın.");
			if (Console.ReadKey().Key == ConsoleKey.Enter)
				Main(args);
		}

		private static T PrintMsgAndGet<T>(string msg, bool hasDefaultValue = false, T defaultValue = default) where T : IConvertible, IFormattable
		{
			try
			{
				Console.Write(msg);
				string enteredValue = Console.ReadLine();
				return (T)Convert.ChangeType(enteredValue, typeof(T));
			}
			catch (Exception)
			{
				if (hasDefaultValue)
					return defaultValue;
				else
					return PrintMsgAndGet<T>(msg);
			}
		}
	}
}
