using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : MonoBehaviour, IWeapons
{
    protected PlayerAnimation m_PlayerAnimation;

    public float m_FireRate = 1;
    public bool m_IsReload = true;
    protected bool m_IsHitObj = false;
    protected System.Action onHitIbj = delegate { };
    private void Start()
    {
        m_PlayerAnimation ??= FindObjectOfType<PlayerAnimation>();
    }
    private void Update()
    {
        var _Shoot = Input.GetMouseButtonDown(0) && m_IsReload;
        Shoot(_Shoot);

        m_PlayerAnimation.PlayHitAnimation(_Shoot);
    }

    public void Shoot(bool shoot)
    {
        if (shoot) StartCoroutine(Reload(m_FireRate));
    }
    public IEnumerator Reload(float fireRate)
    {
        m_IsReload = false;
        yield return new WaitForSeconds(fireRate);
        m_IsReload = true;
        m_IsHitObj = false;

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Damageable damageable) && !m_IsReload && m_PlayerAnimation.IsHit)
        {
            if (!m_IsHitObj)
            {
                m_IsHitObj = true;
                damageable.Damage(Random.Range(10, 90));
            }
        }

    }
}
