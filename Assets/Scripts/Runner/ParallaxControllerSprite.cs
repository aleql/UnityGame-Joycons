using UnityEngine;

public class ParallaxControllerSprite : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private bool _scrollLeft;
    [SerializeField] private bool _scrollVertical;
    private float _singleTextureWidth;


    void Start()
    {
        Sprite sprite = GetComponent<SpriteRenderer>().sprite;
        _singleTextureWidth = sprite.texture.width / sprite.pixelsPerUnit;
        if (_scrollLeft )
        {
            _moveSpeed = -_moveSpeed;
        }

    }
    void Update()
    {
        if (RunnerController.Instance.currentGameState == RunnerController.RunnerGameStates.Playing)
        {
            if (_scrollVertical)
            {
                transform.position += new Vector3(0f, _moveSpeed * Time.deltaTime, 0f);
                if ((Mathf.Abs(transform.position.y) - _singleTextureWidth) > 0)
                {
                    transform.position = new Vector3(transform.position.x, 0f, transform.position.z);
                }
            }
            else
            {
                transform.position += new Vector3(_moveSpeed * Time.deltaTime, 0f, 0f);
                if ((Mathf.Abs(transform.position.x) - _singleTextureWidth) > 0)
                {
                    transform.position = new Vector3(0.0f, transform.position.y, transform.position.z);
                }
            }
        }
    }
}
