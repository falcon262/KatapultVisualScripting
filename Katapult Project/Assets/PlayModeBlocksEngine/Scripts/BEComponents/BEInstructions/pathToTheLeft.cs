using UnityEngine;
using System.Collections;

public class pathToTheLeft : BEInstruction
{
 
	// Use this for Operations
	public override string BEOperation(BETargetObject targetObject, BEBlock beBlock)
	{
		string result = "0";

		// Use "beBlock.BeInputs" to get the input values
		RaycastHit hit;
		if (Physics.Raycast(targetObject.transform.position, targetObject.transform.TransformDirection(Vector3.left), out hit))
		{
			if(hit.transform.gameObject.name == "LeftAhead")
            {
				result = "1";
            }
		}

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
