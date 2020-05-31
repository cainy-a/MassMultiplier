using System.Collections.Generic;

namespace MassMultiplier
{
	public class MultiplicationSum
	{
		public MultiplicationSum(int firstNum, int lastNum)
		{
			FirstNum = firstNum;
			LastNum = lastNum;
		}

		public int FirstNum;
		public int LastNum;

		public override string ToString()
			=> $"{FirstNum} x {LastNum} = {FirstNum * LastNum}";

		public int GetProduct() => FirstNum * LastNum;

		public static int[] GetProducts(MultiplicationSum[] sums, out List<int> list)
		{
			var toReturn = new List<int>();
			foreach (var sum in sums)
			{
				toReturn.Add(sum.GetProduct());
			}

			list = toReturn;
			return toReturn.ToArray();
		}
	}
}