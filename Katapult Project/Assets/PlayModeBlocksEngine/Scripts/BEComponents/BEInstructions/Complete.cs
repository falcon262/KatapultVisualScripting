using UnityEngine;
using System.Collections;

public class Complete : BEInstruction
{
 
	// Use this for Operations
	public override string BEOperation(BETargetObject targetObject, BEBlock beBlock)
	{
		string result = "0";
		
		// Use "beBlock.BeInputs" to get the input values
		if(targetObject.crystalCount == 0)
        {
			result = "1";
			FindObjectOfType<Dino>().moveSpeed = 0;
			FindObjectOfType<Dino>().transform.gameObject.GetComponentInChildren<Animator>().SetBool("Follow", false);
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
