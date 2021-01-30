using UnityEngine;

namespace GGJ
{
	public class CharacterProp : DynamicProp
	{
		[SerializeField]
		private float moveSpeed = 1f;
		[SerializeField]
		private float depthZ = -1f;
		[SerializeField]
		private float minSearchInterval = 1f;
		private float timeOfLastSearch;
		private Vector3 moveTarget = default;
		public Level Level { get; set; }
		public PathFinder PathFinder { get; set; } = new PathFinder();
		private PathNode goalNode;
		private Path path;

		private void Update()
		{
			if(Time.time > timeOfLastSearch + minSearchInterval)
			{
				timeOfLastSearch = Time.time;
				FindPath();
			}
			MoveAlongPath();
		}

		private void FindPath()
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
			}
		}

		private void MoveAlongPath()
		{
			if (path != null)
			{
				float distance = Vector3.Distance(transform.position, moveTarget);
				Vector3 dir = (moveTarget - transform.position).normalized;
				Vector3 move = dir * Time.deltaTime * moveSpeed;
				transform.Translate(move);
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
				Vector2 coord = path.Current.End.Coordinates;
				moveTarget = new Vector3(coord.x, coord.y, depthZ);
				Debug.Log(moveTarget);
			}
		}
	}
}
