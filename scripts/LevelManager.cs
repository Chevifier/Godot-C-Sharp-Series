using Godot;
using System;

public partial class LevelManager : Node
{
	public int score = 0;

	public void AddScore(int amount){
		score += amount;
		GD.Print(score);
	}

	public void Reset(){
		score = 0;
	}

}
