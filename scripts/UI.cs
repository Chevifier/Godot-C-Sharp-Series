using Godot;
using Godot.Collections;
using System;

public partial class UI : CanvasLayer
{

	public Label score_lbl;
	public int score = 0;
	public override void _Ready()
	{
		score_lbl = GetNode<Label>("hb/score_lbl");
		//get all the nodes in the coin group
		Array<Node> coins = GetTree().GetNodesInGroup("coins");
		//Connect the CoinCollected signal of each coin to AddScore function 
		for(int i = 0; i < coins.Count; i++){
			Coin n = (Coin)coins[i];
			n.CoinCollected += () => AddScore();
		}

	}

	public void AddScore(){
		score = score + 1;
		score_lbl.Text = "x " + score.ToString();
	}
}
