using Godot;
using System;

public partial class Coin : Area3D
{
	//create custom signal "EventHandler" after name required
 	[Signal]
	public delegate void CoinCollectedEventHandler();

	public CollisionShape3D col;

	public AudioStreamPlayer coin_fx;

	[Export]
	public float spin_speed = 5.0f;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		coin_fx = GetNode<AudioStreamPlayer>("coin_fx");
		col = GetNode<CollisionShape3D>("col");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		RotateY(spin_speed * (float)delta);
	}
public void setcoloff(){
	col.Disabled = true;
}

	public void Collected(Player body){
		//prevent node queue error
		CallDeferred("setcoloff");
		//emit signal only using name without 'EventHandler'
		EmitSignal("CoinCollected");
		//Hide so it seams like it was remove
		Hide();
		//disable collision checking
		Tween tw = CreateTween();
		//play audio and set a long enough delay to removce it afterwards
		tw.TweenCallback(Callable.From(()=>coin_fx.Play()));
		tw.TweenCallback(Callable.From(()=> QueueFree())).SetDelay(2f);
		
		
	}
}
