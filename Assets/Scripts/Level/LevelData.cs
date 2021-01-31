using Newtonsoft.Json;
using System.Collections.Generic;
using UnityEngine;

namespace GGJ
{
	public class LevelData
	{
		public List<LevelPropData> Props { get; set; }
		public List<LevelDynamicPropData> DynamicProps { get; set; }
		public List<string> TileSetIds { get; set; }
		public List<int> BlockedTiles { get; set; }
		[JsonIgnore]
		public int Width => Layout.Length / Height;
		public int Height { get; set; }
		public int[] Layout { get; set; }

		public void Resize(int width, int height)
		{
			int length = width * height;
			if (length > 0)
			{
				int[] newLayout = new int[length];
				for (int y = 0; y < height && y < Height; y++)
				{
					for (int x = 0; x < width && x < Width; x++)
					{
						int newIndex = y * height + x;
						int oldIndex = y * Height + x;
						newLayout[newIndex] = Layout[oldIndex];
					}
				}
				Height = height;
				Layout = newLayout;
			}
		}

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

		private LevelDynamicPropData GetDynamicPropAtCoord(Vector2Int coord)
		{
			if (GetCoordIsInBounds(coord))
			{
				foreach (LevelDynamicPropData dynamicProp in DynamicProps)
				{
					if (dynamicProp.X == coord.x && dynamicProp.Y == coord.y)
					{
						return dynamicProp;
					}
				}
			}
			return null;
		}

		private LevelPropData GetPropAtCoord(Vector2Int coord)
		{
			if (GetCoordIsInBounds(coord))
			{
				foreach (LevelPropData prop in Props)
				{
					if (prop.X == coord.x && prop.Y == coord.y)
					{
						return prop;
					}
				}
			}
			return null;
		}

		public bool SetPropAtCoord(Vector2Int coord, string propId)
		{
			if (GetCoordIsInBounds(coord))
			{
				LevelPropData existingProp = GetPropAtCoord(coord);
				if (existingProp != null)
				{
					existingProp.Id = propId;
					return true;
				}
				Props.Add(new LevelPropData
				{
					Id = propId,
					X = coord.x,
					Y = coord.y
				});
				return true;
			}
			return false;
		}

		public bool SetDynamicPropAtCoord(Vector2Int coord, string dynamicPropId)
		{
			if (GetCoordIsInBounds(coord))
			{
				LevelDynamicPropData dynamicProp = GetDynamicPropAtCoord(coord);
				if (dynamicProp != null)
				{
					dynamicProp.Id = dynamicPropId;
					return true;
				}
				DynamicProps.Add(new LevelDynamicPropData
				{
					Id = dynamicPropId,
					X = coord.x,
					Y = coord.y
				});
				return true;
			}
			return false;
		}

		public bool ClearTileAtCoord(Vector2Int coord)
		{
			if (GetCoordIsInBounds(coord))
			{
				string existingId = GetTileSetIdAtPos(coord.x, coord.y);
				if (!string.IsNullOrEmpty(existingId))
				{
					SetTileSetIdAtPos(coord.x, coord.y, string.Empty);
					return true;
				}
			}
			return false;
		}

		public bool ClearPropAtCoord(Vector2Int coord)
		{
			if (GetCoordIsInBounds(coord))
			{
				LevelPropData prop = GetPropAtCoord(coord);
				if (prop != null)
				{
					Props.Remove(prop);
					return true;
				}
			}
			return false;
		}

		public bool ClearDynamicPropAtCoord(Vector2Int coord)
		{
			if (GetCoordIsInBounds(coord))
			{
				LevelDynamicPropData dynamicProp = GetDynamicPropAtCoord(coord);
				if (dynamicProp != null)
				{
					DynamicProps.Remove(dynamicProp);
					return true;
				}
			}
			return false;
		}

		public bool GetCoordIsInBounds(Vector2Int coord)
		{
			return coord.x >= 0 && coord.x < Width && coord.y >= 0 && coord.y < Height;
		}
	}
}
