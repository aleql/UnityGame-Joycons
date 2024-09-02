using DG.Tweening;
using System.Collections;
using UnityEngine;

public class HapticsControllerRunner : MonoBehaviour
{
    public static HapticsControllerRunner Instance;
    [SerializeField] private CanvasGroup _DMGUIcanvasGroup;

    private bool _DMGUIisFaded;

    [SerializeField] private ParticleSystem _hitEffect;
    [SerializeField] private ParticleSystem _destroyEffect;


    private void Awake()
    {
        Instance = this;
        _DMGUIisFaded = false;

        _hitEffect.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        _hitEffect.gameObject.SetActive(false);

        _destroyEffect.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        _destroyEffect.gameObject.SetActive(false);
    }

    private void VibrateJoycons()
    {
        foreach (Joycon joycon in BirdPlayerController.Instance.joycons)
        {
            joycon.SetRumble(160, 320, 0, time:5);
        }
        
    }

    public void OnDamageReceived()
    {
        // SFX

        // Vibration
        //VibrateJoycons();

        // Camera Shake
        CameraShake.Shake(1f, 0.5f);

        // Red UI
        StartCoroutine(DMGUIFadeInAndOut());

        // Bird Animation

        // Hit Effect
        StartCoroutine(HitEffect());
    }

    private IEnumerator DMGUIFadeInAndOut()
    {
        FadeDMGUI();
        yield return new WaitForSeconds(1);
        FadeDMGUI();
    }

    private void FadeDMGUI()
    {
        _DMGUIisFaded = !_DMGUIisFaded;
        if (_DMGUIisFaded )
        {
            _DMGUIcanvasGroup.DOFade(1, 1);
        }
        else
        {
            _DMGUIcanvasGroup.DOFade(0, 1);
        }
    }

    public IEnumerator HitEffect()
    {
        Vector3 position = PlayerController.Instance.transform.position;
        // position += new Vector3(0f, 3f, 0f);

        _hitEffect.gameObject.transform.position = position;
        _hitEffect.gameObject.SetActive(true);
        _hitEffect.Play(true);
        yield return new WaitForSeconds(2);
        _hitEffect.gameObject.SetActive(false);
        _hitEffect.Play(false);
    }

    public IEnumerator DestroyEffect(Vector3 position)
    {
        // Vector3 position = PlayerController.Instance.transform.position;
        // position += new Vector3(0f, 3f, 0f);

        _destroyEffect.gameObject.transform.position = position;
        _destroyEffect.gameObject.SetActive(true);
        _destroyEffect.Play(true);
        yield return new WaitForSeconds(2);
        _destroyEffect.gameObject.SetActive(false);
        _destroyEffect.Play(false);
    }

}
