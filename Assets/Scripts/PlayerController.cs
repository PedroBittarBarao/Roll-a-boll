using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

[RequireComponent(typeof(AudioSource))]
public class PlayerController : MonoBehaviour
{

    private Rigidbody rb;
    private float movementX;
    private float movementY;
    private int count;


    public float speed = 0;
    public TextMeshProUGUI countText;
    public GameObject winTextObject;
    public AudioClip winSound;
    public AudioClip coinSound;
    AudioSource audioSource;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;
        SetCountText();
        winTextObject.SetActive(false);
        audioSource = GetComponent<AudioSource>();
    }

    void OnReload()
    { 
        UnityEngine.SceneManagement.SceneManager.LoadScene("Minigame");
    }

    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();
        movementX = movementVector.x;
        movementY = movementVector.y;
    }
    private void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);
        rb.AddForce(movement * speed);
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            count += 1;
            SetCountText();
            audioSource.PlayOneShot(coinSound, 0.7F);
        }
        if (count >= 8)
        {
            winTextObject.SetActive(true);
            audioSource.PlayOneShot(winSound, 0.7F);
        }
    }

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
    }
}
