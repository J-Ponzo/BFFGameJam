using UnityEngine;

public class FlyCamera : MonoBehaviour {

	public float cameraMoveSpeed = 7;
	public float cameraRotateSpeed = 5;

	private Vector3 pos;
	private Vector3 smoothPos;
	[Range(0,1)]
	public float smoothPosSpeed = 0.2f;

	private Vector3 rot;
	private Vector3 smoothRot;
	[Range(0,1)]
	public float smoothRotSpeed = 0.5f;

	private float smoothUpMove; 

	[Tooltip("Lock the Y axis (checked is hovering mode otherwise noclip)")]
	public bool isYClamped = false;

	[Tooltip("Lock and hide the cursor (shortcut CTRL+P to stop the game)")]
	public bool isCursorLocked = false;

	void Start () {
		rot = smoothRot = transform.rotation.eulerAngles;
		if (isCursorLocked) Cursor.lockState = CursorLockMode.Locked;
	}

	void LateUpdate () {
		pos.y = 0;
		pos.x = Input.GetAxisRaw ("Horizontal");
		pos.z = Input.GetAxisRaw ("Vertical");
		pos = pos.normalized * cameraMoveSpeed * Time.deltaTime;

		smoothPos.x = Mathf.Lerp (smoothPos.x, pos.x, smoothPosSpeed);
		smoothPos.z = Mathf.Lerp (smoothPos.z, pos.z, smoothPosSpeed);

		Vector3 posRight = transform.right;
		Vector3 posForward = transform.forward;

		if (isYClamped) {
			posRight.Scale (new Vector3 (1, 0, 1));
			posRight.Normalize ();

			posForward.Scale (new Vector3 (1, 0, 1));
			posForward.Normalize ();
		}

		transform.position = new Vector3 (transform.position.x + posRight.x * smoothPos.x + posForward.x * smoothPos.z,
										  transform.position.y + ((isYClamped) ? 0 : transform.forward.y * smoothPos.z),
										  transform.position.z + posForward.z * smoothPos.z + posRight.z * smoothPos.x);

		float upMove = 0;

		if (Input.GetKey (KeyCode.Space))
			upMove = 1;
		else if (Input.GetKey (KeyCode.LeftShift))
			upMove = -1;

		smoothUpMove = Mathf.Lerp (smoothUpMove, upMove, smoothPosSpeed);
		
		transform.Translate (0, smoothUpMove * cameraMoveSpeed * Time.deltaTime, 0, (isYClamped) ? Space.World : Space.Self);

		rot.x -= Input.GetAxis ("Mouse Y") * cameraRotateSpeed * Time.deltaTime;
		rot.y += Input.GetAxis ("Mouse X") * cameraRotateSpeed * Time.deltaTime;

		rot.x = Mathf.Clamp (rot.x, -89.9f, 89.9f);

		smoothRot.x = Mathf.Lerp (smoothRot.x, rot.x, smoothRotSpeed);
		smoothRot.y = Mathf.Lerp (smoothRot.y, rot.y, smoothRotSpeed);

		transform.rotation = Quaternion.Euler (new Vector3 (smoothRot.x, smoothRot.y, 0));
	}
}