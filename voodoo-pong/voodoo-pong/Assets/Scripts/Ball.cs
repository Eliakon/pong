using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class Ball : MonoBehaviour
{
    private const float speed = 4f;
    private const float velocityMultiplier = 0.05f;
    private const int winScore = 5;

    [SerializeField]
    private Rigidbody2D rigibody;

    [SerializeField]
    private RectTransform rectTransform;

    [SerializeField]
    private GameObject leftPlayer;

    [SerializeField]
    private GameObject rightPlayer;

    [SerializeField]
    private Text leftPlayerScore;

    [SerializeField]
    private Text rightPlayerScore;

    [SerializeField]
    private UnityEvent startGame;

    [SerializeField]
    private UnityEvent endGame;

    [SerializeField]
    private UnityEvent showTarget;

    private Vector2 currentVelocity;
    private int leftScore;
    private int rightScore;
    private GameObject lastPlayer;

    private void SetVelocity(float x, float y)
    {
        currentVelocity = new Vector2(x, y);
        rigibody.velocity = currentVelocity;
    }

    private void ChangeScore(int addToLeftScore, int addToRightScore)
    {
        leftScore += addToLeftScore;
        rightScore += addToRightScore;
        leftPlayerScore.text = leftScore.ToString();
        rightPlayerScore.text = rightScore.ToString();
    }

    public void StartGame()
    {
        lastPlayer = leftPlayer;
        SetVelocity(speed, 0);
        leftScore = 0;
        rightScore = 0;
        startGame.Invoke();
        showTarget.Invoke();
    }

    private void StartGameAfterDelay()
    {
        StartCoroutine(StartGameAfterDelayCoroutine());
    }

    private IEnumerator StartGameAfterDelayCoroutine()
    {
        yield return new WaitForSeconds(1f);
        var velocity = speed;
        if (lastPlayer == rightPlayer) {
            velocity *= -1;
        }
        SetVelocity(velocity, 0);
        startGame.Invoke();
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
            lastPlayer = collision.gameObject;
        }

        if (collision.gameObject.CompareTag("Wall"))
        {
            SetVelocity(currentVelocity.x, currentVelocity.y * -1);
        }

        if (collision.gameObject.CompareTag("Target"))
        {
            ChangeScore(lastPlayer == leftPlayer ? 1 : 0, lastPlayer == rightPlayer ? 1 : 0);
            showTarget.Invoke();
            SetVelocity(currentVelocity.x, currentVelocity.y);
        }

        if (collision.gameObject.CompareTag("Lose"))
        {
            endGame.Invoke();
            SetVelocity(0, 0);
            ChangeScore(lastPlayer == leftPlayer ? 1 : 0, lastPlayer == rightPlayer ? 1 : 0);
            rectTransform.localPosition = Vector2.zero;

            if (leftScore >= winScore)
            {
                Debug.Log("Left player wins!");
                return;
            }
            if (rightScore >= winScore)
            {
                Debug.Log("Right player wins!");
                return;
            }
            StartGameAfterDelay();
        }
    }
}
