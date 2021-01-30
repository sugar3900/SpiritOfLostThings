using UnityEngine;

namespace GGJ
{
	public class Level : MonoBehaviour
	{
		[SerializeField]
		private SpriteRenderer backgroundPrefab;
		[SerializeField]
		private float backgroundZ = 2f;
		public bool[,] NavGrid { get; set; }
		public Tile[,] TileGrid { get; set; }
		public SpriteRenderer[,] Backgrounds { get; set; }
		public Prop[] Props { get; set; }
		public DynamicProp[] DynamicProps { get; set; }

		public void GenerateBackgrounds(int width, int height)
		{
			Vector3 bgTileSize = backgroundPrefab.size;
			int xCount = Mathf.CeilToInt(backgroundPrefab.size.x / width);
			int yCount = Mathf.CeilToInt(backgroundPrefab.size.y / height);
			Backgrounds = new SpriteRenderer[xCount, yCount];
			for (int y = 0; y < yCount; y++)
			{
				for (int x = 0; x < xCount; x++)
				{
					Vector3 pos = new Vector3(
						bgTileSize.x * (x + 0.5f) - 0.5f,
						bgTileSize.y * (y + 0.5f) - 0.5f,
						backgroundZ);
					SpriteRenderer bg = Instantiate(backgroundPrefab, pos, Quaternion.identity, transform);
					Backgrounds[x, y] = bg;
				}
			}
		}

		public Tile GetTileAtCoord(Vector2Int coord)
		{
			if (coord.x >= 0 && coord.x < TileGrid.GetLength(0) && coord.y >= 0 && coord.y < TileGrid.GetLength(1))
			{
				return TileGrid[coord.x, coord.y];
			}
			return null;
		}

		public DynamicProp GetDynamicPropAtCoord(Vector2Int coord)
		{
			foreach (DynamicProp dynamicProp in DynamicProps)
			{
				Vector2Int dynamicPropCoord = new Vector2Int(
					(int)dynamicProp.transform.position.x,
					(int)dynamicProp.transform.position.y);
				if (coord == dynamicPropCoord)
				{
					return dynamicProp;
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
			for (int i = DynamicProps.Length - 1; i >= 0; i--)
			{
				DynamicProp dynamicProp = DynamicProps[i];
				if (dynamicProp != null)
				{
					Destroy(dynamicProp.gameObject);
				}
			}
			for (int y = 0; y < Backgrounds.GetLength(1); y++)
			{
				for (int x = 0; x < Backgrounds.GetLength(0); x++)
				{
					SpriteRenderer bg = Backgrounds[x, y];
					if (bg != null)
					{
						Destroy(bg.gameObject);
					}
				}
			}
			NavGrid = null;
			TileGrid = null;
			Backgrounds = null;
			DynamicProps = null;
			Props = null;
		}
	}
}
