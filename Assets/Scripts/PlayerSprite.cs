using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSprite : MonoBehaviour
{
    [Serializable]
    public struct PlayerAnims
    {
        public Sprite idle, parrying, dodge, attack, defeat, win;
    }

    [SerializeField] PlayerAnims anims;

    private Image playerImage;

    void Start()
    {
        if (gameObject.TryGetComponent(out Image image)) playerImage = image;
    }

    public void SetSprite(PlayerAction action)
    {
        switch (action)
        {
            case PlayerAction.NONE:
                if (anims.idle != null) playerImage.sprite = anims.idle;
                break;
            case PlayerAction.GUARD:
                if (anims.parrying != null) playerImage.sprite = anims.parrying;
                break;
            case PlayerAction.AVOID:
                if (anims.dodge != null) playerImage.sprite = anims.dodge;
                break;
            case PlayerAction.ATTACK:
                if (anims.attack != null) playerImage.sprite = anims.attack;
                break;
            case PlayerAction.DEFEAT:
                if (anims.defeat != null) playerImage.sprite = anims.defeat;
                break;
            case PlayerAction.WIN:
                if (anims.win != null) playerImage.sprite = anims.win;
                break;
            default:
                if (anims.idle != null) playerImage.sprite = anims.idle;
                break;
        }
    }
}