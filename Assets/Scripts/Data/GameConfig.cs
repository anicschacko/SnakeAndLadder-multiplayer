using System;
using System.Collections.Generic;
using UnityEngine;

namespace SAL.Data
{
	[CreateAssetMenu(menuName = "SnakesLadder/GameConfig")]
	public class GameConfig : ScriptableObject
	{
		[SerializeField] private TextAsset boardDataTextAsset;

		[SerializeField] private char snakeSymbol;
		[SerializeField] private char ladderSymbol;
		[SerializeField] private char emptySymbol;
		[SerializeField] private char playerSymbol;
		
		public List<TileData> TilesData;
		
		
		#region BoardData

		[ContextMenu("SetData")]
		public void SetData()
		{
			TilesData = new List<TileData>();
			FetchData(ParseBoardData());
		}

		public void ClearData()
		{
			TilesData.Clear();
		}

		private void FetchData(List<List<string>> listData)
		{
			int count = 1;
			var type = SpecialType.None;
			for (int i = listData.Count - 1; i >= 0; i--)
			{
				foreach (var item in listData[i])
				{
					type = SpecialType.None;
					if (item.StartsWith(snakeSymbol))
						type = SpecialType.Snakes;
					if (item.StartsWith(ladderSymbol))
						type = SpecialType.Ladder;

					TilesData.Add(new TileData()
					{
						id = count,
						isSpecial = type is SpecialType.Snakes or SpecialType.Snakes,
						specialType = type,
						specialTileData = new SpecialTileData()
						{
							startTileId = item.EndsWith("S") ? count : -1,
							endTileId = item.EndsWith("E") ? count : -1
						},
						mapData = item,
					});
					count++;
				}
			}
		}

		private List<List<string>> ParseBoardData()
		{
			if (boardDataTextAsset == null)
			{
				Debug.LogError("Board data text asset is not assigned!");
				return null;
			}

			List<List<string>> boardData = new List<List<string>>();

			string[] lines = boardDataTextAsset.text.Trim().Split('\n');

			foreach (string line in lines)
			{
				List<string> row = new List<string>();
				foreach (var symbol in line.Trim().Split('\t', StringSplitOptions.RemoveEmptyEntries))
				{
					row.Add(symbol);
				}

				boardData.Add(row);
			}

			return boardData;
		}

		#endregion
		
	}
}