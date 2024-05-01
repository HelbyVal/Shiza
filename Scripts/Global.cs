using Godot;
using System;
using System.Collections.Generic;

public partial class Global : Node
{
	public float MouseSens = (float)0.003;
	public bool MouseEnteredInFloor = false;

	public first_scena FirstScena;
	public SideStreet SideStreet;
	public UI UI;
	//public CursorMode PreviousModeCursor
	//{
	//	get { return previousModeCursor; }
	//}

	//public CursorMode ModeCursor
	//{
	//	get { return modeCursor; }
	//}
	//private CursorMode modeCursor = CursorMode.Common;
	//private CursorMode previousModeCursor;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
        var streetPack = (PackedScene)ResourceLoader.Load("res://Scenes/2d/SideStreet.tscn");
        var street = (SideStreet)streetPack.Instantiate();
		SideStreet = street;

        var loadingScene = (PackedScene)ResourceLoader.Load("res://Scenes/2d/first_scena.tscn");
        var scene = (first_scena)loadingScene.Instantiate();
		FirstScena = scene;

        var loadingUI = (PackedScene)ResourceLoader.Load("res://UI/UI.tscn");
		var UI = (UI)loadingUI.Instantiate();
		this.UI = UI;
    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	//public void ChangeMouse(CursorMode mode)
	//{
	//	if (previousModeCursor != mode)
	//	{
	//		previousModeCursor = modeCursor;
	//		modeCursor = mode;
	//		var cur = cursors.Find(c => c.Mode == mode);
	//		Input.SetCustomMouseCursor(GD.Load(cur.Path), cur.Shape, cur.Pos);
	//	}
	//}

	//public enum CursorMode
	//{
	//	Common,
	//	Pickup,
	//	Walk,
	//	Talk,
	//	Inspect,
	//	WayLeft
	//}

	//private List<Cursors> cursors = new List<Cursors>()
	//{
	//	new Cursors() { Mode = CursorMode.Common, Shape = Input.CursorShape.Arrow, Pos = new Vector2(50,0), Path = "res://Other/Icons/CommonCursor.png" },
	//	new Cursors() { Mode = CursorMode.Walk, Shape = Input.CursorShape.Arrow, Pos = new Vector2(25,25), Path = "res://Other/Icons/StepCursor.png" },
	//	new Cursors() { Mode = CursorMode.Pickup, Shape = Input.CursorShape.Arrow, Pos = new Vector2(25,25), Path = "res://Other/Icons/ClickCursor.png" },
	//	new Cursors() { Mode = CursorMode.WayLeft, Shape = Input.CursorShape.Arrow, Pos = new Vector2(50,25), Path = "res://Other/Icons/Cursor.png" }

	//   };

}
//public class Cursors
//{
//	public Global.CursorMode Mode;
//	public Input.CursorShape Shape;
//	public Vector2 Pos;
//	public string Path;
//}


