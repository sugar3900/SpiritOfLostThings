using UnityEngine;

namespace GGJ
{
	public class CharacterProp : DynamicProp
	{
		[SerializeField]
		private float depthZ = -1f;
		private Vector3 moveTarget = default;
		public Level Level { get; set; }
		public PathFinder PathFinder { get; set; } = new PathFinder();
		private PathNode goalNode;
		private Path path;

		private void OnUpdate()
		{
			if (Level != null)
			{
				PathNode end = Level.PathGrid.GetNearestNode(Camera.main.transform.position);
				if (goalNode != end)
				{
					goalNode = end;
					PathNode start = Level.PathGrid.GetNearestNode(transform.position);
					path = PathFinder.FindPath(Level.PathGrid, start, end);
					if (path != null)
					{
						MoveTowardPathEnd();
					}
				}
				MoveAlongPath();
			}
		}
		private void MoveAlongPath()
		{
			if (path != null)
			{
				float distance = Vector3.Distance(transform.position, moveTarget);
				Vector3 dir = (moveTarget - transform.position).normalized * Time.deltaTime;
				transform.Translate(dir);
				if (distance < 0.1f)
				{
					if (path.MoveNext())
					{
						PathNode node = path.Current.End;
						if (node != null)
						{
							MoveTowardPathEnd();
						}
					}
					else
					{
						path = null;
						moveTarget = transform.position;
					}
				}
			}
		}

		private void MoveTowardPathEnd()
		{
			PathNode node = path.Current.End;
			if (node != null)
			{
				Vector2Int coord = path.Current.End.Coordinates;
				moveTarget = new Vector3(coord.x, coord.y, depthZ);
			}
		}
	}
}
