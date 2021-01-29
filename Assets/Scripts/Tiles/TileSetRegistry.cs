using UnityEngine;

namespace GGJ
{
	public class TileSetRegistry : ScriptableObject
	{
		[SerializeField]
		private TileSet[] tileSets;

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
