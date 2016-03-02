using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
//using UnityEngine.SceneManagement;
using System.Collections;

public class UIManager : MonoBehaviour
{
    public static UIManager UI_Manager;

    GameObject _bgm;

    [SerializeField] GameObject titleScreen;
    [SerializeField] GameObject soundScreen;
    [SerializeField] GameObject pauseScreen;
    [SerializeField] GameObject loseScreen;
    [SerializeField] GameObject fadeScreen;
    [SerializeField] GameObject overlayFadeScreen;

    Animator _titleScreenAnim;
    Animator _soundScreenAnim;
    Animator _pauseScreenAnim;
    Animator _loseScreenAnim;
    Animator _fadeScreenAnim;
    Animator _oFScreenAnim;

    CanvasGroup _titleCanvas;
    CanvasGroup _pauseCanvas;
    CanvasGroup _loseCanvas;

    Text _finalScore;

    AudioSource _source;

    // AudioClips
    public AudioClip pressedSFX;
    public AudioClip hoverSFX;

    public AudioMixer _sfxMixer;
    public AudioMixer _bgmMixer;

    // Booleans
	public static bool _allowOptions;
	public static bool _restartGame;
    public static bool _lose;
    bool _openLoseScreen;

    BGM_Manager _bgmScript;

	
    void Awake()
    {
        if (UI_Manager == null)
        {
            UI_Manager = this;
            
            DontDestroyOnLoad(gameObject);            
        }
        else if (UI_Manager != null)
        {
            Destroy(gameObject);
        }        

        _titleScreenAnim = titleScreen.GetComponent<Animator>();
        _soundScreenAnim = soundScreen.GetComponent<Animator>();
        _pauseScreenAnim = pauseScreen.GetComponent<Animator>();
        _loseScreenAnim = loseScreen.GetComponent<Animator>();
        _fadeScreenAnim = fadeScreen.GetComponent<Animator>();
        _oFScreenAnim = overlayFadeScreen.GetComponent<Animator>();

        _titleCanvas = titleScreen.GetComponent<CanvasGroup>();
        _pauseCanvas = pauseScreen.GetComponent<CanvasGroup>();
        _loseCanvas = loseScreen.GetComponent<CanvasGroup>();

        _source = GetComponent<AudioSource>();        

        Setup();
    }

    public void Setup()
    {
        _bgm = GameObject.Find("BGM_Manager");
        _bgmScript = _bgm.GetComponent<BGM_Manager>();

        // Restarts the Game
        switch (_restartGame)
        {
            case true:

                _allowOptions = true;

                _titleCanvas.alpha = 0;
                _titleCanvas.interactable = false;
                _titleCanvas.blocksRaycasts = false;

                _oFScreenAnim.SetBool("Fade", false);
                _fadeScreenAnim.SetBool("Fade", false);

                if (_bgm != null)
                {
                    _bgmScript.ActiveTransition("Gameplay");
                }

                break;

            case false:

                // If this game
                // was entirely new
                // then start from the Title page
                _titleCanvas.alpha = 1;
                _titleCanvas.interactable = true;
                _titleCanvas.blocksRaycasts = true;

                _allowOptions = false;

                _oFScreenAnim.SetBool("Fade", false);
                _fadeScreenAnim.SetBool("Fade", false);
                _titleScreenAnim.SetTrigger("Appear");

                if (_bgm != null)
                {
                    _bgmScript.QuietTransition();
                }

                break;
        }       
    }

    void Update()
    {
        // Pause Game
        if (_allowOptions)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (!_pauseScreenAnim.GetBool("Appear"))
                {
                    PauseButton();
                }
            }
        }       
             
        // Lose Screen
        if (_lose)
        {
            if (!_openLoseScreen)
            {
                _openLoseScreen = true;

                LoseMenu();
            }
        }

        if (Input.GetKeyDown(KeyCode.D) && Input.GetKeyDown(KeyCode.L))
        {
            _lose = true;
        }

        // Display final score
        //_finalScore.text = .ToString();   
    }

    // Title-Screen UIs
    public void PlayButton()
    {
        _titleScreenAnim.SetTrigger("Disappear");        
        _allowOptions = true;

        _bgmScript.ActiveTransition("Gameplay");
    }   

    public void ExitButton()
    {
        _oFScreenAnim.SetBool("Fade", true);

        Invoke("QuitApplication", 1);
    }

    void QuitApplication()
    {
        Application.Quit();
    }

    // Game-Screen UIs
    public void PauseButton()
    {
        Time.timeScale = 0;

        _pauseCanvas.alpha = 1;
        _pauseCanvas.interactable = true;
        _pauseCanvas.blocksRaycasts = true;

        _fadeScreenAnim.SetBool("Fade", true);
        _pauseScreenAnim.SetBool("Appear", true);

        // Lower volume in the background
        AudioListener.volume = 0.5f;
    }

    // Pause Screen UIs        
    public void ResumeButton()
    {
        Time.timeScale = 1;

        _fadeScreenAnim.SetBool("Fade", false);
        _pauseScreenAnim.SetBool("Appear", false);

        // Increase volume in the background
        AudioListener.volume = 1;
    }

    public void RestartButton()
    {
        // Fade to black when it happens
        _oFScreenAnim.SetBool("Fade", true);
        _fadeScreenAnim.SetBool("Fade", false);
        _pauseScreenAnim.SetBool("Appear", false);

        _titleCanvas.alpha = 0;
        _restartGame = true;
        Time.timeScale = 1;
        
        StartCoroutine(RestartGame());
    }

    IEnumerator RestartGame()
    {
        _lose = false;
        _openLoseScreen = false;

        yield return new WaitForSeconds(1);
		Application.LoadLevel(Application.loadedLevel);
       // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);        
    }

    public void SoundButton()
    {
        _pauseScreenAnim.SetBool("Appear", false);
        _soundScreenAnim.SetBool("Appear", true);
    }

    public void MenuButton()
    {
        // Fade to black when it happens
        _allowOptions = false;
        _restartGame = false;

        Time.timeScale = 1;

        _oFScreenAnim.SetBool("Fade", true);
        _fadeScreenAnim.SetBool("Fade", false);
        _pauseScreenAnim.SetBool("Appear", false);

        StartCoroutine(ReturnToMenu());
    }

    IEnumerator ReturnToMenu()
    {
        _lose = false;
        _openLoseScreen = false;

        yield return new WaitForSeconds(1);
		Application.LoadLevel(Application.loadedLevel);
    }

    // Sound Screen UIs
    public void SFX_Slider(float _vol)
    {
        // Adjust SFX Volume
        _sfxMixer.SetFloat("SFX_Vol", _vol * AudioListener.volume);
    }

    public void BGM_Slider(float _vol)
    {
        // Adjust BGM volume 
        _bgmMixer.SetFloat("BGM_Vol", _vol * AudioListener.volume);
    }

    public void SoundBackButton()
    {
        _soundScreenAnim.SetBool("Appear", false);
        _pauseScreenAnim.SetBool("Appear", true);
    }

    // Lose-Screen UIs
    public void LoseMenu()
    {
        // Appears when the player loses
        Time.timeScale = 0;

        // Display final score
        //_finalScore.text = .ToString();

        _loseCanvas.alpha = 1;
        _loseCanvas.interactable = true;
        _loseCanvas.blocksRaycasts = true;

        _fadeScreenAnim.SetBool("Fade", true);
        _loseScreenAnim.SetBool("Appear", true);
    }

    public void LoseRestartButton()
    {
        // Fade to black when it happens
        _oFScreenAnim.SetBool("Fade", true);
        _fadeScreenAnim.SetBool("Fade", false);
        _pauseScreenAnim.SetBool("Appear", false);
        _loseScreenAnim.SetBool("Appear", false);

        _loseCanvas.interactable = false;
        _loseCanvas.blocksRaycasts = false;

        _titleCanvas.alpha = 0;
        _restartGame = true;
        Time.timeScale = 1;

        StartCoroutine(RestartGame());
    }

    public void LoseMenuButton()
    {
        // Fade to black when it happens
        _allowOptions = false;
        _restartGame = false;

        Time.timeScale = 1;

        _oFScreenAnim.SetBool("Fade", true);
        _fadeScreenAnim.SetBool("Fade", false);
        _pauseScreenAnim.SetBool("Appear", false);
        _loseScreenAnim.SetBool("Appear", false);

        _loseCanvas.interactable = false;
        _loseCanvas.blocksRaycasts = false;

        StartCoroutine(ReturnToMenu());
    }

    // UI-SFXs
    public void PressedSFX()
    {
        _source.PlayOneShot(pressedSFX);
    }

    public void HoverSFX()
    {
        _source.PlayOneShot(hoverSFX);
    }
}
