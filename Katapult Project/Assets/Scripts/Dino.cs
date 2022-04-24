using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dino : MonoBehaviour
{
    public ModernCity city;
    public Transform target;
    public float moveSpeed;

    private void Start()
    {
        if (city.male.transform.gameObject.activeSelf)
        {
            target = city.male.transform;
        }
        else if (city.female.transform.gameObject.activeSelf)
        {
            target = city.female.transform;
        }
    }

    private void Update()
    {
        Logic();
    }

    void Logic()
    {
        float move = moveSpeed * Time.deltaTime;
        transform.LookAt(target);
        transform.Translate(0,0,move);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.gameObject.tag == "Player")
        {
            city.timer = 0;
            city.dino.moveSpeed = 0;
            city.dino.transform.gameObject.GetComponentInChildren<Animator>().SetBool("Follow", false);
        }
    }
}
