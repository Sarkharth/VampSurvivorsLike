using Godot;
using System;
using System.IO;

public partial class EnemyMovement : CharacterBody2D
{
    public const float Speed = 300.0f;
    public CharacterBody2D Player;

    [Signal]
    public delegate void OnEnemyKilledEventHandler();
    private int Health = 3;
    private slime Slime;
    private CollisionShape2D SlimeCollider;
    private PackedScene SmokeScene;

    public override void _Ready()
    {
        //get the player
        Player = GetNode<CharacterBody2D>("/root/Game/Player");
        if (Player == null) { throw new Exception("Enemy missing Player"); }
        Slime = GetNode<slime>("Slime");
        //load the smoke scene for when the enemy dies
        SmokeScene = GD.Load<PackedScene>("res://smoke_explosion/smoke_explosion.tscn");
        //launch default animation
        Slime.PlayWalkAnimation();
    }
    public override void _PhysicsProcess(double delta)
    {
        //Enemy movement towards the player
        if (!IsQueuedForDeletion())
        {
            Vector2 PlayerPosition = Player.Position;
            Vector2 EnemyPosition = Position;
            Vector2 direction = EnemyPosition.DirectionTo(PlayerPosition); //calculates a vector going from the enemy to the player

            Velocity = direction * Speed;

            MoveAndSlide();
        }


    }

    public void TakeDamage()
    {
        if (!IsQueuedForDeletion()) //checking if the enemy still lives before interacting with it
        {
            Health -= 1;
            Slime.PlayHurtAnimation();
            if (Health == 0) //handles enemy death
            {
                //adds smoke explosion
                Node2D SmokeInstance = (Node2D)SmokeScene.Instantiate();
                AddChild(SmokeInstance);
                (SmokeInstance as SmokeExplosion).PlayExplosionAnimation();
                (SmokeInstance as SmokeExplosion).SmokeAnimPlayer.AnimationFinished += OnAnimationFinished; //get signal that animation is finished
                //disable collider and enemy visibility
                SlimeCollider = GetNode<CollisionShape2D>("CollisionShape2D");
                SlimeCollider.Disabled = true;
                Slime.Visible = false;
            }
        }

    }

    private void OnAnimationFinished(StringName animName)
    {
        EmitSignal(SignalName.OnEnemyKilled);
        QueueFree();
    }
}