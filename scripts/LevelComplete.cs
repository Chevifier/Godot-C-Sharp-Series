using Godot;
using System;
using System.IO;

public partial class LevelComplete : Area3D
{
	// Called when the node enters the scene tree for the first time.
	[Export(PropertyHint.File)]

	public String NextLevelPath = "";


	public void Complete(Player body){
		if(NextLevelPath != ""){
			GetTree().ChangeSceneToFile(NextLevelPath);

		}else{
			GD.Print("Path not set");
		}
	}
}
