using UnityEngine;

namespace slaughter.de.Managers
{
    public class Manager: MonoBehaviour
    {
    public static Manager Instance { get; private set; }

    private void Awake()
    {
        MakeSingelton();
    }

    private void MakeSingelton()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Optional: Verhindert das Zerstören beim Laden
        }
        else
        {
            Destroy(gameObject); // Sicherstellen, dass keine Duplikate existieren
        }
    }
    }

}