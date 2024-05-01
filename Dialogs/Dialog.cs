using System;
using Godot;

[GlobalClass]
public partial class Dialog: Node{
    [Export(PropertyHint.File, "*.txt")]
	public string DialogPath {get; set;}
    [Export]
    public bool IsRepeatable {get; set;}
    [Export]
    public Dialog NextDialog;



}