using UnityEngine;

namespace GGJ
{
	public class Prop : MonoBehaviour, IProp
	{
		public string Id => gameObject.name;
		[SerializeField]
		private bool isBlocking;
		public bool IsBlocking => isBlocking;
		public int X => Mathf.FloorToInt(transform.position.x);
		public int Y => Mathf.FloorToInt(transform.position.y);
		[SerializeField]
		private SpriteRenderer spriteRenderer;
		public Sprite Sprite => (spriteRenderer != null) ? spriteRenderer.sprite : null;

		private void Start()
		{
			if (spriteRenderer != null)
			{
				spriteRenderer.spriteSortPoint = SpriteSortPoint.Pivot;
			}
		}
	}
}
