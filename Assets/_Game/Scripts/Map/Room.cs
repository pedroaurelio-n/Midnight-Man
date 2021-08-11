using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MapRooms
{
    Entry,
    Stairs1,
    Bathroom1,
    Garage,
    GarageCorridor,
    Library,
    Dining,
    Living1,
    Kitchen,
    Pantry,
    Bedroom1,
    BathroomBed1,
    MasterSuite,
    Closet,
    BathroomSuite,
    Corridor,
    Gameroom,
    Studio,
    Bathroom2,
    Bedroom2,
    Stairs2,
    Studyroom,
    Living2,
    Stairs3,
    Attic,

    TestRoomCenter,
    TestRoomUp,
    TestRoomLeft,
    TestRoomRight,
    SquareTopL,
    SquareTopR,
    SquareBottomL,
    SquareBottomR
}

public class Room : MonoBehaviour
{
    public MapRooms currentRoom;
    public List<Door> DoorList;
}
