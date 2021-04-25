using Godot;
using System;

public class Projectile : KinematicBody2D
{
    public float MoveSpeed { get; set; } = 250f;
    public Vector2 MoveVector { get; set; } = Vector2.Zero;
    public float Damage { get; set; } = 40f;
    
    public override void _PhysicsProcess(float delta)
    {
        var collision = MoveAndCollide(MoveVector * MoveSpeed * delta);
        if (collision?.Collider is Enemy enemy)
        {
            enemy.GetHit(Damage);
            QueueFree();
        }
    }
}
