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

	public void BodyEntered(Node2D body)
	{
		if (body is character)
		{
			var player = (character)body;
			if (player.Picking)
			{
				player.MoveTo(player.Position);
				player.Pickup(this);
			}
		}
	}

	public void Pickup()
	{

	}

	public void ClickInputEvent(Node veiwport, InputEvent e, int shape_idx) 
	{
		if (e.IsActionPressed("mouse_click"))
		{
			if (isDroped)
			{
				GD.Print("OnPessed");
				var parent = GetParent();
				if (parent is first_scena)
				{
					var scene = (first_scena)parent;
					var person = scene.GetNode<character>("CharacterBody2D");
					person.MoveTo(this.Position);
				}
			}
		}
	} 
}
