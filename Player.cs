using Godot;
using System;

public class Player : KinematicBody2D
{
    [Export]
    private float _moveSpeed = 200f;
    private PackedScene _projectileScene;
    private DateTime _lastShootTime = DateTime.MinValue;

    public override void _Ready()
    {
        _projectileScene = GD.Load<PackedScene>("res://Projectile.tscn");
    }
    
    public override void _PhysicsProcess(float delta)
    {
        Move(delta);
        
        if (ShouldShoot())
        {
            Shoot();
        }
    }

    private void Move(float delta)
    {
        var moveVector = CalculateMoveVector();
        MoveAndCollide(moveVector * _moveSpeed * delta);
        AdjustGlobalPositionIfMovedOutsideCamera();
    }

    private Vector2 CalculateMoveVector()
    {
        var moveVector = new Vector2();
        if (Input.IsActionPressed("move_up"))
            moveVector.y--;
        if (Input.IsActionPressed("move_down"))
            moveVector.y++;
        if (Input.IsActionPressed("move_left"))
            moveVector.x--;
        if (Input.IsActionPressed("move_right"))
            moveVector.x++;
        
        moveVector = moveVector.Normalized();

        return moveVector;
    }

    private void AdjustGlobalPositionIfMovedOutsideCamera()
    {
        var globalPosition = GlobalPosition;

        const int minX = 28;
        const int minY = 20;
        const int maxX = 450;
        const int maxY = 348;
        
        if (globalPosition.x < minX)
            globalPosition.x = minX;
        if (globalPosition.y < minY)
            globalPosition.y = minY;
        if (globalPosition.x > maxX)
            globalPosition.x = maxX;
        if (globalPosition.y > maxY)
            globalPosition.y = maxY;

        GlobalPosition = globalPosition;
    }

    private bool ShouldShoot()
    {
        var minimalTimeSpanBetweenShots = TimeSpan.FromMilliseconds(200);
        var timeSpanFromLastShot = DateTime.Now - _lastShootTime;
        
        return Input.IsActionPressed("shoot") && timeSpanFromLastShot >= minimalTimeSpanBetweenShots;
    }
    
    private void Shoot()
    {
        _lastShootTime = DateTime.Now;
        
        var newProjectile = (Projectile) _projectileScene.Instance();
        var projectilePosition = Position;
        projectilePosition.x += 40;
        newProjectile.Position = projectilePosition;
        newProjectile.MoveVector = Vector2.Right;
        
        GetTree().Root.GetNode("World").AddChild(newProjectile);
    }
}
