using Godot;
using System;

public partial class MainMenu : Control
{
	[Export(PropertyHint.File)]

	public String LevelOnePath = "";
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		
	}

	public void StartGame(){
		GetTree().ChangeSceneToFile(LevelOnePath);
	}
}
