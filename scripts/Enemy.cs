using Godot;
using System;

public partial class Enemy : CharacterBody3D
{


	public float SPEED = 4.0f;
	public float ATTACKRANGE = 10.0f;
	public float JumpVelocity = 2.0f;

	public AnimationPlayer anim;

	public Node3D target;

	public float GRAVITY = -9.85f;

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
		target = GetParent().GetNode<Node3D>("character");
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
		}
		if (new Vector2(Velocity.X,Velocity.Z).Length() > 0.0){
			rotation_direction = new Vector2(Velocity.Z, Velocity.X).Angle();
			Vector3 rot = Rotation;
			rot.Y = (float)(Mathf.LerpAngle(Rotation.Y, rotation_direction, delta * 10));
			Rotation = rot;
		}

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
	}
	public void Animate(){
		if(Velocity.Length() > 0.0){
			anim.Play("walk");

		}else{
			anim.Play("idle");
		}
	}

}
