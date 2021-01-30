using UnityEngine;

namespace GGJ
{
	public class MemoryItemRegistry : ScriptableObject
	{
		[SerializeField]
		private MemoryItem[] memoryItems;
		public MemoryItem[] MemoryItems => memoryItems;
		public int Count => memoryItems.Length;
		public MemoryItem this[int index] => index < Count ? memoryItems[index] : null;

		public MemoryItem GetMemoryItem(string id)
		{
			foreach (MemoryItem prop in memoryItems)
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
