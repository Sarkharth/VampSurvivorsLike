using Godot;
using Godot.Collections;
using System;

public partial class Game : Node2D
{

    private PathFollow2D EnemySpawnPath;
    private PackedScene MobScene;
    public Dictionary SceneParameters;
    public override void _Ready() //loads resources necessary for other functions
    {
        EnemySpawnPath = GetNode<PathFollow2D>("Player/Path2D/PathFollow2D");
        MobScene = GD.Load<PackedScene>("res://Scenes/Enemy.tscn");
    }

    private void SpawnMob() //randomly spawns mobs along a Path2D situated around the viewport
    {
        Node2D MobInstance = (Node2D)MobScene.Instantiate();
        RandomNumberGenerator rand = new RandomNumberGenerator();
        EnemySpawnPath.ProgressRatio = rand.Randf();
        MobInstance.GlobalPosition = EnemySpawnPath.GlobalPosition;
        AddChild(MobInstance);
    }

    public void OnTimerTimeout() //spawns mob every time its timer goes out
    {
        SpawnMob();
    }

    public void OnHealthDepleted() //handles player death
    {
        //transferring enemy killcount to Game Over scene
        PackedScene CurrentScene = GD.Load<PackedScene>("res://Scenes/Game.tscn");
        PackedScene TargetScene = GD.Load<PackedScene>("res://Scenes/GameOver.tscn");
        TransferData(CurrentScene, TargetScene);
        //switching scenes
        GetTree().ChangeSceneToFile("res://Scenes/GameOver.tscn");
    }

    public void OnEnemyKilled() //increments the killcount
    {

    }

    public void TransferData(PackedScene OldScene, PackedScene NewScene) //handles data transfer between main game scene and game over scene
    {
        //OldScene.SceneParameters = NewScene.SceneParameters;
        GD.Print("Data Transfered");
    }
}
