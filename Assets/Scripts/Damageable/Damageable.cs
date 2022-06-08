using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Damageable : MonoBehaviour
{
    public bool m_CanSpawnItems = true;
    public int m_Health = 100;
    public int m_ItemsAmount = 5;

    public GameObject m_ItemPrefab;
    protected GameObject m_TextPrefab;

    private void Start()
    {
        // Add random value
        m_ItemsAmount = Random.Range(3, 5);
        // Get ref to text prefab
        m_TextPrefab = Resources.Load<GameObject>("Text/Text prefab");
    }
    public void Damage(int damage)
    {
        m_Health -= damage;

        // If health > zero , create text to show the damage
        if (m_Health > 0)
            CreateText(damage);
        else // Else we spawn items
            StartCoroutine(SpwanItem(0.03f));
    }

    protected void CreateText(int damage)
    {
        // If this null return
        if (!m_TextPrefab)
            return;

        var _Text = Instantiate(m_TextPrefab, transform.position + Vector3.up, Quaternion.identity);
        _Text.GetComponent<TextMeshPro>().text = "-" + damage;
        LeanTween.moveLocalY(_Text, Random.Range(3, 6), 0.5f).setOnComplete(() => Destroy(_Text)).setEaseOutCirc();
    }
    protected IEnumerator SpwanItem(float delay)
    {
        var _Dir = Vector3.zero;
        var _Loop = 0;
        while (_Loop < m_ItemsAmount && m_CanSpawnItems)
        {
            // Add to loop
            _Loop++;
            // Change dir 
            {
                _Dir.x = Mathf.Sin(Mathf.Rad2Deg * 5 + _Loop);
                _Dir.y = _Dir.y + _Loop / 2;
                _Dir.z = Mathf.Cos(Mathf.Rad2Deg * 5 + _Loop);
            }
            // Spawn item
            Instantiate(m_ItemPrefab, transform.position + _Dir, Quaternion.identity);
            // return new delay
            yield return new WaitForSeconds(delay);
        }
        // Destroy this gameobject
        Destroy(gameObject);
    }

}
