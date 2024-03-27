using Godot;
using System;

public partial class character : CharacterBody2D
{
	private NavigationAgent2D navigationAgent;
	public const float Speed = 300.0f;
	Vector2 click_position = new Vector2();
	Vector2 target_position = new Vector2();

    public override void _Ready()
    {
        click_position = Position;
		navigationAgent = GetNode<NavigationAgent2D>("NavigationAgent2D");
		navigationAgent.PathDesiredDistance = 4;
		navigationAgent.TargetDesiredDistance = 4;

		Callable.From(ActorSetup).CallDeferred();
    }

    public override void _PhysicsProcess(double delta)
	{
		//Vector2 velocity = Velocity;

		if (Input.IsActionJustPressed("mouse_click")){
			var mousePosition = GetGlobalMousePosition();
			navigationAgent.TargetPosition = mousePosition;
		}

		if(GlobalPosition.DistanceTo(navigationAgent.TargetPosition) > 5){
			Vector2 nextPathPosition = navigationAgent.GetNextPathPosition();
			var velocity = (nextPathPosition - GlobalPosition).Normalized() * Speed;
			Velocity = velocity;
			MoveAndSlide();
		}
	}

	private async void ActorSetup()
    {
        // Wait for the first physics frame so the NavigationServer can sync.
        await ToSignal(GetTree(), SceneTree.SignalName.PhysicsFrame);

        // Now that the navigation map is no longer empty, set the movement target.
        navigationAgent.TargetPosition = GlobalPosition;
    }
}
