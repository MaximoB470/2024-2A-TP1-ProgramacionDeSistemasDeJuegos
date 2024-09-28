using Audio;
using UnityEngine;


namespace Enemies
{
    [RequireComponent(typeof(Enemy))]
    public class EnemySfx : MonoBehaviour
    {
        [SerializeField] private AudioPlayer audioSourcePrefab;
        [SerializeField] private SoundScriptable spawnClips;
        [SerializeField] private SoundScriptable explosionClips;
        private AudioService audioService;
        private Enemy _enemy;
        private void Reset() => FetchComponents();
        private void Awake() => FetchComponents();
        private void FetchComponents()
        {
            // "a ??= b" is equivalent to "if(a == null) a = b" 
            _enemy ??= GetComponent<Enemy>();
        }
        
        private void OnEnable()
        {
            audioService = ServiceLocator.Instance.GetService("AudioService") as AudioService;
            if (!audioSourcePrefab)
            {
                Debug.LogError($"{nameof(audioSourcePrefab)} is null!");
                return;
            }
            _enemy.OnSpawn += HandleSpawn;
            _enemy.OnDeath += HandleDeath;
        }
        
        private void OnDisable()
        {
            _enemy.OnSpawn -= HandleSpawn;
            _enemy.OnDeath -= HandleDeath;
        }

        private void HandleDeath()
        {
            PlayRandomClip(audioService.DeathFiles(), audioSourcePrefab);

        }

        private void HandleSpawn()
        {
            PlayRandomClip(spawnClips.SpawnSounds, audioSourcePrefab);
        }

        private void PlayRandomClip(RandomContainer<AudioClipData> container, AudioPlayer sourcePrefab)
        {
            if (!container.TryGetRandom(out var clipData))
                return;
            
            SpawnSource(sourcePrefab).Play(clipData);
        }

        private AudioPlayer SpawnSource(AudioPlayer prefab)
        {
            return Instantiate(prefab, transform.position, transform.rotation);
        }
    }
}
