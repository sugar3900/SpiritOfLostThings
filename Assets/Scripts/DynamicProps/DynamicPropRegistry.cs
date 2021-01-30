using UnityEngine;

namespace GGJ
{
	public class DynamicPropRegistry : ScriptableObject
	{
		[SerializeField]
		private DynamicProp[] dynamicProps;
		public DynamicProp[] DynamicProps => dynamicProps;
		public int Count => dynamicProps.Length;
		public DynamicProp this[int index] => index < Count ? dynamicProps[index] : null;

		public DynamicProp GetDynamicProp(string id)
		{
			foreach (DynamicProp prop in dynamicProps)
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
