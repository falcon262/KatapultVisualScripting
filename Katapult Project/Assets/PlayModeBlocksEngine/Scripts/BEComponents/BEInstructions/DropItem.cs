using UnityEngine;
using System.Collections;

public class DropItem : BEInstruction
{
 
	// Use this for Operations
	public override string BEOperation(BETargetObject targetObject, BEBlock beBlock)
	{
		string result = "0";
		
		// Use "beBlock.BeInputs" to get the input values
		
		return result;
	}
	
	// Use this for Functions
	public override void BEFunction(BETargetObject targetObject, BEBlock beBlock)
	{
        Debug.Log("We are functioning");
        RaycastHit hit;
        if (Physics.Raycast(targetObject.transform.position, targetObject.transform.TransformDirection(Vector3.forward), out hit))
        {
            if (hit.transform.gameObject.tag == "chest")
            {
                Debug.Log("We've hit something" + hit.transform.gameObject.tag);
                targetObject.itemCount -= 1;
                targetObject.items[0].transform.position = hit.transform.position;               
                targetObject.items[0].transform.gameObject.transform.SetParent(hit.transform);
                targetObject.items[0].transform.gameObject.SetActive(true);
                if(targetObject.items[0].tag == "crystal")
                {
                    targetObject.score += 100;
                    targetObject.scoreText.text = targetObject.score.ToString();
                }
                targetObject.items.RemoveAt(0);
            }
            else if (hit.transform.gameObject.tag == "nest")
            {
                Debug.Log("We've hit something" + hit.transform.gameObject.tag);
                targetObject.itemCount -= 1;
                targetObject.items[0].transform.position = hit.transform.position;
                targetObject.items[0].transform.gameObject.transform.SetParent(hit.transform);
                targetObject.items[0].transform.gameObject.SetActive(true);
                if (targetObject.items[0].tag == "egg")
                {
                    targetObject.score += 100;
                    targetObject.scoreText.text = targetObject.score.ToString();
                }
                targetObject.items.RemoveAt(0);
            }
        }
        BeController.PlayNextOutside(beBlock);
	}
 
}
