using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private const float speed = 4f;
    private const float velocityMultiplier = 0.05f;

    [SerializeField]
    private Rigidbody2D rigibody;

    [SerializeField]
    private RectTransform rectTransform;

    private bool isMoving = false;
    private Vector2 currentVelocity;

    private void SetVelocity(float x, float y)
    {
        currentVelocity = new Vector2(x, y);
        rigibody.velocity = currentVelocity;
    }

    public void StartGame()
    {
        SetVelocity(speed, 0);
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Paddle"))
        {
            var ballPosition = rectTransform.localPosition.y;
            var paddlePosition = (collision.transform as RectTransform).localPosition.y;
            var diff = ballPosition - paddlePosition;
            var velocityY = velocityMultiplier * diff;
            var velocityX = Mathf.Abs(speed - velocityY);

            if (currentVelocity.x >= 0) {
                velocityX *= -1;
            }

            SetVelocity(velocityX, velocityY);
        }

        if (collision.gameObject.CompareTag("Wall"))
        {
            SetVelocity(currentVelocity.x, currentVelocity.y * -1);
        }
    }
}
