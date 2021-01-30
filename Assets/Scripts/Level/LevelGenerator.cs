using Newtonsoft.Json;
using System.IO;
using UnityEngine;

namespace GGJ
{
	public class LevelGenerator : MonoBehaviour
	{
		[SerializeField]
		private GameObject levelPrefab;
		[SerializeField]
		private string _levelFileName = "Level";
		[SerializeField]
		private TileSetRegistry tileSetRegistry;
		[SerializeField]
		private PropRegistry propRegistry;
		private string LevelFilePath => $"{Application.streamingAssetsPath}/{_levelFileName}.json";

		private void Start()
		{
			GenerateLevel();
		}

		public Level GenerateLevel()
		{
			LevelData levelData = LoadLevelData();
			return levelData != null ? CreateLevelObject(levelData) : null;
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

		private Level CreateLevelObject(LevelData levelData)
		{
			GameObject instance = GameObject.Instantiate(levelPrefab);
			Level level = instance.GetComponent<Level>();
			try
			{
				bool[,] navGrid = GetNavGrid(levelData);
				GameObject[,] tiles = GenerateTiles(levelData, level.transform);
				GameObject[] props = GenerateProps(levelData, level.transform);
				level.Initialize(navGrid, tiles, props);
			}
			catch
			{
				throw;
			}
			return level;
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

		private GameObject[,] GenerateTiles(LevelData levelData, Transform parent)
		{
			int width = levelData.Width;
			int height = levelData.Height;
			GameObject[,] tileGrid = new GameObject[width, height];
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
						if (tileCase != TileCase.Empty)
						{
							tileGrid[x, y] = GenerateTile(x, y, tileCase, tileSetId, parent);
						}
					}
				}
			}
			return tileGrid;
		}

		public GameObject GenerateTile(int x, int y, TileCase tileCase, string tileSetId, Transform parent)
		{
			TileSet tileSet = tileSetRegistry.GetTileSet(tileSetId);
			if (tileSet != null)
			{
				GameObject prefab = tileSet.GetPrefab(tileCase);
				if (prefab != null)
				{
					float rotZ = tileCase.GetRotationZ();
					return GameObject.Instantiate(prefab, new Vector3(x, y), Quaternion.Euler(0f, 0f, rotZ), parent);
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

		private GameObject[] GenerateProps(LevelData levelData, Transform parent)
		{
			GameObject[] props = new GameObject[levelData.Props.Count];
			for (int i = 0; i < levelData.Props.Count; i++)
			{
				LevelPropData levelPropData = levelData.Props[i];
				GameObject prop = GenerateProp(levelPropData, parent);
				props[i] = prop;
			}
			return props;
		}

		public GameObject GenerateProp(LevelPropData propData, Transform parent)
		{
			GameObject prefab = propRegistry.GetProp(propData.Id);
			if (prefab != null)
			{
				return GameObject.Instantiate(prefab, propData.Position, propData.Rotation, parent);
			}
			else
			{
				Debug.LogWarning("No prefab found with the id " + propData.Id);
			}
			return null;
		}
	}
}
