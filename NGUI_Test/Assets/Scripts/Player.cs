using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float _speed;
    public float _hp;
    public float _hpMax;
    public GameObject _GM;
    public GM _GMst;

    public UISprite _hpBar;

    void Start()
    {
        _hp = _hpMax;
    }

    void Update()
    {
        if(Input.GetKey("up"))
        {
            transform.position += new Vector3(0, _speed * Time.deltaTime, 0);
        }
        if (Input.GetKey("down"))
        {
            transform.position -= new Vector3(0, _speed * Time.deltaTime, 0);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        _hp -= 10f;
        _hpBar.fillAmount = _hp * 0.01f;
        if(_hp <= 0)
        {
            GameEnd();
        }
    }

    private void GameEnd()
    {
        Debug.Log("Game End");
        Time.timeScale = 0;
    }

    void TouchMove()
    {
        if(Input.touchCount >0)
        {
            if(Input.GetTouch(0).deltaPosition.y > 1f)
            {
                transform.position += new Vector3(0, Mathf.Lerp(0, _speed * Time.deltaTime, Time.time), 0);
            }

            if (Input.GetTouch(0).deltaPosition.y < -1f)
            {
                transform.position -= new Vector3(0, Mathf.Lerp(0, _speed * Time.deltaTime, Time.time), 0);
            }
        }

        transform.localPosition = new Vector3(transform.localPosition.x, Mathf.Clamp(transform.localPosition.y, -200f, 180f), transform.localPosition.z);
    }
}
