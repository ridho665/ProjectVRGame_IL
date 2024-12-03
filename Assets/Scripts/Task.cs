using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Task
{
    public int budget;           // Budget untuk task
    public string style;           // Gaya bangunan (misal: modern, klasik, dll.)
    public string room;            // Ruangan (misal: kamar tidur, ruang tamu, dapur, dll.)

    public float time;
    // List gaya bangunan yang akan diacak
    private string[] availableStyles = { "Modern", "Classic" };

    // List jenis ruangan yang akan diacak
    private string[] availableRooms = { "Kitchen Room", "Work Room", "Family Room", "Bed Room" };

    // Daftar budget yang tersedia
    // private float[] availableBudgets = { 1800, 1850, 1900, 1950, 2000, 2050, 2100, 2150, 2200, 2250, 2300, 2350, 2400, 2450, 2500 };

    private float[] availableTimes = { 600 };

    // Constructor untuk membuat task baru dengan budget, style, dan ruangan acak
    public void Initialize()
    {
        // budget = availableBudgets[Random.Range(0, availableBudgets.Length)];
        budget = Random.Range(1800, 2501);
        style = availableStyles[Random.Range(0, availableStyles.Length)];
        room = availableRooms[Random.Range(0, availableRooms.Length)];
        time = availableTimes[Random.Range(0, availableTimes.Length)];
    }

    // Method untuk menampilkan informasi task (untuk debugging)
    public void PrintTaskDetails()
    {
        Debug.Log($"Task: Design {room} with a {style} style. Budget: {budget}. Time: {time} seconds.");
    }
}
