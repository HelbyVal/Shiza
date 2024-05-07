using System;
using Godot;

public abstract partial class PickableItem3D: RigidBody3D
{
    protected bool _isTaken = false;
    protected Vector3 _prevPoint;
    protected Vector3 _target;
    protected float gravity;
    protected Node3D holder;
    public override void _Ready()
    {
        _prevPoint = GlobalPosition;
        gravity = ProjectSettings.GetSetting("physics/3d/default_gravity").AsSingle();
    }
    public void MoveOnPosition(Vector3 target)
    {
        _target = target;
    }

    public void Take(Node3D holder){
        GD.Print("Taken!");
        _isTaken = true;
        this.holder = holder;
    }

    public void Drop(double delta){
        GD.Print("Droped!");
        _isTaken = false;
        SetInertia(delta);
        holder = null;
    }

    private void SetInertia(double delta){
        var iner = _prevPoint.DirectionTo(GlobalPosition) * (_prevPoint.DistanceTo(GlobalPosition) / (float)delta);
        LinearVelocity = new Vector3(Sqrt(iner.X), Sqrt(iner.Y), Sqrt(iner.Z));
    }

    private float Sqrt(float value){
        if(value < 0){
            return -Mathf.Sqrt(-value);
        }
        else{
            return Mathf.Sqrt(value);
        }
    }

    public override void _PhysicsProcess(double delta)
    {
        GD.Print(LinearVelocity);
        if (_isTaken )
        {
			var dist = this.GlobalPosition.DistanceTo(_target);
            var dir = this.GlobalPosition.DirectionTo(_target).Normalized();
            LookAt(holder.GlobalPosition);
            _prevPoint = GlobalPosition;
            MoveAndCollide(dir * dist);
        }
    }
}

