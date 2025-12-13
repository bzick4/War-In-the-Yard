using System.Collections;
using UnityEngine;

[DisallowMultipleComponent]
public class RegenStats : MonoBehaviour
{
    [Header("Regen rates (units per second)")]
    [SerializeField] private float _HealthRegenPerSec = 0f;
    [SerializeField] private float _MannaRegenPerSec = 5f;
    [SerializeField] private float _StaminaRegenPerSec = 8f;

    [Header("Delay after use/damage (seconds)")]
    [SerializeField] private float _HealthRegenDelay = 5f;
    [SerializeField] private float _MannaRegenDelay = 1f;
    [SerializeField] private float _StaminaRegenDelay = 1f;

    [Header("Tick settings")]
    [SerializeField] private float _TickInterval = 0.2f;

    private DataView _dataView => FindObjectOfType<DataView>();

    private float _lastDamageTime = -Mathf.Infinity;
    private float _lastMannaUseTime = -Mathf.Infinity;
    private float _lastStaminaUseTime = -Mathf.Infinity;

    private void Awake()
    {
        if (_dataView == null)
        {
            Debug.LogWarning("RegenStats: DataView not found in parents/scene. Disabling regen.");
            enabled = false;
            return;
        }

        _dataView.OnDamaged += OnDamaged;
        _dataView.OnUsedManna += OnUsedManna;
        _dataView.OnUsedStamina += OnUsedStamina;
    }

    private void OnDestroy()
    {
        if (_dataView == null) return;
        _dataView.OnDamaged -= OnDamaged;
        _dataView.OnUsedManna -= OnUsedManna;
        _dataView.OnUsedStamina -= OnUsedStamina;
    }

    private void OnDamaged() => _lastDamageTime = Time.time;
    private void OnUsedManna() => _lastMannaUseTime = Time.time;
    private void OnUsedStamina() => _lastStaminaUseTime = Time.time;

    private void OnEnable()
    {
        if (_dataView != null)
            StartCoroutine(RegenLoop());
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    private IEnumerator RegenLoop()
    {
        while (true)
        {
            float dt = _TickInterval;

            if (_HealthRegenPerSec > 0f && Time.time - _lastDamageTime >= _HealthRegenDelay)
            {
                float delta = _HealthRegenPerSec * dt;
                _dataView.CurrentHealth = Mathf.Min(_dataView.CurrentHealth + delta, GetMaxHealth());
            }

            if (_MannaRegenPerSec > 0f && Time.time - _lastMannaUseTime >= _MannaRegenDelay)
            {
                float delta = _MannaRegenPerSec * dt;
                _dataView.CurrentManna = Mathf.Min(_dataView.CurrentManna + delta, GetMaxManna());
            }

            if (_StaminaRegenPerSec > 0f && Time.time - _lastStaminaUseTime >= _StaminaRegenDelay)
            {
                float delta = _StaminaRegenPerSec * dt;
                _dataView.CurrentStamina = Mathf.Min(_dataView.CurrentStamina + delta, GetMaxStamina());
            }

            yield return new WaitForSeconds(dt);
        }
    }

    private float GetMaxHealth()
    {
        return (_dataView != null) ? _dataView.MaxHealth : 100f;
    }

    private float GetMaxManna()
    {
        return (_dataView != null) ? _dataView.MaxManna : 100f;
    }

    private float GetMaxStamina()
    {
        return (_dataView != null) ? _dataView.MaxStamina : 100f;
    }
}