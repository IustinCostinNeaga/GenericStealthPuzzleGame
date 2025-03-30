using UnityEngine;

public class Player : MonoBehaviour{
    
    [SerializeField] private Vector2 currentPosition = new(0, 0);
    [SerializeField] private Vector2 newPosition = new(0, 0);
    [SerializeField] private float movementSpeed = 20f;

    void Start() {
        currentPosition = transform.position;
        newPosition = transform.position;
    }
    
    void Update()
    {
        if (transform.position.x % 1 == 0 && transform.position.y % 1 == 0) {
            currentPosition = transform.position;
            var input = getInputDirection();
            if (!detectComponent<Rigidbody2D>(currentPosition + input)) {
                newPosition = currentPosition + input;
            }
        }


        transform.position = Vector2.Lerp(transform.position, newPosition, Time.deltaTime * movementSpeed);
        if (Vector2.Distance(transform.position, newPosition) < 0.01f)
        {
            transform.position = newPosition; 
        }
    }



    Vector2 getInputDirection() {
        return new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }
    
    bool detectComponent<T>(Vector2 position){
        var colliders = Physics2D.OverlapCircleAll(position, 0.49f);

        foreach (var collider in colliders) {
            if (collider.GetComponent<T>() != null) {
                return true;
            }
        }
        
        return false;
    }
}
