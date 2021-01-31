using UnityEngine;

namespace GGJ
{
	public class CharacterProp : DynamicProp
	{
		[SerializeField]
		private float moveSpeed = 8f;
		[SerializeField]
		private float radiusForMovementCollision = 0.225f;
		[SerializeField]
		private float radiusForEdgeCollision = 0.225f;
		[SerializeField]
		private LayerMask movementCollisionLayers;
		[SerializeField]
		private CharacterAnimationController animationController;

		private void Update()
		{
			float horizontal = Input.GetAxis("Horizontal");
			float vertical = Input.GetAxis("Vertical");
			if (vertical > Mathf.Epsilon)
			{
				animationController.Up();
			}
			else if (vertical < -Mathf.Epsilon)
			{
				animationController.Down();
			}
			else if (horizontal > Mathf.Epsilon)
			{
				animationController.Right();
			}
			else if (horizontal < -Mathf.Epsilon)
			{
				animationController.Left();
			}
			else
			{
				animationController.Idle();
			}
			Vector2 input = new Vector2(horizontal, vertical);
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
				{
					transform.position += new Vector3(moveDirection.x, 0f);
				}
				else if (CanMoveInDirection(new Vector2(0f, moveDirection.y)))
				{
					transform.position += new Vector3(0f, moveDirection.y);
				}
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

		public float GetDistanceFrom(GameObject gameObject){
            
			return Vector3.Distance(transform.position, gameObject.transform.position);
		}
	}
}
