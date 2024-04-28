using Godot;
using System;

public partial class BaseItem : Node2D
{
	public bool IsDroped { get { return isDroped; } }
	private bool isDroped = true;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void OnItemMouseEntered()
	{
        var Global = GetNode<Global>("/root/Global");
        Global.ChangeMouse(Global.CursorMode.Pickup);
    } 

	public void OnItemMouseExited()
	{
        var Global = GetNode<Global>("/root/Global");
        Global.ChangeMouse(Global.PreviousModeCursor);
    }

	public void OnPressed()
	{
		if (isDroped)
		{
			GD.Print("OnPessed Сработал");
			var parent = GetParent();
			if (parent is first_scena)
			{
				var scene = (first_scena)parent;
				var person = scene.GetNode<character>("CharecterBody2D");
				person.MoveTo(this.Position);
			}
		}
	}

	public void BodyEntered(Node2D body)
	{
		if (body is character)
		{
			var player = (character)body;
			player.MoveTo(player.Position);
			player.Pickup(this);
		}
	}

	public void Pickup()
	{

	}
}
