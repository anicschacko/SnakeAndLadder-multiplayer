namespace SAL
{
	using UnityEngine;

	[CreateAssetMenu(fileName = "EnumGridData", menuName = "Custom/EnumGridData")]
	public class EnumGridData : ScriptableObject
	{
		public enum MyEnum
		{
			E,
			P,
			S,
			L
		
		}

		public MyEnum[,] grid = new MyEnum[10, 10];
	}
}