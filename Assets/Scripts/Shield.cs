using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Shield : MonoBehaviour
{
    PlayerInput mPlayerInput;
    InputAction mMovePosition;

    void Awake()
    {
        mPlayerInput = GetComponent<PlayerInput>();
        mMovePosition = mPlayerInput.actions["Move"];
        transform.position = Vector3.zero;
    }

    void OnEnable()
    {
        mMovePosition.performed += MovePosition;
    }

    void OnDisable()
    {
        mMovePosition.performed -= MovePosition;
    }

    void MovePosition(InputAction.CallbackContext context)
    {
        if (!GameManager.Instance.isPlaying)
        { 
            return;
        }

        Vector3 touchPosition = context.ReadValue<Vector2>();
        Vector3 position = Camera.main.ScreenToWorldPoint(touchPosition);
        position.z = 0;
        transform.position = position;
    }
}
