using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PauseManager : MonoBehaviour {
    public static PauseManager instance;
    public static bool Paused;

    public event Action<bool> PauseEvent;
    public event Action<float> VolumeChangedEvent, SensChangedEvent;

    public static float audioVolume;
    public static float sens;

    [SerializeField] CanvasGroup pauseCanvas;
    [SerializeField] Slider audioSlider, sensSlider;
    [SerializeField] TextMeshProUGUI audioText, sensText;

    AudioSource source;

    private void Awake() {
        if (instance != null && instance != this) Destroy(this); else instance = this;
        source = GetComponent<AudioSource>();
    }
    void Start() {
        Paused = false;
        StaticStuff.input.General.Pause.performed += Pause;
        StaticStuff.input.Paused.Unpause.performed += Pause;
        audioSlider.onValueChanged.AddListener(SetAudioVolume);
        sensSlider.onValueChanged.AddListener(SetSens);

        audioVolume = 1;
        audioSlider.value = audioVolume;
        audioText.text = audioVolume.ToString();

        sens = .2f;
        sensSlider.value = sens;
        sensText.text = sens.ToString();


        pauseCanvas.FullDisable();
    }

    void Update() {

    }

    void Pause(InputAction.CallbackContext ctx) {
        Paused = !Paused;
        PauseEvent?.Invoke(Paused);

        if (Paused) {
            // Game is paused
            Time.timeScale = 0;
            source.Play();
            StaticStuff.input.General.Disable();
            StaticStuff.input.Paused.Enable();
            Cursor.lockState = CursorLockMode.None;
            pauseCanvas.FullEnable();
        } else {
            // Game is unpaused
            Time.timeScale = 1;
            source.Stop();
            StaticStuff.input.Paused.Disable();
            StaticStuff.input.General.Enable();
            Cursor.lockState = CursorLockMode.Locked;
            pauseCanvas.FullDisable();
        }
    }

    void SetAudioVolume(float value) {
        audioVolume = value;
        source.volume = value;
        audioText.text = MathF.Round(value, 2).ToString();
        VolumeChangedEvent?.Invoke(value);
    }

    void SetSens(float sens) {
        sensText.text = MathF.Round(sens, 2).ToString();
        SensChangedEvent?.Invoke(sens);
    }
}