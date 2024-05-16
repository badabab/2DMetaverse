using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UI_MapItem : MonoBehaviour
{
    private Address _address;
    public Text NameTextUI;

    public void Init(Address address)
    {
        _address = address;

        NameTextUI.text = address.jibunAddress;
    }

    public void OnClickFindButton()
    {
        UI_Map.Instance.Find(_address);
    }
}