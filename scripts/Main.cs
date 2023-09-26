using Godot;// Use all the functionallity and classes from the Godot namespace
using System; // Use all the functionallity and classes from the System namespace

//create a partial class that inherits Node3D
public partial class Main : Node3D
{
    public override void _Ready()
    {
        //Print to the Godot debug conssole
        GD.Print("Hello Godot");
    }
}

