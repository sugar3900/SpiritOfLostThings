using UnityEngine;

namespace GGJ
{
	public class TileSet : ScriptableObject
	{
		[SerializeField]
		private string id;
		public string Id => id;

		[SerializeField]
		private Sprite cornerPrefab;
		[SerializeField]
		private Sprite sidePrefab;
		[SerializeField]
		private Sprite diagonalPrefab;
		[SerializeField]
		private Sprite innerCornerPrefab;
		[SerializeField]
		private Sprite solidPrefab;

		public Sprite GetSprite(TileCase tileCase)
		{

			switch (tileCase)
			{
				default:
				case TileCase.Empty:
					return null;
				case TileCase.Corner:
				case TileCase.Corner90:
				case TileCase.Corner180:
				case TileCase.Corner270:
					return cornerPrefab;
				case TileCase.Side:
				case TileCase.Side90:
				case TileCase.Side180:
				case TileCase.Side270:
					return sidePrefab;
				case TileCase.Diagonal:
				case TileCase.Diagonal90:
					return diagonalPrefab;
				case TileCase.InnerCorner:
				case TileCase.InnerCorner90:
				case TileCase.InnerCorner180:
				case TileCase.InnerCorner270:
					return innerCornerPrefab;
				case TileCase.Solid:
					return solidPrefab;
			}
		}
	}
}
