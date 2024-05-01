using Godot;
using System;

public partial class Mafia : Node3D
{
	[Export(PropertyHint.Enum, "ListeningOnWall,SittingTalking")]
	public string animationName;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GetNode<AnimationPlayer>("AnimationPlayer").Play(animationName);
		var a = GetNode<AnimationPlayer>("AnimationPlayer").GetAnimationList();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
