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
        GD.Print("Taken!");
        _isTaken = true;
        Freeze = true;
        //Sleeping = true;
    }

    public void Drop(double delta){
        GD.Print("Droped!");
        _isTaken = false;
        SetInertia(delta);
        //Sleeping = false;
        Freeze = false;
    }

    private void SetInertia(double delta){
        var iner = _prevPoint.DirectionTo(GlobalPosition) * (_prevPoint.DistanceTo(GlobalPosition) / (float)delta);
        //Inertia = new Vector3(Mathf.Log(iner.X), Mathf.Log(iner.Y)+(float)9.7, Mathf.Log(iner.Z));
        //Inertia = iner;
    }

    public override void _PhysicsProcess(double delta)
    {
        GD.Print(LinearVelocity);
        if (_isTaken && Freeze)
        {
			var dist = this.GlobalPosition.DistanceTo(_target);
            var dir = this.GlobalPosition.DirectionTo(_target).Normalized();
            _prevPoint = GlobalPosition;
            MoveAndCollide(dir * dist);
        }
    }
}

