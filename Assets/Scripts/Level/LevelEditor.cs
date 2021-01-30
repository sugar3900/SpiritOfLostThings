using System;
using UnityEngine;

namespace GGJ
{
	[Serializable]
	public class LevelEditor
	{
		[SerializeField]
		private bool canEdit;
		public Level Level { get; set; }
		public LevelGenerator LevelGenerator { get; set; }
		public LevelData LevelData { get; set; }
		public void OnUpdate()
		{
			if (canEdit && Input.GetMouseButtonDown(0))
			{
				Vector2Int coord = GetMouseCoord();
				GameObject tile = Level.GetTileAtCoord(coord);
				if (tile != null)
				{
					SwitchTileAtMouse(coord.x, coord.y);
				}
			}
		}

		private void SwitchTileAtMouse(int x, int y)
		{
			string currentTileId = LevelData.GetTileSetIdAtPos(x, y);
			int index = LevelData.TileSetIds.IndexOf(currentTileId) + 1;
			if (index < -1 || index >= LevelData.TileSetIds.Count)
			{
				index = 0;
			}
			string newId = LevelData.TileSetIds[index];
			LevelData.SetTileSetIdAtPos(x, y, newId);
			LevelGenerator.RebuildGrid(Level, LevelData);
		}

		private Vector2Int GetMouseCoord()
		{
			if (Level != null)
			{
				Vector2 mousePos = Input.mousePosition;
				Vector2 worldPos = Camera.main.ScreenToWorldPoint(mousePos) + new Vector3(0.5f, 0.5f);
				Vector2 posInLevel = Level.transform.InverseTransformPoint(worldPos);
				return new Vector2Int((int)posInLevel.x, (int)posInLevel.y);
			}
			return default;
		}
	}
}
