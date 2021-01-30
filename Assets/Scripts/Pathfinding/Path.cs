using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace GGJ
{
	public class Path : IEnumerator<PathEdge>
	{
		public bool IsComplete { get; }
		public List<PathEdge> Edges { get; }
		public float Cost { get; }
		public PathEdge Current => Edges[index];
		object IEnumerator.Current => Edges.GetEnumerator();
		public bool MoveNext() => ++index < Edges.Count;
		public void Reset() => index = 0;
		public void Dispose() => Edges.Clear();
		public override string ToString() => $"Path: Cost: {Cost}, Edges: {Edges.Count}, IsComplete: {IsComplete}";
		private int index;
		public Path(List<PathEdge> edges, bool isComplete)
		{
			Edges = edges;
			Cost = edges.Sum(e => e.Cost);
			IsComplete = isComplete;
		}
	}
}
