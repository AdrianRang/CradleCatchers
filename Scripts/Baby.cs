using Godot;
using System;

public partial class Baby : Node2D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	double time = 0;
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		time+=delta;
		GD.Print("time");
		if(time > 1) GD.Print("AAAAA");
	}
}
