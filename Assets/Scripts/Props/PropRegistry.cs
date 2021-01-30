using UnityEngine;

namespace GGJ
{
	public class PropRegistry : ScriptableObject
	{
		[SerializeField]
		private Prop[] props;
		public Prop[] Props => props;
		public int Count => props.Length;
		public Prop this[int index] => index < Count ? props[index] : null;

		public Prop GetProp(string id)
		{
			foreach (Prop prop in props)
			{
				if (prop.Id == id)
				{
					return prop;
				}
			}
			return null;
		}
	}
}
