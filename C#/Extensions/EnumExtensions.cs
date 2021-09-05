using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;

public static class EnumExtensions
{
	/// <summary>
	/// Returns an enumerator description attribute if present, else the enum as string.
	/// </summary>
	public static string GetDescription(this Enum en)
	{
		var fi = en.GetType().GetField(en.ToString());
		if (fi == null) return en.ToString();
		var attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);
		return ((attributes.Length > 0)
				&& (!string.IsNullOrEmpty(attributes[0].Description)))
			? attributes[0].Description
			: en.ToString();
	}
	
	/// <summary>
	/// Returns a list of enum values of the given type.
	/// </summary>
	/// <typeparam name="T">the enum type</typeparam>
	/// <returns>the List of enum values</returns>
	public static List<T> ListFromEnum<T>()
	{
		return Enum.GetValues(typeof(T)).Cast<T>().ToList();
	}
}