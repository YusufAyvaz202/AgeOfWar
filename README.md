# Unity Fighter Game

A 2D side-scrolling tower defense-style game built with Unity, featuring resource management, fighter spawning, and strategic combat mechanics.

***Note:** This project is only an internship project prepared in a few days. It is not a completed game. It may contain errors and performance issues.*

## 🎮 Overview

This Unity project is a 2D strategy game where players manage resources (meat and gold) to spawn fighters and defend their castle against enemy waves. The game features multiple fighter types, an economy system, level progression, and save/load functionality.

## ✨ Features

### Core Gameplay
- **Resource Management**: Dual currency system (Meat & Gold)
- **Fighter Spawning**: Multiple fighter types with different costs and abilities
- **Wave-based Combat**: Progressive enemy waves with increasing difficulty
- **Castle Defense**: Protect your castle while attacking the enemy's
- **Level Progression**: Multiple levels with unique wave configurations

### Technical Features
- **Object Pooling**: Efficient memory management for fighters
- **Save System**: JSON-based game state persistence
- **Event-driven Architecture**: Decoupled systems using EventManager
- **ScriptableObject Configuration**: Data-driven design for fighters and levels
- **UI Management**: Comprehensive UI system with animations

### Fighter Types
- **CaveMan**: Basic melee fighter (Cost: 3 meat)
- **Ninja**: Advanced melee fighter (Cost: 5 meat)

## 🏗️ Technical Architecture

### Core Systems

#### 1. Fighter System
- **BaseFighter**: Abstract base class for all fighter types
- **IAttacker/IAttackable**: Interface-based combat system
- **FighterAnimationController**: Manages fighter animations
- **AI Navigation**: NavMesh-based movement and pathfinding

#### 2. Economy System
- **EconomyManager**: Handles meat production and consumption
- **GoldManager**: Manages gold acquisition and spending
- **Upgrade System**: Configurable production rate improvements

#### 3. Object Pool System
- Efficient fighter instantiation and reuse
- Separate pools for different fighter types
- Automatic cleanup and recycling

#### 4. Save System
- JSON-based serialization
- Persistent player progress
- Automatic save on game exit
- Automatic load on game start

#### 5. Event Management
- Centralized EventManager for system communication
- Game state change notifications
- Fighter death events
- Decoupled architecture

## 🚀 Installation

### Prerequisites
- Unity 2022.3 LTS or later
- DOTween (for UI animations)
- NavMesh components

### Setup
1. Clone or download the project
2. Open in Unity Hub
3. Import DOTween package if not already present
4. Ensure all scenes are added to build settings
5. Configure layer masks for fighter targeting

### Dependencies
- Unity NavMesh AI
- DOTween (UI animations)
- TextMeshPro (UI text)

## 👨‍💻 Development

### Key Design Patterns
- **Singleton Pattern**: Managers (GameManager, EconomyManager, etc.)
- **Object Pool Pattern**: Fighter instantiation
- **Observer Pattern**: Event-driven communication
- **Strategy Pattern**: Fighter variants with different behaviors
- **Data-Driven Design**: ScriptableObject configurations

### Performance Optimizations
- Object pooling prevents garbage collection issues
- NavMesh provides efficient pathfinding
- Event system reduces coupling and improves performance
- Coroutine-based timing for smooth gameplay

### Extensibility
- Easy to add new fighter types by extending BaseFighter
- Level configuration through ScriptableObjects
- Modular system architecture allows easy feature addition
- Interface-based design supports multiple combat behaviors

---
