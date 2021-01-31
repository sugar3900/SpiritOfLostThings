﻿using System.Collections.Generic;
using UnityEngine;

namespace GGJ
{
	public class DynamicProp : MonoBehaviour, IProp
	{
		[SerializeField]
		private bool isPersistent;
		public bool IsPersistent => isPersistent;
		[SerializeField]
		private bool isBlocking;
		public bool IsBlocking => isBlocking;
		public int X => Mathf.FloorToInt(transform.position.x);
		public int Y => Mathf.FloorToInt(transform.position.y);
		public string Id => gameObject.name;
		[SerializeField]
		private SpriteRenderer spriteRenderer;
		public Sprite Sprite => (spriteRenderer != null) ? spriteRenderer.sprite : null;

		public static List<DynamicProp> Where { get; internal set; }
		private void Start()
		{
			if (spriteRenderer != null)
			{
				spriteRenderer.spriteSortPoint = SpriteSortPoint.Pivot;
			}
		}

		public virtual void Initialize(Level level)
		{
		}
	}
}
