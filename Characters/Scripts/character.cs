using Godot;
using System;

public partial class character : CharacterBody2D
{
	private NavigationAgent2D navigationAgent;
	public const float Speed = 300.0f;
	Vector2 click_position = new Vector2();
	Vector2 target_position = new Vector2();
	AnimatedSprite2D animatedSprite;
	CharacterAction action = CharacterAction.Idle;
	BaseItem PickupItem;



	public override void _Ready()
    {
        animatedSprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");

        click_position = Position;
		navigationAgent = GetNode<NavigationAgent2D>("NavigationAgent2D");
		navigationAgent.PathDesiredDistance = 4;
		navigationAgent.TargetDesiredDistance = 4;

		Callable.From(ActorSetup).CallDeferred();

        animatedSprite.Play("Idle");
    }

    public override void _PhysicsProcess(double delta)
	{
//		if (animatedSprite.Animation.ToString() == "Use")
//			return;
        
        var global = GetNode<Global>("/root/Global");
        if (Input.IsActionJustPressed("mouse_click") && (global.ModeCursor == Global.CursorMode.Walk || global.ModeCursor == Global.CursorMode.Pickup)){
			var mousePosition = GetGlobalMousePosition();
			navigationAgent.TargetPosition = mousePosition;
		}

		if (GlobalPosition.DistanceTo(navigationAgent.TargetPosition) > 5)
		{
			Vector2 nextPathPosition = navigationAgent.GetNextPathPosition();
			var velocity = (nextPathPosition - GlobalPosition).Normalized() * Speed;
			Velocity = velocity;
			if (Velocity.X > 0)
			{
				animatedSprite.Play("Walk");
				if (!animatedSprite.FlipH)
					animatedSprite.FlipH = true;
			}
			else
			{
				animatedSprite.Play("Walk");
				if (animatedSprite.FlipH)
					animatedSprite.FlipH = false;
			}


			MoveAndSlide();
		}
		else
            animatedSprite.Play("Idle");
    }

	public void MoveTo(Vector2 position)
	{
//        GD.Print("MoveTo Сработал");
//        target_position = position;
	}

	private async void ActorSetup()
    {
        // Wait for the first physics frame so the NavigationServer can sync.
        await ToSignal(GetTree(), SceneTree.SignalName.PhysicsFrame);

        // Now that the navigation map is no longer empty, set the movement target.
        navigationAgent.TargetPosition = GlobalPosition;
    }

	public void AnimationFineshed()
	{
		//if(animatedSprite.Animation.ToString() == "Pickup")
		//{
		//	animatedSprite.Play("Idle");
		//	if (PickupItem != null)
		//	{
		//		PickupItem.Pickup();
		//	}
		//}
	}

	public void AnimationChanged()
	{
		//if(animatedSprite.Animation.ToString() == "Pickup")
		//{
		//	animatedSprite.Play("Idle");
		//}
	}

	public void Pickup(BaseItem item)
	{
		//PickupItem = item;
		//animatedSprite.Play("Pickup");
	}

	enum CharacterAction
	{
		Idle,
		Walk	
	}

}
