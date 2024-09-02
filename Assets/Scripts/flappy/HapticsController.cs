using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.VirtualTexturing;

public class HapticsController : MonoBehaviour
{
    public static HapticsController Instance;
    [SerializeField] private CanvasGroup _DMGUIcanvasGroup;

    private bool _DMGUIisFaded;

    [SerializeField] private ParticleSystem _appleEffect;
    [SerializeField] private ParticleSystem _hitEffect;

    private void Awake()
    {
        Instance = this;
        _DMGUIisFaded = false;
        //OnDamageReceived();

        _appleEffect.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        _appleEffect.gameObject.SetActive(false);

        _hitEffect.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        _hitEffect.gameObject.SetActive(false);
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
        BirdPlayerController.Instance.animator.SetTrigger("Hit");

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

    public void PickUpEffect(int score)
    {
        Vector3 position = BirdPlayerController.Instance.transform.position;
        position += new Vector3(0f, 3f, 0f);

        ScoreManager.Instance.Score(score.ToString(), position);
        StartCoroutine(AppleEffect(position));
    }

    public IEnumerator AppleEffect(Vector3 position)
    {
        _appleEffect.gameObject.transform.position = position;
        _appleEffect.gameObject.SetActive(true);
        _appleEffect.Play(true);
        yield return new WaitForSeconds(2);
        _appleEffect.gameObject.SetActive(false);
        _appleEffect.Play(false);
    }

    public IEnumerator HitEffect()
    {
        Vector3 position = BirdPlayerController.Instance.transform.position;
        position += new Vector3(0f, 3f, 0f);

        _hitEffect.gameObject.transform.position = position;
        _hitEffect.gameObject.SetActive(true);
        _hitEffect.Play(true);
        yield return new WaitForSeconds(2);
        _hitEffect.gameObject.SetActive(false);
        _hitEffect.Play(false);
    }
}
