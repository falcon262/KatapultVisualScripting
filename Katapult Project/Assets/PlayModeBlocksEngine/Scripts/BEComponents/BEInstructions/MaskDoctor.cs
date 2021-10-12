using UnityEngine;
using System.Collections;
using AdvancedCustomizableSystem;

public class MaskDoctor : BEInstruction
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
		targetObject.gameObject.GetComponent<CharacterCustomization>().SetElementByIndex(ClothesPartType.Accessory, 9);
		BeController.PlayNextOutside(beBlock);
	}
 
}
