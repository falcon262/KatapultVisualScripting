using UnityEngine;
using System.Collections;

public class CarryObject : BEInstruction
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
		// Use "beBlock.BeInputs" to get the input values
		Debug.Log("We are functioning");
        GameObject robotClaw = GameObject.FindGameObjectWithTag("Claw").gameObject;
        RaycastHit hit;
        if (Physics.Raycast(targetObject.transform.position, targetObject.transform.TransformDirection(Vector3.forward), out hit))
        {
            if (hit.transform.gameObject.tag == "FirstAid" || hit.transform.gameObject.tag == "Safe")
            {
				Debug.Log("We've hit something"+ hit.transform.gameObject.tag);
                robotClaw.GetComponentInChildren<Animator>().SetTrigger("Carry");
                hit.transform.gameObject.transform.position = robotClaw.transform.position + new Vector3(0.6f, -0.5f, 0.4f);
                hit.transform.gameObject.transform.SetParent(robotClaw.transform);
            }
            /*else if (hit.transform.gameObject.tag == "Barricade")
            {
				Debug.Log("We've hit something"+ hit.transform.gameObject.tag);
                robotClaw.GetComponentInChildren<Animator>().SetTrigger("Carry");
                hit.transform.gameObject.transform.position = robotClaw.transform.position + new Vector3(0f, -1f, 0f);
                hit.transform.gameObject.transform.SetParent(robotClaw.transform);
            }
            else if (hit.transform.gameObject.tag == "Ball")
            {
                hit.transform.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                Debug.Log("We've hit something" + hit.transform.gameObject.tag);
                robotClaw.GetComponentInChildren<Animator>().SetTrigger("Carry");
                hit.transform.gameObject.transform.position = robotClaw.transform.position + new Vector3(0f, 0.2f, 0.2f);
                hit.transform.gameObject.transform.SetParent(robotClaw.transform);
            }*/
        }
        // Make sure to end the function with a "BeController.PlayNextOutside" method and use "BeController.PlayNextInside" to play child blocks if needed
        BeController.PlayNextOutside(beBlock);
	}
 
}
