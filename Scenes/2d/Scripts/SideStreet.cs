using Godot;
using Shiza.Scripts;
using System;

public partial class SideStreet : Node2D, IChar
{
	character Character;
	Global Global;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Character = GetNode<character>("CharacterBody2D");
        Global = GetNode<Global>("/root/Global");
    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void CharacterMove(Vector2 position)
	{
		Character.GlobalPosition = position;
	}

    public void MurderZoneMouseEntered()
	{
	}

    public void MurderZoneMouseExited()
    {
        Global.MouseEnteredInFloor = false;
    }

    public void CrossCityMouseEntered()
    {
    }

    public void CrossCityMouseExited()
    {
        Global.MouseEnteredInFloor = false;
    }
    
    public void FloorMouseEntered()
    {
        Global.MouseEnteredInFloor = true;
    }

    public void FloorMouseExited()
    {
        Global.MouseEnteredInFloor = false;
    }


    public void PlayerEnterOnMurderZone(Node2D body)
    {
        if (body is character)
        {
            var parent = (Game)GetParent();
            parent.MoveLocation(this, Global.FirstScena, new Vector2(1610,835), new Vector2(1.5f,1.5f));
        }
    }

    public void ClickOnMurderZone(Node veiwport, InputEvent e, int shape_idx)
    {
        if (e.IsActionPressed("mouse_click"))
        {
            var player = GetNode<character>("CharacterBody2D");
            player.MoveToWay(new Vector2(100, 835));
        }
    }

    public void MoveCharacter(Vector2 pos)
    {
        var player = GetNode<character>("CharacterBody2D");
        player.GlobalPosition = pos;
        player.MoveToWay(pos);
    }

}
