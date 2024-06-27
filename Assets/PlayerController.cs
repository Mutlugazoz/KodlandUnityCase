using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float characterSpeed = 7f;
    [SerializeField] private CharacterController characterController;
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform cam, rifleStart;
    [SerializeField] private Text HpText;

    public float health = 0;

    public Action Died;

    private void Start()
    {
        HpText.text = health.ToString();
    }

    private void HandleMovement()
    {
        Vector3 movementDirection = Vector3.zero;
        Vector3 characterForwardTopDown = Vector3.Normalize(new Vector3(transform.forward.x, 0, transform.forward.z));
        if (Input.GetKey(KeyCode.A))
        {
            movementDirection += Quaternion.Euler(0, -90, 0) * characterForwardTopDown;
        }
        if (Input.GetKey(KeyCode.D))
        {
            movementDirection += Quaternion.Euler(0, 90, 0) * characterForwardTopDown;
        }
        if (Input.GetKey(KeyCode.W))
        {
            movementDirection += characterForwardTopDown;
        }
        if (Input.GetKey(KeyCode.S))
        {
            movementDirection -= characterForwardTopDown;
        }

        characterController.Move(movementDirection * (characterSpeed * Time.deltaTime));
    }

    public void OnGameEnded()
    {
        Destroy(GetComponent<PlayerLook>());
        Cursor.lockState = CursorLockMode.None;
        enabled = false;
    }

    public void ChangeHealth(int hp)
    {
        health += hp;
        if (health > 100)
        {
            health = 100;
        }
        else if (health <= 0)
        {
            Died?.Invoke();
        }
        HpText.text = health.ToString();
    }

    void Update()
    {
        HandleMovement();

        if (Input.GetMouseButtonDown(0))
        {
            GameObject buf = Instantiate(bullet);
            buf.transform.position = rifleStart.position;
            buf.GetComponent<Bullet>().setDirection(cam.forward);
            //buf.transform.rotation = transform.rotation;
        }
    }
}
