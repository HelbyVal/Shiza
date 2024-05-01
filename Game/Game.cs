using Godot;
using Shiza.Scripts;
using System;

public partial class Game : Node2D
{
	Global Global;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
        Global = GetNode<Global>("/root/Global");

        AddChild(Global.FirstScena);
		AddChild(Global.UI);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void MoveLocation<T1, T2>(T1 oldScene, T2 newScene, Vector2 CharPos, Vector2 Scale) where T1 : Node2D, IChar
																							   where T2 : Node2D, IChar
	{
		RemoveChild(GetNode<UI>("Ui"));
		RemoveChild(oldScene);
		AddChild(newScene);
		newScene.MoveCharacter(CharPos, Scale);
        AddChild(Global.UI);
    }
}
