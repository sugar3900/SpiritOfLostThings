using UnityEngine;

namespace GGJ
{
	public class CameraControl : MonoBehaviour
	{
		[SerializeField]
		private Camera localCamera;
		[SerializeField]
		private float moveSpeed = 1f;
		[SerializeField]
		private float ZoomSpeed = 3f;
		[SerializeField]
		private float ZoomDelta = 1f;
		[SerializeField]
		private float minZoom = 16f;
		[SerializeField]
		private float maxZoom = 40f;
		[SerializeField]
		private float maxLerpSqrDist = 32f;
		[SerializeField]
		private float speedDamp = 0.2f;
		protected float endZoom { get; set; }
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

		public void Update()
		{
			ApplyZoomInput(Input.mouseScrollDelta.x);
			ProgressZoom();
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
			endZoom = value;
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


		public void ApplyZoomInput(float adjustInput)
		{
			if (adjustInput != GetZoom())
			{
				if (adjustInput > 0f)
				{
					endZoom -= ZoomDelta;
				}
				else if (adjustInput < 0f)
				{
					endZoom += ZoomDelta;
				}
				endZoom = Mathf.Clamp(endZoom, minZoom, maxZoom);
			}
		}

		private void ProgressZoom()
		{
			SetZoom(Mathf.Lerp(GetZoom(), endZoom, Time.deltaTime * ZoomDelta));
		}
	}
}
