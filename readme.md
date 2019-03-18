# Battleships

## Brief

The challenge is to program a simple version of the game Battleships. Create an application to allow a single human player to play a one-sided game of Battleships against ships placed by the computer.

The program should create a 10x10 grid, and place several ships on the grid at random with the following sizes:
1x Battleship (5 squares)
2x Destroyers (4 squares)

The player enters or selects coordinates of the form “A5”, where “A” is the column and “5” is the row, to specify a square to target. Shots result in hits, misses or sinks. The game ends when all ships are sunk.
You can write a console application or UI to complete the task.

Try to code the challenge as you would approach any typical work task; we are not looking for you to show knowledge of frameworks or unusual programming language features. Most importantly, keep it simple.
Please include a link to the source and instructions on how to build and run it.

## Approach

I used TDD to develop the complete game without the random placement of ships. I then decided to introduce a inject ship positions as a strategy in the constructor. The tests still pass with both algorithms for ship placement, though the tests assume a certain number and size of ships.

I've tried to ensure that any of the "data" for the game can be changed in one place. This means it is easy to extend for new ship types and board sizes. Maximum board size is 26x26 due to using alphabet for the column names.

Initialisation is so simple in Program.cs I felt no need for any kind of DI framework.
