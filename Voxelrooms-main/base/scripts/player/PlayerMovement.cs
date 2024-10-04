using Godot;
using System;

public partial class PlayerMovement : CharacterBody3D
{
	public const float Speed = 5.0f;
	public const float JumpVelocity = 4.5f;
	public float Sensitivity = 0.005f;
	private Camera3D camera;
	
	public override void _Ready()
	{
		Input.MouseMode = Input.MouseModeEnum.Captured;
		camera = GetNode<Camera3D>("Camera3D");
	}

	public override void _UnhandledInput(InputEvent @e)
	{
		switch(@e)
		{
			case InputEventMouseMotion:
				InputEventMouseMotion mMotion = e as InputEventMouseMotion;
				RotateY(-mMotion.Relative.X * Sensitivity);
				camera.RotateX(-mMotion.Relative.Y * Sensitivity);
				
				Vector3 camRot = camera.Rotation;
				camRot.X = Mathf.Clamp(camRot.X, Mathf.DegToRad(-90f), Mathf.DegToRad(90f));
				camera.Rotation = camRot;
				break;
		}
	}

	public override void _PhysicsProcess(double delta)
	{
		Vector3 velocity = Velocity;

		// Add the gravity.
		if (!IsOnFloor())
		{
			velocity += GetGravity() * (float)delta;
		}

		// Handle Jump.
		if (Input.IsActionJustPressed("ui_accept") && IsOnFloor())
		{
			velocity.Y = JumpVelocity;
		}

		// Get the input direction and handle the movement/deceleration.
		// As good practice, you should replace UI actions with custom gameplay actions.
		Vector2 inputDir = Input.GetVector("pLeft", "pRight", "pForward", "pBack");
		Vector3 direction = (Transform.Basis * new Vector3(inputDir.X, 0, inputDir.Y)).Normalized();
		if (direction != Vector3.Zero)
		{
			velocity.X = direction.X * Speed;
			velocity.Z = direction.Z * Speed;
		}
		else
		{
			velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
			velocity.Z = Mathf.MoveToward(Velocity.Z, 0, Speed);
		}

		Velocity = velocity;
		MoveAndSlide();
	}
}
