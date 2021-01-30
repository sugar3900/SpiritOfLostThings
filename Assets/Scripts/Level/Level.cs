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
	}
}
