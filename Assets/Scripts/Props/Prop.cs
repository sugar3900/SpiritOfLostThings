using UnityEngine;

namespace GGJ
{
	public class Prop : MonoBehaviour
	{
		public string Id => gameObject.name;
		[SerializeField]
		public bool isBlocking ;
		public bool IsBlocking => isBlocking;
		[SerializeField]
		private SpriteRenderer spriteRenderer;
		public Sprite Sprite => (spriteRenderer != null) ? spriteRenderer.sprite : null;
	}
}
