using Godot;
using System;

public partial class Plane : RigidBody2D
{
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
		if (Input.IsActionPressed("ui_right"))
		{
			// Vector2 force = new Vector2(2500, 0);
			Vector2 force = new Vector2(Mathf.Max((Mathf.Cos(Rotation-Mathf.Pi/2)+0.9f)/2f*6000, 0), 0);
			ApplyCentralForce(force.Rotated(Rotation));
		}
		if (Input.IsActionPressed("ui_left"))
		{
			LinearVelocity = new Vector2(LinearVelocity.X*0.99f, LinearVelocity.Y);
		}
		if (Input.IsActionPressed("ui_up"))
		{
			float force = -40000f;
			ApplyTorque(force);
		}
		if (Input.IsActionPressed("ui_down"))
		{
			float force = 40000f;
			ApplyTorque(force);
		}

		// Water
		if (Position.Y > 600)
		{
			ApplyForce(new Vector2(0, -2500));
			LinearVelocity = new Vector2(LinearVelocity.X * 0.95f, LinearVelocity.Y * 0.95f);
		}

		// Plane physics
		// Add torque to look toward velocity
		float targetRotation = LinearVelocity.Angle();
		float rotationDifference = Mathf.Wrap(targetRotation - Rotation, -Mathf.Pi, Mathf.Pi);
		
		float torque = rotationDifference * 50000; // Adjust the multiplier as needed
		ApplyTorque(torque);
		// Ensure velocity always points forward
		float speed = Mathf.Sqrt(Mathf.Pow(LinearVelocity.X, 2) + Mathf.Pow(LinearVelocity.Y, 2));
		// Fix for angle Wrapping;
		if(Rotation*LinearVelocity.Angle() < 0 && Mathf.Abs(Rotation) > 1){
			GD.Print("AAA");
			LinearVelocity = new Vector2(speed, 0).Rotated(LinearVelocity.Angle());
		} else {
			LinearVelocity = new Vector2(speed, 0).Rotated((Rotation+LinearVelocity.Angle()*10)/11);
		}
	}

	public override void _Input(InputEvent @event)
	{
		if (@event.IsActionPressed("ui_reset"))
		{
			GetTree().ReloadCurrentScene();
		}
	}
}
