using UnityEngine;
using System.Collections;

public class SmoothCamFollow : MonoBehaviour {

	 public float dampTime = 0.25f;
     private Vector3 velocity = Vector3.zero;
     public Transform target;
	 [SerializeField]
	 Camera mainCam;
	 void Start()
	 {
		 mainCam = mainCam.GetComponent<Camera>();
	 }
     void Update () 
     {
		 Vector3 point = mainCam.WorldToViewportPoint(target.position);
		 Debug.Log(point);
		 //clamp at 0.1 and 0.4 maybe 0.2 and 0.5?
		 
		 
		 //if (point.x > 0)
		 //{
             
		 //	Vector3 delta = target.position - mainCam.ViewportToWorldPoint(new Vector3(0.3f, 0.4f, point.z));
		 //	Vector3 destination = transform.position + delta;
		 //	transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime);
		 //}
     }
}
