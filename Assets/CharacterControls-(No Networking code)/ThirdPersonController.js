// The speed when walking
var walkSpeed = 2.0;
// after trotAfterSeconds of walking we trot with trotSpeed
var trotSpeed = 6.0;
// when pressing "Fire3" button (cmd) we start running
var runSpeed = 6.0;


var speedSmoothing = 10.0;
var rotateSpeed = 500.0;
var trotAfterSeconds = 3.0;

var canJump = true;

// The camera doesnt start following the target immediately but waits for a split second to avoid too much waving around.
private var lockCameraTimer = 0.0;

// Are we moving backwards (This locks the camera to not do a 180 degree spin)
private var movingBack = false;
// The current move direction in x-z
private var moveDirection = Vector3.zero;
// The current vertical speed
private var verticalSpeed = 0.0;
// The current x-z move speed
private var moveSpeed = 0.0;

// The last collision flags returned from uLinkStrictPlatformer
private var collisionFlags : CollisionFlags; 

// Is the user pressing any keys?
private var isMoving = false;


// The vertical/horizontal input axes from user input
public var verticalInput : float;
public var horizontalInput : float;

public var gravityAcceleration = 55.0f;

public var getUserInput : boolean = true;

private var character;

private var vel;
	

function Awake ()
	{
		 character = GetComponent("CharacterController");
	}


function UpdateSmoothedMovementDirection ()
{
	var cameraTransform = Camera.main.transform;
	
	// Forward vector relative to the camera along the x-z plane	
	var forward = cameraTransform.TransformDirection(Vector3.forward);
	forward.y = 0;
	forward = forward.normalized;

	// Right vector relative to the camera
	// Always orthogonal to the forward vector
	var right = Vector3(forward.z, 0, -forward.x);

	if (getUserInput) {
		verticalInput = Input.GetAxisRaw("Vertical");
		horizontalInput = Input.GetAxisRaw("Horizontal");
	}

	// Are we moving backwards or looking backwards
	if (verticalInput < -0.2)
		movingBack = true;
	else
		movingBack = false;
	
	var wasMoving = isMoving;
	isMoving = Mathf.Abs (horizontalInput) > 0.1 || Mathf.Abs (verticalInput) > 0.1;
		
	// Target direction relative to the camera
	var targetDirection = horizontalInput * right + verticalInput * forward;
	
	// Grounded controls
	if (IsGrounded())
	{
		// Lock camera for short period when transitioning moving & standing still
		lockCameraTimer += Time.deltaTime;
		if (isMoving != wasMoving)
			lockCameraTimer = 0.0;

		// We store speed and direction seperately,
		// so that when the character stands still we still have a valid forward direction
		// moveDirection is always normalized, and we only update it if there is user input.
		if (targetDirection != Vector3.zero)
		{
			// If we are really slow, just snap to the target direction
			if (moveSpeed < walkSpeed * 0.9)
			{
				moveDirection = targetDirection.normalized;
			}
			// Otherwise smoothly turn towards it
			else
			{
				moveDirection = Vector3.RotateTowards(moveDirection, targetDirection, rotateSpeed * Mathf.Deg2Rad * Time.deltaTime, 1000);
				
				moveDirection = moveDirection.normalized;
			}
		}
		
		// Smooth the speed based on the current target direction
		var curSmooth = speedSmoothing * Time.deltaTime;
		
		// Choose target speed
		//* We want to support analog input but make sure you cant walk faster diagonally than just forward or sideways
		var targetSpeed = Mathf.Min(targetDirection.magnitude, 1.0);
	
		// Pick speed modifier
		if (Input.GetKey("left shift"))
		{
			targetSpeed *= runSpeed;
		}
		else
		{
			targetSpeed *= walkSpeed;
		}
		
		moveSpeed = Mathf.Lerp(moveSpeed, targetSpeed, curSmooth);
		
	}

}



function Update() {

	UpdateSmoothedMovementDirection();

    // Set rotation to the move direction
	if (IsGrounded() && moveDirection != Vector3.zero)
	{
		transform.rotation = Quaternion.LookRotation(moveDirection);
	}	
	
	// We are in jump mode but just became grounded
	if (IsGrounded())
	{
		lastGroundedTime = Time.time;
	}
		
	if (Input.GetButtonDown("Jump") && canJump) 
	{
		//TODO: implement jump
	}
		
	if (verticalSpeed < -20) verticalSpeed = -20; //TODO remove when jumping is implemented.
	verticalSpeed -= gravityAcceleration * Time.deltaTime;
	vel = new Vector3(moveDirection.x*moveSpeed, verticalSpeed, moveDirection.z*moveSpeed);

	character.Move(vel * Time.deltaTime);
}

function IsJumping () {
	return (!IsGrounded()) ;
}

function IsGrounded () {
	//TODO: implement jump and gravity. 
	//return (collisionFlags & CollisionFlags.CollidedBelow) != 0;
	return true;
}


@script RequireComponent(CharacterController)