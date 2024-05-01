using Godot;
using System;

public partial class character : CharacterBody2D
{
	private NavigationAgent2D navigationAgent;
	private CollisionShape2D collisionShape;
	public const float Speed = 250.0f;
	public const float ChangeScale = 0.01f;
	Vector2 click_position = new Vector2();
	Vector2 target_position = new Vector2();
	AnimatedSprite2D animatedSprite;
	CharacterAction action = CharacterAction.Idle;
    CharacterAction previousAction = CharacterAction.Idle;
    BaseItem PickupItem;
	public bool Picking { get { return picking; } }
	bool picking = false;



	public override void _Ready()
    {
		collisionShape = GetNode<CollisionShape2D>("CollisionShape2D");
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
		if (action == CharacterAction.Pickup)
		{
			return;
		}

		var global = GetNode<Global>("/root/Global");
        if (Input.IsActionJustPressed("mouse_click") && global.MouseEnteredInFloor){
			var mousePosition = GetGlobalMousePosition();
			navigationAgent.TargetPosition = mousePosition;
		}

		if (GlobalPosition.DistanceTo(navigationAgent.TargetPosition) > 15)
		{
			Vector2 nextPathPosition = navigationAgent.GetNextPathPosition();
			var velocity = (nextPathPosition - GlobalPosition).Normalized() * Speed;
			Velocity = velocity;

			if (velocity.Y > 0)
			{
				animatedSprite.Scale = animatedSprite.Scale + (new Vector2(ChangeScale, ChangeScale) * velocity.Abs().Y/Speed);
				collisionShape.Scale = collisionShape.Scale + (new Vector2(ChangeScale, ChangeScale) * velocity.Abs().Y/Speed);
            }
			else
			{
                animatedSprite.Scale = animatedSprite.Scale - (new Vector2(ChangeScale, ChangeScale) * velocity.Abs().Y/Speed);
                collisionShape.Scale = collisionShape.Scale - (new Vector2(ChangeScale, ChangeScale) * velocity.Abs().Y/Speed);
            }

			if (Velocity.X > 0)
			{
                ChangeAnimation(CharacterAction.Walk);
                action = CharacterAction.Walk;
				if (!animatedSprite.FlipH)
					animatedSprite.FlipH = true;
			}
			else
			{
                ChangeAnimation(CharacterAction.Walk);
                if (animatedSprite.FlipH)
					animatedSprite.FlipH = false;
			}


			MoveAndSlide();
		}
		else
		{
            ChangeAnimation(CharacterAction.Idle);
            action = CharacterAction.Idle;
        }
    }

	public void MoveToItem(Vector2 position)
	{
        navigationAgent.TargetPosition = position;
		picking = true;
	}

    public void MoveToWay(Vector2 position)
    {
        navigationAgent.TargetPosition = position;
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
	}

	public void AnimationChanged()
	{
	}

	public void Pickup(BaseItem item)
	{
		PickupItem = item;
        ChangeAnimation(CharacterAction.Pickup);
    }

	public void ChangeAnimation(CharacterAction act)
	{
		previousAction = action;
		action = act;
        animatedSprite.Play(act.ToString());
    }

	public enum CharacterAction
	{
		Idle,
		Walk,
		Pickup
	}

	public void FrameChanged()
	{
		if (action == CharacterAction.Pickup && animatedSprite.Frame == 5)
		{
            if (PickupItem != null)
            {
                PickupItem.Pickup();
            }
            ChangeAnimation(CharacterAction.Idle);
            picking = false;
			PickupItem = null;
        }
	}
}
