using UnityEngine;
using System;
using System.Collections;

public class AIPlayer : CarMechanicsBase 
{

// Here's all the variables for the AI, the waypoints are determined in the "GetWaypoints" void.
// the way point container is used to search for all the waypoints in the scene, and the current
// way point is used to determine which way point in the array the car is aiming for.
public GameObject waypointContainer ;
private  Transform[] waypoints;
private int currentWaypoint = 0;

// input steer and input torque are the values substituted out for the player input. The 
// "NavigateTowardsWaypoint" void determines values to use for these variables to move the car
// in the desired direction.
private float inputSteer  = 0.0f;
private float inputTorque  = 0.0f;

private int AppropriateGear = 0;


void Start () {
	// I usually alter the center of mass to make the car more stable. Its less likely to flip this way.
    rigidbody.centerOfMass = new Vector3(rigidbody.centerOfMass.x, -1.5f, rigidbody.centerOfMass.z);
	
	// Call the void to determine the array of waypoints. This sets up the array of points by finding
	// transform components inside of a source container.
	GetWaypoints();
}

void Update () {
	
	// This is to limit the maximum speed of the car, adjusting the drag probably isn't the best way of doing it,
	// but it's easy, and it doesn't interfere with the physics processing.
	rigidbody.drag = rigidbody.velocity.magnitude / 250;
	
	// Call the function to determine the desired input values for the car. This essentially steers and
	// applies gas to the engine.
	NavigateTowardsWaypoint();

    Vector3 input = new Vector3(inputSteer,inputTorque,0);
    CarUpdate(input);
	
}

void GetWaypoints () {
	// Now, this void basically takes the container object for the waypoints, then finds all of the transforms in it,
	// once it has the transforms, it checks to make sure it's not the container, and adds them to the array of waypoints.
	Transform[] potentialWaypoints  = waypointContainer.GetComponentsInChildren<Transform>();
	//waypoints = new Array();

    foreach (Transform potentialWaypoint in potentialWaypoints)
    {
		if ( potentialWaypoint != waypointContainer.transform ) {
			waypoints[ waypoints.Length] = potentialWaypoint;
		}
	}
}

void NavigateTowardsWaypoint () {
	// now we just find the relative position of the way point from the car transform,
	// that way we can determine how far to the left and right the way point is.
    Vector3 RelativeWaypointPosition = transform.InverseTransformPoint(new Vector3( 
												waypoints[currentWaypoint].position.x, 
												transform.position.y, 
												waypoints[currentWaypoint].position.z ) );
																				
																				
	// by dividing the horizontal position by the magnitude, we get a decimal percentage of the turn angle that we can use to drive the wheels
	inputSteer = RelativeWaypointPosition.x / RelativeWaypointPosition.magnitude;
	
	// now we do the same for torque, but make sure that it doesn't apply any engine torque when going around a sharp turn...
	if ( Mathf.Abs( inputSteer ) < 0.5 ) 
    {
		inputTorque = RelativeWaypointPosition.z / RelativeWaypointPosition.magnitude - Mathf.Abs( inputSteer );
	}else
    {
        inputTorque = 0.0f;
	}
	
	// this just checks if the car's position is near enough to a way point to count as passing it, if it is, then change the target way point to the
	// next in the list.
	if ( RelativeWaypointPosition.magnitude < 20 ) {
		currentWaypoint ++;
		
		if ( currentWaypoint >= waypoints.Length ) {
			currentWaypoint = 0;
		}
	}
	
}
}
