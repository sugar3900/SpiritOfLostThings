using Newtonsoft.Json;
using System.Collections.Generic;
using UnityEngine;

namespace GGJ
{
	public class LevelData
	{
		public List<LevelPropData> Props { get; set; }
		public List<string> TileSetIds { get; set; }
		public List<int> BlockedTiles { get; set; }
		[JsonIgnore]
		public int Width => Layout.Length / Height;
		public int Height { get; set; }
		public int[] Layout { get; set; }

		public string GetTileSetIdAtPos(int x, int y)
		{
			int layoutIndex = y * Height + x;
			int tileSetIndex = layoutIndex < Layout.Length ? Layout[layoutIndex] : 0;
			return tileSetIndex < TileSetIds.Count ? TileSetIds[tileSetIndex] : string.Empty;
		}

		public void SetTileSetIdAtPos(int x, int y, string value)
		{
			int layoutIndex = y * Height + x;
			int tileSetIndex = TileSetIds.IndexOf(value);
			if (tileSetIndex >= 0)
			{
				Layout[layoutIndex] = tileSetIndex;
			}
		}

		public TileCase GetTileCaseAt(string src, int x, int y)
		{
			int index = 0;
			// Lower-Left
			if (GetMatchAt(src, x, y))
			{
				index |= 1 << 0;
			}
			// Lower-Right
			if (GetMatchAt(src, x + 1, y))
			{
				index |= 1 << 1;
			}
			// Upper-Right
			if (GetMatchAt(src, x + 1, y + 1))
			{
				index |= 1 << 2;
			}
			// Upper-Left
			if (GetMatchAt(src, x, y + 1))
			{
				index |= 1 << 3;
			}
			return (TileCase)index;
		}

		private bool GetMatchAt(string src, int x, int y)
		{
			return GetTileSetIdAtPos(x, y) == src;
		}

		public bool GetIsBlockedAt(int x, int y)
		{
			string tileType = GetTileSetIdAtPos(x, y);
			if (TileSetIds != null)
			{
				int index = TileSetIds.IndexOf(tileType);
				if (BlockedTiles != null)
				{
					if (BlockedTiles.Contains(index))
					{
						return true;
					}
					else
					{
						foreach (LevelPropData levelProp in Props)
						{
							if (levelProp.IsBlocking && x == levelProp.X && y == levelProp.Y)
							{
								return true;
							}
						}
					}
				}
				else
				{
					Debug.LogWarning("No BlockedTiles found");
				}
			}
			else
			{
				Debug.LogWarning("No TileSetIds found");
			}
			return false;
		}
	}
}
