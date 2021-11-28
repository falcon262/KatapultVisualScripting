using UnityEngine;
using System.Collections;
using AdvancedCustomizableSystem;

public class HatsPolice : BEInstruction
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
		targetObject.gameObject.GetComponent<CharacterCustomization>().SetElementByIndex(ClothesPartType.Hat, 3);
		UserController.instance.hat = "3";
		BeController.PlayNextOutside(beBlock);
	}
 
}
