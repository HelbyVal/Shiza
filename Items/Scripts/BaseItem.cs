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
        var Global = GetNode<Global>("/root/Global");
        Global.ChangeMouse(Global.CursorMode.Pickup);
    } 

	public void OnItemMouseExited()
	{
        var Global = GetNode<Global>("/root/Global");
        Global.ChangeMouse(Global.PreviousModeCursor);
    }
}
