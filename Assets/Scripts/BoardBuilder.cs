using System;
using System.Collections.Generic;
using SAL.Data;
using UnityEngine;

public class BoardBuilder : MonoBehaviour
{
	[SerializeField] private TextAsset boardDataTextAsset;

	[SerializeField] private char snakeSymbol;
	[SerializeField] private char ladderSymbol;
	[SerializeField] private char emptySymbol;
	[SerializeField] private char playerSymbol;

	private List<TileData> tilesdata;
	
	
	private void Start()
	{
		var data = ParseBoardData();
		tilesdata = new List<TileData>();
		FetchData(data);
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

				tilesdata.Add(new TileData()
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
}