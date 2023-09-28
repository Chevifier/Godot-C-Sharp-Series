using Godot;// Use all the functionallity and classes from the Godot namespace
using System; // Use all the functionallity and classes from the System namespace

//create a partial class that inherits Node3D
public partial class Level : Node3D
{
    [Export]
    public int level = 1;
    //Get reference to save system 
    public SaveSystem saveSystem;
    public override void _Ready()
    {
        saveSystem = GetTree().Root.GetNode<SaveSystem>("SaveSystem");
        
    }
    public void LevelComplete(){
        //get the ui and grab its score
        UI ui = GetNode<UI>("UI");
        //Save the score using the nodes name i.e Level1 
        saveSystem.AddLevelScore(Name,ui.score);
        saveSystem.Save();
    }
}

