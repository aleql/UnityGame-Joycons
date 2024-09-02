
using System.Collections;
using UnityEngine;
using UnityEngine.U2D;
using Vector3 = UnityEngine.Vector3;

public class GrassSplineController : MonoBehaviour
{

    [SerializeField] private SpriteShapeController _spriteShapeController;
    [SerializeField] private float _extensionStep;

    [SerializeField] private float _moveSpeed;
    [SerializeField] private bool _scrollLeft;

    private UnityEngine.U2D.Spline _spline;
    [SerializeField] private float _distanceCovered;

    [SerializeField] private bool _scrollVertical;
    [SerializeField] private bool _ownSpeed;


    // Start is called before the first frame update
    void Start()
    {
        //_moveSpeed = RunnerManager.speed;
        if (_scrollLeft)
        {
            _moveSpeed = -_moveSpeed;
        }

        if (_spriteShapeController == null)
        {
            _spriteShapeController = GetComponent<SpriteShapeController>();
        }
        _spline = _spriteShapeController.spline;
        StartCoroutine(ExtendSpline());


        _distanceCovered = 0;
    }

    private IEnumerator ExtendSpline()
    {
        yield return new WaitUntil(() => RunnerController.Instance.currentGameState == RunnerController.RunnerGameStates.Playing);

        while (RunnerController.Instance.currentGameState == RunnerController.RunnerGameStates.Playing)
        {
            int pointCount = _spline.GetPointCount();
            Vector3 lastPointPosition = _spline.GetPosition(pointCount - 1);
            Vector3 tangent = Vector3.zero;
            Vector3 newPointPosition = Vector3.zero;

            /*
            if (_scrollVertical)
            {
                newPointPosition = new Vector3(lastPointPosition.x, lastPointPosition.y + _extensionStep, lastPointPosition.z);
                tangent = new Vector3(0, _extensionStep / 2, 0);
            }
            else
            {
                newPointPosition = new Vector3(lastPointPosition.x + _extensionStep, lastPointPosition.y, lastPointPosition.z);
                tangent = new Vector3(_extensionStep / 2, 0, 0);
            }
            */

            newPointPosition = new Vector3(lastPointPosition.x + _extensionStep, lastPointPosition.y, lastPointPosition.z);
            tangent = new Vector3(_extensionStep / 2, 0, 0);

            // Add a new point to the spline
            _spline.InsertPointAt(pointCount, newPointPosition);
            _spline.SetTangentMode(pointCount, ShapeTangentMode.Linear);

            _spline.SetLeftTangent(pointCount, -tangent);
            _spline.SetRightTangent(pointCount, tangent);

            yield return new WaitUntil(() => Mathf.Abs(_distanceCovered) > 180.0f);
            _distanceCovered = 0;
            _spline.RemovePointAt(0);
        }
    }

    private void Update()
    {
        if (RunnerController.Instance.currentGameState == RunnerController.RunnerGameStates.Playing)
        {
            if (_scrollVertical)
            {
                transform.position += new Vector3(0f, _moveSpeed * Time.deltaTime, 0f);
            }
            else
            {
                transform.position += new Vector3(_moveSpeed * Time.deltaTime, 0f, 0f);
            }
            _distanceCovered += _moveSpeed * Time.deltaTime;

            if (!_ownSpeed)
            {
                _moveSpeed = RunnerManager.speed;
            }
            if (_scrollLeft)
            {
                _moveSpeed = -_moveSpeed;
            }
        }
    }



}
