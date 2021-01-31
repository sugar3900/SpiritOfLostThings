using System;
using UnityEngine;

namespace GGJ
{
	public class CharacterProp : DynamicProp
	{
		[SerializeField]
		private float moveSpeed = 8f;
		[SerializeField]
		private float moveColRadius = 0.225f;
		[SerializeField]
		private LayerMask movementCollisionLayers;
		[SerializeField]
		private Animator animator;

		private void Update()
		{
			float horizontal = Input.GetAxis("Horizontal");
			float vertical = Input.GetAxis("Vertical");
			if (vertical > Mathf.Epsilon)
			{
				Up();
			}
			else if (vertical < -Mathf.Epsilon)
			{
				Down();
			}
			else if (horizontal > Mathf.Epsilon)
			{
				Right();
			}
			else if (horizontal < -Mathf.Epsilon)
			{
				Left();
			}
			else
			{
				Idle();
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
			Collider2D[] blockingObjects = Physics2D.OverlapCircleAll(newPosition, moveColRadius, movementCollisionLayers);
			foreach (Collider2D blockingObject in blockingObjects)
			{
				if (blockingObject != null && !blockingObject.isTrigger)
				{
					return false;
				}
			}
			return true;
		}

		public float GetDistanceFrom(GameObject gameObject)
		{

			return Vector3.Distance(transform.position, gameObject.transform.position);
		}

		private CharacterState currentState;

		public void Idle() => SetState(CharacterState.Idle);

		public void Left() => SetState(CharacterState.Left);

		public void Right() => SetState(CharacterState.Right);

		public void Down() => SetState(CharacterState.Down);

		public void Up() => SetState(CharacterState.Up);

		private void SetState(CharacterState state)
		{

			if (state == currentState)
			{
				return;
			}

			currentState = state;

			PlayAnimation(GetAnimNameForState(state));
		}

		private void PlayAnimation(string stateName) => animator.CrossFade(stateName, 0.2f);

		private string GetAnimNameForState(CharacterState state)
		{
			switch (state)
			{
				case CharacterState.Idle:
					return "Idle";
				case CharacterState.Left:
					return "WalkRight";
				case CharacterState.Right:
					return "WalkRight";
				case CharacterState.Up:
					return "WalkRight";
				case CharacterState.Down:
					return "WalkRight";
				default:
					throw new ArgumentOutOfRangeException(nameof(state), state, null);
			}
		}
	}
}
