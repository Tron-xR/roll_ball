using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public float speed=0;
    public TextMeshProUGUI CountText;
    public GameObject WinText;
    private Rigidbody rb;
    private int count;
    private float movementX;
    private float movementY;
    //calls rigidbody component
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;
        SetCountText();
        WinText.SetActive(false);
    }
    //add force to control the gameobject
    private void FixedUpdate()
    {
        Vector3 movement= new Vector3(movementX,0.0f,movementY);
        rb.AddForce(movement*speed);
    }
    //it takes input from user to move gameobject
    void OnMove(InputValue movementValue)
    {
        Vector2 momentVector = movementValue.Get<Vector2>();
        movementX = momentVector.x;
        movementY = momentVector.y;
    }
    //PickUp object will disappear when collieded
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp")) {
            other.gameObject.SetActive(false);
            count++;
            SetCountText();
        }
        
    }
    void SetCountText()
    {
        //for counting points
        CountText.text = count.ToString();
        //Display you win text
        if (count >= 9)
        {
            WinText.SetActive(true);
            Destroy(gameObject);
            SceneManager.LoadScene(2);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(gameObject);
            WinText.gameObject.SetActive(true);
            WinText.GetComponent<TextMeshProUGUI>().text = "You lose!";
            SceneManager.LoadScene(2);
        }
    }
}
