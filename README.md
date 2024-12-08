# Shooter
Shooter Game
Overview
This is a first-person shooter game where the player must avoid or defeat an enemy that patrols a designated area. The enemy follows a random patrol path but will aggressively chase and attack the player upon detection. The goal is to eliminate the enemy.

Gameplay Instructions
Movement: Use W, A, S, D to move around, and Space to jump.
Shooting: Use the Left Mouse Button to shoot at the enemy.
Objective: Hit the enemy 10 times to defeat it.
Features
Dynamic Enemy AI: The enemy patrols the map, detects and chases the player, and attacks when in range.
Health System: The enemy has health that decreases with each successful hit. The player also has health managed by a damage system.
Shooting Mechanics: Features include visual effects like muzzle flashes, raycasting for hit detection, and damage application.
Interactive Environment: Includes covers, ramps, and walls to help players strategically avoid the enemy.
Project Structure
Scripts:
EnemyAI: Handles enemy behavior (patrolling, chasing, attacking, health).
Gun: Manages shooting mechanics, including visual effects and damage detection.
PlayerInputManager: Captures user inputs and controls player actions.
PlayerMovement: Manages player movement and gravity checks.
PlayerVision: Handles camera rotation for a first-person view.
Objects:
Player: Includes movement, shooting, and first-person camera.
Enemy: Patrols the map and attacks the player.
Environment: Contains the ground, walls, ramps, and covers for strategic gameplay.
Materials
Simplistic materials are used for the environment and characters to enhance visibility and gameplay focus.
Development Details
Tools Used: Unity and C#.
References:
Various YouTube tutorials (see the full report for links).
Links
Gameplay Video
GitHub Repository
