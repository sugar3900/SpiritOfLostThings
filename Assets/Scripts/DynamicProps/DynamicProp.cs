using UnityEngine;

namespace GGJ
{
	public class DynamicProp : MonoBehaviour
	{
		[SerializeField]
		private string id;
		public string Id => id;
		[SerializeField]
		private SpriteRenderer spriteRenderer;
		public Sprite Sprite => spriteRenderer.sprite;
	}
}
