using System;
using UnityEngine;
using UnityEngine.EventSystems;

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
		private DynamicPropRegistry dynamicPropRegistry;
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
			DynamicPropRegistry dynamicPropRegistry)
		{
			this.tileSetRegistry = tileSetRegistry;
			this.propRegistry = propRegistry;
			this.dynamicPropRegistry = dynamicPropRegistry;
			toolbar.SetRegistries(tileSetRegistry, propRegistry, dynamicPropRegistry);
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
			if (!EventSystem.current.IsPointerOverGameObject())
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
						case LevelCreatorMode.DynamicProp:
							ChangeDynamicProp();
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
						case LevelCreatorMode.DynamicProp:
							ClearDynamicProp();
							break;
					}
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

		private void ChangeDynamicProp()
		{
			Debug.Log("ChangeDynamicProp");
			Vector2Int coord = GetMouseCoord();
			DynamicProp dynamicProp = level.GetDynamicPropAtCoord(coord);
			DynamicProp newDynamicProp = dynamicPropRegistry[toolbar.Selection];
			Debug.Log(newDynamicProp);
			if (newDynamicProp != null && (dynamicProp == null || dynamicProp.Id != newDynamicProp.Id))
			{
				levelData.SetDynamicPropAtCoord(coord, newDynamicProp.Id);
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

		private void ClearDynamicProp()
		{
			Vector2Int coord = GetMouseCoord();
			if (levelData.ClearDynamicPropAtCoord(coord))
			{
				RebuildAndSave();
			}
		}

		private Vector2Int GetMouseCoord()
		{
			if (level != null)
			{
				Vector2 mousePos = Input.mousePosition;
				Vector2 worldPos = Camera.main.ScreenToWorldPoint(mousePos) + Vector3.one;
				Vector2 posInLevel = level.transform.InverseTransformPoint(worldPos);
				return new Vector2Int((int)posInLevel.x, (int)posInLevel.y);
			}
			return default;
		}
	}
}
