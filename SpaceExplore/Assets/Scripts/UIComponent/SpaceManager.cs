using UnityEngine;

public enum ScrollDirection { LeftToRight, RightToLeft, DownToUp, UpToDown };

public class SpaceManager : MonoBehaviour
{

    public static SpaceManager instance { get; private set; } // Singleton
    [Header("Set movement direction")]
    public ScrollDirection scrollDirection = ScrollDirection.LeftToRight;

    private void Awake()
    {
       
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }
}
