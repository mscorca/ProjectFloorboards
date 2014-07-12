using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	// Layer Mask for raycasting, only want to raycast objects in scene
	// that are on the Interactable layer
	[HideInInspector] public static int layerMask;

	public const int INTERACTABLE = 8;
	public const float CHARSPEED = 5;
	public const float GRAVITY = 100;

	private float speedMult = 1;
	private bool grabbed;
	private Vector3 moveDirection;
	private Vector3 screenPoint;
	private Ray ray;
	private RaycastHit hit;
	private GameObject pickedUpObject;
	private Camera playerCam;


	
	void Start(){
		grabbed = false;
		layerMask = INTERACTABLE;
		screenPoint = new Vector3(Screen.width/2, Screen.height/2, 0);
		playerCam = Camera.main;
		ray = playerCam.ScreenPointToRay(screenPoint);
	}

	void Update (){
		MovementInput();
		ObjectGrab();
	}


	//////////////////////////////////////
	//									//
	//      Player input handlers		//
	//									//
	//////////////////////////////////////

	// Handles player translation of position
	void MovementInput(){
		moveDirection = Vector3.zero;
		
		// Take input from axis (controller or keyboard)
		moveDirection.x += Input.GetAxis("Horizontal") * CHARSPEED * speedMult;
		moveDirection.y -= GRAVITY;
		moveDirection.z += Input.GetAxis("Vertical") * CHARSPEED * speedMult;

		moveDirection = transform.TransformDirection(moveDirection);

		GetComponent<CharacterController>().Move(moveDirection * Time.deltaTime); 
	}


	// Allows player to pickup objects
	void ObjectGrab(){
		ray = playerCam.ScreenPointToRay(screenPoint);

		Debug.DrawRay(ray.origin, ray.direction, Color.red, 1.0f, false);
		if (Physics.Raycast(ray, out hit, 10f, 1 << INTERACTABLE)) {
			if(Input.GetMouseButtonDown(0) && grabbed == false){
				grabbed = true;				
				pickedUpObject = hit.collider.gameObject;
			} else if(Input.GetMouseButtonDown(0) && grabbed == true){
				grabbed = false;
			}
		}

		if(grabbed){
			//Moving object with player, 2 units in front of him cause we want to see it

			Vector3 nextPos = playerCam.transform.position + ray.direction * 2;
			Vector3 currPos = pickedUpObject.transform.position;
		
			pickedUpObject.rigidbody.velocity = (nextPos - currPos) * 10;	
		}
	}
}
