using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class CameraMovement : MonoBehaviour
{
    public float cameraSpeed = 5f;
    private Vector2 direction;
    void Start()
    {
        if (SpaceManager.instance == null)
        {
            Debug.LogError("SpaceManager instance not found!");
            return;
        }
        switch (SpaceManager.instance.scrollDirection)
        {
            case ScrollDirection.LeftToRight:
                direction = Vector2.right;
                break;
            case ScrollDirection.RightToLeft:
                direction = Vector2.left;
                break;
            case ScrollDirection.DownToUp:
                direction = Vector2.up;
                break;
            case ScrollDirection.UpToDown:
                direction = Vector2.down;
                break;
        }
    }

    private void Update()
    {
        if (SpaceManager.instance == null) return;

       
        transform.position += new Vector3(direction.x, direction.y, 0) * cameraSpeed * Time.deltaTime;
    }
}