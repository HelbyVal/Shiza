using Godot;
using System;

public partial class OldMan : Node2D
{
	Dialog currentDialog;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		currentDialog = GetNode<Dialog>("IdleDialog");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public Dialog GetCurrentDialog(){
        var curDialog = currentDialog;
        currentDialog = currentDialog.NextDialog;
        return curDialog;
    }
}
