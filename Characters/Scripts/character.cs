using Godot;
using System;

public partial class character : CharacterBody2D
{
	public const float Speed = 300.0f;
	Vector2 click_position = new Vector2();
	Vector2 target_position = new Vector2();

    public override void _Ready()
    {
        click_position = Position;
    }

    public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;

		// Handle Jump.
		if (Input.IsActionJustPressed("mouse_click")){
			click_position = GetGlobalMousePosition();
		}

		if(Position.DistanceTo(click_position) > 5){
			target_position = (click_position - Position).Normalized();
			velocity = target_position * Speed;
			Velocity = velocity;
			MoveAndSlide();
		}
		
	}
}
