using UnityEngine;

public class ParallaxControllerSpriteShape : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private bool scrollLeft;
    [SerializeField] private float singleShapeWidth; // Manually define the width for the wrap-around logic
    [SerializeField] private bool _scrollVertical;

    private Vector3 initialPosition;

    void Start()
    {
        initialPosition = transform.position;
        if (scrollLeft)
        {
            moveSpeed = -moveSpeed;
        }
    }

    void Update()
    {
        if (_scrollVertical)
        {
            // Move the sprite shape
            transform.position += new Vector3(0f, moveSpeed * Time.deltaTime, 0f);

            // Wrap-around logic to simulate infinite scrolling
            if (scrollLeft && transform.position.y < initialPosition.y - singleShapeWidth)
            {
                transform.position = new Vector3(transform.position.x, initialPosition.y, transform.position.z);
            }
            else if (!scrollLeft && transform.position.y > initialPosition.y + singleShapeWidth)
            {
                transform.position = new Vector3(transform.position.x, initialPosition.y, transform.position.z);
            }
        }
        else
        {
            // Move the sprite shape
            transform.position += new Vector3(moveSpeed * Time.deltaTime, 0f, 0f);

            // Wrap-around logic to simulate infinite scrolling
            if (scrollLeft && transform.position.x < initialPosition.x - singleShapeWidth)
            {
                transform.position = new Vector3(initialPosition.x, transform.position.y, transform.position.z);
            }
            else if (!scrollLeft && transform.position.x > initialPosition.x + singleShapeWidth)
            {
                transform.position = new Vector3(initialPosition.x, transform.position.y, transform.position.z);
            }
        }
        
    }
}
