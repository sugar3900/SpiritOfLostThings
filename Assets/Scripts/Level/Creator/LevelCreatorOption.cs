using UnityEngine;
using UnityEngine.UI;
using System;

namespace GGJ
{
	public class LevelCreatorOption : MonoBehaviour
	{
		[SerializeField]
		private Button button;
		[SerializeField]
		private Image image;
		[SerializeField]
		private Image highlighter;
		[SerializeField]
		private Text label;

		public void Initialize(string id, Sprite sprite, Action selectCallback)
		{
			image.sprite = sprite;
			label.text = id.Replace("TileSet", string.Empty);
			button.onClick.AddListener(() => selectCallback());
		}

		public void Highlight()
		{
			highlighter.enabled = true;
		}

		public void Unhighlight()
		{
			highlighter.enabled = false;
		}
	}
}
