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

		public enum Direction {
			Left,
			Right,
			Up,
			Down,
			None
		}

		private void Update()
		{
			float horizontal = Input.GetAxis("Horizontal");
			float vertical = Input.GetAxis("Vertical");
			
			// Input -> bool
			bool pressingUp = vertical > Mathf.Epsilon;
			bool pressingDown = vertical < -Mathf.Epsilon;
			bool notPressingUpOrDown = !pressingUp && !pressingDown;
			bool pressingLeft = horizontal < -Mathf.Epsilon;
			bool pressingRight = horizontal > Mathf.Epsilon;
			bool notPressingLeftOrRight = !pressingLeft && !pressingRight;

			// bool -> Direction
			Direction verticalDirection = pressingUp ? Direction.Up : Direction.Down;
			verticalDirection = notPressingUpOrDown ? Direction.None : verticalDirection;
			Direction horizontalDirection = pressingLeft ? Direction.Left : Direction.Right;
			horizontalDirection = notPressingLeftOrRight ? Direction.None : horizontalDirection;

			// Direction -> SetState()
			switch (verticalDirection)
			{
				case Direction.Up when horizontalDirection == Direction.Left:
					SetState(CharacterState.UpLeft);
					break;
				case Direction.Up when horizontalDirection == Direction.Right:
					SetState(CharacterState.UpRight);
					break;
				case Direction.Up:
					SetState(CharacterState.Up);
					break;
				case Direction.Down when horizontalDirection == Direction.Left:
					SetState(CharacterState.DownLeft);
					break;
				case Direction.Down when horizontalDirection == Direction.Right:
					SetState(CharacterState.DownRight);
					break;
				case Direction.Down:
					SetState(CharacterState.Down);
					break;
				case Direction.None when horizontalDirection == Direction.Left:
					SetState(CharacterState.Left);
					break;
				case Direction.None when horizontalDirection == Direction.Right:
					SetState(CharacterState.Right);
					break;
				case Direction.None:
					SetState(CharacterState.Idle);
					break;
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
				case CharacterState.Idle: return "Idle";
				case CharacterState.Left: return "WalkLeft";
				case CharacterState.Right: return "WalkRight";
				case CharacterState.Up: return "WalkUp";
				case CharacterState.Down: return "WalkDown";
				case CharacterState.UpLeft: return "WalkUpLeft";
				case CharacterState.UpRight: return "WalkUpRight";
				case CharacterState.DownLeft: return "WalkDownLeft";
				case CharacterState.DownRight: return "WalkDownRight";
				default:
					throw new ArgumentOutOfRangeException(nameof(state), state, null);
			}
		}
	}
}
