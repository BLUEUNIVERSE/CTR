	using UnityEngine;
	using System.Collections;

	public class WheelAlignment : MonoBehaviour {

		// Define the variables used in the script, the Corresponding collider is the wheel collider at the position of
		// the visible wheel, the slip prefab is the prefab instantiated when the wheels slide, the rotation value is the
		// value used to rotate the wheel around it's axel.
		public WheelCollider CorrespondingCollider;
		public GameObject SlipPrefab;
		private float RotationValue = 0.0f;
		
		void Update () {
			
//			// define a hit point for the raycast collision
			RaycastHit hit;
			// Find the collider's center point, you need to do this because the center of the collider might not actually be
			// the real position if the transform's off.
			Vector3 ColliderCenterPoint  = CorrespondingCollider.transform.TransformPoint( CorrespondingCollider.center );
			
			// now cast a ray out from the wheel collider's center the distance of the suspension, if it hit something, then use the "hit"
			// variable's data to find where the wheel hit, if it didn't, then se tthe wheel to be fully extended along the suspension.
			if ( Physics.Raycast( ColliderCenterPoint, -CorrespondingCollider.transform.up, out hit, CorrespondingCollider.suspensionDistance + CorrespondingCollider.radius ) ) 
			{
				Vector3 pos = hit.point + (CorrespondingCollider.transform.up * CorrespondingCollider.radius);
				if(!float.IsNaN(pos.x))
					transform.position = pos;
			}else
			{
				Vector3 pos =  ColliderCenterPoint - (CorrespondingCollider.transform.up * CorrespondingCollider.suspensionDistance);
				if(!float.IsNaN(pos.x))
					transform.position = pos;

			}
			
			// now set the wheel rotation to the rotation of the collider combined with a new rotation value. This new value
			// is the rotation around the axle, and the rotation from steering input.
			Quaternion newRotation =(CorrespondingCollider.transform.rotation * Quaternion.Euler( RotationValue, CorrespondingCollider.steerAngle, 0 ));
			
			transform.rotation = newRotation;// increase the rotation value by the rotation speed (in degrees per second)
			RotationValue += CorrespondingCollider.rpm * ( 360/60 ) * Time.deltaTime;
			
			// define a wheelhit object, this stores all of the data from the wheel collider and will allow us to determine
			// the slip of the tire.
			WheelHit CorrespondingGroundHit;
			CorrespondingCollider.GetGroundHit( out CorrespondingGroundHit );
			
			// if the slip of the tire is greater than 2.0, and the slip prefab exists, create an instance of it on the ground at
			// a zero rotation.
			if ( Mathf.Abs( CorrespondingGroundHit.sidewaysSlip ) > 2.0f ) {
				if ( SlipPrefab ) {
					//Instantiate( SlipPrefab, CorrespondingGroundHit.point, Quaternion.identity );
				}
			}
			
	}
}
