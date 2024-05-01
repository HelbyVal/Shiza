using Godot;
using System;

public partial class Father : Node3D
{
	[Signal]
	public delegate void RotationFinishedEventHandler();
	private AnimationPlayer animationPlayer;
	public const float RotationSpeed = 0.02f;
	// Called when the node enters the scene tree for the first time.
	private bool isMovementOn = true;
	private bool isRotation = false;
	private Vector3 rotationTargetVector;
	private float degreeTarget;
	public override void _Ready()
	{
		animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if(isRotation){
			SmoothRotate(delta);
		}
	}

	public void ChangeAnimation(string name){
		animationPlayer.Play(name);
	}

	public void SmoothRotateOn(Node3D body){
		rotationTargetVector = body.Position- Position;
		isRotation = true;
		degreeTarget = Mathf.LerpAngle(Rotation.Y, Mathf.Atan2(rotationTargetVector.X, rotationTargetVector.Z), 1);
	}

	private void SmoothRotate(double delta){
		if(Mathf.Abs(Rotation.Y - degreeTarget) < 0.01)
		{
			isRotation = false;
			EmitSignal(SignalName.RotationFinished);
		}
		var y = Mathf.LerpAngle(Rotation.Y, Mathf.Atan2(rotationTargetVector.X, rotationTargetVector.Z), RotationSpeed);
		Rotation = new Vector3(Rotation.X, y, Rotation.Z);
	}
}
