using Godot;
using System;

public partial class RottenMeat : BaseItem
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		base._Ready();
		ItemName = "RottenMeat";
		var oldMan = GetNode<OldMan>("res://Characters/OldMan.tscn");
		InteractNode = oldMan;
        Hide();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

    public override void Pickup()
    {
        var sc = GetParent();
        var scene = sc.GetParent();
        var parent = (Game)scene.GetParent();
        UI ui = parent.GetNode<UI>("Ui");
        if (ui.InsertItem(this))
        {
            SetIsDroped = false;
            //Global.ChangeMouse(Global.PreviousModeCursor);
            DeleteOnScene(ui, sc);
            var res = GetParent();
        }
        Show();
    }
}
