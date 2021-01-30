using UnityEngine;

namespace GGJ
{
	public class CameraControl : MonoBehaviour
	{
		[SerializeField]
		private float moveSpeed = 1f;
		private void Update()
		{
			float horizontal = Input.GetAxis("Horizontal");
			float vertical = Input.GetAxis("Vertical");
			Vector3 targetPos = transform.position + new Vector3(horizontal, vertical);
			transform.position = Vector3.Lerp(transform.position, targetPos, Time.deltaTime * moveSpeed);
		}
	}
}
