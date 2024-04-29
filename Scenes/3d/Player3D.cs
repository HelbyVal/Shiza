using Godot;
using System;

public partial class Player3D : CharacterBody3D
{
	 [Signal]
	 public delegate void RotationFinishedEventHandler();
	//Приветик
	public const float Speed = 5.0f;
	public const float JumpVelocity = 4.5f;
	public const float RotationSpeed = 0.02f;
	private float cameraLimitUp = Godot.Mathf.DegToRad(60);
	private float cameraLimitDown = Godot.Mathf.DegToRad(-60);

	private float mouseSens;
	private Camera3D camera;
	// Get the gravity from the project settings to be synced with RigidBody nodes.
	public float gravity = ProjectSettings.GetSetting("physics/3d/default_gravity").AsSingle();
	private bool isMovementOn = true;
	private bool isRotation = false;
	private bool isCameraRotation = false;
	private Vector3 rotationTargetVector;
	private float degreeTarget;
	private float cameraDegreeTarget;

    public override void _Ready()
    {
    	camera = GetNode<Camera3D>("Camera3D");
		Input.MouseMode = Input.MouseModeEnum.Captured;
		mouseSens = GetNode<Global>("/root/Global").MouseSens;
		GetNode<MeshInstance3D>("BodyMesh").Visible = false;
    }

    public override void _Process(double delta)
    {
		if(isRotation){
			SmoothRotate();
		}
		if(isCameraRotation){
			SmoothRotateCamera();
		}
    }

    public override void _Input(InputEvent @event)
    {
		if(@event is InputEventMouseMotion ev && isMovementOn){
			Rotation = new Vector3(0, this.Rotation.Y + (-ev.Relative.X * mouseSens), 0);
			var cameraX = Mathf.Clamp(camera.Rotation.X + (-ev.Relative.Y * mouseSens), cameraLimitDown, cameraLimitUp);
			camera.Rotation = new Vector3(cameraX, camera.Rotation.Y, 0);
		}
    }

    public override void _PhysicsProcess(double delta)
	{
		if(isMovementOn){
			Movement(delta);
		}
	}

	private void Movement(double delta){
		Vector3 velocity = Velocity;
		Vector2 inputDir = Input.GetVector("moveRight", "moveLeft", "moveBack", "moveForward");
		Vector3 direction = (Transform.Basis * new Vector3(inputDir.X, 0, inputDir.Y)).Normalized();

		if (!IsOnFloor())
			velocity.Y -= gravity * (float)delta;

		if (direction != Vector3.Zero)
		{
			velocity.X = direction.X * Speed;
			velocity.Z = direction.Z * Speed;
		}
		else
		{
			velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
			velocity.Z = Mathf.MoveToward(Velocity.Z, 0, Speed);
		}

		Velocity = velocity;
		MoveAndSlide();
	}

	public void TurnOffMovement(){
		isMovementOn = false;
	}

	public void TurnOnMovement(){
		isMovementOn = true;
	}

	public void SmoothRotateOn(Node3D body){
		rotationTargetVector = body.Position- Position;
		isRotation = true;
		degreeTarget = Mathf.LerpAngle(Rotation.Y, Mathf.Atan2(rotationTargetVector.X, rotationTargetVector.Z), 1);
	}

	private void SmoothRotate(){
		if(Mathf.Abs(Rotation.Y - degreeTarget) < 0.01)
		{
			isRotation = false;
			EmitSignal(SignalName.RotationFinished);
		}
		var y = Mathf.LerpAngle(Rotation.Y, Mathf.Atan2(rotationTargetVector.X, rotationTargetVector.Z), RotationSpeed);
		Rotation = new Vector3(Rotation.X, y, Rotation.Z);
	}

	public void SmoothRotateCameraOnDegree(float deg){
		cameraDegreeTarget = Mathf.DegToRad(deg);
		isCameraRotation = true;
	}

	private void SmoothRotateCamera(){
		if(Mathf.Abs(camera.Rotation.X - cameraDegreeTarget) < 0.01)
		{
			isCameraRotation = false;
		}
		var x = Mathf.LerpAngle(camera.Rotation.X, cameraDegreeTarget, RotationSpeed);
		camera.Rotation = new Vector3(x, camera.Rotation.Y, 0);
	}
}
