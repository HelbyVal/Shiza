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
	public delegate void NextReplicaEventHandler();

	[Export(PropertyHint.File, "*.txt")]
	public string DialogPath {get; set;}
	private List<Replica> _dialog = new List<Replica>();
	private int _replicaCount;
	private int _dialogCounter = 0;
	private bool _isFinished = false;
	private bool _isStarted = false;
	private Label _text;
	// Called when the node enters the scene tree for the first time.
	public override async void _Ready()
	{
		Visible = false;
		using (FileStream fs = new FileStream(ProjectSettings.GlobalizePath(DialogPath), FileMode.OpenOrCreate))
		{
			_dialog = await JsonSerializer.DeserializeAsync<List<Replica>>(fs);
		}
		_text = GetNode<Label>("Text");
		_replicaCount = _dialog.Count-1;
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


    public void StartDialog(){
		_isStarted = true;
		Visible = true;
		GetNextReplica();
	}

	public void GetNextReplica(){
		if(_dialogCounter > _replicaCount){
			_isFinished = true;
			Visible = false;
			EmitSignal(SignalName.DialogFinished);
			return;
		}
		_text.Text = _dialog[_dialogCounter].Text;
		_dialogCounter += 1;
		EmitSignal(SignalName.NextReplica);
	}
}
