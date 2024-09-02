using UnityEngine;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
    [SerializeField] private float _fadeTime=1.0f;
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private RectTransform _reactTransform;

    public void PanelFadeIn()
    {
        _canvasGroup.alpha = 0.0f;
        _reactTransform.transform.localPosition = new Vector3(0f, -1000f, 0f) ;
        _reactTransform.DOAnchorPos(new Vector2(0f, -1000f), _fadeTime, false).SetEase(Ease.OutElastic);

    }
}
