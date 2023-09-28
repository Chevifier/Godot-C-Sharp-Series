using Godot;
using System;
using System.Security.Cryptography.X509Certificates;

public partial class MainMenu : Control
{

	public SaveSystem saveSystem;

	[Export(PropertyHint.File)]
	public String LevelOnePath = "";
	public Label scores_lbl;

	public bool showhide = false;

    public override void _Ready()
	{
		saveSystem = GetTree().Root.GetNode<SaveSystem>("SaveSystem");
		scores_lbl = GetNode<Label>("scores");
		scores_lbl.Hide();
	}

	public void LoadScores(){
		int l1 = (int)saveSystem.player_data["Level1"];
		int l2 = (int)saveSystem.player_data["Level2"];
		scores_lbl.Text = "SCORES \n";
		scores_lbl.Text += "Level 1: " + l1.ToString() + "\n";
		scores_lbl.Text += "Level 2: " + l2.ToString() + "\n";
	}
	public void StartGame(){
		GetTree().ChangeSceneToFile(LevelOnePath);
	}

	public void ShowHideScores(){
		if (showhide==false){
			LoadScores();
			scores_lbl.Show();
			showhide = true;
		}else{
			scores_lbl.Hide();
			showhide = false;
		}
	}
}
