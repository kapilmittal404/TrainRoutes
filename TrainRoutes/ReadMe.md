# Train Routes Finder

## Overview

This .NET console application finds train routes between towns given an input file. The input file contains lines representing direct links from one town to another and their distances. The application calculates various route distances, counts trips with specific criteria, and finds the shortest routes.

## Design Decisions

1. **Graph Representation**: The train routes are represented using an adjacency list, which is implemented as a dictionary of dictionaries. This allows efficient lookups and updates of routes and distances.

   ![Graph Representation](Relevant Diagrams/graph_representation_diagram.png)

2. **Algorithms**: 
   - **Route Distance Calculation**: Simple traversal of the adjacency list.

     ![Route Distance Calculation](Relevant Diagrams/route_distance_calculation_diagram.png)

   - **Counting Trips**: Recursive depth-first search (DFS) with constraints on stops or distance.

     ![Counting Trips](Relevant Diagrams/counting_trips_diagram.png)

   - **Shortest Route**: Dijkstra's algorithm for finding the shortest path in a weighted graph.

     ![Shortest Route](Relevant Diagrams/shortest_route_diagram.png)

3. **Unit Tests**: The `GraphTests` class uses the xUnit framework to validate the functionality of the `Graph` class. Each test corresponds to one of the specified requirements.

## Usage

1. **Input File**: Ensure the input file `Input.txt` is in the same directory as the executable. The file should contain lines in the format `<From Town>,<To Town>,<Distance>`.
2. **Running the Application**: Execute the console application. The results of the tests will be printed to the console.

## Unit Tests

The unit tests are implemented in the `GraphTests.cs` file. To run the tests, use the xUnit test runner.

## Example Input File
