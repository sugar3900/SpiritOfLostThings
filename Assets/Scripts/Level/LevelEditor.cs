using UnityEngine;

namespace GGJ
{
	public class LevelEditor : MonoBehaviour
	{
		[SerializeField]
		private bool isEnabled;
		public LevelGenerator LevelGenerator { get; set; }
		public Level Level { get; set; }

		public void Update()
		{
			if (isEnabled && Input.GetMouseButtonDown(0))
			{
				Vector2 mousePos = Input.mousePosition;
				Vector2 worldPos = Camera.main.ScreenToWorldPoint(mousePos);
				Vector2 posInLevel = Level.transform.InverseTransformPoint(worldPos);
				GameObject prim = GameObject.CreatePrimitive(PrimitiveType.Sphere);
				prim.transform.position = posInLevel;
			}
		}
	}
}
