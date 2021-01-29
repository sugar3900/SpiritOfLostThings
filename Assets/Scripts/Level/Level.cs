using System.Collections.Generic;
using UnityEngine;

namespace GGJ
{
	public class Level : MonoBehaviour
	{
		public bool[,] NavGrid { get; private set; }
		public GameObject[,] TileGrid { get; private set; }
		public GameObject[] Props { get; private set; }

		public void Initialize(bool[,] navGrid, GameObject[,] tileGrid, GameObject[] props)
		{
			NavGrid = navGrid;
			TileGrid = tileGrid;
			Props = props;
		}
	}
}
