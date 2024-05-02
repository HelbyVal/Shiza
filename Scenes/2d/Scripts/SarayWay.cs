using Godot;
using Shiza.Scripts;
using System;

public partial class SarayWay : Node2D, IChar
{
    character Player;
    Global Global;
    public const float speedScaling = 0f;
    private dialog_ui _dialogUI;
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

    public void ClickOnCrossCity(Node veiwport, InputEvent e, int shape_idx)
    {
        if (e.IsActionPressed("mouse_click"))
        {
            var player = GetNode<character>("CharacterBody2D");
            player.MoveToWay(new Vector2(100, 745));
        }
    }

    public void PlayerEnterOnCrossCity(Node2D body)
    {
        if (body is character)
        {
            var parent = (Game)GetParent();
            parent.MoveLocation(this, Global.CrossCity, new Vector2(1715, 740), new Vector2(0.7f, 0.7f));
        }
    }

    public void MoveCharacter(Vector2 pos, Vector2 scale)
    {
        var player = GetNode<character>("CharacterBody2D");
        player.GlobalPosition = pos;
        player.Scale = scale;
        player.MoveToWay(pos);
    }


    private void OnDedClickEvent(Node viewport, InputEvent @event, int shape_idx){
        if(_dialogUI.IsStarted) return;
        if (@event.IsActionPressed("mouse_click"))
        {
            _dialogUI.StartDialog(GetNode<OldMan>("OldMan").GetCurrentDialog());
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
