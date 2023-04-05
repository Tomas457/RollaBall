using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public float speed = 0;
    private float movementX;
    private float movementY;


    public float Jump = 0;
    public bool isGrounded;

    public TextMeshProUGUI countText16;
    public TextMeshProUGUI countText18;
    private int count;

    public GameObject winTextObject;
    public GameObject resetButton;
    public GameObject nextLevelbutton;

    public GameObject Gate;

    private Rigidbody rb;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;

        SetCountText();
        SetCountTextLevel2();
        winTextObject.SetActive(false);
        resetButton.gameObject.SetActive(false);
        nextLevelbutton.gameObject.SetActive(false);
        isGrounded = true;
    }

    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();

        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    void SetCountText()
    { 
        countText16.text = "Count: " + count.ToString();
        if (count >= 16)
        {
            winTextObject.SetActive(true);
            resetButton.gameObject.SetActive(true);
            nextLevelbutton.gameObject.SetActive(true);
        }
    }

    void SetCountTextLevel2()
    {
        countText18.text = "Count: " + count.ToString();
        if (count >= 18)
        {
            winTextObject.SetActive(true);
            resetButton.gameObject.SetActive(true);
            nextLevelbutton.gameObject.SetActive(true);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

    void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);

        rb.AddForce(movement * speed);

        if (isGrounded == true && Input.GetKey(KeyCode.Space))
        {
            rb.AddForce(new Vector3(0.0f, Jump, 0.0f));
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            count = count + 1;

            SetCountText();
            SetCountTextLevel2();
            OpenGate();
        }
    }

    void OpenGate()
    {
        if (count == 3)
        {
            Gate.SetActive(false);
        }
    }
}