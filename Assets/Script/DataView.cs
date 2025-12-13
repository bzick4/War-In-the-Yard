using UnityEngine;
using System;

public class DataView : MonoBehaviour
{
    [Header("Максимальные значения")]
    public float MaxHealth {get; private set;} = 100f;
    public float MaxManna {get; private set;} = 100f;
    public float MaxStamina {get; private set;} = 100f;

    private float _currentHealth;
    private float _currentManna;
    private float _currentStamina;

    private ViewModel _viewModel => GetComponentInParent<ViewModel>();
    
    public Action OnDamaged;
    public Action OnUsedManna;
    public Action OnUsedStamina;

    private void Awake()
    {
        _currentHealth = MaxHealth;
        _currentManna = MaxManna;
        _currentStamina = MaxStamina;

        if (_viewModel != null)
        {
            _viewModel.SetHealthTarget(SafeDivide(_currentHealth, MaxHealth), 0f);
            _viewModel.SetMannaTarget(SafeDivide(_currentManna, MaxManna), 0f);
            _viewModel.SetStaminaTarget(SafeDivide(_currentStamina, MaxStamina), 0f);
        }
    }

    private float SafeDivide(float value, float max)
    {
        if (max <= 0f) return 0f;
        return Mathf.Clamp01(value / max);
    }

    public float CurrentHealth
    {
        get => _currentHealth;
        set
        {
            value = Mathf.Clamp(value, 0f, MaxHealth);
            if (Mathf.Approximately(_currentHealth, value)) return;
            _currentHealth = value;
            if (_viewModel != null) _viewModel.SetHealthTarget(SafeDivide(_currentHealth, MaxHealth));
            if (_currentHealth <= 0f) Death();
        }
    }

    public float CurrentManna
    {
        get => _currentManna;
        set
        {
            value = Mathf.Clamp(value, 0f, MaxManna);
            if (Mathf.Approximately(_currentManna, value)) return;
            _currentManna = value;
            if (_viewModel != null) _viewModel.SetMannaTarget(SafeDivide(_currentManna, MaxManna));
            if (_currentManna <= 0f) ZeroManna();
        }
    }

    public float CurrentStamina
    {
        get => _currentStamina;
        set
        {
            value = Mathf.Clamp(value, 0f, MaxStamina);
            if (Mathf.Approximately(_currentStamina, value)) return;
            _currentStamina = value;
            if (_viewModel != null) _viewModel.SetStaminaTarget(SafeDivide(_currentStamina, MaxStamina));
            if (_currentStamina <= 0f) ZeroStamina();
        }
    }

    public void TakeDamage(float damage)
    {
        CurrentHealth -= damage;
        Debug.Log("Игрок получил урон: " + damage + " Текущее здоровье: " + _currentHealth);
        OnDamaged?.Invoke();
    }

    public void UseManna(float amount)
    {
        CurrentManna -= amount;
        Debug.Log("Использовано маны: " + amount + " Текущая мана: " + _currentManna);
        OnUsedManna?.Invoke();
    }

    public void UseStamina(float amount)
    {
        CurrentStamina -= amount;
        Debug.Log("Использовано выносливости: " + amount + " Текущая выносливость: " + _currentStamina);
        OnUsedStamina?.Invoke();
    }

    private void Death()
    {
        Debug.Log("Игрок умер.");
    }

    private void ZeroManna()
    {
        Debug.Log("Мана игрока равна нулю.");
    }
    private void ZeroStamina()
    {
        Debug.Log("Выносливость игрока равна нулю.");
    }
}
