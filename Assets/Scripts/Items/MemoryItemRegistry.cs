using UnityEngine;

namespace GGJ
{
	public class MemoryItemRegistry : ScriptableObject
	{
		[SerializeField]
		private GameObject[] memoryItems;

		public GameObject GetMemoryItem(string id)
		{
			foreach (GameObject item in memoryItems)
			{
				if (item.name == id)
				{
					return item;
				}
			}
			return null;
		}
	}
}
