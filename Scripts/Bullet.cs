using Godot;
using System;

public partial class Bullet : Area2D
{
    public const float Speed = 1000.0f;
    private float TravelledDistance = 0;

    private const float MaxRange = 1200;
    public override void _PhysicsProcess(double delta)
    {
        // move bullet in the direction it's rotated towards
        Vector2 Direction = Vector2.Right.Rotated(Rotation);
        Position += Direction * Speed * (float)delta;
        TravelledDistance += Speed * (float)delta;
        if (TravelledDistance > MaxRange)
        {
            QueueFree();
        }
    }

    public void OnBodyEntered(Node2D body)
    {
        EnemyMovement enemy = body as EnemyMovement;
        if (enemy != null)
        {
            enemy.TakeDamage();
        }
        QueueFree();
    }

}
