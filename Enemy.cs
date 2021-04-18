using Godot;
using System;

public class Enemy : KinematicBody2D
{
    private float _hp = 100f;
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        
    }

    public void GetHit(float damage)
    {
        _hp -= damage;
        if (_hp <= 0)
        {
            QueueFree();
        }
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
