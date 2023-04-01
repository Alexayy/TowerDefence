using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttachOnPlayStart : MonoBehaviour
{
    [SerializeField] private AudioClip _clip;

    private void Start()
    {
        SFXManager.Instance.PlaySound(_clip);
    }
}
