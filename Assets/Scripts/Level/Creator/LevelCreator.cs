using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityObject = UnityEngine.Object;

namespace GGJ
{
	[Serializable]
	public class LevelCreator
	{
		[SerializeField]
		private LevelCreatorToolbar toolbar;
		[SerializeField]
		private bool canEdit;
		[SerializeField]
		private float indicatorZ = -3f;
		[SerializeField]
		private GameObject indicatorPrefab;
		private List<GameObject> indicators = new List<GameObject>();
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
			toolbar.OnModeChanged += IndicateProps;
		}

		public void CleanUp()
		{
			toolbar.OnModeChanged -= IndicateProps;
		}

		public void OnUpdate()
		{
			if (canEdit)
			{
				toolbar.IsShown = true;
				HandleInputs();
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
			IndicateProps(toolbar.Mode);
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
			Vector2Int coord = GetMouseCoord();
			DynamicProp dynamicProp = level.GetDynamicPropAtCoord(coord);
			DynamicProp newDynamicProp = dynamicPropRegistry[toolbar.Selection];
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

		private void IndicateProps(LevelCreatorMode mode)
		{
			try
			{
				ClearIndicators();
				switch (mode)
				{
					case LevelCreatorMode.Tiles:
						break;
					case LevelCreatorMode.Props:
						foreach (IProp prop in level.Props)
						{
							CreatePropIndicator(prop);
						}
						break;
					case LevelCreatorMode.DynamicProp:
						foreach (IProp prop in level.DynamicProps)
						{
							CreatePropIndicator(prop);
						}
						break;
				}
			}
			catch
			{
			}
		}

		private void ClearIndicators()
		{
			for (int i = indicators.Count - 1; i >= 0; i--)
			{
				UnityObject.Destroy(indicators[i]);
			}
		}

		private void CreatePropIndicator(IProp prop)
		{
			if (prop != null && !(prop is CharacterProp))
			{
				Vector3 pos = new Vector3(prop.X + 0.5f, prop.Y + 0.5f, indicatorZ);
				GameObject indicator = UnityObject.Instantiate(indicatorPrefab, pos, Quaternion.identity, level.transform);
				indicators.Add(indicator);
			}
		}
	}
}
