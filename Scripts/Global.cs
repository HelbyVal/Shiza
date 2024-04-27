using Godot;
using Godot.Collections;
using System;

public partial class Global : Node
{
    public float MouseSens = (float)0.003;
	public CursorMode PreviousModeCursor 
	{
		get { return previousModeCursor; }
	}

    public CursorMode ModeCursor
    {
        get { return modeCursor; }
    }
    private CursorMode modeCursor = CursorMode.Common;
    private CursorMode previousModeCursor;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
        
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void ChangeMouse(CursorMode mode)
	{
        var cursor = GD.Load(Cursors[mode]);
		previousModeCursor = modeCursor;
		modeCursor = mode;
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

	private Dictionary<CursorMode, string> Cursors = new Dictionary<CursorMode, string>()
	{
		{ CursorMode.Common, "res://Other/Icons/CommonCursor.png" },
		{ CursorMode.Walk, "res://Other/Icons/StepCursor.png" },
		{ CursorMode.Pickup, "res://Other/Icons/ClickCursor.png" }
	};


}


