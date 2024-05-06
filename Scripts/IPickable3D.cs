using System;
using Godot;

public abstract partial class PickableItem3D: RigidBody3D
{
    protected bool _isTaken = false;
    protected Vector3 _prevPoint;
    protected Vector3 _target;
    public override void _Ready()
    {
        _prevPoint = GlobalPosition;
    }
    public void MoveOnPosition(Vector3 target)
    {
        _target = target;
    }

    public void Take(){
        _isTaken = true;
    }

    public void Drop(double delta){
        _isTaken = false;
        SetInertia(delta);
    }

    private void SetInertia(double delta){
        // var iner = _prevPoint.DirectionTo(GlobalPosition) * (_prevPoint.DistanceTo(GlobalPosition) / (float)delta);
        // LinearVelocity = iner/2;
    }

    public override void _PhysicsProcess(double delta)
    {
        if (_isTaken)
        {
			var dist = this.GlobalPosition.DistanceTo(_target);
            var dir = this.GlobalPosition.DirectionTo(_target).Normalized();
            _prevPoint = GlobalPosition;
            //LinearVelocity = new Vector3(0,-1,0);
            MoveAndCollide(dir * dist);
        }
        // GD.Print("Curren position: ", GlobalPosition);
        // GD.Print("Prev position: ", _prevPoint);
        // GD.Print("DirectionTo: ", _prevPoint.DirectionTo(GlobalPosition));
        // GD.Print("DistanceTo: ", _prevPoint.DistanceTo(GlobalPosition));
        // GD.Print("delta: ", delta);
        // GD.Print("prev velocity: ", LinearVelocity);
        // //var t = (float)delta * (float)1;
        // //LinearVelocity = _prevPoint.DirectionTo(GlobalPosition) * (_prevPoint.DistanceTo(GlobalPosition) / (t));
        // //_prevPoint = GlobalPosition;
        // GD.Print("current velocity: ", LinearVelocity);
        // GD.Print("-------------------------");
    }
}

