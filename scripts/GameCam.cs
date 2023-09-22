using Godot;
using System;

public partial class GameCam : Node3D
{
    public Node3D target;

    public override void _Ready()
    {
        //get player
        target = GetParent().GetNode<Node3D>("character");
    }

    public override void _Process(double delta)
    {
        //follow player
        Position = target.Position;
    }


}
