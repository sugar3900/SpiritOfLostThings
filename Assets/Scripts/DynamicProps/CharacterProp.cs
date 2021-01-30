using UnityEngine;

namespace GGJ
{
	public class CharacterProp : DynamicProp
	{
		[SerializeField]
		private float minPositionLerp = 0.01f;
		[SerializeField]
		private float maxPositionLerp = 30f;
		[SerializeField]
		private float zDepth = -0.5f;
		[SerializeField]
		private float moveSpeed = 8f;
		[SerializeField]
		private float rotateSpeed = 1f;
		[SerializeField]
		private float radiusForMovementCollision = 0.225f;
		[SerializeField]
		private float radiusForEdgeCollision = 0.225f;
		[SerializeField]
		private  LayerMask movementCollisionLayers;

		private void Update()
		{
			Vector2 input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
			ApplyMovement(input);
		}

		public void ApplyMovement(Vector2 moveDirection)
		{
			float deltaTime = Time.deltaTime;

				moveDirection *= (moveSpeed * deltaTime);
				if (moveDirection != Vector2.zero)
				{
					if (CanMoveInDirection(moveDirection))
					{
						transform.position += (Vector3)moveDirection;
					}
					else if (CanMoveInDirection(new Vector2(moveDirection.x, 0f)))
					{   //Remove the Y value, move horizontally if unblocked.
						transform.position += new Vector3(moveDirection.x, 0f);
					}
					else if (CanMoveInDirection(new Vector2(0f, moveDirection.y)))
					{   //Remove the X value, move vertically if unblocekd.
						transform.position += new Vector3(0f, moveDirection.y);
					}
					float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
					Quaternion newRot = Quaternion.AngleAxis(angle, Vector3.forward);
					transform.rotation = Quaternion.Slerp(transform.rotation, newRot, deltaTime * rotateSpeed);
			}
		}

		private bool CanMoveInDirection(Vector2 moveDirection)
		{
			Vector2 newPosition = (Vector2)transform.position + moveDirection;
			Collider2D[] blockingObjects = Physics2D.OverlapCircleAll(newPosition, radiusForMovementCollision, movementCollisionLayers);
			foreach (Collider2D blockingObject in blockingObjects)
			{
				if (blockingObject != null && !blockingObject.isTrigger)
				{
					return false;
				}
			}
			return true;
		}
	}
}
