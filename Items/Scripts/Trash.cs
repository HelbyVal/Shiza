using Godot;
using System;

public partial class Trash : Node2D
{
	// Called when the node enters the scene tree for the first time.
	BaseItem PickableItem;
    bool active = true;
    Global Global;
	public override void _Ready()
	{
		PickableItem = GetNode<RottenMeat>("RottenMeat");
        Global = GetNode<Global>("/root/Global");
    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

    public void MouseEnteredInTrash()
    {
        Global.MouseEnteredInFloor = false;
    }
    public void ClickOnTrash(Node viewport, InputEvent e, int shape_idx)
	{
        if (e.IsActionPressed("mouse_click") && active)
        {
            var parent = GetParent();
            if (parent is Shiza.Scripts.IChar)
            {
                var scene = (Node2D)parent;
                var person = scene.GetNode<character>("CharacterBody2D");
                person.MoveToItem(this.Position);
            }
        }
    }

    public void PlayerEntered(Node2D body)
    {
        if (body is character player && active)
        {
            if (player.Picking)
            {
                player.MoveToItem(player.Position);
                player.Pickup(PickableItem);
                active = false;
            }
        }
    }

}
