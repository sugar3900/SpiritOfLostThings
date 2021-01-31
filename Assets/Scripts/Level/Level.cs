using UnityEngine;

namespace GGJ
{
	public class Level : MonoBehaviour
	{
		public Tile[,] TileGrid { get; set; }
		public SpriteRenderer[,] Backgrounds { get; set; }
		public Prop[] Props { get; set; }
		public DynamicProp[] DynamicProps { get; set; }
		public int Width => TileGrid.GetLength(0);
		public int Height => TileGrid.GetLength(1);

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
				if (prop != null)
				{
					Vector2Int propCoord = new Vector2Int(
						(int)prop.transform.position.x,
						(int)prop.transform.position.y);
					if (coord == propCoord)
					{
						return prop;
					}
				}
			}
			return null;
		}

		public void ClearTiles()
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
			TileGrid = null;
		}

		private void ClearProps()
		{
			for (int i = Props.Length - 1; i >= 0; i--)
			{
				Prop prop = Props[i];
				if (prop != null)
				{
					Destroy(prop.gameObject);
				}
			}
			Props = null;
		}

		private void ClearDynamicProps()
		{
			for (int i = DynamicProps.Length - 1; i >= 0; i--)
			{
				DynamicProp dynamicProp = DynamicProps[i];
				if (dynamicProp != null)
				{
					Destroy(dynamicProp.gameObject);
				}
			}
			DynamicProps = null;
		}

		private void ClearBackground()
		{
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
			Backgrounds = null;
		}

		public void Clear()
		{
			ClearTiles();
			ClearBackground();
			ClearProps();
		}
	}
}
