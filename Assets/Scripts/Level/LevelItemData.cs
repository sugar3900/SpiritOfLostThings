using Newtonsoft.Json;
using UnityEngine;

namespace GGJ
{
	public class LevelItemData
	{
		public bool IsBlocking { get; set; }
		public string Id { get; set; }
		public float X { get; set; }
		public float Y { get; set; }
		public float Z { get; set; }
		public float RotZ { get; set; }
		[JsonIgnore]
		public Vector3 Position => new Vector3(X, Y, Z);
		[JsonIgnore]
		public Quaternion Rotation => Quaternion.Euler(0f, 0f, RotZ);
	}
}
