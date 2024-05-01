using Godot;
using Shiza.Scripts;
using System;

public partial class CrossCity : Node2D, IChar
{
    character Player;
    Global Global;
    public const float speedScaling = 0.0075f;
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        Player = GetNode<character>("CharacterBody2D");
        Global = GetNode<Global>("/root/Global");
        Player.ChangeScale = speedScaling;
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
    }

    public void SideStreetMouseEntered()
    {
    }

    public void SideStreetMouseExited()
    {
        Global.MouseEnteredInFloor = false;
    }

    public void SarayMouseEntered()
    {
    }

    public void SarayMouseExited()
    {
        Global.MouseEnteredInFloor = false;
    }

    public void HatStreetMouseEntered()
    {
    }

    public void HatStreetMouseExited()
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


    public void PlayerEnterOnSideStreet(Node2D body)
    {
        if (body is character)
        {
            var parent = (Game)GetParent();
            parent.MoveLocation(this, Global.SideStreet, new Vector2(1680, 835), new Vector2(1f, 1f));
        }
    }

    public void ClickOnSideStreet(Node veiwport, InputEvent e, int shape_idx)
    {
        if (e.IsActionPressed("mouse_click"))
        {
            Player.MoveToWay(new Vector2(65, 805));
        }
    }

    public void ClickOnSarayWay(Node veiwport, InputEvent e, int shape_idx)
    {
        if (e.IsActionPressed("mouse_click"))
        {
            Player.MoveToWay(new Vector2(1850, 860));
        }
    }

    public void ClickOnHatStreet(Node veiwport, InputEvent e, int shape_idx)
    {
        if (e.IsActionPressed("mouse_click"))
        {
            Player.MoveToWay(new Vector2(670, 525));
        }
    }
    public void PlayerEnterOnHatStreet(Node2D body)
    {
        if (body is character)
        {
            var parent = (Game)GetParent();
            parent.MoveLocation(this, Global.HatStreet, new Vector2(1680, 790), new Vector2(1.1f, 1.1f));
        }
    }

    public void PlayerEnterOnSarayWay(Node2D body)
    {
        if (body is character)
        {
            var parent = (Game)GetParent();
            parent.MoveLocation(this, Global.SarayWay, new Vector2(220, 740), new Vector2(0.85f, 0.85f));
        }
    }

    public void MoveCharacter(Vector2 pos, Vector2 scale)
    {
        Player.GlobalPosition = pos;
        Player.Scale = scale;
        Player.MoveToWay(pos);
    }

}
