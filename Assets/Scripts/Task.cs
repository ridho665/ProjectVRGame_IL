using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Task
{
    public float budget;           // Budget untuk task
    public string style;           // Gaya bangunan (misal: modern, klasik, dll.)
    public string room;            // Ruangan (misal: kamar tidur, ruang tamu, dapur, dll.)

    // List gaya bangunan yang akan diacak
    private string[] availableStyles = { "Modern", "Klasik", "Elegant" };

    // List jenis ruangan yang akan diacak
    private string[] availableRooms = { "Kamar Tidur", "Ruang Tamu", "Dapur", "Kamar Mandi", "Ruang Makan" };

    // Daftar budget yang tersedia
    private float[] availableBudgets = { 10, 15, 20, 25, 30 };

    // Constructor untuk membuat task baru dengan budget, style, dan ruangan acak
    public void Initialize()
    {
        budget = availableBudgets[Random.Range(0, availableBudgets.Length)];
        style = availableStyles[Random.Range(0, availableStyles.Length)];
        room = availableRooms[Random.Range(0, availableRooms.Length)];
    }

    // Method untuk menampilkan informasi task (untuk debugging)
    public void PrintTaskDetails()
    {
        Debug.Log($"Task: Design {room} with a {style} style. Budget: {budget}");
    }
}
