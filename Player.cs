using Godot;
using System;

public class Player : KinematicBody2D
{
    [Export]
    private float _moveSpeed = 300f;

    private PackedScene _projectileScene;
    

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        _projectileScene = GD.Load<PackedScene>("res://Projectile.tscn");
    }
    
    public override void _PhysicsProcess(float delta)
    {
        var moveVec = new Vector2();
        if (Input.IsActionPressed("move_up"))
            moveVec.y--;
        if (Input.IsActionPressed("move_down"))
            moveVec.y++;
        if (Input.IsActionPressed("move_left"))
            moveVec.x--;
        if (Input.IsActionPressed("move_right"))
            moveVec.x++;
        moveVec = moveVec.Normalized();

        MoveAndCollide(moveVec * _moveSpeed * delta);

        if (Input.IsActionJustPressed("shoot"))
        {
            var newProjectile = (Projectile)_projectileScene.Instance();
            var newPosition = Position;
            newPosition.x += 150;
            newProjectile.Position = newPosition;
            newProjectile.MoveVector = Vector2.Right;
            GetTree().Root.GetNode("World").AddChild(newProjectile);
        }
    }
    

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
