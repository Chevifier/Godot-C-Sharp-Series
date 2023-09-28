using Godot;
using Godot.Collections;
using System;
using System.Security.Cryptography.X509Certificates;

public partial class Enemy : CharacterBody3D
{


	public float SPEED = 4.0f;
	public float ATTACKRANGE = 10.0f;
	public float JumpVelocity = 2.0f;

	public AnimationPlayer anim;

	public Node3D target;

	public float GRAVITY = 9.85f;

	public float rotation_direction = 0.0f;

	public Vector3 velocity;

	public enum State{
		IDLE,
		ATTACK,
		STRUGGLE,
		DIE
	}

	public State CurrentState;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		CurrentState = State.IDLE;
		target = GetParent().GetParent().GetNode<Node3D>("character");
		anim = GetNode<AnimationPlayer>("AnimationPlayer");

	}

    public override void _PhysicsProcess(double delta)
    {
        switch(CurrentState){
			case State.IDLE:
				UpdateIdle();
				break;
			case State.ATTACK:
				UpdateAttack(delta);

				break;
			case State.DIE:
				UpdateDie();
				break;
		}

				// Add the gravity.
		velocity = Velocity;
		if (!IsOnFloor())
			velocity.Y -= GRAVITY * (float)delta;

		if (new Vector2(Velocity.X,Velocity.Z).Length() > 0.0){
			rotation_direction = new Vector2(Velocity.Z, Velocity.X).Angle();
			Vector3 rot = Rotation;
			rot.Y = (float)(Mathf.LerpAngle(Rotation.Y, rotation_direction, delta * 10));
			Rotation = rot;
		}
		Velocity = velocity;
		Animate();
    }

    private void UpdateIdle(){
		if (Position.DistanceTo(target.Position) < ATTACKRANGE)
		{
			CurrentState = State.ATTACK;
		}
	}

	private void UpdateAttack(double delta)
	{
		Vector3 t = target.Position;
		t.Y = Position.Y;
		Vector3 direction = Position.DirectionTo(t);
		velocity = Velocity;
		velocity.X = direction.X * SPEED;
		velocity.Z = direction.Z * SPEED;
		Velocity = velocity;
		MoveAndSlide();
		CheckforPlayer();
	}

public void CheckforPlayer(){
		//Hit Enemy
		for(int i = 0; i < GetSlideCollisionCount();i++){
			//Get all current collision
			KinematicCollision3D c = GetSlideCollision(i);
			//get the colliding object
			GodotObject co = c.GetCollider();
			//check if its the player
			//if the player is on the floor when colliding "Game Over" Restart level
			if(co is Player){
				if(((Player)co).IsOnFloor()){
					GetTree().ReloadCurrentScene();
				}
			}
		}
	}
	public void Animate(){
		if(new Vector2(Velocity.X,Velocity.Z).Length() > 0.0){
			anim.Play("walk");

		}else{
			anim.Play("idle");
		}
	}


	public void UpdateDie(){
		//create a tween that scales the enemy to a pan cake shape the removes it from the scene
		Tween tw = CreateTween();
		tw.TweenProperty(this, "scale", new Vector3(1.5f,0.25f,1.5f), 0.2f);
		tw.TweenCallback(Callable.From(() => QueueFree()));
		}

	public void Die(){
		CurrentState = State.DIE;
	}

}
