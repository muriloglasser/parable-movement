# Projectile Parabolic Movement

The `Projectile` and `ProjectileController` classes work together to create a parabolic trajectory for a projectile in Unity. This setup allows for customized, physics-based movement that mimics a natural arc, enabling both manual and automated control of the projectile’s path. Additionally, it includes features for visualizing the path, adjusting trajectory points, and managing trajectory execution.

![](Gifs/Parable.gif)

## Table of Contents

- [Classes](#classes)
  - [Projectile](#projectile)
  - [ProjectileController](#projectilecontroller)
- [Enums](#enums)
  - [ParableMode](#parablemode)
- [Usage](#usage)
  - [Setting Trajectory Points](#setting-trajectory-points)
  - [Launching the Projectile](#launching-the-projectile)
  - [Pause Control](#pause-control)
- [Example](#example)

## Classes

### Projectile

The `Projectile` class handles the parabolic motion logic for the projectile. Key properties include:

- **parableMode**: Specifies if the trajectory should be automatically or manually controlled.
- **speed**: Sets the projectile’s movement speed.
- **body**: A reference to the Rigidbody component used to apply movement.
- **isPaused**: Allows pausing of the projectile.
- **startPoint, controlPoint, endPoint**: Define the parabolic trajectory.

#### Methods

- **SetPause**: Sets the pause state, controlling whether the projectile is paused.
- **GetPauseFactor**: Returns a factor (0 or 1) based on the pause state.
- **SetTrajectoryPoints**: Configures the start, control, and end points for the parabolic path.
- **LaunchProjectileRoutine**: Coroutine that animates the projectile along its parabolic path.

### ProjectileController

The `ProjectileController` class manages the execution and visualization of the projectile’s path.

- **sphereRadius**: Defines the radius for trajectory gizmos.
- **tile**: Reference to the projectile being managed.
- **startPoint, controlPoint, endPoint**: Transform objects that mark the projectile’s path.

#### Methods

- **LaunchProjectile**: Initializes and starts the `LaunchProjectileRoutine` coroutine from `Projectile`.
- **OnDrawGizmos**: Visualizes the trajectory path with Gizmos and handles in the Unity Editor.

## Enums

### ParableMode

Defines the mode for controlling the parabolic movement:

- **Auto**: The trajectory control point is automatically set halfway between start and end points.
- **Custom**: The control point can be customized by the user.

## Usage

### Setting Trajectory Points

To define a trajectory, call `SetTrajectoryPoints` with specified start, control, and end positions:

```csharp
tile.SetTrajectoryPoints(startPosition, controlPosition, endPosition);
```
### Launching the Projectile

Use LaunchProjectile to start the parabolic movement:

```csharp
if (Input.GetKeyDown(KeyCode.Space))
{
    LaunchProjectile(startPoint.position, controlPoint.position, endPoint.position);
}
```

### Pause Control

To pause the projectile’s movement, use:

```csharp
tile.SetPause(true);
```

# Thank You

I sincerely appreciate you taking the time to explore this project. I hope you enjoyed the experience and found valuable information. If you have any questions or suggestions, feel free to share them.
