using UnityEngine;
using UnityEngine.UI;

public class SpriteSizeController : MonoBehaviour
{
    public Image spriteImage;

    public void reduceSizeOfImage()
    {
        spriteImage.rectTransform.localScale = new Vector3(0.85f, 0.85f, 0.85f);
    }

    public void normalSizeOfImage() 
    {
        spriteImage.rectTransform.localScale = new Vector3(1, 1, 1);
    }
}