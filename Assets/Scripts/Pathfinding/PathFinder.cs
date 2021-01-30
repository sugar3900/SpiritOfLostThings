
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GGJ
{
	public class PathFinder
	{
		private readonly HashSet<PathNode> tempPath = new HashSet<PathNode>();
		private readonly HashSet<PathNode> open = new HashSet<PathNode>();
		private readonly HashSet<PathNode> closed = new HashSet<PathNode>();

		public Path FindPath(PathGrid grid, PathNode start, PathNode goal)
		{
			if (start == null || goal == null)
			{
				return null;
			}
			grid.Reset();
			closed.Clear();
			open.Clear();
			open.Add(start);
			start.HeuristicCost = GetHeurstic(start, goal);
			PathNode current = null;
			while (open.Count > 0)
			{
				current = open.FirstOrDefault();
				open.Remove(current);
				closed.Add(current);
				if (current == goal)
				{
					break;
				}
				foreach (PathEdge edge in current.Out)
				{
					PathNode neighbor = edge.End;
					if (!closed.Contains(neighbor))
					{
						float cost = current.Cost + GetHeurstic(current, neighbor);
						if (cost < neighbor.Cost || !open.Contains(neighbor))
						{
							neighbor.Cost = cost;
							neighbor.HeuristicCost = GetHeurstic(neighbor, goal);
							neighbor.Parent = current;
							if (!open.Contains(neighbor))
							{
								open.Add(neighbor);
							}
						}
					}
				}
			}
			if (goal.Parent == null)
			{
				return null;
			}
			return ConstructPath(goal, current == goal);
		}

		private Path ConstructPath(PathNode current, bool isComplete)
		{
			tempPath.Clear();
			List<PathEdge> edges = new List<PathEdge>();
			while (!tempPath.Contains(current) && current != null)
			{
				tempPath.Add(current);
				PathEdge edge = current.In.FirstOrDefault(e => e.Start == current.Parent);
				if (edge != null)
				{
					edges.Add(edge);
				}
				current = current.Parent;
			}
			edges.Reverse();
			return new Path(edges, isComplete);
		}

		public static float GetHeurstic(PathNode a, PathNode b) =>
			(a.Coordinates - b.Coordinates).sqrMagnitude;
	}
}
