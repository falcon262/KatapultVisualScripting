using UnityEngine;
using UnityEngine.SceneManagement;
using AdvancedCustomizableSystem;

public class SimpleMove : BEInstruction
{

    int counterForRepetitions;
    float counterForMovement = 0;
    float movementDuration = 0.5f; //seconds
    Vector3 startPos;
    Vector3 direction;


    public override void BEFunction(BETargetObject targetObject, BEBlock beBlock)
    {
        if (beBlock.beBlockFirstPlay)
        {


            counterForRepetitions = 2;
            startPos = targetObject.transform.position;
            beBlock.beBlockFirstPlay = false;
        }

        if (counterForMovement == 0)
        {
            startPos = targetObject.transform.position;
        }

        if (counterForRepetitions > 0)
        {
            if (counterForMovement <= movementDuration)
            {
                if (SceneManager.GetActiveScene().name == "ModernCity")
                {
                    counterForMovement += Time.deltaTime * 9f;
                }
                else
                {
                    counterForMovement += Time.deltaTime * 3f;
                }

                if (targetObject.GetComponent<Collider2D>())
                {
                    direction = targetObject.transform.right;
                }
                else if (targetObject.GetComponent<Collider>())
                {
                    direction = targetObject.transform.forward;
                }
                //targetObject.Wheels.SetBool("IsMoving", true);
                foreach (Animator a in targetObject.gameObject.GetComponent<CharacterCustomization>().GetAnimators())
                    a.SetBool("walk", true);

                targetObject.transform.position = Vector3.Lerp(startPos, startPos + direction, counterForMovement / movementDuration);
            }
            else
            {
                counterForMovement = 0;
                counterForRepetitions--;

                if (counterForRepetitions <= 0)
                {
                    //targetObject.Wheels.SetBool("IsMoving", false);
                    foreach (Animator a in targetObject.gameObject.GetComponent<CharacterCustomization>().GetAnimators())
                        a.SetBool("walk", false);
                    beBlock.beBlockFirstPlay = true;
                    BeController.PlayNextOutside(beBlock);
                }
            }
        }
        else if (counterForRepetitions < 0)
        {
            if (counterForMovement <= movementDuration)
            {
                if (SceneManager.GetActiveScene().name == "ModernCity")
                {
                    counterForMovement += Time.deltaTime * 9f;
                }
                else
                {
                    counterForMovement += Time.deltaTime * 3f;
                }
                if (targetObject.GetComponent<Collider2D>())
                {
                    direction = targetObject.transform.right;
                }
                else if (targetObject.GetComponent<Collider>())
                {
                    direction = targetObject.transform.forward * -1;
                }
                //targetObject.Wheels.SetBool("IsMovingBack", true);
                targetObject.transform.position = Vector3.Lerp(startPos, startPos + direction, counterForMovement / movementDuration);
            }
            else
            {
                counterForMovement = 0;
                counterForRepetitions++;

                if (counterForRepetitions >= 0)
                {
                    //targetObject.Wheels.SetBool("IsMovingBack", false);
                    beBlock.beBlockFirstPlay = true;
                    BeController.PlayNextOutside(beBlock);
                }
            }
        }
        else
        {
            if (counterForRepetitions == 0)
            {
                //targetObject.Wheels.SetBool("IsMovingBack", false);
                //targetObject.Wheels.SetBool("IsMoving", false);
                beBlock.beBlockFirstPlay = true;
                BeController.PlayNextOutside(beBlock);
            }
        }



    }

}