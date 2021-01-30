using UnityEngine;

namespace GGJ
{
	public class DynamicProp : MonoBehaviour
	{
		[SerializeField]
		private bool isBlocking;
		public bool IsBlocking => IsBlocking;
		public string Id => gameObject.name;
		[SerializeField]
		private SpriteRenderer spriteRenderer;
		public Sprite Sprite => (spriteRenderer != null) ? spriteRenderer.sprite : null;
	}
}
