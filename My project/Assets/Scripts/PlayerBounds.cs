using UnityEngine;

public class PlayerBounds : MonoBehaviour
{
    private Camera mainCamera;

    float playerHalfWidth;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mainCamera = Camera.main;
        playerHalfWidth = GetComponent<SpriteRenderer>().bounds.extents.x / 8; // metade da largura do Sprite
    }

    // Update is called once per frame
    void LateUpdate()
    {
        KeepPlayerInsideCamera();
    }

    void KeepPlayerInsideCamera()
    {
        if(mainCamera == null) return;

        Vector3 screenBounds = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));

        float clampedX = Mathf.Clamp(transform.position.x, -screenBounds.x - playerHalfWidth, screenBounds.x - playerHalfWidth);
        float clampedY = Mathf.Clamp(transform.position.y, -screenBounds.y, screenBounds.y);

        transform.position = new Vector2(clampedX, clampedY);

    }
}
