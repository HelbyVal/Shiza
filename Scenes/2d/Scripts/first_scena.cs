using Godot;
using Shiza.Scripts;
using System;

public partial class first_scena : Node2D, IChar
{
    Global Global;
    character Player;
    private dialog_ui _dialogUI;
    public const float speedScaling = 0.007f;
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
        Global = GetNode<Global>("/root/Global");
        Player = GetNode<character>("CharacterBody2D");
        Player.ChangeScale = speedScaling;
        _dialogUI = GetNode<dialog_ui>("DialogUI");
    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void OnFloorMouseEntered()
	{
        //Global.ChangeMouse(Global.CursorMode.Walk);
        //Global.MouseEntered = true;
        Global.MouseEnteredInFloor = true;
	}

    public void OnFloorMouseExited()
    {
        //if(!Global.MouseEntered)
        //    Global.ChangeMouse(Global.CursorMode.Common);
        //Global.MouseEntered= false;
        Global.MouseEnteredInFloor = false;
    }

    public void StreetWayMouseEntered()
	{
        //Global.ChangeMouse(Global.CursorMode.WayLeft);
    }

    public void StreetWayMouseExited()
    {
        Global.MouseEnteredInFloor = false;
        //Global.ChangeMouse(Global.CursorMode.WayLeft);
    }

    public void ClickOnStreetWay(Node veiwport, InputEvent e, int shape_idx)
    {
        if (e.IsActionPressed("mouse_click"))
        {
            var player = GetNode<character>("CharacterBody2D");
            player.MoveToWay(new Vector2(1785, 805));
        }
    }

    public void PlayerEnterOnStreetWay(Node2D body)
    {
        if (body is character)
        {
            var parent = (Game)GetParent();
            parent.MoveLocation(this,Global.SideStreet, new Vector2(186, 740), new Vector2(1f, 1f));
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
        if (@event.IsActionPressed("mouse_click"))
        {
            _dialogUI.StartDialog(GetNode<Rel>("Rel").GetCurrentDialog());
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
