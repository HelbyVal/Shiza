using Godot;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;

public partial class FlashBack1 : Node3D
{
	private Father _father;
	private Player3D _player;
	private dialog_ui _dialog;
	public override void _Ready()
	{
		_father = GetNode<Father>("doneBoss");
		_father.ChangeAnimation("Talk");
		_player = GetNode<Player3D>("CharacterBody3D2");
		_dialog = GetNode<dialog_ui>("DialogUI");
    }

	public override void _Process(double delta)
	{

	}

	private async void OnArea3dBodyEntered(Node3D body){
		if(body is Player3D){
			_father.SmoothRotateOn(body);
			_player.SmoothRotateOn(_father);
			_player.TurnOffMovement();
			_player.SmoothRotateCameraOnDegree(9);
			await ToSignal(_player, "RotationFinished");

			_dialog.StartDialog();
			await ToSignal(_dialog, "DialogFinished");

			_father.ChangeAnimation("Punch");
		}
	}
}
