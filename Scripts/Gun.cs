using Godot;
using Godot.Collections;
using System;
using System.Linq;

public partial class Gun : Area2D
{
    private PackedScene BulletScene;
    public override void _Ready()
    {
        base._Ready();
        BulletScene = GD.Load<PackedScene>("res://Scenes/Bullet.tscn");
    }
    public override void _PhysicsProcess(double delta)
    {
        //find bodies that interact with the gun's collider and rotate the gun towards them
        Array<Node2D> OverlapArray = GetOverlappingBodies();
        if (OverlapArray.Count > 0)
        {
            Node2D Target = OverlapArray.First();
            LookAt(Target.GlobalPosition);
        }

    }

    public void Shoot()
    {
        //spawn bullets as children of the bullet spawner node
        Marker2D BulletSpawn = (Marker2D)GetNode("WeaponPivot/Pistol/BulletSpawn");
        Node2D BulletInstance = (Node2D)BulletScene.Instantiate();
        BulletSpawn.AddChild(BulletInstance);
        BulletInstance.GlobalPosition = BulletSpawn.GlobalPosition;
        BulletInstance.GlobalRotation = BulletSpawn.GlobalRotation;
    }

    public void OnTimerTimeout()
    {
        //if there are enemies in the gun's collider
        Array<Node2D> OverlapArray = GetOverlappingBodies();
        if (OverlapArray.Count > 0)
        {
            Shoot();
        }
    }
}
