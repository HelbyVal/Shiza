using Godot;
using System;

public partial class Global : Node
{
    public float MouseSens = (float)0.003;
	public CursorMode ModeCursor 
	{
		get { return modeCursor; }
	}
	private CursorMode modeCursor = CursorMode.Common;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
        
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public static void ChangeMouse(string pathCursor)
	{
        var cursor = GD.Load(pathCursor);
        Input.SetCustomMouseCursor(cursor, Input.CursorShape.Arrow, new Vector2(50,0));
    }

    public enum CursorMode
    {
		Common,
		Pickup,
		Walk,
		Talk,
		Inspect
    }

}


