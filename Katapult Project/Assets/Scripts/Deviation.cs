using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deviation : MonoBehaviour
{
    public ModernCity modernCity;
    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.gameObject.tag == "Player")
        {
            modernCity.timeUp = true;
            modernCity.ChallengeComplete.Show();
            modernCity.starLeft.Show();
            modernCity.starCenter.Show();
            modernCity.starRight.Show();
        }
    }
}
