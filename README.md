# Train Routes Solution

## Overview
This solution is designed to calculate various properties of train routes, such as route distances, shortest paths, and the number of trips based on specific criteria. It is implemented using a modular architecture to ensure scalability, maintainability, and testability.

---

## Design Decisions

### 1. **Domain-Driven Design**
The solution follows a domain-driven design (DDD) approach, where the core business logic resides in the `Domain` layer. This ensures that the domain logic is independent of infrastructure concerns, making it reusable and testable.

- **Key Domain Entities**:
  - `TownGraph`: Represents the graph structure of towns and routes.
  - `IRouteCalculator`: Interface defining the contract for route calculations.
  - `RouteCalculator`: Implements the logic for calculating distances, shortest paths, and trips.

---

### 2. **Graph Representation**
The train routes are represented as a directed graph using an adjacency list (`Dictionary<string, List<(string, int)>>`). This structure was chosen for its efficiency in representing sparse graphs and its ability to quickly access neighbors of a node.

- **Why Adjacency List?**
  - Efficient for traversing neighbors.
  - Scales well for the problem size.
  - Easy to extend for additional graph operations.

---

### 3. **Separation of Concerns**
The solution is divided into distinct layers to ensure clear separation of responsibilities:
- **Domain Layer**: Contains core business logic and entities.
- **Infrastructure Layer**: Handles data access and graph loading.
- **Application Layer**: Contains use cases that orchestrate domain logic.
- **Presentation Layer**: Handles user interaction and input/output.

---

### 4. **Graph Loading**
The `GraphRepository` in the `Infrastructure` layer is responsible for loading the graph data. This design abstracts the data source, allowing flexibility to load the graph from different sources (e.g., files, databases, or APIs) in the future.

- **Example Implementation**:
  - The `LoadGraph` method initializes the graph with hardcoded data for simplicity.
  - This can be extended to parse input files or other data sources.

---

### 5. **Algorithm Design**
The `RouteCalculator` implements algorithms for:
- **Route Distance Calculation**: Iterates through the adjacency list to compute the total distance of a given route.
- **Shortest Path Calculation**: Uses a recursive depth-first search (DFS) to find the shortest path between two towns.
- **Trip Counting**: Uses recursive DFS to count trips based on stops or distance constraints.

- **Why Recursive DFS?**
  - Simple to implement for this problem size.
  - Provides flexibility for constraints like maximum stops or distance.

---

### 6. **Extensibility**
The solution is designed to be extensible:
- New graph operations can be added to `RouteCalculator` without affecting other components.
- The `GraphRepository` can be extended to support different data sources.
- The modular architecture allows easy integration of new features.

---

### 7. **Testing**
Unit tests are written for all key functionalities to ensure correctness and prevent regressions. The tests cover:
- Route distance calculations.
- Shortest path calculations.
- Trip counting with various constraints.

---

## Future Improvements
- **Dynamic Graph Loading**: Extend `GraphRepository` to load graphs from external files or databases.
- **Performance Optimization**: Use algorithms like Dijkstra's or A* for shortest path calculations in larger graphs.
- **Error Handling**: Add robust error handling for invalid inputs or graph inconsistencies.
- **User Interface**: Develop a more user-friendly interface for interacting with the solution.

---

## How to Run
1. Clone the repository.
2. Open the solution in Visual Studio 2022.
3. Run the project to execute the main program.
4. Run the test project to validate functionality.

---

## Conclusion
This solution is designed to be modular, efficient, and extensible, making it suitable for solving the train routes problem while allowing for future enhancements.
