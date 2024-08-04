using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D _rb;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;
    

    private float _xInput;
    
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        _xInput = Input.GetAxis("Horizontal"); 
        _rb.velocity = new Vector2(_xInput * moveSpeed,_rb.velocity.y);
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _rb.velocity = new Vector2(_rb.velocity.x, jumpForce);
        }
    }
}
