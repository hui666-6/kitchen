using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private SoundsSO audiocliprefsSO;
    private int volume = 5;
    private const string SOUNDMANAGER_VOLUME = "SoundManagerVolume";
    public static SoundManager instance { get; private set; }
    public void Awake()
    {
        instance = this;
        LoadVolume();
    }
    private void Start()
    {
        OrderManager.Instance.OnRecipeSuccessed += Instance_OnRecipeSuccessed;
        OrderManager.Instance.OnRecipeFailed += Instance_OnRecipeFailed;
        CuttingCounter.onchop += CuttingCounter_onchop;
        KitchenObjectHolder.ondrop += KitchenObjectHolder_ondrop;
        KitchenObjectHolder.onpickup += KitchenObjectHolder_onpickup;
        TrashCounter.onobjecttransh += TrashCounter_onobjecttransh;
    }

    private void TrashCounter_onobjecttransh(object sender, System.EventArgs e)
    {
        playSound(audiocliprefsSO.trash);
    }

    private void KitchenObjectHolder_onpickup(object sender, System.EventArgs e)
    {
        playSound(audiocliprefsSO.pickup);
    }

    private void KitchenObjectHolder_ondrop(object sender, System.EventArgs e)
    {
        playSound(audiocliprefsSO.drop);
    }

    private void CuttingCounter_onchop(object sender, System.EventArgs e)
    {
        playSound(audiocliprefsSO.chop);
    }

    private void Instance_OnRecipeFailed(object sender, System.EventArgs e)
    {
        playSound(audiocliprefsSO.deliveryfail);
    }

    private void Instance_OnRecipeSuccessed(object sender, System.EventArgs e)
    {
        playSound(audiocliprefsSO.deliversuccess);
    }

    private void playSound(AudioClip[]clips,float volumemutipler=1.0f)
    {
        playsound(clips, Camera.main.transform.position);

    }
    private void playsound(AudioClip[] clips, Vector3 position, float volumemutipler = 0.2f)
    {
        if (volume == 0) return;
        int index=Random.Range(0,clips.Length);
        AudioSource.PlayClipAtPoint(clips[index], position, volumemutipler*(volume/10.0f));
    }
   public void stepsound(float volumemutipler = 0.2f)
   {
        playSound(audiocliprefsSO.footstep, volumemutipler = 0.2f);
   }
    public void countdownsound()
    {
        playSound(audiocliprefsSO.warning);
    }
    public void warningsound(float volumemutipler = 0.2f)
    { 
        playSound(audiocliprefsSO.warning,  volumemutipler = 0.2f); 
    }
    public void ChangeVolume()
    {
        volume++;
        if (volume > 10)
        {
            volume = 0;
        }
        SaveVolume();

    }
    public int GetVolume()
    { 
      return volume;
    }
    private void SaveVolume()
    {
        PlayerPrefs.SetInt(SOUNDMANAGER_VOLUME,volume);
    }
    private void LoadVolume()
    {
        volume = PlayerPrefs.GetInt(SOUNDMANAGER_VOLUME, volume);
    }
}
