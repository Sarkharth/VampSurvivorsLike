using Godot;
using System;

public partial class slime : Node2D
{
	private AnimationPlayer AnimPlayer;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		AnimPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	public void PlayIdleAnimation()
	{
		AnimPlayer.Play("idle");
	}

	public void PlayWalkAnimation()
	{
		AnimPlayer.Play("walk");
	}

	public void PlayHurtAnimation()
	{
		AnimPlayer.Play("hurt");
	}
}
