using UnityEngine;

namespace GGJ
{
	public class CameraControl : MonoBehaviour
	{
		[SerializeField]
		private Camera localCamera;
		[SerializeField]
		private float zoomSpeed = 3f;
		[SerializeField]
		private float zoomInputMult = 3f;
		[SerializeField]
		private float minZoom = 10f;
		[SerializeField]
		private float maxZoom = 20f;
		[SerializeField]
		private float maxLerpSqrDist = 32f;
		[SerializeField]
		private float speedDamp = 0.2f;
		[SerializeField]
		private Transform target;
		[SerializeField]
		private LevelGenerator levelGenerator;

		private float targetZoom;
		private float currentVelocityX;
		private float currentVelocityY;

		public Transform CameraTr => localCamera.transform;
		public Transform Target
		{
			get => target;
			set => target = value;
		}
		private float CameraZ => CameraTr.position.z;
		private float Zoom
		{
			get => localCamera.orthographicSize;
			set => localCamera.orthographicSize = value;
		}

		private void Start()
		{
			targetZoom = (minZoom + maxZoom) / 2;
			levelGenerator.onDynamicPropCreated += DetectCharacterGeneration;
		}

		private void DetectCharacterGeneration(DynamicProp dynamicProp)
		{
			if (dynamicProp != null && dynamicProp is CharacterProp)
			{
				target = dynamicProp.transform;
			}
		}	

		private void Update()
		{
			ApplyInput();
			if (localCamera.orthographicSize != targetZoom)
			{
				Zoom = Mathf.Lerp(Zoom, targetZoom, Time.deltaTime * zoomSpeed);
			}
			Move();
		}

		private void ApplyInput()
		{
			float scrollY = -Input.mouseScrollDelta.y;
			if (Mathf.Abs(scrollY) > Mathf.Epsilon)
			{
				float zoomDelta = scrollY * zoomInputMult;
				targetZoom = Mathf.Clamp(targetZoom + zoomDelta, minZoom, maxZoom);
			}
		}

		private void Move()
		{
			if (Target != null)
			{
				if (Vector2.SqrMagnitude(CameraTr.position - Target.position) < maxLerpSqrDist)
				{
					LerpToTarget();
				}
				else
				{
					SnapToTarget();
				}
			}
		}

		private void LerpToTarget()
		{
			CameraTr.position = new Vector3(
				Mathf.SmoothDamp(CameraTr.position.x, Target.position.x, ref currentVelocityX, speedDamp),
				Mathf.SmoothDamp(CameraTr.position.y, Target.position.y, ref currentVelocityY, speedDamp),
				CameraZ);
		}


		private void SnapToTarget()
		{
			CameraTr.position = new Vector3(Target.position.x, Target.position.y, CameraZ);
		}
	}
}
