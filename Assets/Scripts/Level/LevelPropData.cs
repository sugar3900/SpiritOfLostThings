using Newtonsoft.Json;
using UnityEngine;

namespace GGJ
{
	public class LevelPropData
	{
		public bool IsBlocking { get; set; }
		public string Id { get; set; }
		public int X { get; set; }
		public int Y { get; set; }
		public float RotZ { get; set; }
		[JsonIgnore]
		public Vector3 Position => new Vector3(X, Y);
		[JsonIgnore]
		public Vector2Int Coord => new Vector2Int(X, Y);
		[JsonIgnore]
		public Quaternion Rotation => Quaternion.Euler(0f, 0f, RotZ);
	}
}
