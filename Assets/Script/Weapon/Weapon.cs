using UnityEngine;


public abstract class Weapon : MonoBehaviour
{
    [SerializeField] protected float _Damage;
    [SerializeField] protected float _FireRate;
    protected float _nextFireTime;

    protected Animator _animator;
    
    public abstract void Shoot();
    public abstract void AnimShoot();



    protected virtual void Awake()
    {
        if (_animator == null)
            _animator = GetComponentInParent<Animator>(); 
    }

    
    protected void PlayTrigger(string triggerName)
    {
        if (_animator == null) { Awake(); if (_animator == null) return; }
        _animator.SetTrigger(triggerName);
    }
    
    protected bool CanShoot()
    {
        return Time.time >= _nextFireTime;
    }
    
    protected void UpdateFireTime()
    {
        _nextFireTime = Time.time + _FireRate;
    }
}