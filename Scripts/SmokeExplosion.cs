using Godot;
using System;

public partial class SmokeExplosion : Node2D
{
    private ColorRect SmokeAsset;
    public AnimationPlayer SmokeAnimPlayer;
    public override void _Ready()
    {
        SmokeAsset = GetNode<ColorRect>("Smoke"); 
        RandomNumberGenerator rand = new RandomNumberGenerator();
        (SmokeAsset.Material as ShaderMaterial).SetShaderParameter("texture_offset", new Vector2(rand.Randf(), rand.Randf())); 
        SmokeAnimPlayer = GetNode<AnimationPlayer>("AnimationPlayer");


    }

    public void PlayExplosionAnimation() //plays smoke animation
    {
        SmokeAnimPlayer.Play("explosion");
    }



}
