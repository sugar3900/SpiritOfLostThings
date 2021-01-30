using System.Collections.Generic;
using UnityEngine;

namespace GGJ
{
	public class PathNode
	{
		public PathNode(Vector2 coord) => Coordinates = coord;
		public Vector2 Coordinates { get; }
		public List<PathEdge> In { get; } = new List<PathEdge>();
		public List<PathEdge> Out { get; } = new List<PathEdge>();

		/// <summary>
		/// The direct distance to the goal
		/// </summary>
		public float HeuristicCost { get; set; }
		public float Cost { get; set; }
		public PathNode Parent { get; set; }

		public void Reset()
		{
			HeuristicCost = 0f;
			Cost = 0f;
			Parent = null;
		}

		public void Connect(PathNode other)
		{
			if (other != null)
			{
				float distance = (Coordinates - other.Coordinates).sqrMagnitude;
				PathEdge edge = new PathEdge(this, other, distance);
				Out.Add(edge);
				other.In.Add(edge);
			}
		}

		public void Disconnect(PathNode other)
		{
			for (int i = Out.Count - 1; i >= 0; i--)
			{
				PathEdge edge = Out[i];
				if (edge.End == other)
				{
					Out.Remove(edge);
					other.In.Remove(edge);
				}
			}
		}
	}
}
