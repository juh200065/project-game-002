using UnityEngine;

public class SuperJump : MonoBehaviour
{
    public Sprite OnSuperJump;
    public SpriteRenderer SpriteRenderer;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.PlayerScript.isSuperJump == true)
        {
            SpriteRenderer.sprite = OnSuperJump;
            SpriteRenderer.color = Color.white;
        }
        else
        {
            SpriteRenderer.sprite = OnSuperJump;
            SpriteRenderer.color = new Color(0f, 0f, 0f, 1f);
        }
    }
}