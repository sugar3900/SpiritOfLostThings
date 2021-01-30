using System;
using UnityEngine;

namespace GGJ
{
	[Serializable]
	public class LevelCreator
	{
		[SerializeField]
		private LevelCreatorToolbar toolbar;
		[SerializeField]
		private bool canEdit;
		private Level level;
		private LevelData levelData;
		private PropRegistry propRegistry;
		private TileSetRegistry tileSetRegistry;
		private MemoryItemRegistry memoryItemRegistry;
		private Action<Level, LevelData> rebuildCallback;
		private Action<LevelData> saveCallback;

		public void SetData(Level level, LevelData levelData)
		{
			this.level = level;
			this.levelData = levelData;
		}

		public void SetCallbacks(
			Action<Level, LevelData> rebuildCallback,
			Action<LevelData> saveCallback)
		{
			this.rebuildCallback = rebuildCallback;
			this.saveCallback = saveCallback;
		}

		public void SetRegistries(
			TileSetRegistry tileSetRegistry,
			PropRegistry propRegistry,
			MemoryItemRegistry memoryItemRegistry)
		{
			this.tileSetRegistry = tileSetRegistry;
			this.propRegistry = propRegistry;
			this.memoryItemRegistry = memoryItemRegistry;
			toolbar.SetRegistries(tileSetRegistry, propRegistry, memoryItemRegistry);
		}

		public void OnUpdate()
		{
			if (canEdit)
			{
				toolbar.IsShown = true;
				HandleInputs();
				toolbar.OnUpdate();
			}
			else
			{
				toolbar.IsShown = false;
			}
		}

		private void Save() => saveCallback?.Invoke(levelData);

		private void Rebuild() => rebuildCallback?.Invoke(level, levelData);

		private void RebuildAndSave()
		{
			Rebuild();
			Save();
		}

		private void HandleInputs()
		{
			if (Input.GetMouseButtonDown(0))
			{
				switch (toolbar.Mode)
				{
					case LevelCreatorMode.Tiles:
						ChangeTile();
						break;
					case LevelCreatorMode.Props:
						ChangeProp();
						break;
					case LevelCreatorMode.Items:
						ChangeItem();
						break;
				}
			}
			else if (Input.GetMouseButtonDown(1))
			{
				switch (toolbar.Mode)
				{
					case LevelCreatorMode.Tiles:
						ClearTile();
						break;
					case LevelCreatorMode.Props:
						ClearProp();
						break;
					case LevelCreatorMode.Items:
						ClearItem();
						break;
				}
			}
		}

		private void ChangeTile()
		{
			Vector2Int coord = GetMouseCoord();
			if (levelData.GetCoordIsInBounds(coord))
			{
				TileSet newTileSet = tileSetRegistry[toolbar.Selection];
				string id = (newTileSet != null) ? newTileSet.Id : string.Empty;
				if (newTileSet != null)
				{
					levelData.SetTileSetIdAtPos(coord.x, coord.y, id);
					RebuildAndSave();
				}
			}
		}

		private void ChangeProp()
		{
			Vector2Int coord = GetMouseCoord();
			Prop prop = level.GetPropAtCoord(coord);
			Prop newProp = propRegistry[toolbar.Selection];
			if (newProp != null && (prop == null || prop.Id != newProp.Id))
			{
				if (levelData.SetPropAtCoord(coord, newProp.Id))
				{
					RebuildAndSave();
				}
			}
		}

		private void ChangeItem()
		{
			Vector2Int coord = GetMouseCoord();
			MemoryItem item = level.GetMemoryItemAtCoord(coord);
			MemoryItem newItem = memoryItemRegistry[toolbar.Selection];
			if (newItem != null && (item == null || item.Id != newItem.Id))
			{
				levelData.SetItemAtCoord(coord, newItem.Id);
				RebuildAndSave();
			}
		}

		private void ClearTile()
		{
			Vector2Int coord = GetMouseCoord();
			if (levelData.ClearTileAtCoord(coord))
			{
				RebuildAndSave();
			}
		}

		private void ClearProp()
		{
			Vector2Int coord = GetMouseCoord();
			if (levelData.ClearPropAtCoord(coord))
			{
				RebuildAndSave();
			}
		}

		private void ClearItem()
		{
			Vector2Int coord = GetMouseCoord();
			if (levelData.ClearItemAtCoord(coord))
			{
				RebuildAndSave();
			}
		}

		private Vector2Int GetMouseCoord()
		{
			if (level != null)
			{
				Vector2 mousePos = Input.mousePosition;
				Vector2 worldPos = Camera.main.ScreenToWorldPoint(mousePos) + new Vector3(0.5f, 0.5f);
				Vector2 posInLevel = level.transform.InverseTransformPoint(worldPos);
				return new Vector2Int((int)posInLevel.x, (int)posInLevel.y);
			}
			return default;
		}
	}
}
