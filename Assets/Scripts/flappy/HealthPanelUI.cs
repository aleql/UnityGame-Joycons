using UnityEngine;

public class HealthPanelUI : MonoBehaviour
{
    [SerializeField] private HealthIconUI[] _healthIcons;
    private int _healthIndex;

    private void Start()
    {
        _healthIndex = _healthIcons.Length;
    }

    public void SetHealth(int health)
    {
        for (int i = 0; i < health; i++)
        {
            _healthIcons[i].ActivateHearth();
        }
        for (int j = health; j<_healthIcons.Length; j++)
        {
            _healthIcons[j].DectivateHearth();
        }
    }
    public void ReceiveDamage()
    {
        _healthIcons[_healthIndex].DectivateHearth();
        _healthIndex -= 1;
    }

    public void ReceiveHealth()
    {
        if (_healthIndex != _healthIcons.Length)
        {
            _healthIcons[_healthIndex].ActivateHearth();
            _healthIndex += 1;
        }
    }
}
