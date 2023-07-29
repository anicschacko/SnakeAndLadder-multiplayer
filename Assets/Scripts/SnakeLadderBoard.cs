using System;
using UnityEngine.UI;

namespace SAL
{
	using System.Collections.Generic;
	using SAL.Data;
	using UnityEngine;

	public class SnakeLadderBoard : MonoBehaviour
	{
		[SerializeField] private GameConfig gameConfig;
		[SerializeField] private Vector2Int gridSize;
		[SerializeField] private GridLayoutGroup grid;
		[SerializeField] private BoardTile tilePrefab;

		private int tilesCount;
		private List<TileData> tilesData;
		private void Awake()
		{
			tilesCount = gridSize.x * gridSize.y;
			tilesData = gameConfig.TilesData;
		}

		private void Start()
		{
			InstantiateItems();
		}

		private void InstantiateItems()
		{
			BoardTile tile = null;
			int order = 1;
			
			for (int i = 0; i < 10; i++)
			{
				InstantiateRow(i, order);
				order = -order;
			}

			void InstantiateRow(int rowCount, int order)
			{
				int startIndex, endIndex, i;
				if (order == 1)
				{
					i = startIndex = rowCount * 10;
					endIndex = startIndex + 10;
					while (i < endIndex)
					{
						InstantiateTile(i);
						i++;
					}
				}
				else
				{
					i = startIndex = (rowCount * 10) + 9;
					endIndex = startIndex - 9;
					while (i >= endIndex)
					{
						InstantiateTile(i);
						i--;
					}
				}
			}
			
			void InstantiateTile(int index)
			{
				tile = Instantiate(tilePrefab, transform);
				tile.Init(tilesData[index]);
			}
		}
	}
}