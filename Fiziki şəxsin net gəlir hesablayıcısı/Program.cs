using System;
using System.Globalization;

namespace Fiziki_şəxsin_net_gəlir_hesablayıcısı
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.Clear();

			Console.OutputEncoding = System.Text.Encoding.UTF8;

			Console.WriteLine("Fiziki şəxsin net gəlir hesablayıcısı");
			Console.WriteLine();

			decimal transferAmount = PrintMsgAndGetAmount<decimal>("Köçürülən məbləği daxil edin (₼): ");
			//byte bankComission = PrintMsgAndGetAmount<byte>("Bank faizini daxil edin: ", true, 0);
			decimal taxesComission = PrintMsgAndGetAmount<decimal>("Nağdlaşdırma faizini daxil edin (susmaya görə 1%): ", true, 1);
			decimal socialComission = PrintMsgAndGetAmount<decimal>("Sosial müdafiə məbləğini daxil edin (susmaya görə 62.5₼): ", true, (decimal)62.5);
			decimal otherCostsTotal = PrintMsgAndGetAmount<decimal>("Digər xərclərin cəmini daxil edin: ");

			Console.WriteLine();

			decimal totalCosts = (transferAmount - transferAmount * taxesComission / 100 - socialComission - otherCostsTotal) * 5 / 100;
			Console.WriteLine($"Xərclərin cəmi: {totalCosts.ToString("C", CultureInfo.CreateSpecificCulture("az-AZ"))}");
			Console.WriteLine($"Net gəlir: {(transferAmount - totalCosts).ToString("C", CultureInfo.CreateSpecificCulture("az-AZ"))}");

			Console.WriteLine();
			Console.Write("Yeni hesablama üçün Enter basın.");
			if (Console.ReadKey().Key == ConsoleKey.Enter)
				Main(args);
		}

		private static T PrintMsgAndGetAmount<T>(string msg, bool hasDefaultValue = false, T defaultValue = default) where T : IConvertible, IFormattable
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
					return PrintMsgAndGetAmount<T>(msg);
			}
		}
	}
}
