using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crystals : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other.transform.gameObject.tag);
        if (other.transform.gameObject.tag == "Player")
        {
            
            other.transform.gameObject.GetComponent<BETargetObject>().score += 100;
            other.transform.gameObject.GetComponent<BETargetObject>().crystalCount -= 1;
            if(this.transform.gameObject.tag == "crystal" || this.transform.gameObject.tag == "egg")
            {
                other.transform.gameObject.GetComponent<BETargetObject>().items.Add(this.transform.gameObject);
                other.transform.gameObject.GetComponent<BETargetObject>().itemCount += 1;
            }
            other.transform.gameObject.GetComponent<BETargetObject>().scoreText.text = other.transform.gameObject.GetComponent<BETargetObject>().score.ToString();
            this.transform.gameObject.SetActive(false);
        }
    }
}
