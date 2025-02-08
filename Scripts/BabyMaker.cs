using Godot;
using System;
using System.Diagnostics.Tracing;

public partial class BabyMaker : Node2D
{
	[Export]
	String hairsPath;
	[Export]
	Vector2 hairOffset;

	[Export]
	String clothesPaths;
	[Export]
	Vector2 clothesOffset;

	[Export]
	String bodiesPath;

	[Export]
	float timeBetweenBaby;

	Texture2D[] hairs;
	Texture2D[] bodies;
	Texture2D[] clothes;


	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		var hairFiles = DirAccess.GetFilesAt(hairsPath);
		hairs = new Texture2D[hairFiles.Length];

		for (int i = 0; i < hairFiles.Length; i++)
		{
			// Concatenate path with filename
			string fullPath = hairsPath + hairFiles[i];
			// Only load .png or .jpg files
			if (fullPath.EndsWith(".tres"))
			{
				hairs[i] = GD.Load<Texture2D>(fullPath);
			}

		}

		var bodyFiles = DirAccess.GetFilesAt(bodiesPath);
		bodies = new Texture2D[bodyFiles.Length];

		for (int i = 0; i < bodyFiles.Length; i++)
		{
			string fullPath = bodiesPath + bodyFiles[i];
			if (fullPath.EndsWith(".tres"))
			{
				bodies[i] = GD.Load<Texture2D>(fullPath);
			}
		}

		var clothesFiles = DirAccess.GetFilesAt(clothesPaths);
		clothes = new Texture2D[clothesFiles.Length];

		for (int i = 0; i < clothesFiles.Length; i++)
		{
			string fullPath = clothesPaths + clothesFiles[i];
			if (fullPath.EndsWith(".tres"))
			{
				clothes[i] = GD.Load<Texture2D>(fullPath);
			}
		}


		GD.Randomize();
	}

	float lastBabyGen;
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		lastBabyGen += (float)delta;
		if (lastBabyGen >= timeBetweenBaby)
		{
			// GenerateBaby(new Vector2(GD.Randf() * GetViewport().Size.x, GD.Randf() * GetViewport().Size.y));
			GenerateBaby(new Vector2((GD.Randf() * 100000) - 50000, -3000) + GlobalPosition);
			lastBabyGen = 0;
		}
	}

	void GenerateBaby(Vector2 pos)
	{
		int randHair = (int)(GD.Randi() % hairs.Length);
		int randBody = (int)(GD.Randi() % bodies.Length);
		int randClothes = (int)(GD.Randi() % clothes.Length);
		RigidBody2D baby = new RigidBody2D();
		baby.Rotate(GD.Randf() * Mathf.Pi * 2 - Mathf.Pi);

		if (bodies[randBody] != null && bodies.Length > 0)
		{
			Sprite2D bodySprite = new Sprite2D();
			bodySprite.Position = new Vector2(0, 0);
			bodySprite.Texture = bodies[randBody];
			bodySprite.Visible = true;
			bodySprite.ZIndex = 0; // Set Z index for body
			baby.AddChild(bodySprite);
		}

		if (hairs[randHair] != null && hairs.Length > 0)
		{
			Sprite2D hairSprite = new Sprite2D();
			hairSprite.Position = hairOffset;
			hairSprite.Texture = hairs[randHair];
			hairSprite.Visible = true;
			hairSprite.ZIndex = 1; // Set Z index for hair
			baby.AddChild(hairSprite);
		}

		if (clothes[randClothes] != null && clothes.Length > 0)
		{
			Sprite2D clothesSprite = new Sprite2D();
			clothesSprite.Position = clothesOffset;
			clothesSprite.Texture = clothes[randClothes];
			clothesSprite.Visible = true;
			clothesSprite.ZIndex = 2; // Set Z index for clothes
			baby.AddChild(clothesSprite);
		}

		AddChild(baby);
		baby.GlobalPosition = pos;

		// ulong babyId = baby.GetInstanceId();
		// Load script with error checking
		// Script babyScript = GD.Load<Script>("res://Scripts/Baby.cs");

		// GD.Print(babyScript);
		// GD.Print(InstanceFromId(babyId).GetScript());
	}
}
