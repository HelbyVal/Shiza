using Godot;
using System;

public partial class BaseItem : Node2D
{
	public bool IsDroped { get { return isDroped; } }
	protected bool SetIsDroped { set { isDroped = value; } }
	private bool isDroped = true;
	public string ItemName = "BaseItem";
	public Node2D InteractNode = null;
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
		//Global.MouseEnteredInFloor = false;
	}

	public void OnItemMouseExited()
	{
        //Global.MouseEnteredInFloor = true;
    }

	public void BodyEntered(Node2D body)
	{
		if (body is character && isDroped)
		{
			var player = (character)body;
			if (player.Picking)
			{
				player.MoveToItem(player.Position);
				player.Pickup(this);
			}
		}
	}

	public virtual void Pickup()
	{
		var scene = GetParent();
		var parent = (Game)scene.GetParent();
		UI ui = parent.GetNode<UI>("Ui");
		if (ui.InsertItem(this)) {
			isDroped = false;
            //Global.ChangeMouse(Global.PreviousModeCursor);
			DeleteOnScene(ui,scene);
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
					person.MoveToItem(this.Position);
				}
			}
		}
	}
	
	public void DeleteOnScene(Node newParent, Node oldParent)
	{
		oldParent.RemoveChild(this);
		newParent.AddChild(this);
	}
}
