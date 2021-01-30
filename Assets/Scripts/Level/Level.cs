using UnityEngine;

namespace GGJ
{
	public class Level : MonoBehaviour
	{
		[SerializeField]
		private SpriteRenderer background;


		public bool[,] NavGrid { get; set; }
		public GameObject[,] TileGrid { get; set; }
		public GameObject[] Props { get; set; }
		public GameObject[] Items { get; set; }

		public void SetSize(int width, int height)
		{
			background.transform.localScale = new Vector2(width, height);
		}

		public GameObject GetTileAtCoord(Vector2Int coord)
		{
			if (coord.x >= 0 && coord.x < TileGrid.GetLength(0) && coord.y >= 0 && coord.y < TileGrid.GetLength(1))
			{
				return TileGrid[coord.x, coord.y];
			}
			return null;
		}

		public void ClearGrid()
		{
			NavGrid = null;
			for (int y = 0; y < TileGrid.GetLength(1); y++)
			{
				for (int x = 0; x < TileGrid.GetLength(0); x++)
				{
					Destroy(TileGrid[x, y]);
				}
			}
			TileGrid = null;
		}
	}
}
