using Godot;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.IO;

public partial class dialog_ui : Control
{
	[Signal]
	public delegate void DialogFinishedEventHandler();
	[Signal]
	public delegate void DialogStartedEventHandler();
	[Signal]
	public delegate void NextReplicaEventHandler();
	private Dialog _dialog {get; set;}
	private List<Replica> _replicas = new List<Replica>();
	private int _replicaCount;
	private int _dialogCounter;
	private bool _isFinished = false;
	private bool _isStarted = false;
	private Label _text;
	private Label _owner;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Visible = false;
		_text = GetNode<Label>("Text");
		_owner = GetNode<Label>("Owner");
		
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

    public override void _Input(InputEvent @event)
    {
        if (Input.IsActionJustPressed("mouse_click") && _isStarted){
			GetNextReplica();
		}
    }


    public void StartDialog(Dialog dialog){
		_dialog = dialog;
		using (FileStream fs = new FileStream(ProjectSettings.GlobalizePath(_dialog.DialogPath), FileMode.OpenOrCreate))
		{
			_replicas = JsonSerializer.Deserialize<List<Replica>>(fs);
		}
		_replicaCount = _replicas.Count-1;
		_dialogCounter = 0;
		_isStarted = true;
		Visible = true;
		GetNextReplica();
		EmitSignal(SignalName.DialogStarted);
	}

	public void StopDialog(){
		Visible = false;
		_isStarted = false;
		EmitSignal(SignalName.DialogFinished);
	}

	public void GetNextReplica(){
		if(_dialogCounter > _replicaCount){
			_isFinished = true;
			Visible = false;
			EmitSignal(SignalName.DialogFinished);
			return;
		}
		_owner.Text = _replicas[_dialogCounter].Owner;
		_text.Text = _replicas[_dialogCounter].Text;

		_dialogCounter += 1;
		EmitSignal(SignalName.NextReplica);
	}
}
