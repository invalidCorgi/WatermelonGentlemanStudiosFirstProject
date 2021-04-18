using Godot;
using System;

public class Projectile : KinematicBody2D
{
    public float MoveSpeed { get; set; } = 300f;
    public Vector2 MoveVector { get; set; } = Vector2.Zero;
    public float Damage { get; set; } = 40f;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        
    }
    
    public override void _PhysicsProcess(float delta)
    {
        var collision = MoveAndCollide(MoveVector * MoveSpeed * delta);
        if (collision?.Collider is Enemy enemy)
        {
            enemy.GetHit(Damage);
            QueueFree();
        }
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
