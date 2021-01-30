using UnityEngine;

namespace GGJ
{
	public class Level : MonoBehaviour
	{
		[SerializeField]
		private SpriteRenderer background;

		public bool[,] NavGrid { get; private set; }
		public GameObject[,] TileGrid { get; private set; }
		public GameObject[] Props { get; private set; }

		public void Initialize(bool[,] navGrid, GameObject[,] tileGrid, GameObject[] props, int width, int height)
		{
			NavGrid = navGrid;
			TileGrid = tileGrid;
			Props = props;
			background.size = new Vector2(width, height);
		}
	}
}
