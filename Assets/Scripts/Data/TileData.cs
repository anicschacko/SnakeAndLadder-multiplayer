using UnityEngine;

namespace SAL.Data
{
	[System.Serializable]
	public enum SpecialType
	{
		None = 0,
		Snakes,
		Ladder
	}
	
	[System.Serializable]
	public class SpecialTileData
	{
		public int startTileId = -1;
		public int endTileId = -1;
	}
	
	[System.Serializable]
	public class TileData
	{
		public int id;
		public bool isSpecial;
		public SpecialType specialType;
		public SpecialTileData specialTileData;
		public string mapData;
	}
}