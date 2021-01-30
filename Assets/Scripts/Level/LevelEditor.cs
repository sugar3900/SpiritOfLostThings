using UnityEngine;

namespace GGJ
{
	public class LevelEditor : MonoBehaviour
	{
		[SerializeField]
		private LevelGenerator levelGenerator;
		public Level level;

		public void SetLevel(Level level)
		{
			this.level = level;
		}

		public void Update()
		{
			if (Input.GetMouseButtonDown(0))
			{
				Vector2 mousePos = Input.mousePosition;
				Vector2 worldPos = Camera.main.ScreenToWorldPoint(mousePos);
				Vector2 posInLevel = level.transform.InverseTransformPoint(worldPos);
				var prim = GameObject.CreatePrimitive(PrimitiveType.Sphere);
				prim.transform.position = posInLevel;
			}
		}
	}
}
