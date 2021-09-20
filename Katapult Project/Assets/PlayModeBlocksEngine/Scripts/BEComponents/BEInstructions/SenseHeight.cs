using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SenseHeight : BEInstruction
{
 
	// Use this for Operations
	public override string BEOperation(BETargetObject targetObject, BEBlock beBlock)
	{
		string result = "0";

		Debug.Log("SaySomething");

		RaycastHit hit;
		if (Physics.Raycast(targetObject.transform.position, targetObject.transform.TransformDirection(Vector3.forward), out hit))
		{
			if (hit.transform.gameObject.tag == "FirstAid" )
			{
					//FindObjectOfType<GameHandler>().isSensing = true;
					result = "50";
				
			}
			else if (hit.transform.gameObject.tag == "Safe")
			{
					//FindObjectOfType<GameHandler>().isSensing = true;
					result = "76";
			}
		}
		// Use "beBlock.BeInputs" to get the input values

		return result;
	}
	
	// Use this for Functions
	public override void BEFunction(BETargetObject targetObject, BEBlock beBlock)
	{
		// Use "beBlock.BeInputs" to get the input values

		// Make sure to end the function with a "BeController.PlayNextOutside" method and use "BeController.PlayNextInside" to play child blocks if needed
		BeController.PlayNextOutside(beBlock);
	}
 
}
