using System.Collections.Generic;
using UnityEngine;

namespace KiwiAttributes.Test
{
	//[CreateAssetMenu(fileName = "KiwiScriptableObject", menuName = "KiwiAttributes/_KiwiScriptableObject")]
	public class _KiwiScriptableObject : ScriptableObject
	{
		[Expandable]
		public List<_TestScriptableObject> list;
	}
}
