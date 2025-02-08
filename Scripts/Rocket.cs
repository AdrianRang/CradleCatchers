using Godot;
using System;

public partial class NewScript : RigidBody2D
{
	float angularSpeed=0;		
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		
	}

    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);
		Vector2 mousePosition = GetGlobalMousePosition() - GlobalPosition;
		// LookAt(mousePosition);
		float angleDifference = Mathf.Wrap(Mathf.Atan2(mousePosition.Y, mousePosition.X) - GlobalRotation, -Mathf.Pi, Mathf.Pi);
		GD.Print(angleDifference);
		angularSpeed += angleDifference*0.03f;
		Rotation += angularSpeed;
		angularSpeed*=0.9f;
    }

    public override void _Input(InputEvent @event)
	{
		if (@event is InputEventKey)
		{
			Vector2 force = new Vector2(0, -500); // Example force vector
			ApplyCentralImpulse(force.Rotated(Rotation + Mathf.Pi/2));
		}
	}
}
