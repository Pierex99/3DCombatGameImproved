using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class Gravity : MonoBehaviour
{
    public float gravity = -9.8f;

    private void Update()
    {
        GetComponent<CharacterController>().Move(
            Vector3.up * gravity * Time.deltaTime
        );
    }
}
