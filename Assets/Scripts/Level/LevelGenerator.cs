using Newtonsoft.Json;
using System;
using System.IO;
using UnityEngine;

namespace GGJ
{
	public class LevelGenerator : MonoBehaviour
	{
		[SerializeField]
		private GameObject levelPrefab;
		[SerializeField]
		private Tile tilePrefab;
		[SerializeField]
		private string _levelFileName = "Level";
		[SerializeField]
		private TileSetRegistry tileSetRegistry;
		[SerializeField]
		private PropRegistry propRegistry;
		[SerializeField]
		private MemoryItemRegistry itemRegistry;
		[SerializeField]
		private float propZ = -0.1f;
		[SerializeField]
		private float itemZ = -0.15f;
		[SerializeField]
		private LevelCreator levelCreator;
		private string LevelFilePath => $"{Application.streamingAssetsPath}/{_levelFileName}.json";

		private void Start()
		{
			LevelData levelData = LoadLevelData();
			Level level = levelData != null ? CreateLevelObject(levelData) : null;
			levelCreator.SetData(level, levelData);
			levelCreator.SetCallbacks(RebuildGrid, SaveLevelData);
			levelCreator.SetRegistries(tileSetRegistry, propRegistry, itemRegistry);
		}

		private void Update()
		{
			levelCreator.OnUpdate();
		}

		private LevelData LoadLevelData()
		{
			if (File.Exists(LevelFilePath))
			{
				string rawData = File.ReadAllText(LevelFilePath);
				JsonTextReader reader = new JsonTextReader(new StringReader(rawData));
				JsonSerializer serializer = new JsonSerializer();
				return serializer.Deserialize<LevelData>(reader);
			}
			return null;
		}

		private void SaveLevelData(LevelData levelData)
		{
			using (StreamWriter file = File.CreateText(LevelFilePath))
			{
				JsonSerializer serializer = new JsonSerializer();
				serializer.Serialize(file, levelData);
			}
		}

		private Level CreateLevelObject(LevelData levelData)
		{
			GameObject instance = Instantiate(levelPrefab);
			Level level = instance.GetComponent<Level>();
			BuildContent(level, levelData);
			return level;
		}

		public void RebuildGrid(Level level, LevelData levelData)
		{
			level.Clear();
			BuildContent(level, levelData);
		}

		private void BuildContent(Level level, LevelData levelData)
		{
			level.NavGrid = GetNavGrid(levelData);
			level.TileGrid = GenerateTiles(levelData, level.transform);
			level.Props = GenerateProps(levelData, level.transform);
			level.Items = GenerateItems(levelData, level.transform);
			level.GenerateBackgrounds(levelData.Width, levelData.Height);
		}

		private bool[,] GetNavGrid(LevelData levelData)
		{
			int width = levelData.Width;
			int height = levelData.Height;
			bool[,] navGrid = new bool[width, height];
			for (int y = 0; y < height; y++)
			{
				for (int x = 0; x < width; x++)
				{
					navGrid[x, y] = levelData.GetIsBlockedAt(x, y);
				}
			}
			return navGrid;
		}

		private Tile[,] GenerateTiles(LevelData levelData, Transform parent)
		{
			int width = levelData.Width;
			int height = levelData.Height;
			Tile[,] tileGrid = new Tile[width, height];
			foreach (string tileSetId in levelData.TileSetIds)
			{
				for (int y = 0; y < height - 1; y++)
				{
					for (int x = 0; x < width - 1; x++)
					{
						TileCase tileCase = TileCase.Solid;
						if (x < width - 1 && y < height - 1)
						{
							tileCase = levelData.GetTileCaseAt(tileSetId, x, y);
						}
						if (tileCase != TileCase.Empty && !string.IsNullOrEmpty(tileSetId))
						{
							tileGrid[x, y] = GenerateTile(x, y, tileCase, tileSetId, parent);
						}
					}
				}
			}
			return tileGrid;
		}

		public Tile GenerateTile(int x, int y, TileCase tileCase, string tileSetId, Transform parent)
		{
			TileSet tileSet = tileSetRegistry.GetTileSet(tileSetId);
			if (tileSet != null)
			{
				Sprite sprite = tileSet.GetSprite(tileCase);
				if (sprite != null)
				{
					float rotZ = tileCase.GetRotationZ();
					Tile tile = Instantiate(
						tilePrefab,
						new Vector3(x, y),
						Quaternion.Euler(0f, 0f, rotZ),
						parent);
					SpriteRenderer spriteRenderer = tile.GetComponent<SpriteRenderer>();
					spriteRenderer.sprite = sprite;
					return tile;
				}
				else
				{
					Debug.LogWarning($"No tile found with the tile set id {tileSetId} and case {tileCase}");
				}
			}
			else
			{
				Debug.LogWarning("No tile set found with the id " + tileSetId);
			}
			return null;
		}

		private Prop[] GenerateProps(LevelData levelData, Transform parent)
		{
			Prop[] props = new Prop[levelData.Props.Count];
			for (int i = 0; i < levelData.Props.Count; i++)
			{
				LevelPropData levelPropData = levelData.Props[i];
				Prop prop = GenerateProp(levelPropData, parent);
				props[i] = prop;
			}
			return props;
		}

		public Prop GenerateProp(LevelPropData propData, Transform parent)
		{
			Prop propPrefab = propRegistry.GetProp(propData.Id);
			if (propPrefab != null)
			{
				Vector3 offset = new Vector3(0.5f, 0.5f, propZ);
				return Instantiate(propPrefab, propData.Position + offset, propData.Rotation, parent);
			}
			else
			{
				Debug.LogWarning("No prefab found with the id " + propData.Id);
			}
			return null;
		}

		private MemoryItem[] GenerateItems(LevelData levelData, Transform parent)
		{
			MemoryItem[] items = new MemoryItem[levelData.Items.Count];
			for (int i = 0; i < levelData.Items.Count; i++)
			{
				LevelItemData levelItemData = levelData.Items[i];
				MemoryItem item = GenerateItem(levelItemData, parent);
				items[i] = item;
			}
			return items;
		}

		public MemoryItem GenerateItem(LevelItemData itemData, Transform parent)
		{
			MemoryItem itemPrefab = itemRegistry.GetMemoryItem(itemData.Id);
			if (itemPrefab != null)
			{
				Vector3 offset = new Vector3(0.5f, 0.5f, itemZ);
				return Instantiate(itemPrefab, itemData.Position + offset, itemData.Rotation, parent);
			}
			else
			{
				Debug.LogWarning("No prefab found with the id " + itemData.Id);
			}
			return null;
		}
	}
}
