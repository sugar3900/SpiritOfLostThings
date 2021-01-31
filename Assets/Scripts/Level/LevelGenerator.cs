using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace GGJ
{
	public class LevelGenerator : MonoBehaviour
	{
		[SerializeField]
		private CharacterProp character;
		[SerializeField]
		private GameObject levelPrefab;
		[SerializeField]
		private SpriteRenderer backgroundPrefab;
		[SerializeField]
		private Tile tilePrefab;
		[SerializeField]
		private string _levelFileName = "Level";
		[SerializeField]
		private TileSetRegistry tileSetRegistry;
		[SerializeField]
		private PropRegistry propRegistry;
		[SerializeField]
		private DynamicPropRegistry dynamicPropRegistry;
		[SerializeField]
		private float propZ = -0.1f;
		[SerializeField]
		private float dynamicPropZ = -0.15f;
		[SerializeField]
		private float backgroundZ = 2f;
		[SerializeField]
		private LevelCreator levelCreator;
		[SerializeField]
		private int obstacleLayer = 8;

		public event Action<Level> onLevelGenerated;
		public event Action<DynamicProp> onDynamicPropCreated;

		private string LevelFilePath => $"{Application.streamingAssetsPath}/{_levelFileName}.json";

		private void Start()
		{
			LevelData levelData = LoadLevelData();
			Level level = levelData != null ? CreateLevelObject(levelData) : null;
			levelCreator.SetData(level, levelData);
			levelCreator.SetCallbacks(RebuildGrid, SaveLevelData);
			levelCreator.SetRegistries(tileSetRegistry, propRegistry, dynamicPropRegistry);
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
			try
			{
				UpdateLevelTileSets(levelData);
				UpdateLevelPropData(levelData);
				UpdateLevelDynamicPropData(levelData);
				using (StreamWriter file = File.CreateText(LevelFilePath))
				{
					JsonSerializer serializer = new JsonSerializer();
					serializer.Serialize(file, levelData);
				}
			}
			catch (Exception ex)
			{
				Debug.LogError(ex);
			}
		}

		private Level CreateLevelObject(LevelData levelData)
		{
			GameObject instance = Instantiate(levelPrefab);
			Level level = instance.GetComponent<Level>();
			BuildContent(level, levelData);
			if(onLevelGenerated != null)
			{
				onLevelGenerated(level);
			}
			return level;
		}

		public void RebuildGrid(Level level, LevelData levelData)
		{
			level.Clear();
			BuildContent(level, levelData);
		}

		private void BuildContent(Level level, LevelData levelData)
		{
			//level.CreateNavGrid(GetNavGrid(levelData));
			level.TileGrid = GenerateTiles(levelData, level.transform);
			level.Props = GenerateProps(levelData, level.transform);
			level.DynamicProps = GenerateDynamicProps(levelData, level.transform);
			level.Backgrounds = GenerateBackgrounds(levelData.Width, levelData.Height, level.transform);
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
					if (tileSet.IsBlocking)
					{
						BoxCollider2D col = tile.gameObject.AddComponent<BoxCollider2D>();
						col.offset = new Vector2(-0.5f, -0.5f);
						col.size = Vector2.one;
						tile.gameObject.layer = obstacleLayer;
					}
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

		private DynamicProp[] GenerateDynamicProps(LevelData levelData, Transform parent)
		{
			DynamicProp[] dynamicProps = new DynamicProp[levelData.DynamicProps.Count];
			for (int i = 0; i < levelData.DynamicProps.Count; i++)
			{
				LevelDynamicPropData levelDynamicPropData = levelData.DynamicProps[i];
				dynamicProps[i] = GenerateDynamicProp(levelDynamicPropData, parent);
			}
			return dynamicProps;
		}

		public DynamicProp GenerateDynamicProp(LevelDynamicPropData dynamicPropData, Transform parent)
		{
			DynamicProp dynamicPropPrefab = dynamicPropRegistry.GetDynamicProp(dynamicPropData.Id);
			if (dynamicPropPrefab != null)
			{
				Vector3 offset = new Vector3(0.5f, 0.5f, dynamicPropZ);
				var dynamicProp = Instantiate(dynamicPropPrefab, dynamicPropData.Position + offset, dynamicPropData.Rotation, parent);
				if (onDynamicPropCreated != null)
				{
					onDynamicPropCreated(dynamicProp);
				}
				return dynamicProp;
			}
			else
			{
				Debug.LogWarning("No prefab found with the id " + dynamicPropData.Id);
			}
			return null;
		}

		public SpriteRenderer[,] GenerateBackgrounds(int width, int height, Transform parent)
		{
			Vector3 bgTileSize = backgroundPrefab.size;
			int xCount = Mathf.CeilToInt(backgroundPrefab.size.x / width);
			int yCount = Mathf.CeilToInt(backgroundPrefab.size.y / height);
			SpriteRenderer[,] backgrounds = new SpriteRenderer[xCount, yCount];
			for (int y = 0; y < yCount; y++)
			{
				for (int x = 0; x < xCount; x++)
				{
					Vector3 pos = new Vector3(
						bgTileSize.x * (x + 0.5f) - 0.5f,
						bgTileSize.y * (y + 0.5f) - 0.5f,
						backgroundZ);
					SpriteRenderer bg = Instantiate(backgroundPrefab, pos, Quaternion.identity, parent);
					backgrounds[x, y] = bg;
				}
			}
			return backgrounds;
		}

		private void UpdateLevelTileSets(LevelData levelData)
		{
			// Initialize with the "empty" tileset at index 0:
			List<string> validTileSetIds = new List<string>() { "" };
			List<int> blockingTileSets = new List<int>();
			for (int i = 0; i < tileSetRegistry.Count; i++)
			{
				TileSet tileSet = tileSetRegistry[i];
				validTileSetIds.Add(tileSet.Id);
				if (tileSet.IsBlocking)
				{
					blockingTileSets.Add(i + 1);
				}
			}
			levelData.TileSetIds = validTileSetIds;
			levelData.BlockedTiles = blockingTileSets;
		}

		private void UpdateLevelPropData(LevelData levelData)
		{
			List<LevelPropData> toRemove = new List<LevelPropData>();
			for (int i = 0; i < levelData.Props.Count; i++)
			{
				LevelPropData data = levelData.Props[i];
				Prop prop = propRegistry.GetProp(data.Id);
				if (prop == null)
				{
					toRemove.Add(data);
				}
				else
				{
					data.IsBlocking = prop.isBlocking;
				}
			}
			if (toRemove.Count > 0)
			{
				foreach (LevelPropData levelPropData in toRemove)
				{
					levelData.Props.Remove(levelPropData);
				}
			}
		}

		private void UpdateLevelDynamicPropData(LevelData levelData)
		{
			List<LevelDynamicPropData> toRemove = new List<LevelDynamicPropData>();
			for (int i = 0; i < levelData.DynamicProps.Count; i++)
			{
				LevelDynamicPropData data = levelData.DynamicProps[i];
				DynamicProp dynamicProp = dynamicPropRegistry.GetDynamicProp(data.Id);
				if (dynamicProp == null)
				{
					toRemove.Add(data);
				}
				else
				{
					data.IsBlocking = dynamicProp.isBlocking;
				}
			}
			if (toRemove.Count > 0)
			{
				foreach (LevelDynamicPropData levelPropData in toRemove)
				{
					levelData.DynamicProps.Remove(levelPropData);
				}
			}
		}
	}
}
