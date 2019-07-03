
using UnityEngine;

public class Game : MonoBehaviour {
  public KeyCode createObjectKey;
  public KeyCode newGameKey;
  public KeyCode saveGameKey;
  public KeyCode loadGameKey;

  // Update is called once per frame
  void Update() {
    if (Input.GetKeyDown(createObjectKey))
      createObject();
    else if (Input.GetKeyDown(newGameKey))
      newGame();
    else if (Input.GetKeyDown(saveGameKey))
      saveGame();
    else if (Input.GetKeyDown(loadGameKey))
      loadGame();
  }

  protected virtual void createObject() {
  }

  protected virtual void newGame() {
  }

  protected virtual void saveGame() {
  }

  protected virtual void loadGame() {
  }
}
