using UnityEngine;
using System.Collections;
using JetBrains.Annotations;

public class StartSound : BEInstruction
{
	string result;

	public bool isGo;

	// Use this for Operations
	public override string BEOperation(BETargetObject targetObject, BEBlock beBlock)
	{

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
