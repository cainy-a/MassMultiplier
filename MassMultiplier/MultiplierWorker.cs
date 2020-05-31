using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MassMultiplier
{
	public enum SaveModes { Full, Compact }
	
	public static class MultiplierWorker
	{
		public static List<MultiplicationSum> Main(string path, bool save = false, string savePath = null, SaveModes saveMode = SaveModes.Full)
		{
			var workingList = Multiply(path);
			if (!save) return workingList;

			var pathToSave = savePath ?? Path.Combine(Directory.GetCurrentDirectory(), "\\multiplied.txt");

			var sw = File.CreateText(pathToSave);
			var toWrite = string.Empty;
			foreach (var sum in workingList)
			{
				if (saveMode == SaveModes.Full)
					toWrite += $"{sum.ToString()}\n";
				else
					toWrite += $"{sum.GetProduct()}\n";
			}
			sw.Write(toWrite);
			sw.Dispose();
			return workingList;
		}

		private static List<MultiplicationSum> Multiply(string path)
		{
			if (!ValidatePath(path))
				throw new FileNotFoundException("The file does not exist.", path);

			string rawData;
			var sr = new StreamReader(path);
			rawData = Task.Factory.StartNew(() => sr.ReadToEndAsync()).Result.Result;
			var multiplicationStrings = rawData.Split('\n').ToList();
			var multiplicationSums = new List<MultiplicationSum>();
			for (var i = 0; i < multiplicationStrings.Count; i++)
			{
				var sum = multiplicationStrings[i];
				int num1;
				int num2;
				if (sum.Contains("x"))
				{
					num1 = Convert.ToInt32(sum.Split('x')[0]);
					num2 = Convert.ToInt32(sum.Split('x')[1]);
				}
				else if (sum.Contains("*"))
				{
					num1 = Convert.ToInt32(sum.Split('*')[0]);
					num2 = Convert.ToInt32(sum.Split('*')[1]);
				}
				else
					throw new InvalidDataException($"Sum number {i} is in an incorrect format.");

				multiplicationSums.Add(new MultiplicationSum(num1, num2));
			}

			return multiplicationSums;
		}

		public static bool ValidatePath(string path) => File.Exists(path);
	}
}