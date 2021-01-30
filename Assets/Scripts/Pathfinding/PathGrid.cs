
using UnityEngine;

namespace GGJ
{
	public class PathGrid
	{
		public PathGrid(bool[,] sourceGrid)
		{
			int width = sourceGrid.GetLength(0);
			int height = sourceGrid.GetLength(1);
			allNodes = new PathNode[width, height];
			for (int y = 0; y < height; y++)
			{
				for (int x = 0; x < width; x++)
				{
					if (!sourceGrid[x, y])
					{
						Vector2 coord = new Vector2(x, y);
						allNodes[x, y] = new PathNode(coord);
					}
				}
			}
			ConnectChildNodes();
		}

		private readonly PathNode[,] allNodes;

		public void Reset()
		{
			foreach (PathNode node in allNodes)
			{
				node.Reset();
			}
		}

		public PathNode GetNearestNode(Vector2 position)
		{
			PathNode closest = null;
			float smallestDist = float.MaxValue;
			foreach (PathNode node in allNodes)
			{
				float dist = (node.Coordinates - position).sqrMagnitude;
				if (dist < smallestDist)
				{
					smallestDist = dist;
					closest = node;
				}
			}
			return closest;
		}

		private void ConnectChildNodes()
		{
			foreach (PathNode node in allNodes)
			{
				if (node != null)
				{
					ConnectNode(node, new Vector2(0, 1));
					ConnectNode(node, new Vector2(1, 0));
					ConnectNode(node, new Vector2(0, -1));
					ConnectNode(node, new Vector2(-1, 0));
				}
			}
		}

		private void ConnectNode(PathNode node, Vector2 offset)
		{

			Vector2 neighborPos = node.Coordinates + offset;
			if (neighborPos.x >= 0
				&& neighborPos.x < allNodes.GetLength(0)
				&& neighborPos.y >= 0
				&& neighborPos.y < allNodes.GetLength(1))
			{
				node.Connect(allNodes[(int)neighborPos.x, (int)neighborPos.y]);
			}
		}
	}
}
