using UnityEngine;
using System.Collections;
using AdvancedCustomizableSystem;

public class HatsFFighter : BEInstruction
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
		targetObject.gameObject.GetComponent<CharacterCustomization>().SetElementByIndex(ClothesPartType.Hat, 4);
		UserController.instance.hat = "4";
		BeController.PlayNextOutside(beBlock);
	}
 
}
