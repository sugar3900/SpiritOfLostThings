using UnityEngine;

namespace GGJ
{
	public class Prop : MonoBehaviour
	{
		public string Id => gameObject.name;
		public bool isBlocking ;
		[SerializeField]
		private SpriteRenderer spriteRenderer;
		public Sprite Sprite => (spriteRenderer != null) ? spriteRenderer.sprite : null;
	}
}
