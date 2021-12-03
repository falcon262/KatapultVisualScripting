using UnityEngine;
using System.Collections;
using AdvancedCustomizableSystem;

public class BottomsPolice : BEInstruction
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
		if (targetObject.transform.gameObject.name == "female_customizable")
		{
			targetObject.gameObject.GetComponent<CharacterCustomization>().SetElementByIndex(ClothesPartType.Pants, 9);
			UserController.instance.bottom = "9";
		}
		else
		{
			targetObject.gameObject.GetComponent<CharacterCustomization>().SetElementByIndex(ClothesPartType.Pants, 10);
			UserController.instance.bottom = "10";
		}
		BeController.PlayNextOutside(beBlock);
	}
 
}
