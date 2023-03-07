using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class Driver : MonoBehaviour
{
    Vector2 rawInput;
    [SerializeField] float steerSpeed = 10;
    [SerializeField] float turnRate = 20f;
    [SerializeField] float slowTurnRate = 15f;
    [SerializeField] float defaultSpeed = 14f;
    [SerializeField] float currentSpeed = 0f;
    [SerializeField] float timeToGetToFullSpeed = 5f;
    [SerializeField] float topMoveSpeed = 14f;
    [SerializeField] float acceleration = 1f;
    [SerializeField] float slowSpeed = 10f;
    [SerializeField] float boostSpeed = 18f;
    [SerializeField] float breakSpeed = 100f;


    [SerializeField] TextMeshProUGUI accelerateText;
    [SerializeField] TextMeshProUGUI decelerateText;
    [SerializeField] TextMeshProUGUI cruiseText;
    [SerializeField] TextMeshProUGUI otherText;
    

    Rigidbody2D rb;


    Coroutine accelerateCoroutine;
    Coroutine decelerateCoroutine;
    Coroutine cruiseCoroutine;


    // Start is called before the first frame update
    // These are callback methods.
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        // transform.Rotate(0, 0, 45);
    }

    // Update is called once per frame
    void Update()
    {
        RotateWheels();
        Accelerate();
    }

    public void OnMove(InputValue value) {
        rawInput = value.Get<Vector2>();
    }

    private void RotateWheels() {
        // If the car is not moving, rotate at a slower rate.
        if (currentSpeed > -Mathf.Epsilon && currentSpeed < Mathf.Epsilon) {
            if (rawInput.x > Mathf.Epsilon) {
                transform.Rotate(0, 0, slowTurnRate * -Time.deltaTime);
            } else if (rawInput.x < -Mathf.Epsilon) {
                transform.Rotate(0, 0, slowTurnRate * Time.deltaTime);
            }
        } else {
            if (rawInput.x > Mathf.Epsilon) {
                transform.Rotate(0, 0, turnRate * -Time.deltaTime);
            } else if (rawInput.x < -Mathf.Epsilon) {
                transform.Rotate(0, 0, turnRate * Time.deltaTime);
            }
        }
        
    }

    private void Accelerate() {
        if (rawInput.y > Mathf.Epsilon) {
            if (decelerateCoroutine != null) {
                StopCoroutine(decelerateCoroutine);
                decelerateCoroutine = null;
            }
            if (cruiseCoroutine != null) {
                StopCoroutine(cruiseCoroutine);
                cruiseCoroutine = null;
            }
            if (accelerateCoroutine == null) {
                accelerateCoroutine = StartCoroutine(IncrementSpeedSteadily());
            }
        } else if (rawInput.y < -Mathf.Epsilon) {
            if (accelerateCoroutine != null) {
                StopCoroutine(accelerateCoroutine);
                accelerateCoroutine = null;
            }
            if (cruiseCoroutine != null) {
                StopCoroutine(cruiseCoroutine);
                cruiseCoroutine = null;
            }
            if (decelerateCoroutine == null) {
                decelerateCoroutine = StartCoroutine(IncrementSpeedSteadily_Reverse());
            }
        } else {
            if (decelerateCoroutine != null) {
                StopCoroutine(decelerateCoroutine);
                decelerateCoroutine = null;
            }
            if (accelerateCoroutine != null) {
                StopCoroutine(accelerateCoroutine);
                accelerateCoroutine = null;
            }
            if (cruiseCoroutine == null) {
                cruiseCoroutine = StartCoroutine(CruiseDeceleration());
            }
        }

        accelerateText.text = $"Accelerate: {(accelerateCoroutine != null ? "Active" : "Not Active")}";
        decelerateText.text = $"Decelerate: {(decelerateCoroutine != null ? "Active" : "Not Active")}";
        cruiseText.text = $"Cruise: {(cruiseCoroutine != null ? "Active" : "Not Active")}";
        Move();
    }

    private IEnumerator IncrementSpeedSteadily() {
        while (true) {
            // If the player is moving backwards, use the breaks, otherwise, go forwards.
            if (currentSpeed < -Mathf.Epsilon) {
                currentSpeed = Mathf.Clamp(currentSpeed + breakSpeed, -topMoveSpeed, 0);
            } else {
                currentSpeed = Mathf.Clamp(currentSpeed + acceleration, -topMoveSpeed, topMoveSpeed);
            }
            yield return new WaitForSeconds(0.1f);

        }
    }

    private IEnumerator IncrementSpeedSteadily_Reverse() {
        while (true) {
            // If the player is moving forward, use the breaks, otherwise, go in reverse.
            if (currentSpeed > Mathf.Epsilon) {
                currentSpeed = Mathf.Clamp(currentSpeed - breakSpeed, 0, topMoveSpeed);
            } else {
                currentSpeed = Mathf.Clamp(currentSpeed - acceleration, -topMoveSpeed, topMoveSpeed);
            }
            yield return new WaitForSeconds(0.1f);
        }
    }

    private IEnumerator CruiseDeceleration() {
        while (currentSpeed > Mathf.Epsilon || currentSpeed < -Mathf.Epsilon) {
            currentSpeed = Mathf.Clamp(
                Mathf.Sign(currentSpeed) > 0 ? currentSpeed - acceleration : currentSpeed + acceleration,
                -topMoveSpeed, 
                topMoveSpeed); // Update this later
            yield return new WaitForSeconds(0.1f);
        }
    }

    private void StopAccelerationCoroutines() {
        if (decelerateCoroutine != null) {
            StopCoroutine(decelerateCoroutine);
            decelerateCoroutine = null;
        }
        if (accelerateCoroutine != null) {
            StopCoroutine(accelerateCoroutine);
            accelerateCoroutine = null;
        }
        if (cruiseCoroutine != null) {
            StopCoroutine(cruiseCoroutine);
            cruiseCoroutine = null;
        }
    }

    private void Move() {
        if (currentSpeed > -(acceleration - 0.01f) && currentSpeed < (acceleration - 0.01f)) {
            currentSpeed = 0f;
        }
        transform.Translate(0, currentSpeed * Time.deltaTime, 0);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Boost") {
            topMoveSpeed = boostSpeed;
        }

        if (other.tag == "Slow Zone") {
            topMoveSpeed = slowSpeed;
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        // Figure This out since we are not using Physics to movement.
    }

    public void SetTopSpeed(float newSpeed) {
        topMoveSpeed = newSpeed;
    }

    public float GetTopSpeed() {
        return topMoveSpeed;
    }
}
