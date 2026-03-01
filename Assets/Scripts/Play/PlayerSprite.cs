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

    public void SetSprite(PlayerAction action)
    {
        switch (action)
        {
            case PlayerAction.NONE:
                _playerIdleAnime.Play();
                break;
            case PlayerAction.PARRYING:
                ResetAnime();
                _playerParryAnime.Play();
                break;
            case PlayerAction.DODGE:
                ResetAnime();
                _playerDodgeAnime.Play();
                break;
            case PlayerAction.ATTACK:
                ResetAnime();
                _playerAttackAnime.Play();
                break;
            case PlayerAction.KNOCKBACK:
                ResetAnime();
                _playerHitAnime.Play();
                break;
            case PlayerAction.DEFEAT:
                ResetAnime();
                _playerDefeatAnime.Play();
                break;
            case PlayerAction.WIN:
                ResetAnime();
                _playerWinAnime.Play();
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