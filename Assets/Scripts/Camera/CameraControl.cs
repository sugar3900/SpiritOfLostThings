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
		private float zoomDelta = 3f;
		[SerializeField]
		private float minZoom = 10f;
		[SerializeField]
		private float maxZoom = 20f;
		[SerializeField]
		private float maxLerpSqrDist = 32f;
		[SerializeField]
		private float speedDamp = 0.2f;
		private float endZoom;
		[SerializeField]
		private Transform target;

		private float currentVelocityX;
		private float currentVelocityY;
		public Transform Target
		{
			get { return target; }
			set
			{
				if (target != value)
				{
					target = value;
					SnapToTarget();
				}
			}
		}

		private void Start()
		{
			endZoom = maxZoom;
		}

		public void Update()
		{
			if (localCamera.orthographicSize != endZoom)
			{
				ProgressZoom();
			}
			Move();
		}

		private void Move()
		{
			if (Target != null)
			{
				if (Vector2.SqrMagnitude(localCamera.transform.position - Target.position) < maxLerpSqrDist)
				{
					LerpToTarget();
				}
				else
				{
					SnapToTarget();
				}
			}
		}

		public float GetZoom()
		{
			return localCamera.orthographicSize;
		}

		public void SetZoom(float value)
		{
			localCamera.orthographicSize = value;
		}

		private void LerpToTarget()
		{
			localCamera.transform.position = new Vector3(
				Mathf.SmoothDamp(localCamera.transform.position.x, Target.position.x, ref currentVelocityX, speedDamp),
				Mathf.SmoothDamp(localCamera.transform.position.y, Target.position.y, ref currentVelocityY, speedDamp),
				localCamera.transform.position.z);
		}


		private void SnapToTarget()
		{
			localCamera.transform.position = new Vector3(
				Target.position.x,
				Target.position.y,
				localCamera.transform.position.z);
		}

		private void ProgressZoom()
		{
			SetZoom(Mathf.Lerp(GetZoom(), endZoom, Time.deltaTime * zoomDelta));
		}
	}
}
