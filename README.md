# 2D Physics-Based Brick Breaker Game

A 2D brick-breaking game implemented using **GXPEngine** (a C# framework based on OpenGL/Mono). This project showcases a **custom physics engine** with real-time collision detection and response involving **walls, squares, and circles**, all driven by a custom `Vec2` math library.

![https://github.com/ilialek/Resources/blob/main/Brick%20Breaker%20Demo.gif](https://github.com/ilialek/Resources/blob/main/Brick%20Breaker%20Demo.gif)

## Gameplay Summary

- **Click and drag** to aim
- **Release** to launch the ball
- The ball **bounces** off walls and destroys obstacles (squares and circles)
- Load different levels using keys **1**, **2**, or **3**
- Optional **debug mode** for physics visualization


## Core Components

### `Vec2.cs` – Custom Vector Math Library  
A lightweight 2D vector class for basic vector operations, essential for movement, collision detection, and reflection.

**Features:**
- Operator overloads: `+`, `-`, `* (scalar)`, `/`
- Vector math: `.Length()`, `.Normalized()`, `.Dot()`, `.CrossProduct()`, `.RotatedVec2()`
- `Normal()` returns a perpendicular unit vector (useful for reflections)

## Collision Detection

### A. Wall Collisions (Line Segments)

Each wall is defined as a `LineSegment`.

**Detection Logic:**
- Uses **vector projection** to find the shortest distance from the ball to the wall  
- If `distance < radius`, a collision is detected  
- Computes the **Point of Intersection (POI)**  
- Reflects the velocity using the wall’s surface normal

### B. Square Obstacles (AABB)

Each square is treated as an Axis-Aligned Bounding Box (AABB).

**Detection Logic:**
- Checks if the ball **overlaps** the bounding box
- Identifies the nearest edge relative to the ball's position
- If a collision occurs:
  - Calculates Time of Impact (TOI)
  - Updates ball position to the POI
  - Reflects velocity using the edge's normal
 
### C. Circle Obstacles

Each circle is treated as a round obstacle, and the ball’s path is modeled as a line segment.

**Detection Logic:**
- Solves a quadratic equation to detect collision between the path and the circular area
- Calculates the Time of Impact (TOI)
- If TOI ∈ [0,1), a collision is detected within the current frame
- Reflects velocity using the vector between the circle center and ball position as the surface normal

