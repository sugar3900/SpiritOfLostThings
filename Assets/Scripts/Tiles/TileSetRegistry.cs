using UnityEngine;

namespace GGJ
{
	public class TileSetRegistry : ScriptableObject
	{
		[SerializeField]
		private TileSet[] tileSets;
		public TileSet[] TileSets => tileSets;
		public int Count => tileSets.Length;

		public TileSet this[int index] => index < Count ? tileSets[index] : null;

		public TileSet GetTileSet(string id)
		{
			foreach (TileSet tileSet in tileSets)
			{
				if (tileSet.Id == id)
				{
					return tileSet;
				}
			}
			return null;
		}
	}
}
