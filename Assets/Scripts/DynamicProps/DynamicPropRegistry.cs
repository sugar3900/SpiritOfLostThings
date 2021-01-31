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
			foreach (DynamicProp dynamicProp in dynamicProps)
			{
				if (dynamicProp != null && dynamicProp.Id == id)
				{
					return dynamicProp;
				}
			}
			return null;
		}
	}
}
