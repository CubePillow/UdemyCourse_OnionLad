using UnityEngine;

public class Player : MonoBehaviour
{
    public Rigidbody2D rb;
    public float moveSpeed;
    public float jumpForce;
    

    private float _xInput;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _xInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(_xInput * moveSpeed,rb.velocity.y);
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }
}
