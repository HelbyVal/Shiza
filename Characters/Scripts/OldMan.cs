using Godot;
using System;

public partial class OldMan : Node2D
{
	public AnimatedSprite2D animatedSprite;
	Dialog currentDialog;
	character Player;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		animatedSprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		currentDialog = GetNode<Dialog>("IdleDialog");
		Player = GetNode<character>("Player");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void ClickOnOldManItem(Node2D viewport, InputEvent e, int shape_idx)
	{
		if (e.IsActionPressed("mouse_click"))
		{
			var parent = (Node2D)GetParent();
			var Player = parent.GetNode<character>("CharacterBody2D");
			if (Player.Picking)
			{
				Player.MoveToItem(Player.Position);
			}
		}
	}

	public void BodyEntered(Node2D body)
	{
        if (body is character)
        {
            var player = (character)body;
            if (player.Picking)
            {
                player.MoveToItem(player.Position);
                player.UseItem();
            }
        }
    }


	public Dialog GetCurrentDialog(){
        var curDialog = currentDialog;
        currentDialog = currentDialog.NextDialog;
        return curDialog;
    }
}
