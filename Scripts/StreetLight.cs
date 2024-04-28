using Godot;
using System;
using System.Threading;
using System.Threading.Tasks;

public partial class StreetLight : Node3D
{
	private OmniLight3D light;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GetNode<AnimationPlayer>("AnimationPlayer").Play("Blink");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
