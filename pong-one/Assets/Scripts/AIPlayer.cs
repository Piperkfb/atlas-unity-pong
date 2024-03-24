using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPlayer : Paddle
{
	private float reAdjustSpeed = 1f;
	public static bool isTwoPlayer;
	public Rigidbody2D rb2d;
    public float delay = 0.01f;
	public float speed;
    private void FixedUpdate()
    {
        speed = 3f;

		//Is the ball going left or right
		if (rb2d.velocity.x > 0) 
        {

			if (rb2d.velocity.y > 0) {
				if (rb2d.position.y > _Ridgy.position.y) 
                {
					MoveUp();
				}
				
				if (rb2d.position.y < _Ridgy.position.y) 
                {
					MoveDown();
				}
			} 

			if (rb2d.velocity.y < 0) 
            {
				if (rb2d.position.y > _Ridgy.position.y) 
                {
					MoveUp ();
				}
				if (rb2d.position.y < _Ridgy.position.y) 
                {
					MoveDown ();
				}
			}

		}

			//Whilst it's not moving at the paddle, let it gain a slight reset by moving with the ball at a slower pace. 
			if (rb2d.velocity.x < 0) {
				if (_Ridgy.position.y < 0) {
					_Ridgy.position += Vector2.up * reAdjustSpeed * delay * 3;
				}

				if (_Ridgy.position.y > 0) {
					_Ridgy.position += Vector2.down * reAdjustSpeed * delay * 3;
				}
			}
		}
	void MoveDown() {
		if (Mathf.Abs(rb2d.velocity.y) > speed) {
			_Ridgy.position += Vector2.down * speed * delay * 3;
		} else {
			_Ridgy.position += Vector2.down * speed * delay * 3;
		}
	}

	void MoveUp() {
		if (Mathf.Abs (rb2d.velocity.y) > speed) {
			_Ridgy.position += Vector2.up * speed * delay * 3;
		} else {
			_Ridgy.position += Vector2.up * speed * delay * 3;
		}
	}

}
