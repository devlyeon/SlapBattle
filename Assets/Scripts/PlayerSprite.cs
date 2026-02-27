using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSprite : MonoBehaviour
{
    [Serializable]
    public struct PlayerSpritePreset
    {
        public Sprite idle, damaged, knockout;
    }

    [SerializeField] PlayerSpritePreset sprites;

    private Image playerImage;
    private Animator animator;

    void Start()
    {
        if (gameObject.TryGetComponent(out Image image)) playerImage = image;
        if (gameObject.TryGetComponent(out Animator animator)) this.animator = animator;
    }

    public void SetSprite(PlayerAction action)
    {
        switch (action)
        {
            case PlayerAction.NONE:
                if (sprites.idle != null) playerImage.sprite = sprites.idle;
                break;
            case PlayerAction.GUARD:
                // animator.SetTrigger("Parrying");
                break;
            case PlayerAction.AVOID:
                // animator.SetTrigger("Dodge");
                break;
            case PlayerAction.ATTACK:
                animator.SetTrigger("Attack");
                break;
            case PlayerAction.DEFEAT:
                // if (sprites.knockout != null) playerImage.sprite = sprites.knockout;
                break;
            case PlayerAction.WIN:
                // if (anims.win != null) playerImage.sprite = anims.win;
                break;
            default:
                // if (anims.idle != null) playerImage.sprite = anims.idle;
                break;
        }
    }
}