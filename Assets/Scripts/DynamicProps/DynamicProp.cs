using UnityEngine;

namespace GGJ
{
	public class DynamicProp : MonoBehaviour
	{
		public bool isBlocking;
		public string Id => gameObject.name;
		[SerializeField]
		private SpriteRenderer spriteRenderer;
		public Sprite Sprite => (spriteRenderer != null) ? spriteRenderer.sprite : null;
	}
}
