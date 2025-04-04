using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public class Graph
{
    private Dictionary<char, Dictionary<char, int>> _adjacencyList;

    public Graph()
    {
        _adjacencyList = new Dictionary<char, Dictionary<char, int>>();
    }

    public void LoadFromFile(string filePath)
    {
        foreach (var line in File.ReadLines(filePath))
        {
            var parts = line.Split(',');
            char from = parts[0].Trim()[0];
            char to = parts[1].Trim()[0];
            int distance = int.Parse(parts[2].Trim());

            if (!_adjacencyList.ContainsKey(from))
            {
                _adjacencyList[from] = new Dictionary<char, int>();
            }
            _adjacencyList[from][to] = distance;
        }
    }

    public int CalculateRouteDistance(string route)
    {
        var towns = route.Split('-');
        int totalDistance = 0;

        for (int i = 0; i < towns.Length - 1; i++)
        {
            char from = towns[i][0];
            char to = towns[i + 1][0];

            if (_adjacencyList.ContainsKey(from) && _adjacencyList[from].ContainsKey(to))
            {
                totalDistance += _adjacencyList[from][to];
            }
            else
            {
                return -1; // Route doesn't exist
            }
        }

        return totalDistance;
    }

    public int CountTripsWithMaxStops(char start, char end, int maxStops)
    {
        return CountTripsWithMaxStopsHelper(start, end, maxStops, 0);
    }

    private int CountTripsWithMaxStopsHelper(char current, char end, int maxStops, int stops)
    {
        if (stops > maxStops) return 0;
        int count = 0;
        if (current == end && stops > 0) count++;

        if (_adjacencyList.ContainsKey(current))
        {
            foreach (var neighbor in _adjacencyList[current])
            {
                count += CountTripsWithMaxStopsHelper(neighbor.Key, end, maxStops, stops + 1);
            }
        }

        return count;
    }

    public int CountTripsWithExactStops(char start, char end, int exactStops)
    {
        return CountTripsWithExactStopsHelper(start, end, exactStops, 0);
    }

    private int CountTripsWithExactStopsHelper(char current, char end, int exactStops, int stops)
    {
        if (stops > exactStops) return 0;
        if (stops == exactStops && current == end) return 1;

        int count = 0;
        if (_adjacencyList.ContainsKey(current))
        {
            foreach (var neighbor in _adjacencyList[current])
            {
                count += CountTripsWithExactStopsHelper(neighbor.Key, end, exactStops, stops + 1);
            }
        }

        return count;
    }

    public int FindShortestRoute(char start, char end)
    {
        var distances = new Dictionary<char, int>();
        var priorityQueue = new SortedSet<(int distance, char town)>();

        foreach (var town in _adjacencyList.Keys)
        {
            distances[town] = int.MaxValue;
        }
        distances[start] = 0;
        priorityQueue.Add((0, start));

        while (priorityQueue.Count > 0)
        {
            var (currentDistance, currentTown) = priorityQueue.Min;
            priorityQueue.Remove(priorityQueue.Min);

            if (currentTown == end) return currentDistance;

            if (_adjacencyList.ContainsKey(currentTown))
            {
                foreach (var neighbor in _adjacencyList[currentTown])
                {
                    int newDistance = currentDistance + neighbor.Value;
                    if (newDistance < distances[neighbor.Key])
                    {
                        priorityQueue.Remove((distances[neighbor.Key], neighbor.Key));
                        distances[neighbor.Key] = newDistance;
                        priorityQueue.Add((newDistance, neighbor.Key));
                    }
                }
            }
        }

        return -1; // No route found
    }

    public int CountTripsWithMaxDistance(char start, char end, int maxDistance)
    {
        return CountTripsWithMaxDistanceHelper(start, end, maxDistance, 0);
    }

    private int CountTripsWithMaxDistanceHelper(char current, char end, int maxDistance, int currentDistance)
    {
        if (currentDistance >= maxDistance) return 0;
        int count = 0;
        if (current == end && currentDistance > 0) count++;

        if (_adjacencyList.ContainsKey(current))
        {
            foreach (var neighbor in _adjacencyList[current])
            {
                count += CountTripsWithMaxDistanceHelper(neighbor.Key, end, maxDistance, currentDistance + neighbor.Value);
            }
        }

        return count;
    }
}
