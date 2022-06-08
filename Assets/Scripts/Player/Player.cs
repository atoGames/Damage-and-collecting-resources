using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Player : MonoBehaviour
{
    protected Transform m_Hand;
    public GameObject m_AxePrefab;

    protected TakeItem m_TakeItem;

    private void Start()
    {
        m_Hand ??= GetComponentsInChildren<Transform>().ToList().Find(hand => hand.name == "Hand");
        CreateWeapon(m_AxePrefab);
    }
    private void Update()
    {
        var _Colliders = Physics.OverlapSphere(transform.position, 5);

        foreach (var col in _Colliders)
        {
            var _IsItemClose = Vector3.Distance(transform.position, col.transform.position) < 3;

            if (col.TryGetComponent(out m_TakeItem) && _IsItemClose)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    // Add item to inventory
                    Destroy(m_TakeItem.gameObject);
                }
            }
            m_TakeItem?.ShowTakeItemButton(_IsItemClose);

        }
    }
    protected void CreateWeapon(GameObject wp)
    {
        Instantiate(wp, m_Hand);
    }
}
