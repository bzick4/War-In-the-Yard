using System.Collections;
using System;
using UnityEngine;
using UnityWeld.Binding;
using System.ComponentModel;

[Binding]
public class ViewModel : MonoBehaviour, INotifyPropertyChanged
{

    private float _smoothDuration = 0.25f;
    [SerializeField] private AnimationCurve _Ease = AnimationCurve.EaseInOut(0, 0, 1, 1);
    private float _healthPercent;
    private float _mannaPercent;
    private float _staminaPercent;

    private Coroutine _healthAnim;
    private Coroutine _mannaAnim;
    private Coroutine _staminaAnim;

    public event PropertyChangedEventHandler PropertyChanged;

    [Binding]
    public float HealthPercent
    {
        get => _healthPercent;
        set
        {
            if (Mathf.Approximately(_healthPercent, value)) return;
            _healthPercent = Mathf.Clamp01(value);
            OnPropertyChange(nameof(HealthPercent));
        }
    }

    [Binding]
    public float MannaPercent
    {
        get => _mannaPercent;
        set
        {
            if (Mathf.Approximately(_mannaPercent, value)) return;
            _mannaPercent = Mathf.Clamp01(value);
            OnPropertyChange(nameof(MannaPercent));
        }
    }

    [Binding]
    public float StaminaPercent
    {
        get => _staminaPercent;
        set
        {
            if (Mathf.Approximately(_staminaPercent, value)) return;
            _staminaPercent = Mathf.Clamp01(value);
            OnPropertyChange(nameof(StaminaPercent));
        }
    }

    private void OnPropertyChange(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    public void SetHealthTarget(float target, float? duration = null)
    {
        if (_healthAnim != null) StopCoroutine(_healthAnim);
        _healthAnim = StartCoroutine(AnimatePercent(v => HealthPercent = v, _healthPercent, Mathf.Clamp01(target), duration ?? _smoothDuration));
    }

    public void SetMannaTarget(float target, float? duration = null)
    {
        if (_mannaAnim != null) StopCoroutine(_mannaAnim);
        _mannaAnim = StartCoroutine(AnimatePercent(v => MannaPercent = v, _mannaPercent, Mathf.Clamp01(target), duration ?? _smoothDuration));
    }

    public void SetStaminaTarget(float target, float? duration = null)
    {
        if (_staminaAnim != null) StopCoroutine(_staminaAnim);
        _staminaAnim = StartCoroutine(AnimatePercent(v => StaminaPercent = v, _staminaPercent, Mathf.Clamp01(target), duration ?? _smoothDuration));
    }

    private IEnumerator AnimatePercent(Action<float> setter, float start, float target, float duration)
    {
        float time = 0f;
        if (Mathf.Approximately(start, target))
        {
            setter(target);
            yield break;
        }

        while (time < duration)
        {
            time += Time.deltaTime;
            float t = Mathf.Clamp01(time / duration);
            float eased = _Ease.Evaluate(t);
            setter(Mathf.Lerp(start, target, eased));
            yield return null;
        }

        setter(target);
    }
}
