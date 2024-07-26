using Godot;
using Godot.Collections;
using System;

public partial class GameOver : Control
{
    private happy_boo_animation HappyBoo;
    public Dictionary SceneParameters;

    public override void _Ready()
    {
        //adds idle HappyBoo animation
        HappyBoo = GetNode<happy_boo_animation>("CanvasLayer/HappyBoo");
        HappyBoo.PlayIdleAnimation();
    }

}
