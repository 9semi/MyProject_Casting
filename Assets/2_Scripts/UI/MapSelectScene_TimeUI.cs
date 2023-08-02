using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapSelectScene_TimeUI : MonoBehaviour
{
    public int _worldTime = 12;

    [SerializeField] WorldManager _worldManager;
    [SerializeField] AdModManager _adModManager;
    [SerializeField] Text _timeText;
    [SerializeField] Text _mapNameText;
    [SerializeField] Text _gamestartText;
    [SerializeField] Image _mapImage;
    [SerializeField] Sprite[] _mapSprites;
    [SerializeField] string[] _mapNames;

    [SerializeField] Button _xButton;
    [SerializeField] Button _leftButton;
    [SerializeField] Button _rightButton;
    [SerializeField] Button _recordButton;
    [SerializeField] Button _startButton;

    private void OnEnable()
    {
        if(DataManager.INSTANCE._tutorialIsInProgress)
        {
            _xButton.interactable = false;
            _recordButton.interactable = false;
            _startButton.interactable = false;
        }
        else
        {
            _xButton.interactable = true;
            _recordButton.interactable = true;
            _startButton.interactable = true;
        }
    }

    public void Up()
    {
        _worldManager.PlayClickEffectAudio();

        if (_worldTime < 23)
            _worldTime++;
        else
            _worldTime = 0;

        if (DataManager.INSTANCE._tutorialIsInProgress)
        {
            if (_worldTime.Equals(6))
            {
                _startButton.interactable = true;

                if (_worldManager.GetStep10Object().activeSelf)
                    _worldManager.GetStep10Object().SetActive(false);
            }
            else
            {
                _startButton.interactable = false;

                if (!_worldManager.GetStep10Object().activeSelf)
                    _worldManager.GetStep10Object().SetActive(true);
            }
        }

        _timeText.text = _worldTime.ToString();
        DataManager.INSTANCE._worldTime = _worldTime;
    }
    public void Down()
    {
        if (_worldManager.GetStep10Object().activeSelf)
            _worldManager.GetStep10Object().SetActive(false);

        _worldManager.PlayClickEffectAudio();

        if (_worldTime > 0)
            _worldTime--;
        else
            _worldTime = 23;

        if (DataManager.INSTANCE._tutorialIsInProgress)
        {
            if (_worldTime.Equals(6))
            {
                _startButton.interactable = true;

                if (_worldManager.GetStep10Object().activeSelf)
                    _worldManager.GetStep10Object().SetActive(false);
            }
            else
            {
                _startButton.interactable = false;

                if (!_worldManager.GetStep10Object().activeSelf)
                    _worldManager.GetStep10Object().SetActive(true);
            }
        }

        _timeText.text = _worldTime.ToString();
        DataManager.INSTANCE._worldTime = _worldTime;
    }
    public void Init(int mapNumber)
    {
        _worldTime = DataManager.INSTANCE._worldTime;
        _timeText.text = _worldTime.ToString();
        _mapImage.sprite = _mapSprites[mapNumber];
        _mapNameText.text = _mapNames[mapNumber];
        gameObject.SetActive(true);
    }
    public void ClickGameStartButton()
    {
        if(DataManager.INSTANCE._tutorialIsInProgress)
        {
            _worldManager.PlayClickEffectAudio();
            DataManager.INSTANCE._isMatch = false;
            _worldManager.MoveMap();
        }
        else
        {
            _xButton.interactable = false;
            _leftButton.interactable = false;
            _rightButton.interactable = false;
            _recordButton.interactable = false;
            _startButton.interactable = false;
            _gamestartText.text = "½ÇÇà Áß...";
            _gamestartText.fontSize = 50;

            _adModManager.ShowAD(() =>
            {
                _worldManager.PlayClickEffectAudio();
                DataManager.INSTANCE._isMatch = false;
                _worldManager.MoveMap();
            });
        }
    }
}
