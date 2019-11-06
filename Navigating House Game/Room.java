import java.util.*;
import java.util.stream.Collectors;

/**
 * Class Room - a room in a game.
 *
 * This class is part of the "World of Home" application. 
 * "World of Home" is a very simple, text based travel game.  
 *
 * A "Room" represents one location in the scenery of the game.  It is 
 * connected to other rooms via exits.  For each existing exit, the room 
 * stores a reference to the neighboring room.
 * 
 * @author  Michael Kölling, David J. Barnes and Olaf Chitil
 * @version 5/2/2019
 */

public class Room 
{
    private String description;
    private HashMap<Direction, Room> exits;        // stores exits of this room.

    private Map<String,ArrayList<Character>> characters;
    //Stores all the character information including the location
    private ArrayList<Character> values;
    //Stores the actual character values themselves

    private int numOfCharacters;
    /**
     * Create a room described "description". Initially, it has
     * no exits. "description" is something like "a kitchen" or
     * "an open court yard".
     * @param description The room's description.
     * Pre-condition: description is not null.
     */
    public Room(String description) 
    {
        assert description != null : "Room.Room has null description";
        this.description = description;

        characters = new HashMap<String,ArrayList<Character>>();
        values = new ArrayList<Character>();

        int numOfCharacters = 0;
        exits = new HashMap<Direction, Room>();
        sane();
    }

    /**
     * Class invariant: getShortDescription() and getLongDescription() don't return null.
     */
    public void sane()
    {
        assert getShortDescription() != null : "Room has no short description" ;
        assert getLongDescription() != null : "Room has no long description" ;
    }

    /**
     * Define an exit from this room.
     * @param direction The direction of the exit.
     * @param neighbor  The room to which the exit leads.
     * Pre-condition: neither direction nor neighbor are null; 
     * there is no room in given direction yet.
     */
    public void setExit(Direction direction, Room neighbor) 
    {
        assert direction != null : "Room.setExit gets null direction";
        assert neighbor != null : "Room.setExit gets null neighbor";
        assert getExit(direction) == null : "Room.setExit set for direction that has neighbor";
        sane();
        exits.put(direction, neighbor);
        sane();
        assert getExit(direction) == neighbor : "Room.setExit has wrong neighbor";
    }

    /**
     * @return The short description of the room
     * (the one that was defined in the constructor).
     */
    public String getShortDescription()
    {
        return description;
    }

    /**
     * Return a description of the room in the form:
     *     You are in the kitchen.
     *     Exits: north west
     *     Characters: Jason
     * @return A long description of this room
     */
    public String getLongDescription()
    {
        String characterString = "";

        //Creates an ArrayList with the results of any characters located in a certain room
        ArrayList<Character> result;
        result = characters.get(description);

        //Checks to see if there are any characters at all
        if (numOfCharacters > 0) {
            //Loops through the result ArrayList of all the characters that are in that room
            for(Character character: result) {
                characterString = characterString + character.toString() + "; ";
            }
            return "You are " + description + ".\n" + getExitString() + "\nCharacters: " + characterString;
        } else {
            return "You are " + description + ".\n" + getExitString();
        }   
    }

    /**
     * Return a string describing the room's exits, for example
     * "Exits: north west".
     * @return Details of the room's exits.
     */
    private String getExitString()
    {
        String returnString = "Exits:";
        // Ensure some fixed ordering of keys, so that return String uniquely defined.
        List<Direction> keys = exits.keySet().stream().sorted().collect(Collectors.toList());
        for(Direction exit : keys) {
            returnString += " " + exit;
        }
        return returnString;
    }

    /**
     * Return the room that is reached if we go from this room in direction
     * "direction". If there is no room in that direction, return null.
     * @param direction The exit's direction.
     * @return The room in the given direction.
     * Pre-condition: direction is not null
     */
    public Room getExit(Direction direction) 
    {
        assert direction != null : "Room.getExit has null direction";
        sane();
        return exits.get(direction);
    }

    /**
     * Add given character to the room.
     * @param c The character to add.
     * Pre-condition: character is not null.
     */
    public void addCharacter(Character c)
    {
        assert c != null : "Room.addCharacter has null character";
        numOfCharacters++;

        //Adds the character to the values ArrayList
        values.add(c);

        //Adds the character to the characters Map with the description of the room they are in
        characters.put(description, values);
    }

    /**
     * Take given item from a character in the room.
     * @param item The item to take.
     * @return true if taking was successful, false otherwise
     * Pre-Condition: item is not null.
     */
    public boolean take(Item item)
    {
        assert item != null : "Room.take is given null item";
        boolean taken = false;

        //loop through all of the characters and check if they are holding the item 
        for(Character character: values) {
            if(character.take(item)){
                taken = true;
            }
        }

        return taken;
    }
}

