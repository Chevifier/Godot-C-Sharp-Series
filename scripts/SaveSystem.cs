using Godot;
using Godot.Collections;
using System;

public partial class SaveSystem : Node
{
	public Dictionary player_data = new Dictionary();

	//Save file path dedfault storage paths
	//Linux: .local/share/godot/app_userdata/app_name/filename
	//Windows: %APPDATA%/godot/appname/filename
	public String save_path = "user://CsharpGame.sav";
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		//load saved data if any
		Load();
	}

	//adds level score
	public void AddLevelScore(String level, int score){
		player_data[level] = score;
	}
	//save all data
	public void Save(){
		FileAccess file = FileAccess.Open(save_path,FileAccess.ModeFlags.Write);
		file.StoreVar(player_data);
	}

	public void Load(){
		//Check if file exist then load it otherwize set a new Dictionary
		if (FileAccess.FileExists(save_path)){
			FileAccess file = FileAccess.Open(save_path,FileAccess.ModeFlags.Read);
			player_data = (Dictionary)file.GetVar();
		}else{
			player_data = new Dictionary();

		}
		GD.Print(player_data);
	}
}
