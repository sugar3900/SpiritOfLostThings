using UnityEngine;

namespace GGJ
{
	public class PropRegistry : ScriptableObject
	{
		[SerializeField]
		private GameObject[] props;

		public GameObject GetProp(string id)
		{
			foreach (GameObject prop in props)
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
