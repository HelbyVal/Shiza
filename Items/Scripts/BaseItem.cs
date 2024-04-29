using Godot;
using System;

public partial class BaseItem : Node2D
{
	public bool IsDroped { get { return isDroped; } }
	private bool isDroped = true;
	Global Global;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
        Global = GetNode<Global>("/root/Global");
    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void OnItemMouseEntered()
	{
        Global.ChangeMouse(Global.CursorMode.Pickup);
    } 

	public void OnItemMouseExited()
	{
        Global.ChangeMouse(Global.PreviousModeCursor);
    }

	public void BodyEntered(Node2D body)
	{
		if (body is character && isDroped)
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
		var parent = (first_scena)GetParent();
		UI ui = parent.GetNode<UI>("Ui");
		if (ui.InsertItem(this)) {
			isDroped = false;
            Global.ChangeMouse(Global.PreviousModeCursor);
			DeleteOnScene(ui);
        }
	}

	public void ClickInputEvent(Node veiwport, InputEvent e, int shape_idx) 
	{
		if (e.IsActionPressed("mouse_click"))
		{
			if (isDroped)
			{
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
	
	public void DeleteOnScene(Node newParent)
	{
		var parent = GetParent();
		parent.RemoveChild(this);
		newParent.AddChild(this);
	}
}
