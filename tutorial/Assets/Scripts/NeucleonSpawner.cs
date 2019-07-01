using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO. Is Intantiate parameter forced as an gameObject?
// TODO. Is script parameter an gameObject?
// TODO. Unity time https://docs.unity3d.com/Manual/TimeFrameManagement.html
// TODO. vsync

public class NeucleonSpawner : MonoBehaviour {
  public float spawnInterval;

  public float spawnDistance;

  public Neucleon[] neuclonePrefabs;

  private float durationSinceLastSpawn;

  // Start is called before the first frame update
  void Start() {
    
  }

  // Update is called once per frame 
  void Update() {
      
  }

  void FixedUpdate() {
    durationSinceLastSpawn += Time.deltaTime;
    if (durationSinceLastSpawn >= spawnInterval) {
      durationSinceLastSpawn -= spawnInterval;
      SpawnNeucleon();
    }
  }

  void SpawnNeucleon() {
    Neucleon prefab = neuclonePrefabs[Random.Range(0, neuclonePrefabs.Length)];
    Neucleon spawn = Instantiate<Neucleon>(prefab);
    spawn.transform.localPosition = Random.onUnitSphere * spawnDistance;
  }
}
