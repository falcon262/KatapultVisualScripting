using UnityEngine;
using System.Collections;
using AdvancedCustomizableSystem;
public class HairF01 : BEInstruction
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
		targetObject.gameObject.GetComponent<CharacterCustomization>().SetHairByIndex(0);
		UserController.instance.hair = "0";
		BeController.PlayNextOutside(beBlock);
	}
 
}
