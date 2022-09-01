using System;
using System.Collections.Generic;

namespace Preference.WPF.MaterialsSelector.Core;

public static class UnitsModeConverter
{
	private static readonly double InchesToMilimeters = 25.4;

	public static double ImperialToMillimeters(int imperial, int numerator, int denominator)
	{
		if (imperial >= 0)
		{
			return ((double)imperial + (double)numerator / (double)denominator) * 25.4;
		}
		return ((double)imperial - (double)numerator / (double)denominator) * 25.4;
	}

	public static string ImperialToString(int imperial, int numerator, int denominator)
	{
		if (numerator > 0 && denominator > 0)
		{
			return $"{imperial} {numerator}/{denominator}";
		}
		return $"{imperial}";
	}

	public static double InchesToMillimeters(double inches)
	{
		double num = 25.4;
		return inches * num;
	}

	public static void MillimetersToImperial(double mm, out int imperial, out int numerator, out int denominator, int maximumFraction = 64)
	{
		double value = mm / InchesToMilimeters;
		imperial = (int)Math.Floor(Math.Abs(value));
		double num = Math.Abs(value) - (double)imperial;
		numerator = (denominator = 0);
		if (num > 0.0)
		{
			List<int> list = new List<int>();
			for (int num2 = 2; num2 <= maximumFraction; num2 *= 2)
			{
				list.Add(num2);
			}
			double num3 = 1.0;
			foreach (int item in list)
			{
				double num4 = 1.0 / (double)item;
				double value2 = Math.IEEERemainder(num, num4);
				value2 = Math.Abs(value2);
				if (value2 <= num3)
				{
					num3 = value2;
					double value3 = num / num4;
					numerator = Convert.ToInt32(value3);
					denominator = item;
				}
			}
			Reduce(ref numerator, ref denominator);
		}
		if (numerator == denominator && numerator != 0)
		{
			imperial++;
			numerator = 0;
			denominator = 2;
		}
	}

	public static string MillimetersToUnitsModeString(double mm, UnitsMode unitsMode, IFormatProvider provider, int maximumFraction = 64)
	{
		switch (unitsMode)
		{
		case UnitsMode.omMetric:
			return string.Format(provider, "{0:N2}", mm);
		case UnitsMode.omImperialDecimal:
			return string.Format(provider, "{0:N4}", MillimetersToInches(mm));
		case UnitsMode.omImperialFraction:
		{
			MillimetersToImperial(mm, out var imperial, out var numerator, out var denominator);
			return ImperialToString(imperial, numerator, denominator);
		}
		default:
			throw new ArgumentException("unitsMode");
		}
	}

	public static double MillimetersToInches(double mm)
	{
		double num = 25.4;
		return mm / num;
	}

	private static void Reduce(ref int numerator, ref int denominator)
	{
		if (numerator < denominator && numerator != 0 && numerator % 2 == 0 && denominator % 2 == 0)
		{
			numerator /= 2;
			denominator /= 2;
			Reduce(ref numerator, ref denominator);
		}
	}
}
