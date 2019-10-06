using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Core : MonoBehaviour {

    private static Core _instance;
    public static Core Instance {
        get {
            if (_instance == null) {
                _instance = FindObjectOfType<Core>();
            }
            return _instance;
        }
    }

    Dictionary<string, int> alphabet;
    Dictionary<string, List<GameObject>> units;
    private TextMeshPro scoreDisplay;
    private Rigidbody2D rb;
    public int score;
    public int size;

    public GameObject GameOverPanel;
    public GameObject WinPanel;

    List<GameObject> attached;

    public Action<int, int> Collected;

    private void Awake() {
        _instance = this;

        scoreDisplay = transform.Find("Score").GetComponent<TextMeshPro>();
        rb = GetComponent<Rigidbody2D>();

        units = new Dictionary<string, List<GameObject>>();
        alphabet = new Dictionary<string, int>();
        attached = new List<GameObject>();
        attached.Add(gameObject);
        score = 0;
        size = 0;
    }

    private void Update() {
        GetSpan();
    }

    public void RegisterUnit(GameObject unit) {
        attached.Add(unit);
        UpdateSize(1);

        string letter = unit.name;
        if (alphabet.ContainsKey(letter)) {
            alphabet[letter]++;
        } else {
            alphabet[letter] = 1;
            UpdateScore(1);
        }

        if (!units.ContainsKey(letter)) {
            units[letter] = new List<GameObject>();
        }
        units[letter].Add(unit);

        Collected?.Invoke(score, size);
    }

    void UpdateSize(int by) {
        size += by;
        GetComponent<Rigidbody2D>().mass += (0.005f * by);
    }

    void UpdateScore(int by) {
        score += by;
        scoreDisplay.text = score.ToString();

        if (score >= 26) {
            WinPanel.SetActive(true);
        }
    }

    public Bounds GetSpan() {
        float minX = Mathf.Infinity;
        float minY = Mathf.Infinity;
        float maxX = -Mathf.Infinity;
        float maxY = -Mathf.Infinity;

        foreach(GameObject unit in attached) {
            minX = Mathf.Min(minX, unit.transform.position.x);
            minY = Mathf.Min(minY, unit.transform.position.y);
            maxX = Mathf.Max(maxX, unit.transform.position.x);
            maxY = Mathf.Max(maxY, unit.transform.position.y);
        }

        Debug.DrawLine(transform.position, new Vector3(minX, minY));
        Debug.DrawLine(transform.position, new Vector3(maxX, maxY));

        return new Bounds(Vector3.zero, new Vector3(maxX - minX, maxY - minY));
    }

    public void Fire(string letter) {
        Debug.Log("Firing " + letter);
        if (!units.ContainsKey(letter)) return;

        foreach(GameObject unit in units[letter]) {
            unit.transform.GetComponent<Shooter>().Fire();
        }
    }

    public void RemoveUnit(GameObject unit) {
        attached.Remove(unit);
        units[unit.name]?.Remove(unit);
        UpdateSize(-1);
        if (alphabet.ContainsKey(unit.name) && alphabet[unit.name] > 0) {
            alphabet[unit.name]--;
            if (alphabet[unit.name] == 0) {
                UpdateScore(-1);
            }
        }
    }

    public void GameOver() {
        GameOverPanel.SetActive(true);
    }

    public void Restart() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
