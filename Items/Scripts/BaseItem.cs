using Godot;
using System;

public partial class BaseItem : Node2D
{
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
        Global.ChangeMouse("res://Other/Icons/ClickCursor.png");
    } 

	public void OnItemMouseExited()
	{
		Global.ChangeMouse("res://Other/Icons/CommonCursor.png");
    }
}
