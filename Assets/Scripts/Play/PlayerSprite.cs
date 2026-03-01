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
    [SerializeField] private PlayerIdleAnime _playerIdleAnime;
    [SerializeField] private PlayerAttackAnime _playerAttackAnime;
    [SerializeField] private PlayerDodgeAnime _playerDodgeAnime;
    [SerializeField] private PlayerParryAnime _playerParryAnime;
    [SerializeField] private PlayerHitAnime _playerHitAnime;
    [SerializeField] private PlayerWinAnime _playerWinAnime;
    [SerializeField] private PlayerDefeatAnime _playerDefeatAnime;

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
                _playerIdleAnime.Play();
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
                ResetAnime();
                _playerDefeatAnime.Play();
                break;
            case PlayerAction.WIN:
                // if (anims.win != null) playerImage.sprite = anims.win;
                ResetAnime();
                _playerWinAnime.Play();
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
        _playerWinAnime.Initialize();
        _playerDefeatAnime.Initialize();
    }
}