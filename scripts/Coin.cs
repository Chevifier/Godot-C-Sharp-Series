using Godot;
using System;

public partial class Coin : Area3D
{
	//create custom signal "EventHandler" after name required
 	[Signal]
	public delegate void CoinCollectedEventHandler();

	LevelManager levelmanager;

	public AudioStreamPlayer coin_fx;
	[Export]
	public float spin_speed = 5.0f;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		coin_fx = GetNode<AudioStreamPlayer>("coin_fx");
		levelmanager = GetTree().Root.GetNode<LevelManager>("LevelManager");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		RotateY(spin_speed * (float)delta);
	}


	public void Collected(Player body){
		//emit signal only using name without 'EventHandler'
		EmitSignal("CoinCollected");
		//Hide so it seams like it was remove
		Hide();
		//disable collision checking
		Monitoring = false;
		Tween tw = CreateTween();
		//play audio and set a long enough delay to removce it afterwards
		tw.TweenCallback(Callable.From(()=>coin_fx.Play()));
		tw.TweenCallback(Callable.From(()=> QueueFree())).SetDelay(2f);
		
	}
}
