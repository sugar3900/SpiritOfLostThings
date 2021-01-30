using UnityEngine;

namespace GGJ
{
	public class Level : MonoBehaviour
	{
		[SerializeField]
		private SpriteRenderer background;

		public bool[,] NavGrid { get; set; }
		public Tile[,] TileGrid { get; set; }
		public Prop[] Props { get; set; }
		public MemoryItem[] Items { get; set; }

		public void SetSize(int width, int height)
		{
			background.size = new Vector2(width * 128, height * 128);
		}

		public Tile GetTileAtCoord(Vector2Int coord)
		{
			if (coord.x >= 0 && coord.x < TileGrid.GetLength(0) && coord.y >= 0 && coord.y < TileGrid.GetLength(1))
			{
				return TileGrid[coord.x, coord.y];
			}
			return null;
		}

		public MemoryItem GetMemoryItemAtCoord(Vector2Int coord)
		{
			foreach (MemoryItem item in Items)
			{
				Vector2Int itemCoord = new Vector2Int(
					(int)item.transform.position.x,
					(int)item.transform.position.y);
				if (coord == itemCoord)
				{
					return item;
				}
			}
			return null;
		}

		public Prop GetPropAtCoord(Vector2Int coord)
		{
			foreach (Prop prop in Props)
			{
				Vector2Int propCoord = new Vector2Int(
					(int)prop.transform.position.x,
					(int)prop.transform.position.y);
				if (coord == propCoord)
				{
					return prop;
				}
			}
			return null;
		}

		public void Clear()
		{
			for (int y = 0; y < TileGrid.GetLength(1); y++)
			{
				for (int x = 0; x < TileGrid.GetLength(0); x++)
				{
					Tile tile = TileGrid[x, y];
					if (tile != null)
					{
						Destroy(tile.gameObject);
					}
				}
			}
			for (int i = Props.Length - 1; i >= 0; i--)
			{
				Prop prop = Props[i];
				if (prop != null)
				{
					Destroy(prop.gameObject);
				}
			}
			for (int i = Items.Length - 1; i >= 0; i--)
			{
				MemoryItem item = Items[i];
				if (item != null)
				{
					Destroy(item.gameObject);
				}
			}
			NavGrid = null;
			TileGrid = null;
			Items = null;
			Props = null;
		}
	}
}
