using UnityEngine;
using System.Collections;

public class Crosshair : MonoBehaviour {

 	[HideInInspector] public bool canInteract = false;

	public Texture2D crosshairNormal;
	public Texture2D crosshairInteract;
	public static GameObject currGo = null;
  	
  	private Vector3 screenPoint;
  	private Rect position;
  	private RaycastHit hit;
  	private Ray ray;




	void Start () {
		position = new Rect((Screen.width - crosshairNormal.width)/2,
 						(Screen.height - crosshairNormal.height)/2,
 						crosshairNormal.width, 
 						crosshairNormal.height);
		
		screenPoint = new Vector3(Screen.width/2, Screen.height/2, 0);

		ray = Camera.main.ScreenPointToRay( screenPoint );
	}

	void Update(){
		if( Physics.Raycast(ray, out hit, 15f, PlayerController.layerMask) ){
			Debug.Log("Layermask worked");
			canInteract = true;
		} else { 
			canInteract = false;
		}
	}

	void OnGUI () {
		if(canInteract)
			GUI.DrawTexture(position, crosshairInteract);
		else
			GUI.DrawTexture(position, crosshairNormal);

	}

}
