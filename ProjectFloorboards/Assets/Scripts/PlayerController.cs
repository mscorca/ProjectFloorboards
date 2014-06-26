using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public const float CHARSPEED = 10;
	public const float GRAVITY = 100;
	private float speedMult = 1;
	private Vector3 moveDirection;
	
	void Update () {
		moveDirection = Vector3.zero;
		
		// Take input from axis (controller or keyboard)
		moveDirection.x += Input.GetAxis("Horizontal") * CHARSPEED * speedMult;
		moveDirection.y -= GRAVITY * Time.deltaTime;
		moveDirection.z += Input.GetAxis("Vertical") * CHARSPEED * speedMult;

		moveDirection = transform.TransformDirection(moveDirection);

		GetComponent<CharacterController>().Move(moveDirection * Time.deltaTime); 
	}
}
