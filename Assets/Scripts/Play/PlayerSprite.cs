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
    [SerializeField] private PlayerAttackAnime _playerAttackAnime;
    [SerializeField] private PlayerDodgeAnime _playerDodgeAnime;
    [SerializeField] private PlayerParryAnime _playerParryAnime;
    [SerializeField] private PlayerHitAnime _playerHitAnime;

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
                // if (sprites.idle != null) playerImage.sprite = sprites.idle;
                ResetAnime();
                break;
            case PlayerAction.PARRYING:
                // animator.SetTrigger("Parrying");
                ResetAnime();
                _playerParryAnime.Play();
                break;
            case PlayerAction.DODGE:
                // animator.SetTrigger("Dodge");
                ResetAnime();
                _playerDodgeAnime.Play();
                break;
            case PlayerAction.ATTACK:
                // animator.SetTrigger("Attack");
                ResetAnime();
                _playerAttackAnime.Play();
                break;
            case PlayerAction.KNOCKBACK:
                // animator.SetTrigger("Damaged");
                ResetAnime();
                _playerHitAnime.Play();
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

    private void ResetAnime()
    {
        _playerAttackAnime.Initialize();
        _playerDodgeAnime.Initialize();
        _playerParryAnime.Initialize();
        _playerHitAnime.Initialize();
    }
}