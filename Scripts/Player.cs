using Godot;
using Godot.Collections;
using System;

public partial class Player : CharacterBody2D
{
	private Area2D HurtBox;
	private ProgressBar HealthBar;
	private happy_boo_animation HappyBoo;
	private const float DamageRate = 5.0f;
	private const float Speed = 600.0f;
	private bool isAnimationRunning = false;

	public float Health = 100.0f;

	[Signal]
	public delegate void OnHealthDepletedEventHandler();

	public override void _Ready()
	{
		HappyBoo = GetNode<happy_boo_animation>("HappyBoo");
		if (HappyBoo == null) { throw new Exception("Player missing Happy Boo"); }
		HappyBoo.HurtAnimationEnd += () => { isAnimationRunning = false; };
		HurtBox = GetNode<Area2D>("HurtBox");
		HealthBar = GetNode<ProgressBar>("HealthBar");

	}

	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;


		// Get the input direction and handle the movement/deceleration.
		// As good practice, you should replace UI actions with custom gameplay actions.
		Vector2 direction = Input.GetVector("move_left", "move_right", "move_up", "move_down");
		if (direction != Vector2.Zero)
		{
			//velocity = direction * Speed, but with a damper to simulate inertia;
			velocity.X = Mathf.MoveToward(Velocity.X, direction.X * Speed, Speed * 0.2f);
			velocity.Y = Mathf.MoveToward(Velocity.Y, direction.Y * Speed, Speed * 0.2f);
		}
		else //same for deceleration
		{
			velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed * 0.2f);
			velocity.Y = Mathf.MoveToward(Velocity.Y, 0, Speed * 0.2f);
		}

		Velocity = velocity;
		MoveAndSlide();

		//set animations and pivot according to the velocity
		if (velocity != Vector2.Zero && !isAnimationRunning) { HappyBoo.PlayWalkAnimation(); }
		else if (!isAnimationRunning) { HappyBoo.PlayIdleAnimation(); }

		if (velocity.X < 0) { HappyBoo.Scale = new Vector2(-1, 1); }
		else { HappyBoo.Scale = new Vector2(1, 1); }

		//deal damage to the player
		Array<Node2D> OverlapArray = HurtBox.GetOverlappingBodies();
		if (OverlapArray.Count > 0)
		{
			Health -= (float)(DamageRate * OverlapArray.Count * delta);
			HealthBar.Value = Health;
			if (!isAnimationRunning)
			{
				HappyBoo.PlayHurtAnimation();
				isAnimationRunning = true;
			}
		}
		if (Health <= 0) //emits signal and removes player when player dies
		{
			EmitSignal(SignalName.OnHealthDepleted);
			GD.Print("Player Dead");
			QueueFree();
		}
	}
}
