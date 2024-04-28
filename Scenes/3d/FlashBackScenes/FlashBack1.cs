using Godot;
using System;

public partial class FlashBack1 : Node3D
{
	Father father;
	Player3D player;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		father = GetNode<Father>("doneBoss");
		father.ChangeAnimation("Talk");
		player = GetNode<Player3D>("CharacterBody3D2");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	private void OnArea3dBodyEntered(Node3D body){
		GD.Print("AAA");
		//father.RotateX((float)50);// LookAt(body.Position);
		father.ChangeAnimation("Punch");
	}
}
