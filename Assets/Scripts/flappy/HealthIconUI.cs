using UnityEngine;
using UnityEngine.UI;

public class HealthIconUI : MonoBehaviour
{
    [SerializeField] private Image _hearthIcon;
    private Color _hearthColor;
    void Awake()
    {
        if (_hearthIcon == null)
        {
            _hearthIcon = GetComponent<Image>();
        }
        _hearthColor = _hearthIcon.color;
    }
    public void ActivateHearth()
    {
        _hearthIcon.color = _hearthColor;
    }

    public void DectivateHearth()
    {
        _hearthIcon.color = Color.clear;
    }

}
