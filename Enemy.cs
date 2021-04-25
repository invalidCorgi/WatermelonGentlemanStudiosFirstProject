using Godot;
using System;

public class Enemy : KinematicBody2D
{
    public float Hp { get; set; } = 100f;

    public void GetHit(float damage)
    {
        Hp -= damage;
        if (Hp <= 0)
        {
            QueueFree();
        }
    }
}
