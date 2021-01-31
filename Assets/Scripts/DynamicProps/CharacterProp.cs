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
		[SerializeField]
		private float animationFadeSpeed;
		private CharacterState currentState;
		private float maxX;
		private float maxY;

		private Vector3 Position
		{
			get => transform.position;
			set => transform.position = new Vector3(Mathf.Clamp(value.x, 0f, maxX), Mathf.Clamp(value.y, 0f, maxY));
		}

		public override void Initialize(Level level)
		{
			maxX = level.Width;
			maxY = level.Height;
		}

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
					Position += (Vector3)moveDirection;
				}
				else if (CanMoveInDirection(new Vector2(moveDirection.x, 0f)))
				{
					Position += new Vector3(moveDirection.x, 0f);
				}
				else if (CanMoveInDirection(new Vector2(0f, moveDirection.y)))
				{
					Position += new Vector3(0f, moveDirection.y);
				}
			}
		}

		private bool CanMoveInDirection(Vector2 moveDirection)
		{
			Vector2 newPosition = (Vector2)Position + moveDirection;
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
			=> Vector3.Distance(Position, gameObject.transform.position);

		public void Idle() => SetState(CharacterState.Idle);
		public void Left() => SetState(CharacterState.Left);
		public void Right() => SetState(CharacterState.Right);
		public void Down() => SetState(CharacterState.Down);
		public void Up() => SetState(CharacterState.Up);
		private void PlayAnimation(string stateName) => animator.CrossFade(stateName, animationFadeSpeed);
		private void SetState(CharacterState state)
		{
			if (state != currentState)
			{
				currentState = state;
				PlayAnimation(GetAnimNameForState(state));
			}
		}

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
