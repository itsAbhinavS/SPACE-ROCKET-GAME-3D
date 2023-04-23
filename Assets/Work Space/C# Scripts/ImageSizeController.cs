using UnityEngine;
using UnityEngine.UI;

public class ImageSizeController : MonoBehaviour
{
    public Image spriteImage;
    public float normal_Size;
    public float reduced_Size;

    public void reduceSizeOfImage()
    {
        spriteImage.rectTransform.localScale = new Vector3(reduced_Size, reduced_Size, reduced_Size);
    }

    public void normalSizeOfImage() 
    {
        spriteImage.rectTransform.localScale = new Vector3(normal_Size, normal_Size, normal_Size);
    }
}