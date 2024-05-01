using Godot;
using System;

public partial class Rel : Node2D
{
	public const float Speed = 350.0f;
    AnimatedSprite2D animatedSprite;

    Dialog currentDialog;

    // Get the gravity from the project settings to be synced with RigidBody nodes.

    public override void _Ready()
    {
        animatedSprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");

        animatedSprite.Play("Idle");

        currentDialog = GetNode<Dialog>("StartDialog");
    }
    public override void _PhysicsProcess(double delta)
	{
        
	}

    public Dialog GetCurrentDialog(){
        var curDialog = currentDialog;
        currentDialog = currentDialog.NextDialog;
        return curDialog;
    }
}
