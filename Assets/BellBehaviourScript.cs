using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BellBehaviourScript : MonoBehaviour {
    AudioClip bellClip = null;
    AudioClip introClip = null;
    bool isIntro = true;
    int beatsLast = 0;
    float beatsCutPlayback = 3.5f;
    float beatsPlayTime;

    // Use this for initialization
    void Start () {
        AudioSource audio_src = this.GetComponent<AudioSource>();
        
        if (audio_src != null) introClip = audio_src.clip;
        bellClip = Resources.FindObjectsOfTypeAll(typeof(AudioClip)).FirstOrDefault(c => c.name == "01-beat") as AudioClip;
        introClip = Resources.FindObjectsOfTypeAll(typeof(AudioClip)).FirstOrDefault(c => c.name == "01-intro") as AudioClip;

        //if (bellClip == null)
        {
            Object obj = Resources.Load("01-beat");
            Debug.Log(string.Format("Loaded: bellClip={0}", obj));
            //bellClip = obj as AudioClip;
        }
        //if (introClip == null)
        {
            Object obj = Resources.Load("Assets/FX/01-intro.wav");
            Debug.Log(string.Format("Loaded: introClip={0}", obj));
            //introClip = obj as AudioClip;
        }
    }
	
	// Update is called once per frame
	void Update () {
        AudioSource audio_src = this.GetComponent<AudioSource>();
        if (audio_src == null) return;
        
        float beatLen = bellClip.length;
        float introLen = introClip.length;
        int minNext = System.DateTime.Now.TimeOfDay.Minutes, hrNext = System.DateTime.Now.TimeOfDay.Hours, hours = 0;
        int curHour = hrNext;

        if (minNext % 5 == 0) minNext++;
        while (minNext % 5 != 0)
            minNext++;
        minNext = 60;//every hour only
        if(minNext >= 60)
        {
            minNext = 0;
            hrNext++;
        }
        
        System.TimeSpan nextHour = new System.TimeSpan( hrNext, minNext, 0);
        System.TimeSpan lastTime = nextHour.Subtract(System.DateTime.Now.TimeOfDay);
        hours = nextHour.Hours;
        hours = hours > 12 ? hours - 12 : hours;
        
        if (hours == 0)
            hours = 12;

        
        float secsBeat = (beatLen) * hours + 1f;
        float secsIntro = introLen + 1f;
        if(Time.frameCount % 30 == 0 && false)
            Debug.Log(string.Format("lastTime={0}, isIntro ={1}, secsIntro={2}, hours={3}, secsBeat={4}", lastTime.TotalSeconds, isIntro, secsIntro, hours, secsBeat));

        if(lastTime.TotalSeconds <= secsIntro && isIntro)
        {
            audio_src.Play();
            isIntro = false;
            beatsLast = hours;
            if (beatsLast == 0)
                beatsLast = 12;
        }
        else if (!isIntro)
        {
            if (lastTime.TotalSeconds > secsIntro)
            {
                audio_src.clip = bellClip;
            }
            if (!audio_src.isPlaying && audio_src.clip == bellClip && beatsLast > 0)
            {
                audio_src.Play();
                beatsPlayTime = Time.time;
                beatsLast--;
            }
            else if(audio_src.isPlaying && beatsPlayTime > 0 && Time.time - beatsPlayTime >= beatsCutPlayback && beatsLast > 0)
            {
                audio_src.Stop();
            }
            else if (beatsLast == 0 && !audio_src.isPlaying)
            {
                isIntro = true;
                beatsPlayTime = 0;
            }
        }
        else if(audio_src.clip != introClip)
        {
            audio_src.clip = introClip;
        }
	}
}
