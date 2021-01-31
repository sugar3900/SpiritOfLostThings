using System.Runtime.Serialization;

namespace GGJ
{
	public class LevelDynamicPropData : ISerializable
	{
		public string Id { get; set; }
		public int X { get; set; }
		public int Y { get; set; }

		public LevelDynamicPropData()
		{
		}

		public LevelDynamicPropData(SerializationInfo info, StreamingContext context)
		{
			Id = (string)info.GetValue("Id", typeof(string));
			X = (int)info.GetValue("X", typeof(int));
			Y = (int)info.GetValue("Y", typeof(int));
		}

		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue("Id", Id);
			info.AddValue("X", Y);
			info.AddValue("Y", Y);
		}
	}
}
