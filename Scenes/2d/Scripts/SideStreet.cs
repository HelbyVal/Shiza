using Godot;
using Shiza.Scripts;
using System;

public partial class SideStreet : Node2D, IChar
{
	character Player;
	Global Global;
    private dialog_ui _dialogUI;
    public const float speedScaling = 0f;
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
		Player = GetNode<character>("CharacterBody2D");
        Global = GetNode<Global>("/root/Global");
        Player.ChangeScale = speedScaling;
        _dialogUI = GetNode<dialog_ui>("DialogUI");
    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
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
            parent.MoveLocation(this, Global.FirstScena, new Vector2(1610,835), new Vector2(1f,1f));
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

    public void ClickOnCrossCity(Node veiwport, InputEvent e, int shape_idx)
    {
        if (e.IsActionPressed("mouse_click"))
        {
            var player = GetNode<character>("CharacterBody2D");
            player.MoveToWay(new Vector2(1825, 815));
        }
    }

    public void PlayerEnterOnCrossCity(Node2D body)
    {
        if (body is character)
        {
            var parent = (Game)GetParent();
            parent.MoveLocation(this, Global.CrossCity, new Vector2(186, 745), new Vector2(0.7f, 0.7f));
        }
    }

    public void MoveCharacter(Vector2 pos, Vector2 scale)
    {
        var player = GetNode<character>("CharacterBody2D");
        player.GlobalPosition = pos;
        player.Scale = scale;
        player.MoveToWay(pos);
    }

    private void OnTaylerClickEvent(Node viewport, InputEvent @event, int shape_idx){
        if(_dialogUI.IsStarted) return;
        if (@event.IsActionPressed("mouse_click"))
        {
            _dialogUI.StartDialog(GetNode<Rel>("Rel").GetCurrentDialog().NextDialog);
            Player.DisableMovement();
        }
    }

    private void OnDialogStarted(){
        Player.DisableMovement();
    }

    private void OnDialogFinished(){
        Player.EnableMovement();
    }

}
