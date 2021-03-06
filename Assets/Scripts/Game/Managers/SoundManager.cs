using UnityEngine;

namespace AlienArenas.Game
{
    public class SoundManager : MonoBehaviour
    {
        private static SoundManager _instance;

        public AudioClip gunFire;
        public AudioClip upgradedGunFire;
        public AudioClip hurt;
        public AudioClip alienDeath;
        public AudioClip marineDeath;
        public AudioClip victory;
        public AudioClip elevatorArrived;
        public AudioClip powerUpPickup;
        public AudioClip powerUpAppear;

        private AudioSource _soundEffectAudio;

        public static SoundManager Instance => _instance;


        private void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void Start()
        {
            
            AudioSource[] sources = GetComponents<AudioSource>();
            foreach (AudioSource source in sources)
            {
                if (source.clip == null)
                {
                    _soundEffectAudio = source;
                }
            }
            StopSoundBG();
        }

        public void PlayOneShot(AudioClip clip)
        {
            _soundEffectAudio.PlayOneShot(clip);
        }
        
        public void StopSoundBG()
        {
            _soundEffectAudio.gameObject.GetComponent<AudioSource>().enabled = false;
        }
        
        public void StartSoundBG()
        {
            _soundEffectAudio.gameObject.GetComponent<AudioSource>().enabled = true;
        }
    }
}