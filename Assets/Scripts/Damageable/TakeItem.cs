using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class TakeItem : MonoBehaviour
{
    protected TextMeshPro _Text;
    private void Start()
    {
        // Freeze rotation
        GetComponent<Rigidbody>().freezeRotation = true;

        // Spawn text as child to item
        _Text = Instantiate(Resources.Load<GameObject>("Text/Text prefab"), transform.position + Vector3.up, Quaternion.identity, transform).GetComponent<TextMeshPro>();

        // Chnage Text and add color
        _Text.text = "TO TAKE \n <color=#7CFC00> PRESS E </color>";
        // Change font size 
        {
            _Text.fontSizeMin = 1;
            _Text.fontSizeMax = 3;
        }

    }
    public void ShowTakeItemButton(bool active) => _Text?.gameObject.SetActive(active);
}
