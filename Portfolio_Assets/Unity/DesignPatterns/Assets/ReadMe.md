Chain of Responsibility:
This is a design pattern wherein you pass a request through a series of classes. Each class inspects the request
to see if it can handle it. If it can’t handle the request, then it passes it to the next class in the chain [of responsibility].
The design pattern is made up of a client, a handler and however many concrete handlers that are needed. 

The program is a basic counter-based inventory that utilizes chain of responsibility to store each "object" in the correct container.
The GUI displays 9 different resources that can be added to the player's inventory.
_____________________________________________________________________________________________________________________________________
Composite:
This particular design pattern involves setting up objects into a sort of tree structure, where every element is responsible
for preforming a specific task. This also means that each node in the structure is either a branch node 
(has something split off of it) or is a leaf node (has nothing after it).

The program associated with this design pattern is a “Faux-Conversation” that guides the user/player in selecting a class for an RPG. 
_____________________________________________________________________________________________________________________________________
Fly Weight:
The flyweight design pattern is a pattern used to save memory by storing a copy of the shared data between
a large pool of similar objects. The data that is shared between all copies of an object is the intrinsic data (such
as textured and mesh), while the data that is state-dependent is called extrinsic data (such as position, 
health, ammo, etc.)

This program uses a set of toggles that allow you to create a set of primitive shapes. 
If the specified shape already exists, a new shape is not generated and the original is reused instead.
_____________________________________________________________________________________________________________________________________
Observer:
The Observer Design Pattern (also referred to as Publish-Subscribe Pattern) defines a one-to-many 
dependency between objects where-in changes to the 'one' object's state is broadcasted to the 'many' 
dependent objects.  

The object that changes state (the 'one' object) is referred to as the "subject" or "publisher". The objects 
that receive the state change information (the 'many') are referred to as the "observers" or "subscribers".

A video game-based example would be having a player die to multiple different AI enemies (in an MMO);
as the player's state changes from alive to dead, the enemies are informed of this and react by returning
to their original spawn areas and resetting their stats.

The program is not-interactive; after a fixed amount of time, the cube will change color which will 
be “noticed” by the observer code tied to the other five shapes (two sphere and three cylinders).
