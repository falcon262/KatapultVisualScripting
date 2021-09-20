using UnityEngine;
using System.Collections;

public class ReleaseObject : BEInstruction
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

		GameObject robotClaw = GameObject.FindGameObjectWithTag("Claw").gameObject;
		GameObject environment = GameObject.FindGameObjectWithTag("Environment").gameObject;

		if (robotClaw.transform.Find("FirstAid"))
		{
			robotClaw.transform.Find("FirstAid").gameObject.transform.SetParent(environment.transform);
		}
		else if (robotClaw.transform.Find("Safe"))
		{
			robotClaw.transform.Find("Safe").gameObject.transform.SetParent(environment.transform);
		}
		

		// Make sure to end the function with a "BeController.PlayNextOutside" method and use "BeController.PlayNextInside" to play child blocks if needed
		BeController.PlayNextOutside(beBlock);
	}
 
}
