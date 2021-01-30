using UnityEngine;

namespace GGJ
{
	public class MemoryItem : MonoBehaviour
	{
		[SerializeField]
		private string id;
		public string Id => id;
		[SerializeField]
		private SpriteRenderer spriteRenderer;
		public Sprite Sprite => spriteRenderer.sprite;
	}
}
