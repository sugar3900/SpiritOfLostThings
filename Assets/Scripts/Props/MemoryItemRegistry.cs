using UnityEngine;

namespace GGJ
{
	public class MemoryItemRegistry : ScriptableObject
	{
		[SerializeField]
		private GameObject[] memoryItems;

		public GameObject GetMemoryItem(string id)
		{
			foreach (GameObject prop in memoryItems)
			{
				if (prop.name == id)
				{
					return prop;
				}
			}
			return null;
		}
	}
}
