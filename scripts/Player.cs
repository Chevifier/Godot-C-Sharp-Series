using Godot;
using System;

public partial class Player : CharacterBody3D
{
	//[Export] makes variables available in the Godot inspecter
	[Export] public float SPEED = 5f;
	[Export] public float JUMPVELOCITY = 8f;

	[Export] public float GRAVITY = 8.19f;
	public float rotation_angle = 0.0f;

	public AnimationPlayer anim;
	public Area3D EnemyHitArea;
	Vector3 velocity;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		//Get the direct child 'AnimationPlayer' and assign it to anim
		anim = GetNode<AnimationPlayer>("AnimationPlayer");
		EnemyHitArea = GetNode<Area3D>("enemy_hit_area");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		//Get the Vector3 Velocity because XY and Z cant be edited individually
		velocity = Velocity;

		Vector2 input = Input.GetVector("left","right","up","down");
		//Get the current camera used by the viewport
		Basis camBasis = GetViewport().GetCamera3D().Basis;
		//Use the basis of the camera to determine where it should move
		Vector3 direction = camBasis * new Vector3(input.X, 0, input.Y);

		//change only the X and Y of our new velocity varriable saving Y for Gravity and Jumping
		velocity.X = direction.X * SPEED;
		velocity.Z = direction.Z * SPEED;

		//if not on floor add gravity else check if jump was press to apply jump velocity
		if(!IsOnFloor()){
			EnemyHitArea.Monitoring = true;
			velocity.Y -= GRAVITY * (float)delta;
		}else{
			EnemyHitArea.Monitoring = false;
			if(Input.IsActionJustPressed("jump")){
				velocity.Y = JUMPVELOCITY;
				Scale = new Vector3(0.5f, 1.5f,0.5f);
				
			}
		}
		Scale = Scale.Lerp(new Vector3(1.0f,1.0f,1.0f), (float)delta * 4.0f);

		//apply the velocity back to the built-in velocity variable
		Velocity = velocity;

		//Rotate to moving direction if Velocity.X and Z are not 0
		if (new Vector2(Velocity.X, Velocity.Z).Length() > 0){
			rotation_angle = new Vector2(Velocity.Z, Velocity.X).Angle();
			Vector3 rot = Rotation;
			rot.Y = (float)(Mathf.LerpAngle(Rotation.Y, rotation_angle, 10 * delta));
			Rotation = rot;

		}
		MoveAndSlide();
		Animate();

	}

	//Animation depending on state
	public void Animate(){
		if (IsOnFloor()){
			if (new Vector2(Velocity.X, Velocity.Y).Length() > 0){
				anim.Play("walk");
			}else{
				anim.Play("idle");
			}
		}else{
			anim.Play("jump");
		}
	}


	//Enemy hit single function connected to enemy_hit_area node
	public void HitEnemy(Enemy body){
		if (body.HasMethod("Die")){
			body.Die();
		}
		velocity.Y = JUMPVELOCITY;
		Velocity = velocity;
		
	}
}
