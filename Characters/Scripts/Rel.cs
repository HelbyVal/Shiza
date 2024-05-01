using Godot;
using System;

public partial class Rel : Node2D
{
	public const float Speed = 350.0f;
    AnimatedSprite2D animatedSprite;

    // Get the gravity from the project settings to be synced with RigidBody nodes.

    public override void _Ready()
    {
        animatedSprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");

        animatedSprite.Play("Idle");
    }
    public override void _PhysicsProcess(double delta)
	{
		
		//MoveAndSlide();
	}

    public Dialog GetCurrentDialog(){
        return GetNode<Dialog>("StartDialog");
    }
}
