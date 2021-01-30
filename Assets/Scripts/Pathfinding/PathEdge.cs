namespace GGJ
{
	public class PathEdge
	{
		public PathEdge(PathNode start, PathNode end, float cost)
		{
			Start = start;
			End = end;
			Cost = cost;
		}
		public PathNode Start { get; }
		public PathNode End { get; }
		public float Cost { get; }
	}
}
