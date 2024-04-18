using Godot;
using System;

public partial class Player3D : CharacterBody3D
{
	//Приветик
	public const float Speed = 5.0f;
	public const float JumpVelocity = 4.5f;
	private float mouseSens;
	private Camera3D camera;

	private float cameraLimitUp = Godot.Mathf.DegToRad(60);
	private float cameraLimitDown = Godot.Mathf.DegToRad(-60);
	// Get the gravity from the project settings to be synced with RigidBody nodes.
	public float gravity = ProjectSettings.GetSetting("physics/3d/default_gravity").AsSingle();

    public override void _Ready()
    {
    	camera = GetNode<Camera3D>("Camera3D");
		var parent = GetParent();
		Input.MouseMode = Input.MouseModeEnum.Captured;
		var Global = GetNode<Global>("/root/Global");
		mouseSens = Global.MouseSens;
		GetNode<MeshInstance3D>("BodyMesh").Visible = false;
    }

    public override void _UnhandledInput(InputEvent @event)
    {
        
    }

    public override void _Input(InputEvent @event)
    {
			if(@event is InputEventMouseMotion ev){
				Rotation = new Vector3(0, this.Rotation.Y + (-ev.Relative.X * mouseSens), 0);
				var cameraX = Mathf.Clamp(camera.Rotation.X + (-ev.Relative.Y * mouseSens), cameraLimitDown, cameraLimitUp);
				camera.Rotation = new Vector3(cameraX, camera.Rotation.Y, 0);
		}
    }

    public override void _PhysicsProcess(double delta)
	{
		Vector3 velocity = Velocity;

		// Add the gravity.
		if (!IsOnFloor())
			velocity.Y -= gravity * (float)delta;

		// Handle Jump.
		if (Input.IsActionJustPressed("ui_accept") && IsOnFloor())
			velocity.Y = JumpVelocity;

		// Get the input direction and handle the movement/deceleration.
		// As good practice, you should replace UI actions with custom gameplay actions.
		Vector2 inputDir = Input.GetVector("moveRight", "moveLeft", "moveBack", "moveForward");
		Vector3 direction = (Transform.Basis * new Vector3(inputDir.X, 0, inputDir.Y)).Normalized();
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
}
